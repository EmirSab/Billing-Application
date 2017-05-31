namespace Billing.Api.Models
{
    public class ItemModel
    {
        public int Id { get; set; }
        public string Invoice { get; set; }
        public string Product { get; set; }
        public int Quantity { get; set; }
        public string Unit { get; set; }
        public double Price { get; set; }
        public double SubTotal { get; set; }
        public int InvoiceId { get; set; }
        public int ProductId { get; set; }
    }
}