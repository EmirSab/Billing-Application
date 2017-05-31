using Billing.Api.Helpers;
using Billing.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Billing.Api.Controllers.ReportControllers
{
    //[TokenAuthorization("user")]
    public class SalesByAgentController : BaseController
    {
        public IHttpActionResult Post(RequestModel request)
        {
            try
            {
                return Ok(Reports.SalesByAgent.Report(request));
            }
            catch (Exception ex)
            {
                Helper.Log(ex.Message, "ERROR");
                return BadRequest(ex.Message);
            }
        }
    }
}
