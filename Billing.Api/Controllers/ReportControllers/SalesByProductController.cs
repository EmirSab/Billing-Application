using Billing.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Billing.Api.Controllers.ReportControllers
{
    public class SalesByProductController : BaseController
    {
        //[TokenAuthorization("user")]
        public IHttpActionResult Post(RequestModel request)
        {
            try
            {
                return Ok(Reports.SalesByProduct.Report(request));

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
