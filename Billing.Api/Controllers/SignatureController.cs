using Billing.Api.Helpers;
using Billing.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Billing.Api.Controllers
{
    public class SignatureController : BaseController
    {
        public string Get()
        {
            ApiUser apiUser = UnitOfWork.ApiUsers.Get().First();
            return Helper.Signature(apiUser.Secret, apiUser.AppId);
        }
    }
}
