using System;
using System.Collections.Generic;

namespace Billing.Api.Models
{
    public class InvoiceModel
    {
        public InvoiceModel()
        {
            Items = new List<ItemModel>();
        }

        public int Id { get; set; }
        public string InvoiceNo { get; set; }
        public DateTime Date { get; set; }
        public DateTime? ShippedOn { get; set; }
        public string Status { get; set; }
        public int StatusId { get; set; }
        public double SubTotal { get; set; }
        public double Vat { get; set; }
        public double VatAmount { get; set; }
        public double Shipping { get; set; }
        public double Total { get; set; }
        public string Agent { get; set; }
        public int AgentId { get; set; }
        public string Customer { get; set; }
        public int CustomerId { get; set; }
        public string Shipper { get; set; }
        public int ShipperId { get; set; }
        public List<ItemModel> Items { get; set; }
    }
}