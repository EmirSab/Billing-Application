using Billing.Database;
using System;
using System.Collections.Generic;

namespace Billing.Api.Models
{
    public class AgentsRegionModel
    {
        public class InputModel
        {
            public string Row { get; set; }
            public Region Column { get; set; }
            public double Value { get; set; }
        }

        public class AgentModel
        {
            public AgentModel()
            {
                Name = " ";
                Turnover = 0;
                Sales = new Dictionary<Region, double>();
                foreach (Region reg in Region.GetValues(typeof(Region))) Sales[reg] = 0;
            }
            public string Name { get; set; }
            public double Turnover { get; set; }
            public Dictionary<Region, double> Sales { get; set; }
        }

        public AgentsRegionModel()
        {
            Agents = new List<AgentModel>();
        }
        public string Title { get; set; }
        public string Agent { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<AgentModel> Agents { get; set; }
    }
}