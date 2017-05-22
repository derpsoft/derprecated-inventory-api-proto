namespace Derprecated.Api.Handlers
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Drawing.Imaging;
    using System.IO;
    using Microsoft.WindowsAzure.Storage;
    using Microsoft.WindowsAzure.Storage.Blob;
    using Models.Configuration;
    using ServiceStack;
    using ServiceStack.Data;
    using ServiceStack.OrmLite;
    using ServiceStack.Web;
    using Image = Models.Image;

    // ReSharper disable once ClassNeverInstantiated.Global
    public class ImageHandler : CrudHandler<Image>
    {
        public ImageHandler(ApplicationConfiguration appSettings, CloudBlobClient blobClient, IDbConnectionFactory db)
            : base(db)
        {
            Container = appSettings.Storage.Container;
            Client = blobClient;
        }

        private CloudBlobClient Client { get; }

        private string Container { get; }

        private static string GetRandomFilename(string folder = "")
        {
            if (!folder.IsNullOrEmpty() && !folder.EndsWith("/"))
                folder = folder + '/';

            return $"{folder}{SessionExtensions.CreateRandomBase62Id(24)}.png";
        }

        private static CloudBlockBlob Upload(CloudBlobContainer container, Stream file, string filename)
        {
            var blob = container.GetBlockBlobReference(filename);

            using (var dest = new MemoryStream())
            {
                using (var raw = System.Drawing.Image.FromStream(file))
                {
                    var w = raw.Width;
                    var h = raw.Height;

                    //using (var source = new Bitmap(raw, d, d))
                    using (var avatar = new Bitmap(w, h, PixelFormat.Format32bppArgb))
                    {
                        using (var g = Graphics.FromImage(avatar))
                        {
                            g.CompositingQuality = CompositingQuality.HighSpeed;
                            g.InterpolationMode = InterpolationMode.Bicubic;
                            g.SmoothingMode = SmoothingMode.AntiAlias;

                            g.DrawImage(raw, new Rectangle(0, 0, w, h),
                                new Rectangle(0, 0, w, h), GraphicsUnit.Pixel);

                            avatar.Save(dest, ImageFormat.Png);
                        }
                    }

                    dest.Position = 0;
                    blob.UploadFromStreamAsync(dest).Wait();
                    blob.Properties.ContentType = "image/png";
                    blob.SetPropertiesAsync().Wait();
                }
            }

            return blob;
        }

        public Uri SaveImage(IHttpFile source, string folder)
        {
            folder.ThrowIfNullOrEmpty(nameof(folder));

            Uri result;
            var container = Client.GetContainerReference(Container);
            var filename = GetRandomFilename(folder);

            container.CreateIfNotExistsAsync(BlobContainerPublicAccessType.Container, new BlobRequestOptions(),
                         new OperationContext())
                     .Wait();
            result = Upload(container, source.InputStream, filename).Uri;

            return result;
        }

        public override List<Image> Typeahead(string q, bool includeDeleted = false)
        {
            var query = Db.From<Image>()
                          .Where(x => x.Filename.Contains(q))
                          .Or(x => x.Url.Contains(q))
                          .Or(x => x.SourceUrl.Contains(q))
                          .Or(x => x.Extension.Contains(q));

            if (!includeDeleted)
                query = query.And(x => !x.IsDeleted);

            return Db.Select(query.OrderByDescending(x => x.CreateDate)
                                  .SelectDistinct());
        }
    }
}
