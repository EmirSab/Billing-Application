using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Billing.Database
{
    public class Partner : Basic
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }

        [Required]
        public virtual Town Town{ get; set;}
    }
}
