using System.Collections.Generic;
using System.Runtime.Serialization;
using ServiceStack;
using ServiceStack.Auth;
using ServiceStack.Data;
using ServiceStack.OrmLite;

namespace BausCode.Api.Models
{
    [DataContract]
    public class UserSession : AuthUserSession
    {
        public override void OnAuthenticated(IServiceBase authService, IAuthSession session, IAuthTokens tokens,
            Dictionary<string, string> authInfo)
        {
            base.OnAuthenticated(authService, session, tokens, authInfo);

            var userId = UserAuthId.ToInt();

            using (var ctx = authService.TryResolve<IDbConnectionFactory>().Open())
            {

            }
        }
    }
}