using Billing.Api.Models;
using Billing.Api.Models.ReportModels;
using Billing.Database;
using Billing.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Billing.Api.Reports
{
    public class SalesByProductReport : BaseReport
    {
        public SalesByProductReport(UnitOfWork unitOfWork) : base(unitOfWork) { }


        public SalesByProductModel Report(RequestModel Request)
        {
            List<Item> Items = UnitOfWork.Items.Get()
                .Where(x => x.Invoice.Date >= Request.StartDate
                && x.Invoice.Date <= Request.EndDate
                && x.Product.Category.Id == Request.Id
                ).ToList();

            List<Invoice> Invoices = UnitOfWork.Invoices.Get().Where(x => x.Date >= Request.StartDate && x.Date <= Request.EndDate).ToList();
            string categoryName = UnitOfWork.Categories.Get(Request.Id).Name;
            double categoryTotal = Items.Sum(x => Math.Round(x.Quantity * x.Price, 2));

            double invoiceTotal = Invoices.Sum(x => x.SubTotal);
            double percentTotal = Math.Round(100 * categoryTotal / invoiceTotal, 2);


            SalesByProductModel result = Factory.SalesByProductCreate(Request.StartDate, Request.EndDate, categoryName, categoryTotal, percentTotal);

            result.ProductSales = Items.Select(x => Factory.Create(Invoices, x.Product.Name, x.SubTotal, categoryTotal, invoiceTotal))
                                            .ToList();
            return result;
        }
    }
}