using Billing.Api.Controllers;
using Billing.Api.Helpers;
using Billing.Api.Models;
using Billing.Database;
using Billing.Repository;
using System.Collections.Generic;
using System.Linq;

namespace Billing.Api.Reports
{
    public class AgentsByRegionReport : BaseReport
    {
        public AgentsByRegionReport(UnitOfWork unitOfWork) : base(unitOfWork) { }

        public AgentsRegionModel Report(RequestModel request)
        {
            AgentsRegionModel result = new AgentsRegionModel()
            {
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                Title = "Sales by Agents / Regions",
                Agent = BillingIdentity.CurrentUser.Name
            };

            List<AgentsRegionModel.InputModel> input;

            var query = UnitOfWork.Invoices.Get().Where(x => (x.Date >= request.StartDate && x.Date <= request.EndDate)).ToList();

            input = query.GroupBy(x => new { region = x.Customer.Town.Region })
                         .Select(x => new AgentsRegionModel.InputModel() { Row = "TOTAL", Column = x.Key.region, Value = x.Sum(y => y.SubTotal) })
                         .ToList();
            AgentsRegionModel.AgentModel agent = new AgentsRegionModel.AgentModel();
            agent.Name = "TOTAL";
            foreach (var item in input)
            {
                agent.Sales[item.Column] = item.Value;
                agent.Turnover += item.Value;
            }

            input = query.OrderBy(x => x.Agent.Name)
                         .GroupBy(x => new { agent = x.Agent.Name, region = x.Customer.Town.Region })
                         .Select(x => new AgentsRegionModel.InputModel { Row = x.Key.agent, Column = x.Key.region, Value = x.Sum(y => y.SubTotal) })
                         .ToList();
            foreach (var item in input)
            {
                if (agent.Name != item.Row)
                {
                    result.Agents.Add(agent);
                    agent = new AgentsRegionModel.AgentModel() { Name = item.Row };
                }
                agent.Sales[item.Column] = item.Value;
                agent.Turnover += item.Value;
            }
            result.Agents.Add(agent);
            return result;
        }
    }
}