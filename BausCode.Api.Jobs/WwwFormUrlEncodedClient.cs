namespace BausCode.Api.Jobs
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using ServiceStack;
    using ServiceStack.Text;
    using ServiceStack.Web;

    public class WwwFormUrlEncodedClient : ServiceClientBase
    {
        public override string ContentType
        {
            get { return ApiConstants.CONTENT_TYPE_X_WWW_FORM_URLENCODED; }
        }

        public override string Format
        {
            get { return ApiConstants.FORMAT_X_WWW_FORM_URLENCODED; }
        }

        public override StreamDeserializerDelegate StreamDeserializer
        {
            get { return DeserializeFromStream; }
        }

        public override void SerializeToStream(IRequest requestContext, object request, Stream stream)
        {
            var body =
                request.ToStringDictionary()
                       .Select(kvp => "{0}={1}".Fmt(kvp.Key.UrlEncode(), kvp.Value.UrlEncode()))
                       .Join("&");
            var bodyBytes = Encoding.ASCII.GetBytes(body);
            stream.Write(bodyBytes, 0, bodyBytes.Length);
        }

        public override T DeserializeFromStream<T>(Stream stream)
        {
            return (T) DeserializeFromStream(typeof (T), stream);
        }

        private static object DeserializeFromStream(Type type, Stream fromStream)
        {
            var bodyBuf = fromStream.ReadFully();
            var body = Encoding.ASCII.GetString(bodyBuf);
            var response = type.CreateInstance();

            body.Split('&').ExecAll(raw =>
                                    {
                                        var split = raw.Split('=');
                                        var kvp = new KeyValuePair<string, string>(split[0], split[1]);

                                        var prop = type.GetProperty(kvp.Key.UrlDecode());
                                        prop.SetValue(response, kvp.Value.UrlDecode().To(prop.PropertyType));
                                    });

            return response;
        }
    }
}
