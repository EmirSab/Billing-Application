using Billing.Database;
using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Billing.Seed
{
    public static class Tokens
    {
        static StreamWriter sw = new StreamWriter(@"C:\NTG\Billing.txt");

        public static void Create()
        {
            AddUser("Alpha-Billing", "Alpha-Mistral-Talents");
            AddUser("Bravo-Billing", "Bravo-Mistral-Talents");
            AddUser("Charlie-Billing", "Charlie-Mistral-Talents");
            AddUser("Delta-Billing", "Delta-Mistral-Talents");
            AddUser("Echo-Billing", "Echo-Mistral-Talents");
            AddUser("Foxtrot-Billing", "Foxtrot-Mistral-Talents");
            Helper.Context.Commit();
            sw.Close();
        }

        static void AddUser(string ApiKey, string SecretKey)
        {
            string[] entry = ApiKey.Split(':');
            var encoding = Encoding.GetEncoding("utf-8");
            ApiUser apiUser = new ApiUser()
            {
                Name = entry[0],
                AppId = Convert.ToBase64String(Encoding.UTF8.GetBytes(ApiKey)),
                Secret = Convert.ToBase64String(Encoding.UTF8.GetBytes(SecretKey))
            };
            Helper.Context.ApiUsers.Insert(apiUser);
            sw.WriteLine($"{apiUser.Name}   {apiUser.AppId}     {Signature(apiUser.Secret, apiUser.AppId)}");
        }

        public static string Signature(string Secret, string AppId)
        {
            byte[] secret = Convert.FromBase64String(Secret);
            byte[] appId = Convert.FromBase64String(AppId);
            var provider = new System.Security.Cryptography.HMACSHA256(secret);
            string key = System.Text.Encoding.Default.GetString(appId);
            var hash = provider.ComputeHash(Encoding.UTF8.GetBytes(key));
            return Convert.ToBase64String(hash);
        }
    }
}