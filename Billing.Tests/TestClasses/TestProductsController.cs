using Microsoft.VisualStudio.TestTools.UnitTesting;
using Billing.Api.Controllers;
using System.Web.Http;
using System.Threading;
using System.Net.Http;
using System.Web.Http.Routing;
using System.Web.Http.Controllers;
using System.Web.Http.Hosting;
using Billing.Api.Models;

namespace Billing.Tests
{
    [TestClass]
    public class TestProductsController
    {
        ProductsController controller = new ProductsController();
        HttpConfiguration config = new HttpConfiguration();
        HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, "api/products");

        void GetReady(string currentRoute = "api/{controller}/{id}")
        {
            var route = config.Routes.MapHttpRoute("default", currentRoute);
            var routeData = new HttpRouteData(route, new HttpRouteValueDictionary { { "controller", "products" } });

            controller.ControllerContext = new HttpControllerContext(config, routeData, request);
            controller.Request = request;
            controller.Request.Properties[HttpPropertyKeys.HttpConfigurationKey] = config;
        }

        //[TestMethod]
        //public void GetAllProducts()
        //{
        //    TestHelper.InitDatabase(); GetReady();
        //    var actRes = controller.Get();
        //    var response = actRes.ExecuteAsync(CancellationToken.None).Result;

        //    Assert.IsNotNull(response.Content);
        //}

        [TestMethod]
        public void GetProductById()
        {
            GetReady();
            var actRes = controller.Get(1);
            var response = actRes.ExecuteAsync(CancellationToken.None).Result;

            Assert.IsNotNull(response.Content);
        }

        [TestMethod]
        public void GetProductByWrongId()
        {
            GetReady("api/{controller}/name/{name}");
            var actRes = controller.Get(99);
            var response = actRes.ExecuteAsync(CancellationToken.None).Result;

            Assert.IsNull(response.Content);
        }

        [TestMethod]
        public void PostProductGood()
        {
            GetReady();
            var actRes = controller.Post(new ProductModel() { Name = "New computer arrived", Unit = "pcs", Price = 800, CategoryId = 1 });
            var response = actRes.ExecuteAsync(CancellationToken.None).Result;

            Assert.IsTrue(response.IsSuccessStatusCode);
        }

        [TestMethod]
        public void PostProductBad()
        {
            GetReady();
            var actRes = controller.Post(new ProductModel() { Name = "Bad computer arrived", Unit = "pcs", Price = 800, CategoryId = 99 });
            var response = actRes.ExecuteAsync(CancellationToken.None).Result;

            Assert.IsFalse(response.IsSuccessStatusCode);
        }

        [TestMethod]
        public void ChangeProductName()
        {
            GetReady();
            var actRes = controller.Put(1, new ProductModel() { Id = 1, Name = "Computer ACER", Unit = "pcs", Price = 400, CategoryId = 1 });
            var response = actRes.ExecuteAsync(CancellationToken.None).Result;

            Assert.IsTrue(response.IsSuccessStatusCode);
        }

        [TestMethod]
        public void ChangeCategory()
        {
            GetReady();
            var actRes = controller.Put(1, new ProductModel() { Id = 1, Name = "Categery changed", Unit = "pcs", Price = 400, CategoryId = 2 });
            var response = actRes.ExecuteAsync(CancellationToken.None).Result;

            Assert.IsTrue(response.IsSuccessStatusCode);
        }

        [TestMethod]
        public void DeleteByWrongId()
        {
            GetReady();
            var actRes = controller.Delete(99);
            var response = actRes.ExecuteAsync(CancellationToken.None).Result;

            Assert.IsFalse(response.IsSuccessStatusCode);
        }

        [TestMethod]
        public void DeleteSingle()
        {
            GetReady();
            var actRes = controller.Delete(2);
            var response = actRes.ExecuteAsync(CancellationToken.None).Result;

            Assert.IsTrue(response.IsSuccessStatusCode);
        }

        [TestMethod]
        public void DeleteWidow()
        {
            GetReady();
            var actRes = controller.Delete(1);
            var response = actRes.ExecuteAsync(CancellationToken.None).Result;

            Assert.IsFalse(response.IsSuccessStatusCode);
        }
    }
}