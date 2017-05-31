using Billing.Api.Helpers;
using Billing.Api.Models;
using Billing.Database;
using Billing.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Billing.Api.Reports
{
    public class CustomersByCategoryReport : BaseReport
    {
        public CustomersByCategoryReport(UnitOfWork unitOfWork) : base(unitOfWork) { }

        public CustomersCategoryModel Report(RequestModel request)
        {
            if (request.EndDate <= request.StartDate) throw new Exception("Incorrect Date");
            List<Item> Items = UnitOfWork.Items.Get().Where(x => x.Invoice.Date >= request.StartDate && x.Invoice.Date <= request.EndDate).ToList();
            CustomersCategoryModel result = new CustomersCategoryModel()
            {
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                GrandTotal = Items.Sum(x => x.SubTotal)
            };

            result.CatRevenue = Items.GroupBy(x => x.Product.Category.Name)
                                .Select(x => Factory.Create(x.Key, x.Sum(y => y.SubTotal)))
                                .ToList();
            var Catquery = result.CatRevenue;
            int number = result.CatRevenue.Count;
            result.CusRevenue = Items.GroupBy(x => x.Invoice.Customer.Name)
                                .Select(x => Factory.Create(x.Key, x.Sum(y => y.SubTotal), Items, number, Catquery, request))
                                .ToList();

            return result;
        }
    }
}