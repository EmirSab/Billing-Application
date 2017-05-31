using System;
using System.Collections.Generic;

namespace Billing.Api.Models
{
    public class SalesByCustomerModel
    {
        public SalesByCustomerModel()
        {
            Sales = new List<CustomerModel>();
        }
        public class CustomerModel
        {
            public string CustomerName { get; set; }
            public double CustomerTurnover { get; set; }
            public double CustomerPercent { get; set; }
        }
        public string Title { get; set; }
        public string Agent { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public double GrandTotal { get; set; }
        public List<CustomerModel> Sales { get; set; }
    }
}