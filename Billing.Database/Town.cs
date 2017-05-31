using System.Collections.Generic;

namespace Billing.Database
{
    public class Town : Basic
    {
        public Town()
        {
            Suppliers = new List<Supplier>();
            Customers = new List<Customer>();
            Shippers = new List<Shipper>();
            Agents = new List<Agent>();
        }

        public int Id { get; set; }
        public string Zip { get; set; }
        public string Name { get; set; }
        public Region Region { get; set; }

        public virtual List<Supplier> Suppliers { get; set; }
        public virtual List<Customer> Customers { get; set; }
        public virtual List<Shipper> Shippers { get; set; }
        public virtual List<Agent> Agents { get; set; }
    }
}
