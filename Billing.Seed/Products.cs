using Billing.Database;
using Billing.Repository;
using System;
using System.Data;

namespace Billing.Seed
{
    public class Products
    {
        public static void Get()
        {
            DataTable rawData = Helper.OpenExcel("Products");
            int N = 0;
            foreach (DataRow row in rawData.Rows)
            {
                int oldId = Helper.getInteger(row, 0);
                int input = Helper.getInteger(row, 5);
                int output = Helper.getInteger(row, 6);
                Product prod = new Product()
                {
                    Name = Helper.getString(row, 1),
                    Unit = Helper.getString(row, 2),
                    Price = Helper.getDouble(row, 3),
                    Category = Helper.Context.Categories.Get(Lexicon.Categories[Helper.getInteger(row, 4)])
                };
                N++;
                Helper.Context.Products.Insert(prod);
                Helper.Context.Commit();
                Helper.Context.Stocks.Insert(new Stock() { Id = prod.Id, Input = input, Output = output, Product = prod });
                Helper.Context.Commit();
                Lexicon.Products.Add(oldId, prod.Id);
            }
            Console.WriteLine(N);
        }
    }
}
