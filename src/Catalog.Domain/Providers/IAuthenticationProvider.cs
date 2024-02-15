using Catalog.Business.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Business.Providers
{
    public interface IAuthenticationProvider
    {
        bool Login(Login login);
    }
}
