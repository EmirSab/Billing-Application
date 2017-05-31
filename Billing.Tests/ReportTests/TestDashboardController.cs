//using System;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using Billing.Api.Controllers;
//using System.Web.Http;
//using System.Net.Http;
//using Billing.Api.Reports;
//using Billing.Repository;
//using System.Web.Http.Routing;
//using System.Web.Http.Controllers;
//using System.Web.Http.Hosting;
//using System.Threading;
//using Billing.Api.Models;

//namespace Billing.Tests.ReportTests
//{
//    [TestClass]
//    public class TestDashboardController
//    {
//        DashboardController controller = new DashboardController();
//        HttpConfiguration config = new HttpConfiguration();
//        HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, "api/dashboard");
//        SetOfReports set = new SetOfReports(new UnitOfWork());
//        private UnitOfWork unit = new UnitOfWork();
//        void GetReady()
//        {
//            var route = config.Routes.MapHttpRoute("default", "api/{controller}/{id}");
//            var routeData = new HttpRouteData(route, new HttpRouteValueDictionary { { "controller", "dashboard" } });

//            controller.ControllerContext = new HttpControllerContext(config, routeData, request);
//            controller.Request = request;
//            controller.Request.Properties[HttpPropertyKeys.HttpConfigurationKey] = config;
//        }
//        [TestMethod]
//        public void GetDashboardInfo()
//        {
//            TestHelper.InitDatabase();
//            GetReady();
//            var actRes = controller.Get();
//            var response = actRes.ExecuteAsync(CancellationToken.None).Result;

//            Assert.IsTrue(response.IsSuccessStatusCode);
//        }

//        [TestMethod]
//        public void GetDashboardCustomers()
//        {
//            //expect 1,in all invoices in "Testera" customer is same (Id=1)
//            DashboardModel model = set.Dashboard.Report();

//            Assert.AreEqual(model.Customers.Count, 1);
//        }
//    }
//}
