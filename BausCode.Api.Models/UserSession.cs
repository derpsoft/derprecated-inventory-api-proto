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
        [DataMember]
        public List<Keyword> UserKeywords { get; set; }

        public override void OnAuthenticated(IServiceBase authService, IAuthSession session, IAuthTokens tokens,
            Dictionary<string, string> authInfo)
        {
            base.OnAuthenticated(authService, session, tokens, authInfo);

            var userId = UserAuthId.ToInt();

            using (var ctx = authService.TryResolve<IDbConnectionFactory>().Open())
            {
                UserKeywords = ctx.LoadSelect(ctx.From<Keyword>().Where(k => k.UserAuthId == userId));
            }
        }
    }
}