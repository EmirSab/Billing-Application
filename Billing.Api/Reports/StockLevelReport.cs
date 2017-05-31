using Billing.Api.Models;
using Billing.Api.Models.ReportModels;
using Billing.Database;
using Billing.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Billing.Api.Reports
{
    public class StockLevelReport : BaseReport
    {
        public StockLevelReport(UnitOfWork unitOfWork) : base(unitOfWork) { }

        public StockLevelModel Report(int id)
        {
            StockLevelModel result = new StockLevelModel();
            Category category = UnitOfWork.Categories.Get(id);
            if (category == null)
                result.Category = "Category does not exists!";
            else
            {
                result.Category = category.Name;
                result.Products = category.Products.OrderBy(x => x.Name).Select(x => Factory.Create(x)).ToList();
            }
            return result;
        }
    }
}