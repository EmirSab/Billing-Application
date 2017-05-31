using Billing.Api.Helpers;
using Billing.Api.Models;
using Billing.Repository;
using System;
using System.Web.Http;

namespace Billing.Api.Reports
{
    public class SetOfReports
    {
        private UnitOfWork _unitOfWork;
        public SetOfReports(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        private DashboardReport _dashboard;
        private SalesByRegionReport _salesByRegion;
        private SalesByCustomerReport _salesByCustomer;
        private SalesByCategoryReport _salesByCategory;
        private AgentsByRegionReport _agentsByRegion;
        private CustomersByCategoryReport _customersByCategory;
        private InvoicesReviewReport _invoicesReview;
        private InvoicesReport _invoice;
        private StockLevelReport _stocklevel;
        private SalesByProductReport _salesByProduct;
        private SalesByAgentReport _salesByAgent;
        private InvoicesReviewPopupReport _invoiceReviewPopup;


        public DashboardReport Dashboard { get { return _dashboard ?? (_dashboard = new DashboardReport(_unitOfWork)); } }
        public SalesByRegionReport SalesByRegion { get { return _salesByRegion ?? (_salesByRegion = new SalesByRegionReport(_unitOfWork)); } }
        public SalesByCustomerReport SalesByCustomer { get { return _salesByCustomer ?? (_salesByCustomer = new SalesByCustomerReport(_unitOfWork)); } }
        public SalesByCategoryReport SalesByCategory { get { return _salesByCategory ?? (_salesByCategory = new SalesByCategoryReport(_unitOfWork)); } }
        public AgentsByRegionReport AgentsByRegion { get { return _agentsByRegion ?? (_agentsByRegion = new AgentsByRegionReport(_unitOfWork)); } }
        public CustomersByCategoryReport CustomersByCategory { get { return _customersByCategory ?? (_customersByCategory = new CustomersByCategoryReport(_unitOfWork)); } }
        public InvoicesReviewReport InvoicesReview { get { return _invoicesReview ?? (_invoicesReview = new InvoicesReviewReport(_unitOfWork)); } }
        public InvoicesReport Invoice { get { return _invoice ?? (_invoice = new InvoicesReport(_unitOfWork)); } }
        public StockLevelReport StockLevel { get { return _stocklevel ?? (_stocklevel = new StockLevelReport(_unitOfWork)); } }
        public SalesByProductReport SalesByProduct { get { return _salesByProduct ?? (_salesByProduct = new SalesByProductReport(_unitOfWork)); } }
        public SalesByAgentReport SalesByAgent { get { return _salesByAgent ?? (_salesByAgent = new SalesByAgentReport(_unitOfWork)); } }
        public InvoicesReviewPopupReport InvoiceReviewPopup { get { return _invoiceReviewPopup ?? (_invoiceReviewPopup = new InvoicesReviewPopupReport(_unitOfWork)); } }


    }
}

