namespace Billing.Database.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Billing.Database.BillingContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(Billing.Database.BillingContext context)
        {
            //context.Products.Add(new Product() { Name = "Laptop", Price = 1000, Unit = "pcs" });
            //context.Towns.Add(new Town() { Zip = "71000", Name = "Sarajevo", Region = Region.Sarajevo });
            //context.SaveChanges();

            //Town town = context.Towns.Find(1);
            //context.Customers.Add(new Customer() { Name = "Mistral", Address=new Address() { Road = "Milana Preloga", House = "12/3" }, Town = town });
            //context.SaveChanges();
        }
    }
}
