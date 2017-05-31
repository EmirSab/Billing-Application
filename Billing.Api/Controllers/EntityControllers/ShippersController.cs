using Billing.Api.Helpers;
using Billing.Api.Models;
using Billing.Database;
using System;
using System.Linq;
using System.Web.Http;

namespace Billing.Api.Controllers
{
    [RoutePrefix("api/shippers")]
    public class ShippersController : BaseController
    {
        [Route("")]
        //[TokenAuthorization("user,admin")]
        public IHttpActionResult Get()
        {
            return Ok(UnitOfWork.Shippers.Get().ToList().Select(x => Factory.Create(x)).ToList());
        }

        [Route("{name}")]
        //[TokenAuthorization("user,admin")]
        public IHttpActionResult Get(string name)
        {
            return Ok(UnitOfWork.Shippers.Get().Where(x => x.Name.Contains(name)).ToList().Select(a => Factory.Create(a)).ToList());
        }

        [Route("{id:int}")]
        //[TokenAuthorization("user,admin")]
        public IHttpActionResult Get(int id)
        {
            try
            {
                Shipper shipper = UnitOfWork.Shippers.Get(id);
                if (shipper == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(Factory.Create(shipper));
                }
            }
            catch (Exception ex)
            {
                Helper.Log(ex.Message, "ERROR");
                return BadRequest(ex.Message);
            }
        }

        //Insert shippers
        [Route("")]
        //[TokenAuthorization("admin")]
        public IHttpActionResult Post(ShipperModel model)
        {
            try
            {
                Shipper shipper = Factory.Create(model); // stvaranje novog shippera
                UnitOfWork.Shippers.Insert(shipper);
                UnitOfWork.Commit();
                return Ok(Factory.Create(shipper));
            }
            catch (Exception ex)
            {
                Helper.Log(ex.Message, "ERROR");
                return BadRequest(ex.Message);
            }
        }

        //update shipprs
        [Route("{id}")]
        //[TokenAuthorization("admin")]
        public IHttpActionResult Put(int id, ShipperModel model)
        {
            try
            {
                Shipper shipper = Factory.Create(model);
                UnitOfWork.Shippers.Update(shipper, id);
                UnitOfWork.Commit();
                return Ok(Factory.Create(shipper));
            }
            catch (Exception ex)
            {
                Helper.Log(ex.Message, "ERROR");
                return BadRequest(ex.Message);
            }
        }

        //delete shipper
        [Route("{id}")]
        //[TokenAuthorization("admin")]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                Shipper entity = UnitOfWork.Shippers.Get(id);
                if (entity == null) return NotFound();
                UnitOfWork.Shippers.Delete(id);
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
