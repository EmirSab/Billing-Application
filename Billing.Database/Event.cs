using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Billing.Database
{
    public class Event : Basic
    {
        public int Id { get; set; }
        public Status Status { get; set; }
        public DateTime Date { get; set; }

        public virtual Invoice Invoice { get; set; }
    }
}
