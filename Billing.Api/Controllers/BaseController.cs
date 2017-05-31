using Billing.Api.Helpers;
using Billing.Api.Models;
using Billing.Api.Reports;
using Billing.Database;
using Billing.Repository;
using System;
using System.Web.Http;

namespace Billing.Api.Controllers
{
// imat cemo sve kontrolere iz api controlera, na ovaj nacin izbjegavamo sve zavisnosti od baze podataka i repositorija
    public class BaseController : ApiController
    {
        private UnitOfWork _unitOfWork;
        private Factory _factory;
        private SetOfReports _reports;

        protected UnitOfWork UnitOfWork
        {
            get { return _unitOfWork ?? (_unitOfWork = new UnitOfWork()); }
        }

        protected Factory Factory
        {
            get { return _factory ?? (_factory = new Factory(UnitOfWork)); }
        }

        protected SetOfReports Reports
        {
            get { return _reports ?? (_reports = new SetOfReports(UnitOfWork)); }
        }

    }
}
