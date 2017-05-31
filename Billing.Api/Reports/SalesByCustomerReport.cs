
using Billing.Api.Helpers;
using Billing.Api.Models;
using Billing.Database;
using Billing.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Billing.Api.Reports
{
    public class SalesByCustomerReport : BaseReport
    {
        public SalesByCustomerReport(UnitOfWork unitOfWork) : base(unitOfWork) { }

        public SalesByCustomerModel Report(RequestModel Request)
        {
            List<Invoice> Invoices = UnitOfWork.Invoices.Get().Where(x => x.Date >= Request.StartDate && x.Date <= Request.EndDate).ToList();
            SalesByCustomerModel result = new SalesByCustomerModel()
            {
                Title = "Sales by Customer",
                Agent = BillingIdentity.CurrentUser.Name,
                StartDate = Request.StartDate,
                EndDate = Request.EndDate,
                GrandTotal = Invoices.Sum(x => x.Total)
            };

            result.Sales = Invoices.OrderBy(x => x.Customer.Id).ToList()
                                   .GroupBy(x => x.Customer.Name)
                                   .Select(x => new SalesByCustomerModel.CustomerModel()
                                   {
                                       CustomerName = x.Key,
                                       CustomerTurnover = Math.Round(x.Sum(y => y.Total), 2),
                                       CustomerPercent = Math.Round(100 * x.Sum(y => y.Total) / result.GrandTotal, 2)
                                   }).OrderByDescending(x => x.CustomerTurnover)
                                   .ToList();
            return result;
        }
    }
}