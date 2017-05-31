using Billing.Api.Helpers;
using Billing.Api.Models;
using System;
using System.Web.Http;

namespace Billing.Api.Controllers
{
    [RoutePrefix("api/stocklevel")]
    public class StockLevelController : BaseController
    {
        [Route("{id:int}")]
        public IHttpActionResult Get(int id)
        {
            try
            {
                return Ok(Reports.StockLevel.Report(id));
            }
            catch (Exception ex)
            {
                Helper.Log(ex.Message, "ERROR");
                return BadRequest(ex.Message);
            }
        }
    }
}

