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
using Billing.Database;

namespace Billing.Tests
{
    [TestClass]
    public class TestInvoicesController
    {
        InvoicesController controller = new InvoicesController();
        HttpConfiguration config = new HttpConfiguration();
        HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, "api/invoices");

        void GetReady(string currentRoute = "api/{controller}/{id}")
        {
            var route = config.Routes.MapHttpRoute("default", currentRoute);
            var routeData = new HttpRouteData(route, new HttpRouteValueDictionary { { "controller", "invoices" } });

            controller.ControllerContext = new HttpControllerContext(config, routeData, request);
            controller.Request = request;
            controller.Request.Properties[HttpPropertyKeys.HttpConfigurationKey] = config;
        }

        //[TestMethod]
        //public void GetAllInvoices()
        //{
        //    TestHelper.InitDatabase();
        //    GetReady();
        //    var response = controller.Get().ExecuteAsync(CancellationToken.None).Result;
        //    Assert.IsNotNull(response.Content);
        //}

        [TestMethod]
        public void GetInvoiceById()
        {
            GetReady();
            var response = controller.Get(1).ExecuteAsync(CancellationToken.None).Result;
            Assert.IsNotNull(response.Content);
        }

        [TestMethod]
        public void GetInvoiceByWrongId()
        {
            GetReady();
            var response = controller.Get(99).ExecuteAsync(CancellationToken.None).Result;
            Assert.IsNull(response.Content);
        }

        [TestMethod]
        public void GetInvoicesByCustomer()
        {
            GetReady("api/{controller}/customer/{id}");
            var response = controller.GetByCustomer(1).ExecuteAsync(CancellationToken.None).Result;
            List<InvoiceModel> dataSet;
            var content = response.TryGetContentValue<List<InvoiceModel>>(out dataSet);
            Assert.AreNotEqual(0, dataSet.Count);
        }

        [TestMethod]
        public void GetInvoicesByWrongCustomer()
        {
            GetReady("api/{controller}/customer/{id}");
            var response = controller.GetByCustomer(99).ExecuteAsync(CancellationToken.None).Result;
            List<InvoiceModel> dataSet;
            var content = response.TryGetContentValue<List<InvoiceModel>>(out dataSet);
            Assert.AreEqual(0, dataSet.Count);
        }

        [TestMethod]
        public void GetInvoicesByAgent()
        {
            GetReady("api/{controller}/agent/{id}");
            var response = controller.GetByAgent(1).ExecuteAsync(CancellationToken.None).Result;
            List<InvoiceModel> dataSet;
            var content = response.TryGetContentValue<List<InvoiceModel>>(out dataSet);
            Assert.AreNotEqual(0, dataSet.Count);
        }

        [TestMethod]
        public void GetInvoicesByWrongAgent()
        {
            GetReady("api/{controller}/agent/{id}");
            var response = controller.GetByAgent(99).ExecuteAsync(CancellationToken.None).Result;
            List<InvoiceModel> dataSet;
            var content = response.TryGetContentValue<List<InvoiceModel>>(out dataSet);
            Assert.AreEqual(0, dataSet.Count);
        }

        [TestMethod]
        public void PostInvoiceGood()
        {
            GetReady();
            var actRes = controller.Post(new InvoiceModel()
            {
                AgentId = 1,
                CustomerId = 1,
                ShipperId = 1,
                Date = new DateTime(2017, 1, 20),
                InvoiceNo = "556-77",
                StatusId = 2,
                ShippedOn = new DateTime(2017, 1, 22),
                Shipping = 49,
                Vat = 17
            });
            var response = actRes.ExecuteAsync(CancellationToken.None).Result;
            Assert.IsTrue(response.IsSuccessStatusCode);
        }

        [TestMethod]
        public void PostInvoiceBad()
        {
            GetReady();
            var actRes = controller.Post(new InvoiceModel()
            {
                AgentId = 99,
                CustomerId = 99,
                ShipperId = 99,
                Date = new DateTime(2017, 1, 20),
                InvoiceNo = "556-77",
                StatusId = 2,
                ShippedOn = new DateTime(2017, 1, 22),
                Shipping = 49,
                Vat = 17
            });
            var response = actRes.ExecuteAsync(CancellationToken.None).Result;
            Assert.IsFalse(response.IsSuccessStatusCode);
        }

        [TestMethod]
        public void ChangeInvoiceDataGood()
        {
            GetReady();
            var actRes = controller.Put(1, new InvoiceModel()
            {
                Id = 1,
                AgentId = 2,
                CustomerId = 2,
                ShipperId = 2,
                Date = new DateTime(2017, 1, 20),
                InvoiceNo = "77-588",
                StatusId = 5,
                ShippedOn = new DateTime(2017, 1, 22),
                Shipping = 94,
                Vat = 17
            });
            var response = actRes.ExecuteAsync(CancellationToken.None).Result;
            Assert.IsTrue(response.IsSuccessStatusCode);
        }

        [TestMethod]
        public void ChangeInvoiceDataBad()
        {
            GetReady();
            var actRes = controller.Put(1, new InvoiceModel()
            {
                Id = 1,
                AgentId = 99,
                CustomerId = 99,
                ShipperId = 99,
                Date = new DateTime(2017, 1, 20),
                InvoiceNo = "77-588",
                StatusId = 6,
                ShippedOn = new DateTime(2017, 1, 22),
                Shipping = 94,
                Vat = 17
            });
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