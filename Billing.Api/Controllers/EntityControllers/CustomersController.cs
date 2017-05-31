using Billing.Api.Helpers;
using Billing.Api.Models;
using Billing.Database;
using Billing.Repository;
using System;
using System.Linq;
using System.Web.Http;

namespace Billing.Api.Controllers
{
    [RoutePrefix("api/customers")]
    public class CustomersController : BaseController
    {
        [Route("")]
        [TokenAuthorization("user,admin")]
        public IHttpActionResult Get()
        {
            return Ok(UnitOfWork.Customers.Get().ToList().Select(x => Factory.Create(x)).ToList());
        }

        [Route("{name}")]
        [TokenAuthorization("user,admin")]
        public IHttpActionResult Get(string name)
        {
            return Ok(UnitOfWork.Customers.Get().Where(x => x.Name.Contains(name)).ToList().Select(a => Factory.Create(a)).ToList());
        }

        [Route("{id:int}")]
        [TokenAuthorization("user,admin")]
        public IHttpActionResult Get(int id)
        {
            try
            {
                Customer customer = UnitOfWork.Customers.Get(id);
                if (customer == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(Factory.Create(customer));
                }
            }
            catch (Exception ex)
            {
                Helper.Log(ex.Message, "ERROR");
                return BadRequest(ex.Message);
            }
        }

        [Route("")]
        [TokenAuthorization("user,admin")]
        public IHttpActionResult Post(CustomerModel model)
        {
            try
            {
                Customer customer = Factory.Create(model);
                UnitOfWork.Customers.Insert(customer);
                UnitOfWork.Commit();
                return Ok(Factory.Create(customer));
            }
            catch (Exception ex)
            {
                Helper.Log(ex.Message, "ERROR");
                return BadRequest(ex.Message);
            }
        }

        [Route("{id}")]
        [TokenAuthorization("user,admin")]
        public IHttpActionResult Put(int id, CustomerModel model)
        {
            try
            {
                Customer customer = Factory.Create(model);
                UnitOfWork.Customers.Update(customer, id);
                UnitOfWork.Commit();
                return Ok(Factory.Create(customer));
            }
            catch (Exception ex)
            {
                Helper.Log(ex.Message, "ERROR");
                return BadRequest(ex.Message);
            }
        }

        [Route("{id}")]
        [TokenAuthorization("admin")]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                Customer entity = UnitOfWork.Customers.Get(id);
                if (entity == null) return NotFound();
                UnitOfWork.Customers.Delete(id);
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
