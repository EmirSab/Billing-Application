using Billing.Api.Helpers;
using Billing.Database;
using Billing.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Billing.Api.Controllers
{
    [RoutePrefix("api/stock")]
    public class StockController : BaseController
    {
        //[Route("")]
        ////TokenAuthorization("user,admin")]
        //public IHttpActionResult GetAll(int page = 0)
        //{
        //    int PageSize = 8;
        //    var query = UnitOfWork.Stocks.Get().OrderBy(x => x.Id).ToList();
        //    int TotalPages = (int)Math.Ceiling((double)query.Count() / PageSize);

        //    var returnObject = new
        //    {
        //        pageSize = PageSize,
        //        currentPage = page,
        //        totalPages = TotalPages,
        //        stocksList = query.Skip(PageSize * page).Take(PageSize).Select(x => Factory.Create(x)).ToList()
        //    };
        //    return Ok(returnObject);
        //}

        [Route("")]
        //TokenAuthorization("user,admin")]
        public IHttpActionResult Get()
        {
            try
            {
                return Ok(UnitOfWork.Stocks.Get().ToList().Select(x => Factory.Create(x)).ToList());
            }
            catch (Exception ex)
            {
                Helper.Log(ex.Message, "ERROR");
                return BadRequest(ex.Message);
            }
        }

        [Route("{id:int}")]
        public IHttpActionResult Get(int id)
        {
            try
            {
                Stock stock = UnitOfWork.Stocks.Get(id);
                if (stock == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(Factory.Create(stock));
                }
            }
            catch (Exception ex)
            {
                Helper.Log(ex.Message, "ERROR");
                return BadRequest(ex.Message);
            }
        }
        [Route("{id:int}")]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                Stock stock = UnitOfWork.Stocks.Get(id);
                if (stock == null) return NotFound();
                UnitOfWork.Stocks.Delete(id);
                UnitOfWork.Stocks.Commit();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}