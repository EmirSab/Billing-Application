using Billing.Database;
using Billing.Repository;
using System;
using System.Data;

namespace Billing.Seed
{
    public class Invoices
    {
        public static void Get()
        {
            DataTable rawData = Helper.OpenExcel("Invoices");
            int N = 0;
            foreach (DataRow row in rawData.Rows)
            {
                int oldId = Helper.getInteger(row, 0);
                DateTime shippedOn = Helper.getDate(row, 5);
                double shipping = Helper.getDouble(row, 6);
                Shipper shipper = Helper.Context.Shippers.Get(Lexicon.Shippers[Helper.getInteger(row, 7)]);

                Invoice invoice = new Invoice()
                {
                    InvoiceNo = Helper.getString(row, 1),
                    Date = Helper.getDate(row, 2),
                    Agent = Helper.Context.Agents.Get(Lexicon.Agents[Helper.getInteger(row, 3)]),
                    Customer = Helper.Context.Customers.Get(Lexicon.Customers[Helper.getInteger(row, 4)]),
                    Vat = Helper.getDouble(row, 8),
                    Status = (Status)Helper.getInteger(row, 9),
                    Shipping = 0
                };
                if (invoice.Status >= Status.InvoiceReady)
                {
                    invoice.Shipping = shipping;
                    invoice.Shipper = shipper;
                }
                N++;
                Helper.Context.Invoices.Insert(invoice);
                Helper.Context.Commit();
                Lexicon.Invoices.Add(oldId, invoice.Id);
            }
            Console.WriteLine(N);
        }

        public static void GetEvents()
        {
            DataTable rawData = Helper.OpenExcel("Events");
            int N = 0;
            Helper.Context.Context.Configuration.AutoDetectChangesEnabled = false;
            Helper.Context.Context.Configuration.ValidateOnSaveEnabled = false;
            foreach (DataRow row in rawData.Rows)
            {
                Event item = new Event()
                {
                    Invoice = Helper.Context.Invoices.Get(Lexicon.Invoices[Helper.getInteger(row, 1)]),
                    Date = Helper.getDate(row, 3),
                    Status = (Status)Helper.getInteger(row, 2)
                };
                N++;
                if (N % 100 == 0) Console.Write($"{N} ");
                Helper.Context.History.Insert(item);
            }
            Helper.Context.Commit();
            Console.WriteLine(N);
        }

        public static void GetItems()
        {
            DataTable rawData = Helper.OpenExcel("Items");
            int N = 0;
            Helper.Context.Context.Configuration.AutoDetectChangesEnabled = false;
            Helper.Context.Context.Configuration.ValidateOnSaveEnabled = false;
            foreach (DataRow row in rawData.Rows)
            {
                Item item = new Item()
                {
                    Invoice = Helper.Context.Invoices.Get(Lexicon.Invoices[Helper.getInteger(row, 0)]),
                    Product = Helper.Context.Products.Get(Lexicon.Products[Helper.getInteger(row, 1)]),
                    Quantity = Helper.getInteger(row, 2),
                    Price = Helper.getDouble(row, 3)
                };
                N++;
                if (N % 100 == 0) Console.Write($"{N} ");
                Helper.Context.Items.Insert(item);
            }
            Helper.Context.Commit();
            Console.WriteLine(N);
        }
    }
}
