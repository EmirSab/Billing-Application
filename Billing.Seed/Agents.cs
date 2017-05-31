using Billing.Database;
using Billing.Repository;
using System;
using System.Data;
using System.Linq;
using WebMatrix.WebData;

namespace Billing.Seed
{
    public class Agents
    {
        public static void Get()
        {
            if (!WebSecurity.Initialized) WebSecurity.InitializeDatabaseConnection("Billing", "Agents", "Id", "Username", autoCreateTables: true);
            DataTable rawData = Helper.OpenExcel("Agents");
            int N = 0;
            foreach (DataRow row in rawData.Rows)
            {
                int oldId = Helper.getInteger(row, 0);
                Agent agent = new Agent()
                {
                    Name = Helper.getString(row, 1),
                    Username = Helper.getString(row, 2)
                };
                N++;
                Helper.Context.Agents.Insert(agent);
                Helper.Context.Commit();
                Lexicon.Agents.Add(oldId, agent.Id);
            }
            Console.WriteLine(N);
        }

        public static void GetTowns()
        {
            DataTable rawData = Helper.OpenExcel("TownAgents");
            int N = 0;
            foreach (DataRow row in rawData.Rows)
            {
                Agent agent = Helper.Context.Agents.Get(Lexicon.Agents[Helper.getInteger(row, 0)]);
                string town = Helper.getString(row, 1);
                N++;
                agent.Towns.Add(Helper.Context.Towns.Get().FirstOrDefault(x => x.Zip == town));
            }
            Helper.Context.Commit();
            Console.WriteLine(N);
        }
    }
}
