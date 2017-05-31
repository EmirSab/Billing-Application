using Billing.Api.Models;
using Billing.Database;
using Billing.Repository;
using System.Linq;
using System.Threading;
using System.Web.Security;

namespace Billing.Api.Helpers
{
    public static class BillingIdentity
    {
        public static Agent Agent;

        public static CurrentUserModel CurrentUser
        {
            get
            {
                Billing.Database.CurrentUser.Id = Agent.Id;
                CurrentUserModel model = new CurrentUserModel()
                {
                    Id = Agent.Id,
                    Name = Agent.Name,
                    Roles = Roles.GetRolesForUser(Agent.Username)
                };
                return model;
            }
        }
    }
}