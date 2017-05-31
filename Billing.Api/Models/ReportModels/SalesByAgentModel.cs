using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Billing.Api.Models
{
    public class SalesByAgentModel
    {
        public SalesByAgentModel()
        {
            Sales = new List<RegionSalesAgentModel>();
        }
        public string AgentName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public double AgentTotal { get; set; }
        public double PercentTotal { get; set; }
        public List<RegionSalesAgentModel> Sales { get; set; }

    }

    public class RegionSalesAgentModel
    {
        public string RegionName { get; set; }
        public double RegionTotal { get; set; }
        public double RegionPercent { get; set; }
        public double TotalPercent { get; set; }
    }
}