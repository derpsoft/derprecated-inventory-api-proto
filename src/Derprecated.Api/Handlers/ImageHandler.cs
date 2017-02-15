namespace Derprecated.Api.Handlers
{
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Drawing.Imaging;
    using System.IO;
    using Microsoft.WindowsAzure.Storage;
    using Microsoft.WindowsAzure.Storage.Blob;
    using Models.Configuration;
    using ServiceStack;
    using ServiceStack.Web;

    public class ImageHandler
    {
        public ImageHandler(ApplicationConfiguration appSettings, CloudBlobClient blobClient)
        {
            Container = appSettings.Storage.Container;
            Client = blobClient;
        }

        private CloudBlobClient Client { get; }

        private string Container { get; }

        public static string GetRandomFilename(string folder = "")
        {
            if (!folder.IsNullOrEmpty() && !folder.EndsWith("/"))
                folder = folder + '/';

            return $"{folder}{SessionExtensions.CreateRandomBase62Id(24)}.png";
        }

        public static CloudBlockBlob Upload(CloudBlobContainer container, Stream file, string filename)
        {
            var blob = container.GetBlockBlobReference(filename);

            using (var dest = new MemoryStream())
            using (var raw = Image.FromStream(file))
            {
                var w = raw.Width;
                var h = raw.Height;

                //using (var source = new Bitmap(raw, d, d))
                using (var avatar = new Bitmap(w, h, PixelFormat.Format32bppArgb))
                using (var g = Graphics.FromImage(avatar))
                {
                    g.CompositingQuality = CompositingQuality.HighSpeed;
                    g.InterpolationMode = InterpolationMode.Bicubic;
                    g.SmoothingMode = SmoothingMode.AntiAlias;

                    g.DrawImage(raw, new Rectangle(0, 0, w, h),
                        new Rectangle(0, 0, w, h), GraphicsUnit.Pixel);

                    avatar.Save(dest, ImageFormat.Png);
                }

                dest.Position = 0;
                blob.UploadFromStreamAsync(dest).Wait();
                blob.Properties.ContentType = "image/png";
                blob.SetPropertiesAsync().Wait();
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
    }
}
