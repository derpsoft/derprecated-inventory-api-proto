using System;
using System.IO;
using ServiceStack;
using ServiceStack.Web;

namespace BausCode.Api.Jobs
{
    public class HybridClient<TRequest, TResponse> : ServiceClientBase
        where TRequest : ServiceClientBase
        where TResponse : ServiceClientBase
    {
        public override string Format
        {
            get { return Activator.CreateInstance<TResponse>().Format; }
        }

        public override string Accept
        {
            get { return Activator.CreateInstance<TResponse>().ContentType; }
        }

        public override string ContentType
        {
            get { return Activator.CreateInstance<TRequest>().ContentType; }
        }

        public override StreamDeserializerDelegate StreamDeserializer
        {
            get { return Activator.CreateInstance<TResponse>().StreamDeserializer; }
        }

        public override void SerializeToStream(IRequest requestContext, object request, Stream stream)
        {
            Activator.CreateInstance<TRequest>().SerializeToStream(requestContext, request, stream);
        }

        public override T DeserializeFromStream<T>(Stream stream)
        {
            return Activator.CreateInstance<TResponse>().DeserializeFromStream<T>(stream);
        }
    }
}