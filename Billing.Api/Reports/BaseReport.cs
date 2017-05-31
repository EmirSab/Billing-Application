using Billing.Api.Helpers;
using Billing.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Billing.Api.Reports
{
    public class BaseReport
    {
        private UnitOfWork _unitOfWork;
        private ReportFactory _factory;

        public BaseReport(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        protected ReportFactory Factory
        {
            get { return _factory ?? (_factory = new ReportFactory()); }
        }

        protected UnitOfWork UnitOfWork
        {
            get { return _unitOfWork ?? (_unitOfWork = new UnitOfWork()); }
        }
    }
}