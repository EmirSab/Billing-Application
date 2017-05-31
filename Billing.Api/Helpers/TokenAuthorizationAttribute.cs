using Billing.Database;
using Billing.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace Billing.Api.Helpers
{
    public class TokenAuthorizationAttribute : AuthorizationFilterAttribute
    {
        private IEnumerable<string> hValues;
        private string[] _roles;
        public TokenAuthorizationAttribute(string role)
        {
            _roles = role.Split(',');
        }

        public override void OnAuthorization(HttpActionContext actionContext)
        {
            string ApiKey = null, Token = null;
            if (actionContext.Request.Headers.TryGetValues("ApiKey", out hValues)) ApiKey = hValues.First();
            if (actionContext.Request.Headers.TryGetValues("Token", out hValues)) Token = hValues.First();
            if (!(ApiKey == null || Token == null))
            {
                using (UnitOfWork unitOfWork = new UnitOfWork())
                {
                    AuthToken token = unitOfWork.Tokens.Get().FirstOrDefault(x => x.Token == Token);
                    if (token != null)
                    {
                        if (token.ApiUser.AppId == ApiKey && token.Expiration > DateTime.Now) //utc uklonjeno za deploy error 401 unauthorized
                        {
                            BillingIdentity.Agent = token.Agent;
                            foreach (string role in _roles)
                                if (BillingIdentity.CurrentUser.Roles.Any(role.Contains)) return;
                        }
                    }
                }
            }
            actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
        }
    }
}
