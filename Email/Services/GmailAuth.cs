using Email.Models;
using Google.Apis.Auth;
using Google.Apis.Auth.OAuth2;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Email.Services
{
    public class GmailAuth
    {
        public async Task<GmailAuthModel> AuthAsync()
        {
            var credential = await GoogleWebAuthorizationBroker.AuthorizeAsync(new ClientSecrets
            {
                ClientId = Settings.gmailClientId,
                ClientSecret = Settings.gmailKey
            },
            new[] { "email", "profile", "https://mail.google.com/" }, "user", CancellationToken.None
            );

            var jwtPayload = GoogleJsonWebSignature.ValidateAsync(credential.Token.IdToken).Result;
            var username = jwtPayload.Email;

            return new GmailAuthModel
            {
                Email = username,
                Token = credential.Token.AccessToken
            };
        }
    }
}
