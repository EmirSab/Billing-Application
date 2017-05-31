////using System;
////using Microsoft.VisualStudio.TestTools.UnitTesting;
////using Billing.Api.Reports;
////using Billing.Api.Controllers.ReportControllers;
////using System.Web.Http;
////using System.Net.Http;
////using Billing.Repository;
////using System.Web.Http.Routing;
////using System.Web.Http.Controllers;
////using System.Web.Http.Hosting;
////using Billing.Api.Controllers;
////using System.Threading;

////namespace Billing.Tests.ReportTests
////{
////    [TestClass]
////    public class TestSalesbyProductReport
////    {
////        SalesByProductReportController controller = new SalesByProductReportController();
////        HttpConfiguration config = new HttpConfiguration();

////        HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, "api/salesbyproducts");

////        private UnitOfWork unit = new UnitOfWork();
////        void GetReady()
////        {
////            var route = config.Routes.MapHttpRoute("default", "api/{controller}/{id}");
////            var routeData = new HttpRouteData(route, new HttpRouteValueDictionary { { "controller", "salesbyproducts" } });
////            controller.ControllerContext = new HttpControllerContext(config, routeData, request);
////            controller.Request = request;
////            controller.Request.Properties[HttpPropertyKeys.HttpConfigurationKey] = config;
////        }
////        [TestMethod]
////        public void GetSalesByProduct()
////        {
////            TestHelper.InitDatabase();
////            GetReady();
////            var actRes = controller.Post(new RequestModel()
////            {
////                Id = 2,
////                StartDate = new DateTime(2016, 1, 1),
////                EndDate = new DateTime(2017, 1, 1)
////            }
////            );
////            var response = actRes.ExecuteAsync(CancellationToken.None).Result;

////            Assert.IsTrue(response.IsSuccessStatusCode);
////        }
////    }
////}
