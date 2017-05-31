using Billing.Api.Helpers;
using Billing.Api.Models;
using Billing.Database;
using Billing.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using static Billing.Api.Models.SalesByCategoryModel;

namespace Billing.Api.Reports
{
    public class SalesByCategoryReport : BaseReport
    {
        public SalesByCategoryReport(UnitOfWork unitOfWork) : base(unitOfWork) { }

        public SalesByCategoryModel Report(RequestModel Request)
        {
            SalesByCategoryModel result = new SalesByCategoryModel()
            {
                StartDate = Request.StartDate,
                EndDate = Request.EndDate,

            };
            //Get list of invoices in Time Period
            var Invoices = UnitOfWork.Invoices.Get().Where(x => x.Date >= Request.StartDate && x.Date <= Request.EndDate).ToList();
            //Returns list of Items for Invoices
            var Items = Invoices.SelectMany(x => x.Items).ToList();
            result.GrandTotal = Invoices.Sum(x => x.SubTotal);

            var query = Items.GroupBy(x => x.Product.Category)
                            .Select(x =>
                            new
                            {
                                CategoryName = x.Key.Name,
                                CategoryTotal = x.Sum(y => y.SubTotal)
                            }).ToList();


            foreach (var item in query)
            {
                CategorySalesModel calculatedCategory = new CategorySalesModel
                {
                    CategoryName = item.CategoryName,
                    CategoryTotal = item.CategoryTotal,
                    CategoryPercent = 100 * item.CategoryTotal / result.GrandTotal
                };
                result.Sales.Add(calculatedCategory);
            }
            return result;
        }

    }
}