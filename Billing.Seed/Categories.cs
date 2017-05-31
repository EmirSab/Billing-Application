using Billing.Database;
using System;
using System.Data;

namespace Billing.Seed
{
    public class Categories
    {
        public static void Get()
        {
            DataTable rawData = Helper.OpenExcel("Categories");
            int N = 0;
            foreach (DataRow row in rawData.Rows)
            {
                int oldId = Helper.getInteger(row, 0);
                Category catt = new Category()
                {
                    Name = Helper.getString(row, 1),
                };
                N++;
                Helper.Context.Categories.Insert(catt);
                Helper.Context.Commit();
                Lexicon.Categories.Add(oldId, catt.Id);
            }
            Console.WriteLine(N);
        }
    }
}
