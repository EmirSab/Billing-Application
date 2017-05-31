using Billing.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Billing.Api.Models.ReportModels
{
    public class StockLevelModel
    {
        public class Inventory
        {
            public string Product { get; set; }
            public int Purchased { get; set; }
            public int Ordered { get; set; }
            public int Sold { get; set; }
            public int OnStock { get; set; }
        }

        public StockLevelModel()
        {
            Products = new List<Inventory>();
        }

        public string Category { get; set; }
        public List<Inventory> Products { get; set; }
    }
}