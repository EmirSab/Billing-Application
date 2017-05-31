using System;

namespace Billing.Seed
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Data migration in progress");
            Console.WriteLine("--------------------------");
            Categories.Get();
            Products.Get();
            Towns.Get();
            Agents.Get();
            Agents.GetTowns();
            Partners.GetShippers();
            Partners.GetSuppliers();
            Partners.GetCustomers();
            Invoices.Get();
            Invoices.GetEvents();
            Invoices.GetItems();
            Procurements.Get();
            Tokens.Create();
            Console.WriteLine("-------------------------");
            Console.WriteLine("Press any key to continue");
            Console.ReadKey();
        }
    }
}








