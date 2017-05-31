using Billing.Api.Controllers;
using Billing.Api.Helpers;
using Billing.Api.Models;
using Billing.Database;
using Billing.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Billing.Api.Reports
{
    public class SalesByRegionReport : BaseReport
    {
        public SalesByRegionReport(UnitOfWork unitOfWork) : base(unitOfWork) { }

        public SalesByRegionModel Report(RequestModel Request)
        {
            List<Invoice> Invoices = UnitOfWork.Invoices.Get().Where(x => x.Date >= Request.StartDate && x.Date <= Request.EndDate).ToList();
            SalesByRegionModel result = new SalesByRegionModel()
            {
                StartDate = Request.StartDate,
                EndDate = Request.EndDate,
                Title = "Sales by Region",
                Agent = BillingIdentity.CurrentUser.Name,
                GrandTotal = Invoices.Sum(x => x.SubTotal)
            };

            result.Sales = Invoices.OrderBy(x => x.Customer.Id).ToList()
                          .GroupBy(x => x.Customer.Town.Region.ToString())
                          .Select(x => new SalesByRegionModel.RegionSales() { RegionName = x.Key, RegionTotal = x.Sum(y => y.SubTotal) })
                          .ToList();

            foreach (var sale in result.Sales)
            {
                sale.RegionPercent = Math.Round(100 * sale.RegionTotal / result.GrandTotal, 2);
                sale.Agents = Invoices.Where(x => x.Customer.Town.Region.ToString() == sale.RegionName)
                             .OrderBy(x => x.Agent.Name)
                             .GroupBy(x => x.Agent.Name)
                             .Select(x => new SalesByRegionModel.AgentSales()
                             {
                                 AgentName = x.Key,
                                 AgentTotal = Math.Round(x.Sum(y => y.Total), 2),
                                 RegionPercent = Math.Round(100 * x.Sum(y => y.Total) / sale.RegionTotal, 2),
                                 TotalPercent = Math.Round(100 * x.Sum(y => y.Total) / result.GrandTotal, 2)
                             }).ToList();
            }

            return result;
        }
    }
}