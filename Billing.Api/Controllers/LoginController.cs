using Billing.Api.Helpers;
using Billing.Api.Models;
using Billing.Database;
using System;
using System.Linq;
using System.Text;
using System.Threading;
using System.Web.Http;
using WebMatrix.WebData;

namespace Billing.Api.Controllers
{
    public class LoginController : BaseController
    {
        [BillingAuthorization]
        [Route("api/login")]
        [HttpPost]
        public IHttpActionResult Login(TokenRequestModel request)
        {
            ApiUser apiUser = UnitOfWork.ApiUsers.Get().FirstOrDefault(x => x.AppId == request.ApiKey);
            if (apiUser == null) return NotFound();
            if (Helper.Signature(apiUser.Secret, apiUser.AppId) != request.Signature) return BadRequest("Bad application signature");

            BillingIdentity.Agent = UnitOfWork.Agents.Get().FirstOrDefault(x => x.Username == Thread.CurrentPrincipal.Identity.Name);

            string rawTokenInfo = DateTime.Now.Ticks.ToString() + apiUser.AppId;
            byte[] rawTokenByte = Encoding.UTF8.GetBytes(rawTokenInfo);
            var authToken = new AuthToken()
            {
                Token = Convert.ToBase64String(rawTokenByte),
                Expiration = DateTime.Now.AddMinutes(20),
                Remember = (request.Remember == "True") ? Factory.Create() : null,
                ApiUser = apiUser,
                Agent = BillingIdentity.Agent
            };

            UnitOfWork.Tokens.Insert(authToken);
            UnitOfWork.Commit();
            return Ok(Factory.Create(authToken));
        }

        [Route("api/logout")]
        [HttpGet]
        public IHttpActionResult Logout()
        {
            if (BillingIdentity.Agent != null)
            {
                string message = $"User {BillingIdentity.CurrentUser.Name} logged out";
                if (!WebSecurity.Initialized) WebSecurity.InitializeDatabaseConnection("Billing", "Agents", "Id", "Username", autoCreateTables: true);
                WebSecurity.Logout();
                return Ok(message);
            }
            else
            {
                return Ok("No user is logged in!!!");
            }
        }

        [Route("api/remember")]
        [HttpPost]
        public IHttpActionResult Remember(TokenRequestModel request)
        {
            AuthToken token = UnitOfWork.Tokens.Get().FirstOrDefault(x => x.Remember == request.Remember);
            if (token == null) return NotFound();

            if (token.ApiUser.AppId != request.ApiKey) return NotFound();

            ApiUser apiUser = UnitOfWork.ApiUsers.Get().FirstOrDefault(x => x.AppId == request.ApiKey);
            if (apiUser == null) return NotFound();
            if (Helper.Signature(apiUser.Secret, apiUser.AppId) != request.Signature) return BadRequest("Bad application signature");

            BillingIdentity.Agent = token.Agent;

            string rawTokenInfo = DateTime.Now.Ticks.ToString() + apiUser.AppId;
            byte[] rawTokenByte = Encoding.UTF8.GetBytes(rawTokenInfo);
            var authToken = new AuthToken()
            {
                Token = Convert.ToBase64String(rawTokenByte),
                Expiration = DateTime.Now.AddMinutes(20),
                Remember = Factory.Create(),
                ApiUser = apiUser,
                Agent = token.Agent
            };

            UnitOfWork.Tokens.Delete(token.Id);
            UnitOfWork.Tokens.Insert(authToken);
            UnitOfWork.Commit();
            return Ok(Factory.Create(authToken));
        }

    }
}