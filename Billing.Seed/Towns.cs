using Billing.Database;
using Billing.Repository;
using System;
using System.Data;

namespace Billing.Seed
{
    public class Towns
    {
        public static void Get()
        {
            
            DataTable rawData = Helper.OpenExcel("Towns");
            int N = 0;
            Helper.Context.Context.Configuration.AutoDetectChangesEnabled = false;
            Helper.Context.Context.Configuration.ValidateOnSaveEnabled = false;
            foreach (DataRow row in rawData.Rows)
            {
                Town town = new Town()
                {
                    Zip = Helper.getString(row, 0),
                    Name = Helper.getString(row, 1),
                    Region = (Region)Helper.getInteger(row, 2)
                };
                N++;
                if (N % 100 == 0) Console.Write($"{N} ");
                Helper.Context.Towns.Insert(town);
            }
            Helper.Context.Towns.Commit();
            Console.WriteLine(N);
        }
    }
}
