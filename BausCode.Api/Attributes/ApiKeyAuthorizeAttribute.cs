using System;
using System.Net;
using BausCode.Api.Models;
using ServiceStack;
using ServiceStack.Web;

namespace BausCode.Api.Attributes
{
    class ApiKeyAuthorizeAttribute : AuthenticateAttribute
    {
        // TODO(jcunnningham)
        // make these configurable
        //
        private const string AuthHeader = @"Authorization";
        private const string AuthPreamble = @"BC-Client";
        private const string AuthApiKeyName = @"apiKey";
        private const char AuthApiKeyValueSeparator = '=';
        private const char AuthApiValueWrapper = '"';
        private const int UnauthorizedStatus = (int)HttpStatusCode.Unauthorized;
        private const string UnauthorizedDescription = @"Unauthorized - Invalid or missing API key";

        public override void Execute(IRequest req, IResponse res, object requestDto)
        {
            var keyStore = req.TryResolve<IApiKeyStore>();
            var header = req.GetHeader(AuthHeader);

            if (!string.IsNullOrEmpty(header) && header.StartsWith(AuthPreamble))
            {
                var kvp = header.Replace(AuthPreamble, "").Trim().Split(new[] { AuthApiKeyValueSeparator }, 2);
                Guid key;
                if (kvp.Length > 1
                    && kvp[0].EqualsIgnoreCase(AuthApiKeyName)
                    && Guid.TryParse(kvp[1].Trim(AuthApiValueWrapper).UrlDecode(), out key)
                    && keyStore.IsValid(key)
                    )
                {
                    // TODO(jcunningham)
                    // throttling, counter increment, things like that.
                    //
                    return;
                }
            }

            // try request parameters
            var param = req.GetParam("apiKey");
            if (!string.IsNullOrEmpty(param))
            {
                Guid key;
                param = param.Trim(AuthApiValueWrapper).UrlDecode();
                if (Guid.TryParse(param, out key) && keyStore.IsValid(key))
                {
                    // TODO(jcunningham)
                    // throttling, counter increment, things like that.
                    //
                    return;
                }
            }


            res.StatusCode = UnauthorizedStatus;
            res.StatusDescription = UnauthorizedDescription;
            res.End();
        }
    }
}
