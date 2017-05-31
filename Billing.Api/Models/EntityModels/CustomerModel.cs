using System.Collections.Generic;

namespace Billing.Api.Models
{
    public class CustomerModel
    {
        public CustomerModel()
        {
            Invoices = new List<string>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Town { get; set; }
        public int TownId { get; set; }
        public List<string> Invoices;
    }
}