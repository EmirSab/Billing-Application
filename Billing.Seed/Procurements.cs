using Billing.Database;
using Billing.Repository;
using System;
using System.Data;

namespace Billing.Seed
{
    public class Procurements
    {
        public static void Get()
        {
            DataTable rawData = Helper.OpenExcel("Procurements");
            int N = 0;
            Helper.Context.Context.Configuration.AutoDetectChangesEnabled = false;
            Helper.Context.Context.Configuration.ValidateOnSaveEnabled = false;
            foreach (DataRow row in rawData.Rows)
            {
                Procurement procurement = new Procurement()
                {
                    Document = Helper.getString(row, 1),
                    Date = Helper.getDate(row, 2),
                    Product = Helper.Context.Products.Get(Lexicon.Products[Helper.getInteger(row, 3)]),
                    Supplier = Helper.Context.Suppliers.Get(Lexicon.Suppliers[Helper.getInteger(row, 4)]),
                    Quantity = Helper.getInteger(row, 5),
                    Price = Helper.getDouble(row, 6)
                };
                N++;
                if (N % 100 == 0) Console.Write($"{N} ");
                Helper.Context.Procurements.Insert(procurement);
            }
            Helper.Context.Commit();
            Console.WriteLine(N);
        }
    }
}
