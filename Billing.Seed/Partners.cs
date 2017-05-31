using Billing.Database;
using Billing.Repository;
using System;
using System.Data;
using System.Linq;

namespace Billing.Seed
{
    public class Partners
    {
        public static void GetCustomers()
        {
            DataTable rawData = Helper.OpenExcel("Customers");
            int N = 0;
            foreach (DataRow row in rawData.Rows)
            {
                int oldId = Helper.getInteger(row, 0);
                string zip = Helper.getString(row, 1);
                Customer cust = new Customer()
                {
                    Name = Helper.getString(row, 2),
                    Address = Helper.getString(row, 3),
                    Town = Helper.Context.Towns.Get().FirstOrDefault(x => x.Zip == zip)
                };
                N++;
                Helper.Context.Customers.Insert(cust);
                Helper.Context.Commit();
                Lexicon.Customers.Add(oldId, cust.Id);
            }
            Console.WriteLine(N);
        }

        public static void GetSuppliers()
        {
            DataTable rawData = Helper.OpenExcel("Suppliers");
            int N = 0;
            foreach (DataRow row in rawData.Rows)
            {
                int oldId = Helper.getInteger(row, 0);
                string zip = Helper.getString(row, 1);
                Supplier supp = new Supplier()
                {
                    Name = Helper.getString(row, 2),
                    Address = Helper.getString(row, 3),
                    Town = Helper.Context.Towns.Get().FirstOrDefault(x => x.Zip == zip)
                };
                N++;
                Helper.Context.Suppliers.Insert(supp);
                Helper.Context.Commit();
                Lexicon.Suppliers.Add(oldId, supp.Id);
            }
            Console.WriteLine(N);
        }

        public static void GetShippers()
        {
            DataTable rawData = Helper.OpenExcel("Shippers");
            int N = 0;
            foreach (DataRow row in rawData.Rows)
            {
                int oldId = Helper.getInteger(row, 0);
                string zip = Helper.getString(row, 1);
                Shipper ship = new Shipper()
                {
                    Name = Helper.getString(row, 2),
                    Address = Helper.getString(row, 3),
                    Town = Helper.Context.Towns.Get().FirstOrDefault(x => x.Zip == zip)
                };
                N++;
                Helper.Context.Shippers.Insert(ship);
                Helper.Context.Commit();
                Lexicon.Shippers.Add(oldId, ship.Id);
            }
            Console.WriteLine(N);
        }
    }
}