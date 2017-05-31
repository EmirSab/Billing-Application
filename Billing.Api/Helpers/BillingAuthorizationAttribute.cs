using System;
using System.Net;
using System.Net.Http;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Web.Security;
using WebMatrix.WebData;

namespace Billing.Api.Helpers
{
    public class BillingAuthorizationAttribute : AuthorizationFilterAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            var authHeader = actionContext.Request.Headers.Authorization;
            if (authHeader != null)
            {
                if (authHeader.Scheme.ToLower() == "basic" && !string.IsNullOrWhiteSpace(authHeader.Parameter))
                {
                    var rawCredentials = authHeader.Parameter;
                    var encoding = Encoding.GetEncoding("utf-8");
                    string credentials = encoding.GetString(Convert.FromBase64String(rawCredentials));

                    string[] split = credentials.Split(':');
                    string username = split[0];
                    string password = split[1];

                    if (!(string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password)))
                    {
                        if (!WebSecurity.Initialized) WebSecurity.InitializeDatabaseConnection("Billing", "Agents", "Id", "Username", autoCreateTables: true);
                        if (WebSecurity.Login(username, password))
                        {
                            string[] roles = Roles.GetRolesForUser(username);
                            Thread.CurrentPrincipal = new GenericPrincipal(new GenericIdentity(username), roles);
                            return;
                        }
                    }
                }
            }
            actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
            actionContext.Response.Headers.Add("WWW-Authenticate", "Basic Scheme='Billing' location=''");
            // location='http://localhost:444/accounts/login'
        }
    }
}