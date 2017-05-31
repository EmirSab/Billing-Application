//using System;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using Billing.Api.Reports;
//using Billing.Api.Models.ReportModels;
//using Billing.Repository;
//using Billing.Api.Models;

//namespace Billing.Tests.ReportTests
//{
//    [TestClass]
//    public class InvoiceReportTest
//    {
//        private InvoiceReport report = new InvoiceReport(new UnitOfWork());
//        private int InvoiceId = 5;
//        private InvoiceReportModel result;

//        [TestInitialize]
//        public void InitReport()
//        {
//            result = report.Report(InvoiceId);
//        }

//        [TestMethod]
//        public void CountSumInvoiceReport()
//        {
//            double sum = 0;
//            foreach (var item in result.Items)
//            {
//                sum += item.Subtotal;
//            }

//            Assert.AreEqual(result.InvoiceSubtotal, Math.Round(sum, 2));

//        }
//        [TestMethod]
//        public void InvoiceTotalInInvoiceReport()
//        {
//            Assert.AreEqual(result.InvoiceTotal, result.InvoiceSubtotal + result.VatAmount);
//        }
//    }
//}
