using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Billing.Api.Models.EntityModels
{
    public class StockModel
    {
        public class StockProduct
        {
            public int Id;
            public string Name;
        }
        public int Id { get; set; }
        public int Input { get; set; }
        public int Output { get; set; }
        public int Inventory { get { return Input - Output; } }
        public StockProduct Product { get; set; }
    }
}