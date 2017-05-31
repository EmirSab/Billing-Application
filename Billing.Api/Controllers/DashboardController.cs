using Billing.Api.Helpers;
using Billing.Api.Reports;
using System;
using System.Web.Http;

namespace Billing.Api.Controllers
{       
    //[TokenAuthorization("user,admin")]
    public class DashboardController : BaseController
    {
 
        public IHttpActionResult Get(int id = 0)
        {
            try
            {
                return Ok(Reports.Dashboard.Report(id));
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
