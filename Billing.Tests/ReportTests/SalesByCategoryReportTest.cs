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
//    public class SalesByCategoryReportTest
//    {
//        SalesByCategoryController controller = new SalesByCategoryController();
//        HttpConfiguration config = new HttpConfiguration();
//        HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, "api/salesbycategory");
//        SetOfReports set = new SetOfReports(new UnitOfWork());
//        private UnitOfWork unit = new UnitOfWork();
//        void GetReady()
//        {
//            var route = config.Routes.MapHttpRoute("default", "api/{controller}/{id}");
//            var routeData = new HttpRouteData(route, new HttpRouteValueDictionary { { "controller", "salesbycategory" } });
//            controller.ControllerContext = new HttpControllerContext(config, routeData, request);
//            controller.Request = request;
//            controller.Request.Properties[HttpPropertyKeys.HttpConfigurationKey] = config;
//        }
//        [TestMethod]
//        public void GetSalesByCategory()
//        {
//            TestHelper.InitDatabase();
//            GetReady();
//            var actRes = controller.Post(new RequestModel()
//            {
//                StartDate = new DateTime(2015, 1, 1),
//                EndDate = new DateTime(2018, 1, 1)
//            }
//            );
//            var response = actRes.ExecuteAsync(CancellationToken.None).Result;

//            Assert.IsTrue(response.IsSuccessStatusCode);
//        }

//        [TestMethod]
//        public void GetCountOfCategory()
//        {
//            RequestModel model = new RequestModel()
//            {
//                StartDate = new DateTime(2015, 1, 1),
//                EndDate = new DateTime(2018, 1, 1)
//            };
//            SalesByCategoryModel categoryModel = set.SalesByCategoryReport.Report(model);
//            Assert.IsNotNull(categoryModel.Sales.Count);
//        }

//        [TestMethod]
//        public void GetGrandTotal()
//        {
//            RequestModel model = new RequestModel()
//            {
//                StartDate = new DateTime(2015, 1, 1),
//                EndDate = new DateTime(2018, 1, 1)
//            };

//            SalesByCategoryModel categoryModel = set.SalesByCategoryReport.Report(model);
//            double total = 2099;
//            Assert.AreEqual(categoryModel.GrandTotal, total);
//        }

//    }
//}
