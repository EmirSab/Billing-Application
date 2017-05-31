using Billing.Api.Helpers;
using Billing.Api.Models;
using Billing.Database;
using System;
using System.Linq;
using System.Web.Http;

namespace Billing.Api.Controllers
{
    [RoutePrefix("api/suppliers")]
    public class SuppliersController : BaseController
    {
        [Route("")]
        //TokenAuthorization("user,admin")]
        public IHttpActionResult GetAll(int page = 0)
        {
            int PageSize = 8;
            var query = UnitOfWork.Suppliers.Get().OrderBy(x => x.Id).ToList();
            int TotalPages = (int)Math.Ceiling((double)query.Count() / PageSize);

            var returnObject = new
            {
                pageSize = PageSize,
                currentPage = page,
                totalPages = TotalPages,
                suppliersList = query.Skip(PageSize * page).Take(PageSize).Select(x => Factory.Create(x)).ToList()
            };
            return Ok(returnObject);
        }
        //[Route("")]
        //[TokenAuthorization("user,admin")]
        //public IHttpActionResult Get()
        //{
        //    return Ok(UnitOfWork.Suppliers.Get().ToList().Select(x => Factory.Create(x)).ToList());
        //}

        [Route("{name}")]
        //[TokenAuthorization("user,admin")]
        public IHttpActionResult Get(string name)
        {
            return Ok(UnitOfWork.Suppliers.Get().Where(x => x.Name.Contains(name)).ToList().Select(a => Factory.Create(a)).ToList());
        }

        [Route("{id:int}")]
        //[TokenAuthorization("user,admin")]
        public IHttpActionResult Get(int id)
        {
            try
            {
                Supplier supplier = UnitOfWork.Suppliers.Get(id);
                if (supplier == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(Factory.Create(supplier));
                }
            }
            catch (Exception ex)
            {
                Helper.Log(ex.Message, "ERROR");
                return BadRequest(ex.Message);
            }
        }

        [Route("")]
        //[TokenAuthorization("admin")]
        public IHttpActionResult Post(SupplierModel model)
        {
            try
            {
                Supplier supplier = Factory.Create(model);
                UnitOfWork.Suppliers.Insert(supplier);
                UnitOfWork.Commit();
                return Ok(Factory.Create(supplier));
            }
            catch (Exception ex)
            {
                Helper.Log(ex.Message, "ERROR");
                return BadRequest(ex.Message);
            }
        }

        [Route("{id}")]
        //[TokenAuthorization("admin")]
        public IHttpActionResult Put(int id, SupplierModel model)
        {
            try
            {
                Supplier supplier = Factory.Create(model);
                UnitOfWork.Suppliers.Update(supplier, id);
                UnitOfWork.Commit();
                return Ok(Factory.Create(supplier));
            }
            catch (Exception ex)
            {
                Helper.Log(ex.Message, "ERROR");
                return BadRequest(ex.Message);
            }
        }

        [Route("{id}")]
        //[TokenAuthorization("admin")]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                Supplier entity = UnitOfWork.Suppliers.Get(id);
                if (entity == null) return NotFound();
                UnitOfWork.Suppliers.Delete(id);
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
