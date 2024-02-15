using Catalog.Business.Model;
using Catalog.Business.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Business.Services
{
    public class AuthenticationService
    {
        private readonly IAuthenticationProvider authenticationProvider;

        public AuthenticationService(IAuthenticationProvider authenticationProvider)
        {
            this.authenticationProvider = authenticationProvider;
        }

        public bool Login(Login login)
        {
            return authenticationProvider.Login(login);
        }
    }
}
