using System.Collections.Generic;

namespace Billing.Database
{
    public class Agent: Basic 
    {
        public Agent()
        {
            Towns = new List<Town>();
            Invoices = new List<Invoice>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }

        public virtual List<Town> Towns { get; set; }
        public virtual List<Invoice> Invoices { get; set; }
    }
}
