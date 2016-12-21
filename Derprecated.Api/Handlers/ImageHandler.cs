namespace Derprecated.Api.Handlers
{
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Drawing.Imaging;
    using System.IO;
    using Microsoft.WindowsAzure.Storage;
    using Microsoft.WindowsAzure.Storage.Blob;
    using ServiceStack;
    using ServiceStack.Configuration;
    using ServiceStack.Web;

    public class ImageHandler
    {
        public ImageHandler(AppSettings appSettings, CloudBlobClient blobClient)
        {
            Container = appSettings.Get("app.storage.container", "dev");
            Client = blobClient;
        }

        private CloudBlobClient Client { get; }

        private string Container { get; }

        public static string GetRandomFilename()
        {
            return $"{SessionExtensions.CreateRandomBase62Id(24)}.png";
        }

        public static CloudBlockBlob Upload(CloudBlobContainer container, Stream file)
        {
            var filename = GetRandomFilename();
            var blob = container.GetBlockBlobReference(filename);

            using (var dest = new MemoryStream())
            using (var raw = Image.FromStream(file))
            {
                var w = raw.Width;
                var h = raw.Height;
                var d = Math.Min(Math.Min(w, h), 600);
                var r = d/2;
                var cx = raw.Width/2;
                var cy = raw.Height/2;

                //using (var source = new Bitmap(raw, d, d))
                using (var avatar = new Bitmap(d, d, PixelFormat.Format32bppArgb))
                using (var g = Graphics.FromImage(avatar))
                {
                    g.CompositingQuality = CompositingQuality.HighSpeed;
                    g.InterpolationMode = InterpolationMode.Bicubic;
                    g.SmoothingMode = SmoothingMode.AntiAlias;

                    var bg = Color.Transparent;
                    var path = new GraphicsPath();

                    using (var br = new SolidBrush(bg))
                    {
                        g.FillRectangle(br, 0, 0, avatar.Width, avatar.Height);
                    }

                    //avatar.MakeTransparent();
                    path.AddEllipse(0, 0, d, d);
                    g.SetClip(path);
                    g.DrawImage(raw, new Rectangle(0, 0, d, d),
                        new Rectangle(cx - r, cy - r, d, d), GraphicsUnit.Pixel);

                    avatar.Save(dest, ImageFormat.Png);
                }

                dest.Position = 0;
                blob.UploadFromStreamAsync(dest).Wait();
                blob.Properties.ContentType = "image/png";
                blob.SetPropertiesAsync().Wait();
            }

            return blob;
        }

        public Uri SaveAvatarImage(IHttpFile source)
        {
            Uri result;
            var container = Client.GetContainerReference(Container);

            container.CreateIfNotExistsAsync(BlobContainerPublicAccessType.Container, new BlobRequestOptions(), new OperationContext())
                .Wait();
            result = Upload(container, source.InputStream).Uri;

            return result;
        }
    }
}
