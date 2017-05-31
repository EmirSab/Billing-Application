using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Billing.Api.Models.ReportModels
{
    public class InvoiceReviewPopupModel
    {
        public InvoiceReviewPopupModel()
        {
            Products = new List<InvoiceReviewProducts>();
        }
        public int InvoiceId { get; set; }
        public string InvoiceNo { get; set; }
        public string CustomerName { get; set; }
        public DateTime InvoiceDate { get; set; }
        public string InvoiceStatus { get; set; }
        public double Subtotal { get; set; }
        public double VatAmount { get; set; }
        public double Shipping { get; set; }
        public string Shipper { get; set; }
        public DateTime ShippedOn { get; set; }
        public List<InvoiceReviewProducts> Products { get; set; }
    }

    public class InvoiceReviewProducts
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string Unit { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public double Subtotal { get; set; }
    }
}
