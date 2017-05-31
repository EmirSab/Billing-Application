//using System;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using Billing.Api.Controllers;
//using System.Web.Http;
//using System.Net.Http;
//using Billing.Repository;
//using Billing.Api.Models;
//using Billing.Database;
//using System.Web.Http.Routing;
//using System.Web.Http.Controllers;
//using System.Web.Http.Hosting;
//using System.Security.Principal;
//using System.Threading;

//namespace Billing.Tests.ReportTests
//{
//    [TestClass]
//    public class InvoiceGetReportTest
//    {
//        InvoicesController controller = new InvoicesController();
//        HttpConfiguration config = new HttpConfiguration();
//        HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, "api/invoices");
//        private UnitOfWork unit = new UnitOfWork();
//        private Factory factory = new Factory(new BillingContext());
//        void GetReady()
//        {
//            var route = config.Routes.MapHttpRoute("default", "api/{controller}/{id}");
//            var routeData = new HttpRouteData(route, new HttpRouteValueDictionary { { "controller", "invoices" } });

//            controller.ControllerContext = new HttpControllerContext(config, routeData, request);
//            controller.Request = request;
//            controller.Request.Properties[HttpPropertyKeys.HttpConfigurationKey] = config;
//            controller.Request.Headers.TryAddWithoutValidation("Content-Type", "application/json");
//            controller.Request.Headers.TryAddWithoutValidation("ApiKey", "YWxwaGE=");
//            string token = "YWxwaGE=" + DateTime.UtcNow.ToString("s");
//            controller.Request.Headers.TryAddWithoutValidation("Token", token);
//            controller.RequestContext.Principal = new GenericPrincipal(new GenericIdentity("Antonio", "TestPassword"), new[] { "admin", "user" });
//        }
//        [TestMethod]
//        public void GetAllInvoices()
//        {
//            TestHelper.InitDatabase();
//            GetReady();
//            var actRes = controller.Get();
//            var response = actRes.ExecuteAsync(CancellationToken.None).Result;

//            Assert.IsNotNull(response.Content);
//        }

//        [TestMethod]
//        public void GetInvoiceById()
//        {
//            GetReady();
//            var actRes = controller.Get(1);
//            var response = actRes.ExecuteAsync(CancellationToken.None).Result;

//            Assert.IsNotNull(response.Content);
//        }

//        [TestMethod]
//        public void GetInvoiceByWrongId()
//        {
//            GetReady();
//            var actRes = controller.Get(99);
//            var response = actRes.ExecuteAsync(CancellationToken.None).Result;

//            Assert.IsNull(response.Content);
//        }

//        [TestMethod]
//        public void GetInvoicesByCustomerId()
//        {
//            GetReady();
//            var actRes = controller.GetByCustomer(1);
//            var response = actRes.ExecuteAsync(CancellationToken.None).Result;

//            Assert.IsNotNull(response.Content);
//        }

//        [TestMethod]
//        public void GetInvoicesByWrongCustomerId()
//        {
//            GetReady();
//            var actRes = controller.GetByCustomer(99);
//            var response = actRes.ExecuteAsync(CancellationToken.None).Result;

//            Assert.IsNull(response.Content);
//        }

//        [TestMethod]
//        public void GetInvoicesByAgentId()
//        {
//            GetReady();
//            var actRes = controller.GetByAgent(1);
//            var response = actRes.ExecuteAsync(CancellationToken.None).Result;

//            Assert.IsNotNull(response.Content);
//        }

//        [TestMethod]
//        public void GetInvoicesByWrongAgentId()
//        {
//            GetReady();
//            var actRes = controller.GetByAgent(99);
//            var response = actRes.ExecuteAsync(CancellationToken.None).Result;

//            Assert.IsNull(response.Content);
//        }

//        [TestMethod]
//        public void PostInvoiceWithAgentId()
//        {
//            GetReady();
//            var actRes = controller.Post(new InvoiceModel()
//            {
//                InvoiceNo = "12345",
//                Date = new DateTime(2017, 1, 10),
//                ShippedOn = new DateTime(2017, 1, 18),
//                Vat = 17,
//                Shipping = 100,
//                AgentId = 1,
//                CustomerId = 1,
//                ShipperId = 1,
//                Status = "OrderCreated",

//            }
//            );
//            var response = actRes.ExecuteAsync(CancellationToken.None).Result;

//            Assert.IsTrue(response.IsSuccessStatusCode);
//        }

//        [TestMethod]
//        public void PostInvoiceWithWrongAgentId()
//        {
//            GetReady();
//            var actRes = controller.Post(new InvoiceModel()
//            {
//                InvoiceNo = "123456",
//                Date = new DateTime(2017, 1, 10),
//                ShippedOn = new DateTime(2017, 1, 18),
//                Vat = 17,
//                Shipping = 110,
//                AgentId = 99,
//                CustomerId = 1,
//                ShipperId = 1,
//                Status = "OrderCreated"
//            }
//            );
//            var response = actRes.ExecuteAsync(CancellationToken.None).Result;

//            Assert.IsFalse(response.IsSuccessStatusCode);
//        }

//        [TestMethod]
//        public void PostInvoiceWithCustomerId()
//        {
//            GetReady();
//            var actRes = controller.Post(new InvoiceModel()
//            {
//                InvoiceNo = "1234567",
//                Date = new DateTime(2017, 1, 10),
//                ShippedOn = new DateTime(2017, 1, 18),
//                Vat = 17,
//                Shipping = 120,
//                AgentId = 1,
//                CustomerId = 1,
//                ShipperId = 1,
//                Status = "OrderCreated"
//            }
//            );
//            var response = actRes.ExecuteAsync(CancellationToken.None).Result;

//            Assert.IsTrue(response.IsSuccessStatusCode);
//        }

//        [TestMethod]
//        public void PostInvoiceWithWrongCustomerId()
//        {
//            GetReady();
//            var actRes = controller.Post(new InvoiceModel()
//            {
//                InvoiceNo = "12345678",
//                Date = new DateTime(2017, 1, 10),
//                ShippedOn = new DateTime(2017, 1, 18),
//                Vat = 17,
//                Shipping = 140,
//                AgentId = 1,
//                CustomerId = 99,
//                ShipperId = 1,
//                Status = "OrderCreated"
//            }
//            );
//            var response = actRes.ExecuteAsync(CancellationToken.None).Result;

//            Assert.IsFalse(response.IsSuccessStatusCode);
//        }

//        [TestMethod]
//        public void ChangeInvoiceData()
//        {
//            GetReady();
//            var actRes = controller.Put(1, new InvoiceModel()
//            {
//                Id = 1,
//                InvoiceNo = "100/18",
//                Date = new DateTime(2017, 1, 10),
//                ShippedOn = new DateTime(2017, 1, 18),
//                Vat = 17,
//                Shipping = 95,
//                AgentId = 1,
//                CustomerId = 1,
//                ShipperId = 1,
//                Status = "OrderCreated"
//            });
//            var response = actRes.ExecuteAsync(CancellationToken.None).Result;

//            Assert.IsTrue(response.IsSuccessStatusCode);
//        }

//        [TestMethod]
//        public void ChangeInvoiceAgent()
//        {
//            GetReady();
//            var actRes = controller.Put(1, new InvoiceModel()
//            {
//                Id = 1,
//                InvoiceNo = "100/17",
//                Date = new DateTime(2017, 1, 10),
//                ShippedOn = new DateTime(2017, 1, 18),
//                Vat = 17,
//                Shipping = 95,
//                AgentId = 2,
//                CustomerId = 1,
//                ShipperId = 1,
//                Status = "OrderCreated"
//            });
//            var response = actRes.ExecuteAsync(CancellationToken.None).Result;

//            Assert.IsTrue(response.IsSuccessStatusCode);
//        }

//        [TestMethod]
//        public void ChangeInvoiceAgentWrong()
//        {
//            GetReady();
//            var actRes = controller.Put(1, new InvoiceModel()
//            {
//                InvoiceNo = "100/17",
//                Date = new DateTime(2017, 1, 10),
//                ShippedOn = new DateTime(2017, 1, 18),
//                Vat = 17,
//                Shipping = 95,
//                AgentId = 99,
//                CustomerId = 1,
//                ShipperId = 1,
//                Status = "OrderCreated"
//            });
//            var response = actRes.ExecuteAsync(CancellationToken.None).Result;

//            Assert.IsFalse(response.IsSuccessStatusCode);
//        }

//        [TestMethod]
//        public void ChangeInvoiceCustomer()
//        {
//            GetReady();
//            var actRes = controller.Put(1, new InvoiceModel()
//            {
//                Id = 1,
//                InvoiceNo = "100/17",
//                Date = new DateTime(2017, 1, 10),
//                ShippedOn = new DateTime(2017, 1, 18),
//                Vat = 17,
//                Shipping = 95,
//                AgentId = 1,
//                CustomerId = 2,
//                ShipperId = 1,
//                Status = "OrderCreated"
//            });
//            var response = actRes.ExecuteAsync(CancellationToken.None).Result;

//            Assert.IsTrue(response.IsSuccessStatusCode);
//        }

//        [TestMethod]
//        public void ChangeInvoiceCustomerWrong()
//        {
//            GetReady();
//            var actRes = controller.Put(1, new InvoiceModel()
//            {
//                Id = 1,
//                InvoiceNo = "100/17",
//                Date = new DateTime(2017, 1, 10),
//                ShippedOn = new DateTime(2017, 1, 18),
//                Vat = 17,
//                Shipping = 95,
//                AgentId = 1,
//                CustomerId = 99,
//                ShipperId = 1,
//                Status = "OrderCreated"
//            });
//            var response = actRes.ExecuteAsync(CancellationToken.None).Result;

//            Assert.IsFalse(response.IsSuccessStatusCode);
//        }

//        [TestMethod]
//        public void DeleteInvoiceById()
//        {
//            GetReady();
//            var actRes = controller.Delete(3);
//            var response = actRes.ExecuteAsync(CancellationToken.None).Result;

//            Assert.IsTrue(response.IsSuccessStatusCode);
//        }

//        [TestMethod]
//        public void DeleteInvoiceByWrongId()
//        {
//            GetReady();
//            var actRes = controller.Delete(99);
//            var response = actRes.ExecuteAsync(CancellationToken.None).Result;

//            Assert.IsFalse(response.IsSuccessStatusCode);
//        }

//        //[TestMethod]
//        //public void GetInvoiceByIdValues()
//        //{

//        //    InvoiceModel model = factory.Create(unit.Invoices.Get(1));

//        //    Assert.AreEqual(model.Date, new DateTime(2017, 2, 18));
//        //    Assert.AreEqual(model.Id, 2);
//        //    Assert.AreEqual(model.InvoiceNo, "101/17");
//        //    Assert.AreEqual(model.Status, "OrderCreated");
//        //    Assert.AreEqual(model.Total, 878);
//        //    Assert.AreEqual(model.ShippedOn, new DateTime(2017, 2, 28));
//        //}
//    }
//}
