//using System;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using Billing.Api.Controllers.ReportControllers;
//using System.Web.Http;
//using System.Net.Http;
//using Billing.Repository;
//using Billing.Api.Reports;
//using System.Web.Http.Routing;
//using System.Web.Http.Controllers;
//using System.Web.Http.Hosting;
//using System.Security.Principal;
//using System.Threading;
//using Billing.Api.Controllers;
//using Billing.Api.Models.ReportModels;
//using Billing.Api.Models;

//namespace Billing.Tests.ReportTests
//{
//    [TestClass]
//    public class CrossCustomersCategoryControllerTest
//    {
//        CustomersByCategoryController controller = new CustomersByCategoryController();
//        HttpConfiguration config = new HttpConfiguration();
//        HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, "api/crosscustomerscategory");
//        private UnitOfWork unit = new UnitOfWork();
//        SetOfReports set = new SetOfReports(new UnitOfWork());
//        void GetReady()
//        {
//            var route = config.Routes.MapHttpRoute("default", "api/{controller}/{id}");
//            var routeData = new HttpRouteData(route, new HttpRouteValueDictionary { { "controller", "crosscustomerscategory" } });
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
//        public void GetCrossCustomerCategory()
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
//        public void GetCrossCustomerCategoryForWrongDates()
//        {
//            GetReady();
//            var actRes = controller.Post(new RequestModel()
//            {
//                StartDate = new DateTime(2018, 1, 1),
//                EndDate = new DateTime(2015, 1, 1)
//            }
//            );
//            var response = actRes.ExecuteAsync(CancellationToken.None).Result;

//            Assert.IsFalse(response.IsSuccessStatusCode);
//        }

//        [TestMethod]
//        public void TestGrandTotalEqualsCustomerSum()
//        {
//            RequestModel requestModel = new RequestModel()
//            {
//                StartDate = new DateTime(2015, 1, 1),
//                EndDate = new DateTime(2018, 1, 1)
//            };

//            CustomersCategoryModel model = set.CrossCustomersCategoryReport.Report(requestModel);

//            double sum = 0;
//            foreach (var item in model.CustomersByCategory)
//            {
//                sum += item.CustomerTurnover;
//            }
//            Assert.AreEqual(model.GrandTotal, sum);
//        }

//        [TestMethod]
//        public void TestModelNoOfCategories()
//        {
//            RequestModel requestModel = new RequestModel()
//            {
//                StartDate = new DateTime(2015, 1, 1),
//                EndDate = new DateTime(2018, 1, 1)
//            };

//            CustomersCategoryModel model = set.CrossCustomersCategoryReport.Report(requestModel);

//            Assert.AreEqual(model.CategoryTotal.Count, 1);
//        }

//    }
//}
