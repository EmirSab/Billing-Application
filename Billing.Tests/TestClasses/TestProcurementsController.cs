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
    public class TestProcurementsController
    {
        ProcurementsController controller = new ProcurementsController();
        HttpConfiguration config = new HttpConfiguration();
        HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, "api/procurements");

        void GetReady(string currentRoute = "api/{controller}/{id}")
        {
            var route = config.Routes.MapHttpRoute("default", currentRoute);
            var routeData = new HttpRouteData(route, new HttpRouteValueDictionary { { "controller", "procurements" } });

            controller.ControllerContext = new HttpControllerContext(config, routeData, request);
            controller.Request = request;
            controller.Request.Properties[HttpPropertyKeys.HttpConfigurationKey] = config;
        }

        //[TestMethod]
        //public void GetAllProcurements()
        //{
        //    TestHelper.InitDatabase();
        //    GetReady();
        //    var response = controller.Get().ExecuteAsync(CancellationToken.None).Result;
        //    Assert.IsNotNull(response.Content);
        //}

        [TestMethod]
        public void GetProcurementById()
        {
            GetReady();
            var response = controller.Get(1).ExecuteAsync(CancellationToken.None).Result;
            Assert.IsNotNull(response.Content);
        }

        [TestMethod]
        public void GetProcurementByWrongId()
        {
            GetReady();
            var response = controller.Get(99).ExecuteAsync(CancellationToken.None).Result;
            Assert.IsNull(response.Content);
        }
        
        [TestMethod]
        public void GetProcurementsByDocument()
        {
            GetReady("api/{controller}/doc/{id}");
            var response = controller.GetByDocument("55-17").ExecuteAsync(CancellationToken.None).Result;
            List<ProcurementModel> dataSet;
            var content = response.TryGetContentValue<List<ProcurementModel>>(out dataSet);
            Assert.AreNotEqual(0, dataSet.Count);
        }

        [TestMethod]
        public void GetProcurementsByWrongDoc()
        {
            GetReady("api/{controller}/doc/{id}");
            var response = controller.GetByDocument("999999").ExecuteAsync(CancellationToken.None).Result;
            List<ProcurementModel> dataSet;
            var content = response.TryGetContentValue<List<ProcurementModel>>(out dataSet);
            Assert.AreEqual(0, dataSet.Count);
        }

        [TestMethod]
        public void GetProcurementsByProduct()
        {
            GetReady("api/{controller}/product/{id}");
            var response = controller.GetByProduct(1).ExecuteAsync(CancellationToken.None).Result;
            List<ProcurementModel> dataSet;
            var content = response.TryGetContentValue<List<ProcurementModel>>(out dataSet);
            Assert.AreNotEqual(0, dataSet.Count);
        }

        [TestMethod]
        public void GetProcurementsByWrongProduct()
        {
            GetReady("api/{controller}/product/{id}");
            var response = controller.GetByProduct(99).ExecuteAsync(CancellationToken.None).Result;
            List<ProcurementModel> dataSet;
            var content = response.TryGetContentValue<List<ProcurementModel>>(out dataSet);
            Assert.AreEqual(0, dataSet.Count);
        }

        [TestMethod]
        public void PostProcurementGood()
        {
            GetReady();
            var actRes = controller.Post(new ProcurementModel() { Document="202/71", Date=new DateTime(2016,12,22), ProductId=1, SupplierId=1, Quantity=1, Price=299 });
            var response = actRes.ExecuteAsync(CancellationToken.None).Result;
            Assert.IsTrue(response.IsSuccessStatusCode);
        }

        [TestMethod]
        public void PostProcurementBad()
        {
            GetReady();
            var actRes = controller.Post(new ProcurementModel() { Document = "272/01", Date = new DateTime(2016, 12, 22), ProductId = 99, SupplierId = 1, Quantity = 1, Price = 299 });
            var response = actRes.ExecuteAsync(CancellationToken.None).Result;
            Assert.IsFalse(response.IsSuccessStatusCode);
        }

        [TestMethod]
        public void ChangeProcurementDataGood()
        {
            GetReady();
            var actRes = controller.Put(1, new ProcurementModel() { Id = 1, Document = "272/01", Date = new DateTime(2016, 12, 22), ProductId = 2, SupplierId = 2, Quantity = 2, Price = 399 });
            var response = actRes.ExecuteAsync(CancellationToken.None).Result;
            Assert.IsTrue(response.IsSuccessStatusCode);
        }

        [TestMethod]
        public void ChangeProcurementDataBad()
        {
            GetReady();
            var actRes = controller.Put(1, new ProcurementModel() { Id = 1, Document = "272/01", Date = new DateTime(2016, 12, 22), ProductId = 99, SupplierId = 99, Quantity = 2, Price = 399 });
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