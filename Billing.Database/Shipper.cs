using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Billing.Database
{
    public class Shipper : Partner
    {
        public Shipper()
        {
            Invoices = new List<Invoice>();
        }
   
        public virtual List<Invoice> Invoices { get; set; }
    }
}
