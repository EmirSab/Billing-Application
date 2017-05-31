using Billing.Api.Helpers;
using Billing.Api.Models;
using Billing.Database;
using Billing.Repository;
using System;
using System.Linq;

namespace Billing.Api.Reports
{
    public class DashboardReport : BaseReport
    {
        public DashboardReport(UnitOfWork unitOfWork) : base(unitOfWork) { }

        public DashboardModel Report(int month = 0)
        {
            if (month == 0) month = DateTime.Today.Month;
            int year = DateTime.Today.Year;
            DashboardModel result = new DashboardModel();


            result.Title = string.Format($"Sales Review for {month}/{year}");
            result.Agent = BillingIdentity.CurrentUser.Name;
            result.AgentsCount = UnitOfWork.Agents.Get().Count();
            result.CategoriesCount = UnitOfWork.Categories.Get().Count();
            result.CustomersCount = UnitOfWork.Customers.Get().Count();
            result.ProductsCount = UnitOfWork.Products.Get().Count();


            var invoices = UnitOfWork.Invoices.Get().Where(x => x.Status > Status.Canceled).ToList();

            var items = invoices.GroupBy(x => (Months)x.Date.Month)
                                .Select(x => new { month = x.Key, value = x.Sum(y => y.SubTotal) })
                                .ToList();
            foreach (var item in items) result.Sales[item.month] = item.value;

            var query = invoices.GroupBy(x => new { x.Customer.Name, x.Status })
                       .Select(x => new InputItem() { Label = x.Key.Name, Index = (x.Key.Status < Status.InvoicePaid) ? 0 : 1, Value = x.Sum(y => y.Total) })
                       .ToList();
            result.Customers = Factory.Create(query);

            var burning = UnitOfWork.Items.Get().Where(x => x.Invoice.Status > Status.Canceled)
                         .OrderBy(x => x.Product.Id).ToList()
                         .Select(x => new BurningItem { ProductId = x.Product.Id, Product = x.Product.Name, Stock = (int)x.Product.Stock.Inventory, Status = x.Invoice.Status, Quantity = x.Quantity })
                         .ToList();
            result.Hots = Factory.Create(burning);

            invoices = invoices.Where(x => x.Date.Month == month).ToList();
            result.Agents = invoices.GroupBy(x => x.Agent.Name)
                                    .Select(x => new DashboardModel.Monthly() { Label = x.Key, Sales = x.Sum(y => y.SubTotal) })
                                    .ToList();
            result.Regions = invoices.GroupBy(x => x.Customer.Town.Region.ToString())
                                     .Select(x => new DashboardModel.Monthly() { Label = x.Key, Sales = x.Sum(y => y.SubTotal) })
                                     .ToList();
            result.Categories = invoices.SelectMany(x => x.Items)
                                        .GroupBy(x => x.Product.Category.Name)
                                        .Select(x => new DashboardModel.Monthly() { Label = x.Key, Sales = x.Sum(y => y.SubTotal) })
                                        .ToList();

            result.Top5 = UnitOfWork.Items.Get().OrderBy(x => x.Product.Id).ToList()
                                  .GroupBy(x => x.Product.Name)
                                  .Select(x => new DashboardModel.Product { Name = x.Key, Quantity = x.Sum(y => y.Quantity), Revenue = x.Sum(y => y.SubTotal) })
                                  .OrderByDescending(x => x.Revenue).Take(5)
                                  .ToList();

            result.Invoices = UnitOfWork.Invoices.Get().OrderBy(x => x.Status).ToList()
                              .GroupBy(x => x.Status.ToString())
                              .Select(x => new DashboardModel.Invoice { Status = x.Key, Count = x.Count() })
                              .ToList();

            return result;
        }
    }
}
