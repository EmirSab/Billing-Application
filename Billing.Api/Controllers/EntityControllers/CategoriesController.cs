using Billing.Api.Helpers;
using Billing.Api.Models;
using Billing.Database;
using Billing.Repository;
using System;
using System.Linq;
using System.Web.Http;

namespace Billing.Api.Controllers
{
    [RoutePrefix("api/categories")]
    public class CategoriesController : BaseController
    {
        [Route("")]
        [TokenAuthorization("user,admin")]
        public IHttpActionResult Get()
        {
            return Ok(UnitOfWork.Categories.Get().ToList().Select(x => Factory.Create(x)).ToList());
        }

        [Route("{id:int}")]
        [TokenAuthorization("user,admin")]
        public IHttpActionResult Get(int id)
        {
            try
            {
                Category category = UnitOfWork.Categories.Get(id);
                if (category == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(Factory.Create(category));
                }
            }
            catch (Exception ex)
            {
                Helper.Log(ex.Message, "ERROR");
                return BadRequest(ex.Message);
            }
        }

        [Route("{name}")]
        public IHttpActionResult Get(string name)
        {
            return Ok(UnitOfWork.Categories.Get().Where(x => x.Name.Contains(name)).ToList().Select(a => Factory.Create(a)).ToList());
        }

        //Insert
        [Route("")]
        [TokenAuthorization("admin")]
        public IHttpActionResult Post(CategoryModel model)
        {
            try
            {
                Category category = Factory.Create(model);
                UnitOfWork.Categories.Insert(category);
                UnitOfWork.Commit();
                return Ok(Factory.Create(category));
            }
            catch(Exception ex)
            {
                Helper.Log(ex.Message, "ERROR");
                return BadRequest(ex.Message);
            }
        }

        //Update
        [Route("{id}")]
        [TokenAuthorization("admin")]
        public IHttpActionResult Put(int id, CategoryModel model)
        {
            try
            {
                Category category = Factory.Create(model);
                UnitOfWork.Categories.Update(category, id);
                UnitOfWork.Commit();
                return Ok(Factory.Create(category));
            }
            catch (Exception ex)
            {
                Helper.Log(ex.Message, "ERROR");
                return BadRequest(ex.Message);
            }
        }

        //Delete
        [Route("{id}")]
        [TokenAuthorization("admin")]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                var items = UnitOfWork.Items.Get().Where(x => x.Product.Category.Id == id).ToList();
                var procurements = UnitOfWork.Procurements.Get().Where(x => x.Product.Category.Id == id).ToList();
                var products = UnitOfWork.Products.Get().Where(x => x.Category.Id == id).ToList();
                foreach (var item in items)
                    UnitOfWork.Items.Delete(item.Id);
                foreach (var procurement in procurements)
                    UnitOfWork.Procurements.Delete(procurement.Id);
                foreach (var product in products)
                    UnitOfWork.Products.Delete(product.Id);
                UnitOfWork.Stocks.Delete(id);
                UnitOfWork.Categories.Delete(id);
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
