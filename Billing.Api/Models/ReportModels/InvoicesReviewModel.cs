using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Billing.Api.Models.ReportModels
{
    public class InvoiceInfoModel
    {
        public int InvoiceId { get; set; }
        public string InvoiceNo { get; set; }
        public DateTime InvoiceDate { get; set; }
        public DateTime ShippedOn { get; set; }
        public double InvoiceTotal { get; set; }
        public string InvoiceStatus { get; set; }
    }


    public class InvoicesReviewModel
    {
        public InvoicesReviewModel()
        {
            InvoiceInfo = new List<InvoiceInfoModel>();
        }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public double GrandTotal { get; set; }
        public List<InvoiceInfoModel> InvoiceInfo { get; set; }
    }
}