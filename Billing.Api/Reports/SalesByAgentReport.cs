using Billing.Api.Models;
using Billing.Database;
using Billing.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Billing.Api.Reports
{
    public class SalesByAgentReport : BaseReport
    {
        public SalesByAgentReport(UnitOfWork unitOfWork) : base(unitOfWork) { }

        public SalesByAgentModel Report(RequestModel Request)
        {
            if (Request.EndDate <= Request.StartDate) throw new Exception("Incorrect Date");
            List<Invoice> Invoices = UnitOfWork.Invoices.Get().Where(x => x.Date >= Request.StartDate && x.Date <= Request.EndDate).ToList();
            List<Invoice> InvoicesOfAgent = UnitOfWork.Invoices.Get().Where(x => x.Date >= Request.StartDate && x.Date <= Request.EndDate && x.Agent.Id == Request.Id).ToList();
            double grandTotal = Math.Round(Invoices.Sum(x => x.SubTotal), 2);
            Agent agent = UnitOfWork.Agents.Get(Request.Id);
            double AgentTotal = Math.Round(InvoicesOfAgent.Sum(x => x.SubTotal), 2);
            SalesByAgentModel result = new SalesByAgentModel()
            {
                AgentName = agent.Name,
                StartDate = Request.StartDate,
                EndDate = Request.EndDate,
                AgentTotal = AgentTotal,
                PercentTotal = Math.Round(100 * InvoicesOfAgent.Sum(x => x.SubTotal) / grandTotal, 2)
            };

            result.Sales = InvoicesOfAgent.OrderBy(x => x.Customer.Id).ToList()
                                          .GroupBy(x => x.Customer.Town.Region.ToString())
                                          .Select(x => Factory.Create
                                          (InvoicesOfAgent, x.Key, x.Sum(y => y.SubTotal), AgentTotal, Invoices))
                                          .ToList();

            return result;
        }
    }
}