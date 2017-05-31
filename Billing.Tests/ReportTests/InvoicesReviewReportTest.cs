using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Billing.Api.Reports;
using Billing.Repository;
using Billing.Api.Models.ReportModels;

namespace Billing.Tests.ReportTests
{
    [TestClass]
    public class InvoicesReviewReportTest
    {
        //private InvoicesReviewReport report = new InvoicesReviewReport(new UnitOfWork());
        //private int customerId = 1;
        //private int customerIdGet = 6;
        //private InvoiceReviewCustomerModel result;
        //private InvoiceReviewGetModel resultGet;

        //[TestInitialize]
        //public void InitReport()
        //{
        //    DateTime start = new DateTime(2016, 1, 1);
        //    DateTime end = new DateTime(2017, 12, 31);
        //    result = report.Report(customerId, start, end);
        //    resultGet = report.Report(customerIdGet);
        //}

        //[TestMethod]
        //public void CountSumInvoicesInCustomer()
        //{
        //    double sum = 0;
        //    foreach (var item in result.Invoices)
        //    {
        //        sum += item.InvoiceTotal;
        //    }

        //    Assert.AreEqual(result.GrandTotal, Math.Round(sum, 2));

        //}
        //[TestMethod]
        //public void CountItemsFromCustomerInvoice()
        //{
        //    double sum = 0;
        //    foreach (var item in resultGet.Items)
        //    {
        //        sum += item.Quantity * item.Price;
        //    }

        //    Assert.AreEqual(resultGet.SubTotal, Math.Round(sum, 2));

        //}
    }
}
