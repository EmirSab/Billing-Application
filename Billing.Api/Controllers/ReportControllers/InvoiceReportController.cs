using System;
using System.Web.Http;

namespace Billing.Api.Controllers
{
    [RoutePrefix("api/invoicereport")]
    //[TokenAuthorization("user")]
    public class InvoiceReportController : BaseController
    {
        [Route("{id:int}")]
        public IHttpActionResult Get(int id)
        {
            try
            {
                return Ok(Reports.Invoice.Report(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
