﻿namespace Derprecated.Api.Services
{
    using System;
    using System.Text.RegularExpressions;
    using MailKit.Net.Smtp;
    using MimeKit;
    using Models.Routing;
    using ServiceStack;
    using ServiceStack.Auth;

    public class PasswordService : BaseService
    {
        private static readonly TimeSpan Expiration = TimeSpan.FromHours(4);
        public SmtpClient SmtpClient { get; set; }

        public object Any(ResetPassword request)
        {
            var res = new ResetPasswordResponse();
            var user = UserAuthRepository.GetUserAuthByUserName(request.Email);
            if (null == user)
            {
                res.Success = false;
                res.Message = "Invalid email address.";
                return res;
            }

            var secret = Cache.Get<string>($"password:secret:{user.Email}");
            if (secret.IsNullOrEmpty() || !secret.Equals(request.Token))
            {
                res.Success = false;
                res.Message = "Reset window expired.";
                return res;
            }

            UserAuthRepository.UpdateUserAuth(user, user, request.Password);
            Cache.Remove($"password:secret:{user.Email}");

            using (var service = ResolveService<AuthenticateService>())
            {
                return service.Authenticate(new Authenticate
                {
                    provider = AuthenticateService.CredentialsProvider,
                    UserName = user.Email,
                    Password = request.Password
                });
            }
        }

        public object Any(ForgotPassword request)
        {
            var res = new ForgotPasswordResponse();
            var user = UserAuthRepository.GetUserAuthByUserName(request.Email);

            if (null == user)
            {
                res.Success = false;
                res.Message = "Invalid email address.";
                return res;
            }

            var secret = Regex.Replace(SessionExtensions.CreateRandomBase62Id(32), @"[^\w\d]", "",
                RegexOptions.IgnoreCase);
            var link = $"{Configuration.Web.Domain}{Configuration.Web.PasswordResetLinkFormat.Fmt(user.Email, secret)}";
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(Configuration.Mail.From));
            message.To.Add(new MailboxAddress(user.Email));
            message.Subject = "[Derprecated] Password Reset";
            message.Body = new TextPart("html")
            {
                Text =
                    $@"
                <html>
                    <head></head>
                    <body>
                        <p>
                            Click on the following link to reset your password:
                            <br/><br/>
                            <a href=""{
                        link}"">{link
                        }</a>
                            <br/><br/>
                            This link will expire in 4 hours.
                        </p>
                    </body>
                </html>
                "
            };

            Cache.Set($"password:secret:{user.Email}", secret, Expiration);
            SmtpClient.Send(message);

            res.Success = true;
            res.Message = null;

            return res;
        }
    }
}
