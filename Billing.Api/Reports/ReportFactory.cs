using Billing.Api.Models;
using Billing.Api.Models.ReportModels;
using Billing.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static Billing.Api.Models.ReportModels.SalesByProductModel;

namespace Billing.Api.Reports
{
    public class ReportFactory
    {
        public List<DashboardModel.Customer> Create(List<InputItem> list)
        {
            List<DashboardModel.Customer> result = new List<DashboardModel.Customer>();
            DashboardModel.Customer current = new DashboardModel.Customer { Name = "", Credit = 0, Debit = 0 };
            foreach (var item in list)
            {
                if (item.Label != current.Name)
                {
                    if (current.Name != "") result.Add(current);
                    current = new DashboardModel.Customer { Name = item.Label, Credit = 0, Debit = 0 };
                }
                current.Debit += Math.Round(item.Value, 2);
                if (item.Index == 1) current.Credit += Math.Round(item.Value, 2);
            }
            if (current.Name != "") result.Add(current);
            return result.OrderByDescending(x => x.Debit).Take(10).ToList();
        }

        public List<DashboardModel.Burning> Create(List<BurningItem> burning)
        {
            List<DashboardModel.Burning> result = new List<DashboardModel.Burning>();

            DashboardModel.Burning current = new DashboardModel.Burning { Name = "", Ordered = 0, Stock = 0, Sold = 0 };
            foreach (var item in burning)
            {
                if (item.Product != current.Name)
                {
                    if (current.Name != "") if (current.Ordered > current.Stock || current.Stock < 0) result.Add(current);
                    current = new DashboardModel.Burning { Name = item.Product, Ordered = 0, Stock = item.Stock, Sold = 0 };
                }
                if (item.Status < Status.InvoicePaid) current.Ordered += item.Quantity; else current.Sold += item.Quantity;
            }
            if (current.Name != "") if (current.Ordered > current.Stock || current.Stock < 0) result.Add(current);

            return result.OrderByDescending(x => x.Difference).ToList();
        }

        public StockLevelModel.Inventory Create(Product product)
        {
            StockLevelModel.Inventory result = new StockLevelModel.Inventory()
            {
                Product = product.Name,
                OnStock = (int)product.Stock.Inventory,
                Ordered = product.Items.Where(x => x.Invoice.Status > Status.Canceled && x.Invoice.Status < Status.InvoiceShipped).Sum(x => x.Quantity),
                Purchased = product.Procurements.Sum(x => x.Quantity),
                Sold = product.Items.Where(x => x.Invoice.Status == Status.InvoiceShipped).Sum(x => x.Quantity),
            };
            return result;
        }
        public CategoryPurchaseModel Create(string Name, double SubTotal)
        {
            CategoryPurchaseModel category = new CategoryPurchaseModel()
            {
                CategoryName = Name,
                CategoryTotal = SubTotal
            };
            return category;
        }

        public CustomerPurchaseModel Create(string Name, double SubTotal, List<Item> Items, int number, List<CategoryPurchaseModel> Catquery, RequestModel Request)

        {
            CustomerPurchaseModel customer = new CustomerPurchaseModel(number)
            {
                CustomerName = Name,
                CustomerTurnover = SubTotal
            };
            int i = 0;
            foreach (var cat in Catquery)
            {
                var query = Items.Where(x => x.Invoice.Customer.Name.Equals(customer.CustomerName) && x.Product.Category.Name.Equals(cat.CategoryName) && x.Invoice.Date >= Request.StartDate && x.Invoice.Date <= Request.EndDate).ToList();
                customer.CategorySales[i] = query.Sum(x => x.SubTotal);
                i++;
            }

            return customer;
        }

        public SalesByProductModel SalesByProductCreate(DateTime startDate, DateTime endDate, string categoryName, double categoryTotal, double percentTotal)
        {

            return new SalesByProductModel
            {
                StartDate = startDate,
                EndDate = endDate,
                CategoryName = categoryName,
                CategoryTotal = categoryTotal,
                PercentTotal = percentTotal
            };
        }
        public ProductSalesModel Create(List<Database.Invoice> sample, string productName, double productTotal, double categoryTotal, double invoiceTotal)
        {
            return new ProductSalesModel
            {
                ProductName = productName,
                ProductTotal = productTotal,
                ProductPercent = Math.Round(100 * productTotal / categoryTotal, 2),
                TotalPercent = Math.Round(100 * productTotal / invoiceTotal, 2)

            };
        }
        public RegionSalesAgentModel Create(List<Invoice> InvoicesOfAgent, string Region, double Sales, double AgentTotal, List<Invoice> Invoices)
        {
            var query = Invoices.GroupBy(x => x.Customer.Town.Region.ToString())
                               .Select(x => new { RegionName = x.Key, RegionTotal = x.Sum(y => y.SubTotal) })
                               .ToList();
            double total = 0;
            foreach (var item in query)
                if (item.RegionName.Equals(Region))
                    total = item.RegionTotal;

            RegionSalesAgentModel region = new RegionSalesAgentModel()
            {
                RegionName = Region,
                RegionTotal = Math.Round(Sales, 2),
                RegionPercent = Math.Round(100 * Sales / total, 2),
                TotalPercent = Math.Round(100 * Sales / AgentTotal, 2)
            };
            return region;
        }
        public InvoiceInfoModel Create(int Id, string InvoiceNo, DateTime Date, DateTime? ShippedOn, double Total, Status Status)
        {

            InvoiceInfoModel invoice = new InvoiceInfoModel();
            invoice.InvoiceId = Id;
            invoice.InvoiceNo = InvoiceNo;
            invoice.InvoiceDate = Date;
            invoice.ShippedOn = (ShippedOn != null) ? ShippedOn.Value : DateTime.Now;
            invoice.InvoiceTotal = Math.Round(Total, 2);
            invoice.InvoiceStatus = Status.ToString();

            return invoice;
        }
        public InvoiceReviewProducts Create(int Id, string Name, double Price, int Quantity, double SubTotal, string Unit)
        {

            InvoiceReviewProducts products = new InvoiceReviewProducts()
            {
                ProductId = Id,
                ProductName = Name,
                Unit = Unit,
                Price = Price,
                Quantity = Quantity,
                Subtotal = SubTotal
            };
            return products;
        }

        public InvoiceProductReport Create(int Id, string Name, string Unit, double Price, int Quantity, double SubTotal)
        {

            InvoiceProductReport products = new InvoiceProductReport()
            {
                ProductId = Id,
                ProductName = Name,
                ProductUnit = Unit,
                Price = Price,
                Quantity = Quantity,
                Subtotal = SubTotal
            };
            return products;
        }
    }
}