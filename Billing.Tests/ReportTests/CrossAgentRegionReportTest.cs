using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Billing.Api.Controllers;
using System.Web.Http;
using System.Net.Http;
using System.Web.Http.Routing;
using System.Security.Principal;
using System.Web.Http.Hosting;
using System.Web.Http.Controllers;
using System.Threading;
using Billing.Api.Models;

namespace Billing.Tests.ReportTests
{
    [TestClass]
    public class CrossAgentRegionReportTest
    {
        AgentsByRegionController controller = new AgentsByRegionController();
        HttpConfiguration config = new HttpConfiguration();
        HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "api/crossagentregion");

        [TestInitialize]
        public void Initializing()
        {
            TestHelper.InitDatabase();
        }

        void GetReady()
        {
            var route = config.Routes.MapHttpRoute("default", "api/{controller}/{id}");
            var routeData = new HttpRouteData(route, new HttpRouteValueDictionary { { "controller", "crossagentregion" } });

            controller.ControllerContext = new HttpControllerContext(config, routeData, request);
            controller.Request = request;
            controller.Request.Properties[HttpPropertyKeys.HttpConfigurationKey] = config;
            //NOVI DIO ZA AUTH
            controller.Request.Headers.TryAddWithoutValidation("Content-Type", "application/json");
            controller.Request.Headers.TryAddWithoutValidation("ApiKey", "YWxwaGE=");
            string token = "YWxwaGE=" + DateTime.UtcNow.ToString("s");
            controller.Request.Headers.TryAddWithoutValidation("Token", token);
            controller.RequestContext.Principal = new GenericPrincipal(new GenericIdentity("Antonio", "TestPassword"), new[] { "admin", "user" });

        }

        [TestMethod]
        public void CrossAgentRegionGoodDate()
        {
            Initializing();
            GetReady();
            var actRes = controller.Post(new RequestModel()
            {
                Id = 1,
                StartDate = new DateTime(2016, 1, 1),
                EndDate = new DateTime(2017, 1, 1)
            }
            );
            var response = actRes.ExecuteAsync(CancellationToken.None).Result;

            Assert.IsTrue(response.IsSuccessStatusCode);
        }

        [TestMethod]
        public void CrossAgentRegionWrongDate()
        {
            Initializing();
            GetReady();
            var actRes = controller.Post(new RequestModel()
            {
                Id = 1,
                StartDate = new DateTime(2018, 1, 1),
                EndDate = new DateTime(2015, 1, 1)
            }
            );
            var response = actRes.ExecuteAsync(CancellationToken.None).Result;

            Assert.IsFalse(response.IsSuccessStatusCode);
        }

        [TestMethod]
        public void CrossAgentRegionContent()
        {//1
            Initializing();
            GetReady();
            var actRes = controller.Post(new RequestModel()
            {
                Id = 1,
                StartDate = new DateTime(2016, 1, 1),
                EndDate = new DateTime(2017, 1, 1)
            }
            );
            var response = actRes.ExecuteAsync(CancellationToken.None).Result;
            Assert.IsNotNull(response.Content);
        }

    }
}
