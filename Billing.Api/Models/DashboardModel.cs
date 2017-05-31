using Billing.Database;
using System.Collections.Generic;

namespace Billing.Api.Models
{
    public class InputItem
    {
        public string Label { get; set; }
        public int Index { get; set; }
        public double Value { get; set; }
    }

    public class BurningItem
    {
        public int ProductId { get; set; }
        public string Product { get; set; }
        public int Stock { get; set; }
        public Status Status { get; set; }
        public int Quantity { get; set; }
    }

    public class DashboardModel
    {
        public DashboardModel()
        {
            Regions = new List<Monthly>();
            Categories = new List<Monthly>();
            Agents = new List<Monthly>();
            Sales = new Dictionary<Months, double>();
            Top5 = new List<Product>();
            Hots = new List<Burning>();
            Invoices = new List<Invoice>();
            Customers = new List<Customer>();
            foreach (Months mon in Months.GetValues(typeof(Months))) Sales[mon] = 0;
        }

        public class Monthly
        {
            public string Label { get; set; }
            public double Sales { get; set; }
        }

        public class Annual
        {
            public double Regions { get; set; }
            public double Agents { get; set; }
            public double Categories { get; set; }
        }

        public class Product
        {
            public string Name { get; set; }
            public int Quantity { get; set; }
            public double Revenue { get; set; }
        }

        public class Burning
        {
            public string Name { get; set; }
            public int Stock { get; set; }
            public int Ordered { get; set; }
            public int Sold { get; set; }
            public int Difference { get { return (Ordered - Stock); } }
        }

        public class Invoice
        {
            public string Status { get; set; }
            public int Count { get; set; }
        }

        public class Customer
        {
            public string Name { get; set; }
            public double Credit { get; set; }
            public double Debit { get; set; }
        }

        public string Title { get; set; }
        public string Agent { get; set; }
        public int CategoriesCount { get; set; }
        public int ProductsCount { get; set; }
        public int CustomersCount { get; set; }
        public int AgentsCount { get; set; }
        public List<Monthly> Regions { get; set; }
        public List<Monthly> Categories { get; set; }
        public List<Monthly> Agents { get; set; }
        public Dictionary<Months, double> Sales { get; set; }
        public List<Product> Top5 { get; set; }
        public List<Burning> Hots { get; set; }
        public List<Invoice> Invoices { get; set; }
        public List<Customer> Customers { get; set; }
    }
}