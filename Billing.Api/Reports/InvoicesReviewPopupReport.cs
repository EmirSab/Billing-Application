using Billing.Api.Models.ReportModels;
using Billing.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Billing.Database;


namespace Billing.Api.Reports
{
    public class InvoicesReviewPopupReport : BaseReport
    {

        public InvoicesReviewPopupReport(UnitOfWork unitOfWork) : base(unitOfWork) { }
        public InvoiceReviewPopupModel Report(int id)
        {
            Invoice Invoice = UnitOfWork.Invoices.Get(id);
            if (Invoice == null) throw new Exception("Invoice not found");
            InvoiceReviewPopupModel result = new InvoiceReviewPopupModel
            {
                InvoiceId=Invoice.Id,
                InvoiceNo = Invoice.InvoiceNo,
                CustomerName = (Invoice.Customer == null) ? "" : Invoice.Customer.Name,
                InvoiceDate = Invoice.Date,
                InvoiceStatus = Invoice.Status.ToString(),
                Subtotal = Invoice.SubTotal,
                VatAmount = Invoice.VatAmount,
                Shipper = (Invoice.Shipper == null) ? "" : Invoice.Shipper.Name,
                Shipping = Invoice.Shipping,
                ShippedOn = (Invoice.ShippedOn == null) ? DateTime.Now : Invoice.ShippedOn.Value
            };
            result.Products = UnitOfWork.Items.Get().Where(x => x.Invoice.Id == id).ToList()
                                        .Select(x => Factory.Create(x.Product.Id, x.Product.Name, x.Price, x.Quantity, x.SubTotal, x.Product.Unit))
                                        .ToList();
            return result;
        }
    }
}