using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Billing.Database
{
    public class Customer : Partner
    {
        public Customer()
        {
            Invoices = new List<Invoice>();
        }
       
        public virtual List<Invoice> Invoices { get; set; }
    }
}
