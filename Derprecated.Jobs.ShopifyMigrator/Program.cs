using System;
using System.Configuration;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using BausCode.Api.Models;
using BausCode.Api.Models.Routing.Shopify;
using Funq;
using ServiceStack;
using ServiceStack.Configuration;
using ServiceStack.Data;
using ServiceStack.MiniProfiler.Storage;
using ServiceStack.OrmLite;
// ReSharper disable AccessToDisposedClosure

namespace Derprecated.Jobs.ShopifyMigrator
{
    internal class Program
    {
        static Program()
        {
            var license = FindLicense();
            if (!license.IsNullOrEmpty())
            {
                Licensing.RegisterLicense(license);
            }

            Container = new Container();
            Configure(Container);
        }

        private static Container Container { get; }

        private static string FindLicense()
        {
            var appSettings = new AppSettings();

            var license = Environment.GetEnvironmentVariable("ss.license") ?? appSettings.GetString("ss.license");

            return license;
        }

        [SuppressMessage("ReSharper", "InvertIf")]
        private static void Configure(Container container)
        {
            var appSettings = new AppSettings();

            // DB
            container.Register<IDbConnectionFactory>(c =>
            {
                var connectionString =
                    ConfigurationManager.ConnectionStrings["AzureSql"].ConnectionString;

                return new OrmLiteConnectionFactory(connectionString, SqlServerDialect.Provider);
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
                var domain = appSettings.Get("shopify.store.domain");
                var apiKey = appSettings.Get("shopify.api.key");
                var password = appSettings.Get("shopify.api.password");

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
                db.CreateTableIfNotExists<ProductVariant>();
                db.CreateTableIfNotExists<ProductTag>();
                db.CreateTableIfNotExists<ProductImage>();
            }
        }

        private static void Main(string[] args)
        {
            using (var client = Container.Resolve<JsonServiceClient>())
            {
                Console.WriteLine(@"Requesting latest product counts...");

                var shopifyCount = client.Get(new GetProductsCount());

                Console.WriteLine($"Found\n\tShopify: {shopifyCount.Count}\n");
                Console.WriteLine("Merging...\n");

                var shopifyProducts = client.Get(new GetProducts { Limit = shopifyCount.Count });

                var count = shopifyProducts.Products.AsParallel().Select(p =>
                {
                    Product product;
                    using (var db = Container.Resolve<IDbConnectionFactory>().Open())
                    {
                        product = db.Where<Product>(new { ShopifyId = p.Id }).SingleOrDefault();

                        if (product == default(Product))
                        {
                            product = Product.From(p);
                            Console.WriteLine($"New [{product.ShopifyId}] {p.Title.Truncate(40)}...");
                        }
                        else
                        {
                            db.LoadReferences(product);
                            product.Merge(p);

                            Console.WriteLine(
                                $"Existing [{product.ShopifyId} -> {product.Id}] {p.Title.Truncate(40)}...");
                        }

                        product.Meta.Tags.Split(',')
                            .Map(x => new Tag { Lowercase = x.ToLowerSafe().Trim(), Name = x.Trim() })
                            .ExecAll(x =>
                            {
                                var tagId = -1;
                                if (!db.Exists<Tag>(new { x.Lowercase }))
                                {
                                    db.Save(x);
                                    tagId = (int)db.LastInsertId();
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

                                var productTag = new ProductTag { ProductId = product.Id, TagId = tagId };
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