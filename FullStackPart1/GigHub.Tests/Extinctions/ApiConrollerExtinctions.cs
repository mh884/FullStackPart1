using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace GigHub.Tests.Extinctions
{
    public static class ApiConrollerExtinctions
    {

        public static void MockCurrentUser(this ApiController conroller, string useid, string username)
        {
            var identity = new GenericIdentity("user1@domain.com");
            identity.AddClaim(
                new Claim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name", username));

            identity.AddClaim(
                new Claim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier", useid));

            var principal = new GenericPrincipal(identity, null);
            conroller.User = principal;
        }
    }
}
