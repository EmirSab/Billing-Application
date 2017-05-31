using Billing.Api.Helpers;
using Billing.Api.Models;
using Billing.Database;
using System;
using System.Linq;
using System.Web.Http;

namespace Billing.Api.Controllers
{
    [RoutePrefix("api/procurements")]
    public class ProcurementsController : BaseController
    {
        [Route("")]
        //TokenAuthorization("user,admin")]
        public IHttpActionResult GetAll(int page = 0)
        {
            int PageSize = 50;
            var query = UnitOfWork.Procurements.Get().OrderBy(x => x.Id).ToList();
            int TotalPages = (int)Math.Ceiling((double)query.Count() / PageSize);

            var returnObject = new
            {
                pageSize = PageSize,
                currentPage = page,
                totalPages = TotalPages,
                procurementsList = query.Skip(PageSize * page).Take(PageSize).Select(x => Factory.Create(x)).ToList()
            };
            return Ok(returnObject);
        }
        //[Route("")]
        //[TokenAuthorization("user,admin")]
        //public IHttpActionResult Get()
        //{
        //    return Ok(UnitOfWork.Procurements.Get().ToList().Select(x => Factory.Create(x)).ToList());
        //}

        [Route("doc/{doc}")]
        //[TokenAuthorization("user,admin")]
        public IHttpActionResult GetByDocument(string doc)
        {
            return Ok(UnitOfWork.Procurements.Get().Where(x => x.Document == doc).ToList().Select(x => Factory.Create(x)).ToList());
        }

        [Route("product/{id}")]
        //[TokenAuthorization("user,admin")]
        public IHttpActionResult GetByProduct(int id)
        {
            return Ok(UnitOfWork.Procurements.Get().Where(x => x.Product.Id == id).ToList().Select(x => Factory.Create(x)).ToList());
        }

        [Route("{id:int}")]
        //[TokenAuthorization("user,admin")]
        public IHttpActionResult Get(int id)
        {
            try
            {
                Procurement procurement = UnitOfWork.Procurements.Get(id);
                if (procurement == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(Factory.Create(procurement));
                }
            }
            catch (Exception ex)
            {
                Helper.Log(ex.Message, "ERROR");
                return BadRequest(ex.Message);
            }
        }

        //Insert procurements
        [Route("")]
        //[TokenAuthorization("admin")]
        public IHttpActionResult Post(ProcurementModel model)
        {
            try
            {
                Procurement procurement = Factory.Create(model); //pravljenje procurementa
                UnitOfWork.Procurements.Insert(procurement); //ubacivanje procurementa
                UnitOfWork.Commit();
                return Ok(Factory.Create(procurement));
            }
            catch (Exception ex)
            {
                Helper.Log(ex.Message, "ERROR");
                return BadRequest(ex.Message);
            }
        }

        //Update procurements
        [Route("{id}")]
        //[TokenAuthorization("admin")]
        public IHttpActionResult Put(int id, ProcurementModel model)
        {
            try
            {
                Procurement procurement = Factory.Create(model);
                UnitOfWork.Procurements.Update(procurement, id);
                UnitOfWork.Commit();
                return Ok(Factory.Create(procurement));
            }
            catch (Exception ex)
            {
                Helper.Log(ex.Message, "ERROR");
                return BadRequest(ex.Message);
            }
        }

        //brisanje procuremnta
        [Route("{id}")]
        //[TokenAuthorization("admin")]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                Procurement entity = UnitOfWork.Procurements.Get(id);
                if (entity == null) return NotFound();
                UnitOfWork.Procurements.Delete(id);
                UnitOfWork.Commit();
                return Ok();
            }
            catch (Exception ex)
            {
                Helper.Log(ex.Message, "ERROR");
                return BadRequest(ex.Message);
            }
        }
    }
}
