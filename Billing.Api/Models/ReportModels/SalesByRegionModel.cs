using System;
using System.Collections.Generic;

namespace Billing.Api.Models
{
    public class SalesByRegionModel
    {
        public class AgentSales
        {
            public int AgentId { get; set; }
            public string AgentName { get; set; }
            public double AgentTotal { get; set; }
            public double RegionPercent { get; set; }
            public double TotalPercent { get; set; }
        }

        public class RegionSales
        {
            public RegionSales()
            {
                Agents = new List<AgentSales>();
            }
            public string RegionName { get; set; }
            public double RegionTotal { get; set; }
            public double RegionPercent { get; set; }
            public List<AgentSales> Agents { get; set; }
        }

        public SalesByRegionModel()
        {
            Sales = new List<RegionSales>();
        }
        public string Title { get; set; }
        public string Agent { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public double GrandTotal { get; set; }
        public List<RegionSales> Sales { get; set; }
    }
}