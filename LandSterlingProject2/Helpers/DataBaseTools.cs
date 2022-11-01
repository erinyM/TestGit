using LandSterlingProject2.Properties;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Web.Configuration;
using System.Web.Script.Serialization;

namespace LandSterlingProject2.Helpers
{
    public class DataBaseTools
    {
        public DataTable TBL;
        public static String CS = Settings.Default.CS; 
        // @"Data Source=192.168.1.191,49170;Initial Catalog=ePMS1000;User Id=sa;Password=Epms4321;MultipleActiveResultSets=True;";//
        public static String ServerPath = Settings.Default.ServerPath;
        public SqlCommand CMD=new SqlCommand();
        public static SqlConnection CONN;
        public DataBaseTools()
        {
           if (CONN == null || CONN.State==ConnectionState.Closed|| CONN.State == ConnectionState.Broken)
            {
                CONN = new SqlConnection
                {
                    ConnectionString = CS
                };
                CONN.Open();
            }
            CMD.Connection = CONN;
            TBL = new DataTable();
        }
        ~DataBaseTools()
        { 
           CMD.Dispose();
        }
        public void ExecuteNonQuery(String comand)
        {
            CMD.CommandText = comand;
            try
            {
                CMD.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
            }
        }
        public void ExecuteReader(String command)
        {
            CMD.CommandText = command;
            try
            {
                TBL.Clear();
                TBL.Load(CMD.ExecuteReader());
            }
            catch { }
        }
        public static string ToMysqlDate(DateTime Date)
        {
            String day = "" + Date.Day; if (day.Length < 2) { day = "0" + day; }
            String month = "" + Date.Month; if (month.Length < 2) { month = "0" + month; }
            String year = "" + Date.Year;
            String date = year + "-" + month + "-" + day;
            return date;
        }
        public static string ToMysqlTime(DateTime time)
        {
            return ToMysqlDate(time) + " " + time.Hour + ":" + time.Minute + ":" + time.Second;
        }
        public static string ToMysqlDate(string date)
        {
            String[] values = date.Split('/');
            date = values[2] + '-' + values[1] + '-' + values[0];
            return date;
        }
        public static int Max(string table, string column)
        {
            DataBaseTools DBT = new DataBaseTools();
            DBT.CMD.CommandText = String.Format("select Max({0}) from {1}", column, table);
            int max = 0;
            try
            {
                max = int.Parse("" + DBT.CMD.ExecuteScalar());
            }
            catch (Exception)
            { }
            
            return max;
        }
    }
}