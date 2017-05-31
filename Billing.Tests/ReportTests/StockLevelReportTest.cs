//using System;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using Billing.Api.Controllers.ReportControllers;
//using System.Web.Http;
//using System.Net.Http;
//using Billing.Api.Reports;
//using Billing.Repository;
//using System.Web.Http.Routing;
//using System.Web.Http.Controllers;
//using System.Web.Http.Hosting;
//using Billing.Api.Models.ReportModels;

//namespace Billing.Tests.ReportTests
//{
//    //ps checkbycategory name i product ne radi
//    [TestClass]
//    public class StockLevelReportTest
//    {
//        private StockLevelReport report = new StockLevelReport(new UnitOfWork());


//        private readonly int categoryId = 2;
//        private readonly string categoryName = "DESKTOP";
//        private readonly int productsNo = 30;
//        private StockLevelModel result;

//        [TestInitialize]
//        public void InitReport()
//        {
//            result = report.Report(categoryId);
//        }

//        [TestMethod]
//        public void GetReportForCategory()
//        {
//            Assert.IsNotNull(result);
//        }

//        //[TestMethod]
//        //public void CheckCategoryId()
//        //{
//        //    Assert.AreEqual(categoryId, result.CategoryId);
//        //}

//        //[TestMethod]
//        //public void CheckCategoryName()
//        //{
//        //    Assert.AreEqual(categoryName.ToLowerInvariant(), result.CategoryName.ToLowerInvariant());
//        //}

//        //[TestMethod]
//        //public void CheckNumberOfProducts()
//        //{
//        //    Assert.AreEqual(productsNo, result.Products.Count);
//        //}

//        //[TestMethod]
//        //public void CheckAllProductsInventory()
//        //{
//        //    foreach (var item in result.Products)
//        //    {
//        //        Assert.AreEqual(item.Stock, item.Input - item.Output);
//        //    }
//        //}
//    }
//}
