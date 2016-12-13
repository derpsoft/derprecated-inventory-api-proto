namespace BausCode.Api.Models
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.Serialization;
    using ServiceStack;
    using ServiceStack.Auth;
    using ServiceStack.Data;
    using ServiceStack.OrmLite;

    [DataContract]
    public class UserSession : AuthUserSession
    {
        public override void OnAuthenticated(IServiceBase authService, IAuthSession session, IAuthTokens tokens,
                                             Dictionary<string, string> authInfo)
        {
            base.OnAuthenticated(authService, session, tokens, authInfo);

            var userRepo = authService.TryResolve<IUserAuthRepository>();
            var user = userRepo.GetUserAuth(UserAuthId);

            Roles = userRepo.GetRoles(user).ToList();
            Permissions = userRepo.GetPermissions(user).ToList();
            //var userId = UserAuthId.ToInt();

            //using (var ctx = authService.TryResolve<IDbConnectionFactory>().Open())
            //{
            //}
        }
    }
}
