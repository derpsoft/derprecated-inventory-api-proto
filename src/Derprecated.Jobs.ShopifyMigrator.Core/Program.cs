namespace Derprecated.Jobs.ShopifyMigrator.Core
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.IO;
    using System.Linq;
    using Api.Models;
    using Api.Models.Attributes;
    using Api.Models.Configuration;
    using Api.Models.Shopify;
    using Funq;
    using Microsoft.Extensions.Configuration;
    using ServiceStack;
    using ServiceStack.Configuration;
    using ServiceStack.Data;
    using ServiceStack.OrmLite;
    using Product = Api.Models.Product;

    public class Program
    {
        static Program()
        {
            var builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory());
            builder.AddJsonFile("appsettings.json");
            Configuration = builder.Build();

            var license = FindLicense();
            if (!license.IsNullOrEmpty())
            {
                Licensing.RegisterLicense(license);
            }

            Container = new Container();
            Configure(Container);
        }

        private static IConfigurationRoot Configuration { get; }
        private static Container Container { get; }

        private static string FindLicense()
        {
            return Configuration.GetSection("serviceStack")["license"];
        }

        [SuppressMessage("ReSharper", "InvertIf")]
        private static void Configure(Container container)
        {
            var appSettings = new AppSettings();
            var configuration = new ApplicationConfiguration();
            configuration.Shopify = new Shopify
                                    {
                                        Domain = Configuration["shopify:domain"],
                                        ApiKey = Configuration["shopify:apiKey"],
                                        Password = Configuration["shopify:password"]
                                    };

            // DB
            container.Register<IDbConnectionFactory>(c =>
                                                     {
                                                         var connectionString =
                                                             Configuration.GetConnectionString("AzureSql");

                                                         return new OrmLiteConnectionFactory(connectionString,
                                                             SqlServerDialect.Provider);
                                                     });

            // Db filters
            OrmLiteConfig.InsertFilter = (dbCmd, row) =>
                                         {
                                             if (row is IInsertFilter)
                                             {
                                                 var insert = row as IInsertFilter;
                                                 insert.OnBeforeInsert();
                                             }

                                             if (row is IAuditable)
                                             {
                                                 var auditRow = row as IAuditable;
                                                 auditRow.CreateDate = auditRow.ModifyDate = DateTime.UtcNow;
                                             }
                                         };
            OrmLiteConfig.UpdateFilter = (dbCmd, row) =>
                                         {
                                             if (row is IUpdateFilter)
                                             {
                                                 var update = row as IUpdateFilter;
                                                 update.OnBeforeUpdate();
                                             }

                                             if (row is IAuditable)
                                             {
                                                 var auditRow = row as IAuditable;
                                                 auditRow.ModifyDate = DateTime.UtcNow;
                                             }
                                         };

            container.Register(c =>
                               {
                                   var domain = configuration.Shopify.Domain;
                                   var apiKey = configuration.Shopify.ApiKey;
                                   var password = configuration.Shopify.Password;

                                   return new JsonServiceClient($"https://{domain}")
                                          {
                                              UserName = apiKey,
                                              Password = password
                                          };
                               });


            using (var db = Container.Resolve<IDbConnectionFactory>().Open())
            {
                db.CreateTableIfNotExists<Tag>();
                db.CreateTableIfNotExists<Product>();
                db.CreateTableIfNotExists<ProductTag>();
                db.CreateTableIfNotExists<ProductImage>();
            }
        }

        private static void Main(string[] args)
        {
            using (var client = Container.Resolve<JsonServiceClient>())
            {
                Console.WriteLine(@"Requesting latest product counts...");

                var shopifyCount = client.Get(new CountProducts());

                Console.WriteLine($"Found\n\tShopify: {shopifyCount.Count}\n");
                Console.WriteLine("Merging...\n");

                var shopifyProducts = client.Get(new GetProducts {Limit = shopifyCount.Count});

                var count = shopifyProducts.Products.AsParallel()
                                           .SelectMany(p =>
                                               p.Variants.Map(v =>
                                                              {
                                                                  var x = Product.From(p)
                                                                                 .PopulateFromPropertiesWithAttribute(
                                                                                     v, typeof (WhitelistAttribute));

                                                                  x.ShopifyVariantId = v.Id;

                                                                  return x;
                                                              })
                    ).Select(p =>
                             {
                                 Product product;

                                 using (var db = Container.Resolve<IDbConnectionFactory>().Open())
                                 {
                                     product =
                                         db.Where<Product>(new {p.ShopifyId, p.ShopifyVariantId}).SingleOrDefault();

                                     if (product == default(Product))
                                     {
                                         Console.WriteLine($"New [{p.ShopifyId}] {p.Title.Take(40).Join()}...");
                                         product = p;
                                     }
                                     else
                                     {
                                         db.LoadReferences(product);
                                         product.Merge(p);

                                         Console.WriteLine(
                                             $"Existing [{product.ShopifyId} -> {product.Id}] {p.Title.Take(40).Join()}...");
                                     }

                                     product.Tags.Split(',')
                                            .Map(x => new Tag {Lowercase = x.ToLowerSafe().Trim(), Name = x.Trim()})
                                            .ExecAll(x =>
                                                     {
                                                         var tagId = -1;
                                                         if (!db.Exists<Tag>(new {x.Lowercase}))
                                                         {
                                                             db.Save(x);
                                                             tagId = (int) db.LastInsertId();
                                                         }
                                                         else
                                                         {
                                                             tagId = db.Scalar<int>(
                                                                 db.From<Tag>()
                                                                   .Select(t => t.Id)
                                                                   .Where(t => t.Lowercase == x.Lowercase)
                                                                   .Limit(1)
                                                                 );
                                                         }

                                                         var productTag = new ProductTag
                                                                          {
                                                                              ProductId = product.Id,
                                                                              TagId = tagId
                                                                          };
                                                         if (!db.Exists<ProductTag>(productTag))
                                                         {
                                                             db.Save(productTag);
                                                         }
                                                     });

                                     db.Save(product, true);
                                 }
                                 return product;
                             }).Count();

                Console.WriteLine($"Saved {count} Products.\n");
            }
            Console.WriteLine(@"DONE");
            Console.WriteLine(@"Newline to quit...");
            Console.ReadLine();
        }
    }
}
