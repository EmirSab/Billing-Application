namespace Billing.Database
{
    public enum Status
    {
        Canceled = -1,
        OrderCreated,
        InvoiceCreated,
        InvoiceSent,
        InvoicePaid,
        InvoiceOnHold,
        InvoiceReady,
        InvoiceShipped
    }
}
