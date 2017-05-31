using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Billing.Database
{
    public class History : Basic
    {
        public History()
        {
            Date = DateTime.Now;
        }
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int Status { get; set; }
        public virtual Invoice Invoice { get; set; }
    }
}
