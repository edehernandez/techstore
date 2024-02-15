using Catalog.Business.Model;
using Catalog.Business.Providers;
using Microsoft.Extensions.Configuration;

namespace Catalog.Data.Providers
{
    public class AuthenticationSettingsProvider : IAuthenticationProvider
    {
        private readonly IConfiguration settings;

        public AuthenticationSettingsProvider(IConfiguration settings)
        {
            this.settings = settings;
        }

        public bool Login(Login login)
        {
            var email = settings["ApiCredendials:Email"];
            var pass = settings["ApiCredendials:Password"];

            return login.Email.Equals(email) && login.Password.Equals(pass);
        }
    }
}
