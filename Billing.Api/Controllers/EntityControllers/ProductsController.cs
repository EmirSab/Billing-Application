using Billing.Api.Helpers;
using Billing.Api.Models;
using Billing.Database;
using Billing.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Billing.Api.Controllers
{
    
    [RoutePrefix("api/products")]
    public class ProductsController : BaseController
    {
        [Route("")]
        //TokenAuthorization("user,admin")]
        public IHttpActionResult GetAll(int page = 0)
        {
            int PageSize = 8;
            var query = UnitOfWork.Products.Get().OrderBy(x => x.Id).ToList();
            int TotalPages = (int)Math.Ceiling((double)query.Count() / PageSize);

            var returnObject = new
            {
                pageSize = PageSize,
                currentPage = page,
                totalPages = TotalPages,
                productsList = query.Skip(PageSize * page).Take(PageSize).Select(x => Factory.Create(x)).ToList()
            };
            return Ok(returnObject);
        }
        //[Route("")]
        //TokenAuthorization("user,admin")]
        //public IHttpActionResult Get()
        //{
        //    return Ok(UnitOfWork.Products.Get().ToList().Select(x => Factory.Create(x)).ToList());
        //}

        [Route("{name}")]
        //[TokenAuthorization("user,admin")]
        public IHttpActionResult Get(string name)
        {
            return Ok(UnitOfWork.Products.Get().Where(x => x.Name.Contains(name)).ToList().Select(a => Factory.Create(a)).ToList());

        }

        [Route("{id:int}")]
        //[TokenAuthorization("user,admin")]
        public IHttpActionResult Get(int id)
        {
            Product product = UnitOfWork.Products.Get(id);
            if (product == null) return NotFound();
            return Ok(Factory.Create(product));
        }

        //[TokenAuthorization("admin")]
        [Route("")]
        public IHttpActionResult Post(ProductModel model)
        {
            try
            {
                Product product = Factory.Create(model);
                UnitOfWork.Products.Insert(product);
                UnitOfWork.Commit();
                return Ok(Factory.Create(product));
            }
            catch (Exception ex)
            {
                Helper.Log(ex.Message, "ERROR");
                return BadRequest(ex.Message);
            }
        }

        //[TokenAuthorization("admin")]
        [Route("{id:int}")]
        public IHttpActionResult Put(int id, ProductModel model)
        {
            try
            {
                Product product = Factory.Create(model);
                UnitOfWork.Products.Update(product, id);
                UnitOfWork.Commit();
                return Ok(Factory.Create(product));
            }
            catch (Exception ex)
            {
                Helper.Log(ex.Message, "ERROR");
                return BadRequest(ex.Message);
            }
        }

        //[TokenAuthorization("admin")]
        [Route("{id:int}")]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                if (UnitOfWork.Products.Get(id) == null) return NotFound();
                var items = UnitOfWork.Items.Get().Where(x => x.Product.Id == id).ToList();
                var procurements = UnitOfWork.Procurements.Get().Where(x => x.Product.Id == id).ToList();
                foreach (var item in items)
                    UnitOfWork.Items.Delete(item.Id);
                foreach (var procurement in procurements)
                    UnitOfWork.Procurements.Delete(procurement.Id);
                UnitOfWork.Stocks.Delete(id);
                UnitOfWork.Products.Delete(id);
                UnitOfWork.Commit();
                return Ok();
            }
            catch (Exception ex)
            {
                Helper.Log(ex.Message, "ERROR");
                return BadRequest(ex.Message);
            }
        }

        //[TokenAuthorization("admin")]
        [Route("stock")]
        [HttpGet]
        public IHttpActionResult Leverage()
        {
            List<Product> products = UnitOfWork.Products.Get().ToList();
            foreach (Product product in products)
            {
                product.Stock.Input = product.Procurements.Sum(x => x.Quantity);
                product.Stock.Output = product.Items.Sum(x => x.Quantity);
                UnitOfWork.Products.Update(product, product.Id);
            }
            UnitOfWork.Commit();
            return Ok("Inventory leveraged for all products.");
        }
    }
}
