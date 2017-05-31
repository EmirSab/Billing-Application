using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Billing.Seed
{
    public static class Lexicon
    {
        public static Dictionary<int, int> Categories = new Dictionary<int, int>();
        public static Dictionary<int, int> Products = new Dictionary<int, int>();
        public static Dictionary<int, int> Agents = new Dictionary<int, int>();
        public static Dictionary<int, int> Customers = new Dictionary<int, int>();
        public static Dictionary<int, int> Suppliers = new Dictionary<int, int>();
        public static Dictionary<int, int> Shippers = new Dictionary<int, int>();
        public static Dictionary<int, int> Invoices = new Dictionary<int, int>();
    }
}
