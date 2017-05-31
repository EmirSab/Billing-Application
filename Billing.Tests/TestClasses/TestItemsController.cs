using Microsoft.VisualStudio.TestTools.UnitTesting;
using Billing.Api.Controllers;
using System.Web.Http;
using System.Threading;
using System.Net.Http;
using System.Web.Http.Routing;
using System.Web.Http.Controllers;
using System.Web.Http.Hosting;
using Billing.Api.Models;
using System;
using System.Collections.Generic;

namespace Billing.Tests
{
    [TestClass]
    public class TestItemsController
    {
        ItemsController controller = new ItemsController();
        HttpConfiguration config = new HttpConfiguration();
        HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, "api/items");

        void GetReady(string currentRoute = "api/{controller}/{id}")
        {
            var route = config.Routes.MapHttpRoute("default", currentRoute);
            var routeData = new HttpRouteData(route, new HttpRouteValueDictionary { { "controller", "items" } });

            controller.ControllerContext = new HttpControllerContext(config, routeData, request);
            controller.Request = request;
            controller.Request.Properties[HttpPropertyKeys.HttpConfigurationKey] = config;
        }

        //[TestMethod]
        //public void GetAllItems()
        //{
        //    TestHelper.InitDatabase();
        //    GetReady();
        //    var response = controller.Get().ExecuteAsync(CancellationToken.None).Result;
        //    Assert.IsNotNull(response.Content);
        //}

        [TestMethod]
        public void GetItemById()
        {
            GetReady();
            var response = controller.Get(1).ExecuteAsync(CancellationToken.None).Result;
            Assert.IsNotNull(response.Content);
        }

        [TestMethod]
        public void GetItemByWrongId()
        {
            GetReady();
            var response = controller.Get(99).ExecuteAsync(CancellationToken.None).Result;
            Assert.IsNull(response.Content);
        }

        [TestMethod]
        public void GetItemsByProduct()
        {
            GetReady("api/{controller}/product/{id}");
            var response = controller.GetByProduct(1).ExecuteAsync(CancellationToken.None).Result;
            List<ItemModel> dataSet;
            var content = response.TryGetContentValue<List<ItemModel>>(out dataSet);
            Assert.AreNotEqual(0, dataSet.Count);
        }

        [TestMethod]
        public void GetItemsByWrongProduct()
        {
            GetReady("api/{controller}/product/{id}");
            var response = controller.GetByProduct(99).ExecuteAsync(CancellationToken.None).Result;
            List<ItemModel> dataSet;
            var content = response.TryGetContentValue<List<ItemModel>>(out dataSet);
            Assert.AreEqual(0, dataSet.Count);
        }

        [TestMethod]
        public void GetItemsByInvoice()
        {
            GetReady("api/{controller}/invoice/{id}");
            var response = controller.GetByInvoice(1).ExecuteAsync(CancellationToken.None).Result;
            List<ItemModel> dataSet;
            var content = response.TryGetContentValue<List<ItemModel>>(out dataSet);
            Assert.AreNotEqual(0, dataSet.Count);
        }

        [TestMethod]
        public void GetItemsByWrongInvoice()
        {
            GetReady("api/{controller}/invoice/{id}");
            var response = controller.GetByInvoice(99).ExecuteAsync(CancellationToken.None).Result;
            List<ItemModel> dataSet;
            var content = response.TryGetContentValue<List<ItemModel>>(out dataSet);
            Assert.AreEqual(0, dataSet.Count);
        }

        [TestMethod]
        public void PostItemGood()
        {
            GetReady();
            var actRes = controller.Post(new ItemModel() { InvoiceId=1, ProductId=1, Price=799, Quantity=1 });
            var response = actRes.ExecuteAsync(CancellationToken.None).Result;
            Assert.IsTrue(response.IsSuccessStatusCode);
        }

        [TestMethod]
        public void PostItemBad()
        {
            GetReady();
            var actRes = controller.Post(new ItemModel() { InvoiceId = 99, ProductId = 99, Price = 799, Quantity = 1 });
            var response = actRes.ExecuteAsync(CancellationToken.None).Result;
            Assert.IsFalse(response.IsSuccessStatusCode);
        }

        [TestMethod]
        public void ChangeItemDataGood()
        {
            GetReady();
            var actRes = controller.Put(1, new ItemModel() { Id = 1, InvoiceId = 2, ProductId = 2, Price = 799, Quantity = 1 });
            var response = actRes.ExecuteAsync(CancellationToken.None).Result;
            Assert.IsTrue(response.IsSuccessStatusCode);
        }

        [TestMethod]
        public void ChangeItemDataBad()
        {
            GetReady();
            var actRes = controller.Put(1, new ItemModel() { Id = 1, InvoiceId = 99, ProductId = 99, Price = 799, Quantity = 1 });
            var response = actRes.ExecuteAsync(CancellationToken.None).Result;
            Assert.IsFalse(response.IsSuccessStatusCode);
        }

        [TestMethod]
        public void DeleteWrongId()
        {
            GetReady();
            var response = controller.Delete(99).ExecuteAsync(CancellationToken.None).Result;
            Assert.IsFalse(response.IsSuccessStatusCode);
        }

        [TestMethod]
        public void DeleteGoodId()
        {
            GetReady();
            var response = controller.Delete(2).ExecuteAsync(CancellationToken.None).Result;
            Assert.IsTrue(response.IsSuccessStatusCode);
        }
    }
}