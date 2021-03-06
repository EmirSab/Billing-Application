﻿//using System;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using Billing.Api.Controllers;
//using System.Web.Http;
//using System.Net.Http;
//using System.Web.Http.Routing;
//using System.Web.Http.Controllers;
//using System.Web.Http.Hosting;
//using System.Security.Principal;
//using System.Threading;

//namespace Billing.Tests.ReportTests
//{
//    [TestClass]
//    public class SalesByCustomerReportTest
//    {
//        SalesByCustomerController controller = new SalesByCustomerController();
//        HttpConfiguration config = new HttpConfiguration();
//        HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "api/salesbycustomer");

//        [TestInitialize]
//        public void Initializing()
//        {
//            TestHelper.InitDatabase();
//        }
//        void GetReady()
//        {
//            var route = config.Routes.MapHttpRoute("default", "api/{controller}/{id}");
//            var routeData = new HttpRouteData(route, new HttpRouteValueDictionary { { "controller", "salesbycustomer" } });
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
//        public void SalesByCustomerGoodDate()
//        {
//            Initializing();
//            GetReady();
//            var actRes = controller.Post(new RequestModel()
//            {
//                Id = 1,
//                StartDate = new DateTime(2016, 1, 1),
//                EndDate = new DateTime(2017, 1, 1)
//            }
//            );
//            var response = actRes.ExecuteAsync(CancellationToken.None).Result;
//            Assert.IsTrue(response.IsSuccessStatusCode);
//        }
//        [TestMethod]
//        public void SalesByCustomerWrongDate()
//        {
//            Initializing();
//            GetReady();
//            var actRes = controller.Post(new RequestModel()
//            {
//                Id = 1,
//                StartDate = new DateTime(2018, 1, 1),
//                EndDate = new DateTime(2015, 1, 1)
//            }
//            );
//            var response = actRes.ExecuteAsync(CancellationToken.None).Result;
//            Assert.IsFalse(response.IsSuccessStatusCode);
//        }
//        [TestMethod]
//        public void SalesByCustomerContent()
//        {
//            Initializing();
//            GetReady();
//            var actRes = controller.Post(new RequestModel()
//            {
//                Id = 1,
//                StartDate = new DateTime(2016, 1, 1),
//                EndDate = new DateTime(2017, 1, 1)
//            }
//            );
//            var response = actRes.ExecuteAsync(CancellationToken.None).Result;
//            Assert.IsNotNull(response.Content);
//        }
//    }
//}
