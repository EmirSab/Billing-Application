﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
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
    public class TestSuppliersController
    {
        SuppliersController controller = new SuppliersController();
        HttpConfiguration config = new HttpConfiguration();
        HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, "api/suppliers");

        void GetReady()
        {
            var route = config.Routes.MapHttpRoute("default", "api/{controller}/{id}");
            var routeData = new HttpRouteData(route, new HttpRouteValueDictionary { { "controller", "suppliers" } });

            controller.ControllerContext = new HttpControllerContext(config, routeData, request);
            controller.Request = request;
            controller.Request.Properties[HttpPropertyKeys.HttpConfigurationKey] = config;
        }

        //[TestMethod]
        //public void GetAllSuppliers()
        //{
        //    TestHelper.InitDatabase();
        //    GetReady();
        //    var actRes = controller.Get();
        //    var response = actRes.ExecuteAsync(CancellationToken.None).Result;

        //    Assert.IsNotNull(response.Content);
        //}

        [TestMethod]
        public void GetSupplierById()
        {
            GetReady();
            //var actRes = ;
            var response = controller.Get(1).ExecuteAsync(CancellationToken.None).Result;

            Assert.IsNotNull(response.Content);
        }

        [TestMethod]
        public void GetSupplierByWrongId()
        {
            GetReady();
            var actRes = controller.Get(99);
            var response = actRes.ExecuteAsync(CancellationToken.None).Result;

            Assert.IsNull(response.Content);
        }

        [TestMethod]
        public void PostSupplierGood()
        {
            GetReady();
            var actRes = controller.Post(new SupplierModel() { Name = "Network doo", Address = "Aleja lipa 33", TownId = 1 });
            var response = actRes.ExecuteAsync(CancellationToken.None).Result;

            Assert.IsTrue(response.IsSuccessStatusCode);
        }

        [TestMethod]
        public void ChangeSupplierName()
        {
            GetReady();
            var actRes = controller.Put(1, new SupplierModel() { Id = 1, Name = "Networks doo", Address = "Aleja lipa 33", TownId = 1 });
            var response = actRes.ExecuteAsync(CancellationToken.None).Result;

            Assert.IsTrue(response.IsSuccessStatusCode);
        }

        [TestMethod]
        public void DeleteWrongId()
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