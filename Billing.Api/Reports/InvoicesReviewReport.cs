using Billing.Api.Models;
using Billing.Api.Models.ReportModels;
using Billing.Database;
using Billing.Repository;
using System;
using System.Linq;

namespace Billing.Api.Reports
{
    public class InvoicesReviewReport : BaseReport
    {
        public InvoicesReviewReport(UnitOfWork unitOfWork) : base(unitOfWork) { }

        public InvoicesReviewModel Report(RequestModel Request)
        {
            if (Request.EndDate <= Request.StartDate) throw new Exception("Incorrect Date");
            var Invoices = UnitOfWork.Invoices.Get()
                                    .Where(x => x.Date >= Request.StartDate && x.Date <= Request.EndDate
                                                                            && x.Customer.Id == Request.Id).ToList();
            Customer Customer = UnitOfWork.Customers.Get(Request.Id);
            double GrandTotal = Math.Round(Invoices.Sum(x => x.Total), 2);
            InvoicesReviewModel result = new InvoicesReviewModel()
            {
                CustomerId = Request.Id,
                CustomerName = Customer.Name,
                StartDate = Request.StartDate,
                EndDate = Request.EndDate,
                GrandTotal = Math.Round(Invoices.Sum(x => x.SubTotal), 2)
            };
            result.InvoiceInfo = Invoices.Select(x => Factory.Create(x.Id, x.InvoiceNo, x.Date, x.ShippedOn, x.SubTotal, x.Status)).ToList();

            return result;
        }
    }
}
