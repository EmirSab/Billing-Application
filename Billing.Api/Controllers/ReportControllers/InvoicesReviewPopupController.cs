using Billing.Api.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Billing.Api.Controllers.ReportControllers
{
    //[TokenAuthorization("user")]
    [RoutePrefix("api/invoicereview")]
    public class InvoicesReviewPopupController : BaseController
    {
        [Route("{id:int}")]
        public IHttpActionResult Get(int id)
        {
            try
            {
                return Ok(Reports.InvoiceReviewPopup.Report(id));
            }
            catch (Exception ex)
            {
                Helper.Log(ex.Message, "ERROR");
                return BadRequest(ex.Message);
            }
        }
    }
}


