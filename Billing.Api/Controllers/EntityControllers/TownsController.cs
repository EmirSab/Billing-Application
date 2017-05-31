using Billing.Api.Helpers;
using Billing.Api.Models;
using Billing.Database;
using System;
using System.Linq;
using System.Web.Http;

namespace Billing.Api.Controllers
{
    [RoutePrefix("api/towns")]
    public class TownsController : BaseController
    {
        [Route("")]
        public IHttpActionResult Get()
        {
            return Ok(UnitOfWork.Towns.Get().ToList().Select(x => Factory.Create(x)).ToList());
        }

        [Route("{name}")]
        public IHttpActionResult Get(string name)
        {
            return Ok(UnitOfWork.Towns.Get().Where(x => x.Name.Contains(name)).ToList().Select(a => Factory.Create(a)).ToList());
        }

        [Route("zip/{zip}")]
        public IHttpActionResult GetZip(string zip)
        {
            try
            {
                Town town = UnitOfWork.Towns.Get().FirstOrDefault(x => x.Zip == zip);
                if (town == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(Factory.Create(town));
                }
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
                Town town = UnitOfWork.Towns.Get(id);
                if (town == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(Factory.Create(town));
                }
            }
            catch (Exception ex)
            {
                Helper.Log(ex.Message, "ERROR");
                return BadRequest(ex.Message);
            }
        }

        [Route("")]
        public IHttpActionResult Post(TownModel model)
        {
            try
            {
                Town town = Factory.Create(model);
                UnitOfWork.Towns.Insert(town);
                UnitOfWork.Commit();
                return Ok(Factory.Create(town));
            }
            catch(Exception ex)
            {
                Helper.Log(ex.Message, "ERROR");
                return BadRequest(ex.Message);
            }
        }

        [Route("{id}")]
        public IHttpActionResult Put(int id, TownModel model)
        {
            try
            {
                Town town = Factory.Create(model);
                UnitOfWork.Towns.Update(town, id);
                UnitOfWork.Commit();
                return Ok(Factory.Create(town));
            }
            catch (Exception ex)
            {
                Helper.Log(ex.Message, "ERROR");
                return BadRequest(ex.Message);
            }
        }

        [Route("{id}")]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                UnitOfWork.Towns.Delete(id);
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
