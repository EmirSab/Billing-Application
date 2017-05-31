using System;
using System.Data;
using System.Data.OleDb;
using Billing.Repository;

namespace Billing.Seed
{
    public static class Helper
    {
        public static UnitOfWork Context = new UnitOfWork();
        
        public static string SourceRoot = @"C:\Users\HP\Documents\GitHub\alpha\billing.xls";

        

        public static DataTable OpenExcel(string sheet)
        {
            var cs = string.Format("Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Properties=Excel 8.0", SourceRoot);
            OleDbConnection conn = new OleDbConnection(cs);
            conn.Open();

            OleDbCommand cmd = new OleDbCommand(string.Format("SELECT * FROM [{0}$]", sheet), conn);
            OleDbDataAdapter da = new OleDbDataAdapter(cmd);

            DataTable dt = new DataTable();
            da.Fill(dt);
            conn.Close();

            Console.Write($"{sheet}: ");
            return dt;
        }

        public static string getString(DataRow row, int index)
        {
            return row.ItemArray.GetValue(index).ToString();
        }

        public static int getInteger(DataRow row, int index)
        {
            return Convert.ToInt32(row.ItemArray.GetValue(index).ToString());
        }

        public static DateTime getDate(DataRow row, int index)
        {
            return Convert.ToDateTime(row.ItemArray.GetValue(index).ToString());
        }

        public static double getDouble(DataRow row, int index)
        {
            return Convert.ToDouble(row.ItemArray.GetValue(index).ToString());
        }
    }
}
