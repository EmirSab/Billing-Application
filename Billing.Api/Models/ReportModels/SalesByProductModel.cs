using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Billing.Api.Models.ReportModels
{
    public class SalesByProductModel
    {
        public class ProductSalesModel
        {

            public string ProductName { get; set; }
            public double ProductTotal { get; set; }
            public double ProductPercent { get; set; }
            public double TotalPercent { get; set; }
        }
            public SalesByProductModel()
            {
                List<ProductSalesModel> ProductSales = new List<ProductSalesModel>();
            }
            public string CategoryName { get; set; }
            public DateTime StartDate { get; set; }
            public DateTime EndDate { get; set; }
            public double CategoryTotal { get; set; }
            public double PercentTotal { get; set; }

            public List<ProductSalesModel> ProductSales { get; set; }
        }
    }