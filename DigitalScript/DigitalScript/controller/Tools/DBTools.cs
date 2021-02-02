﻿using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace DigitalScript
{
    /// <summary>
    /// database access action
    /// </summary>
    class DBTools
    {
        /// <summary>
        /// get db connection
        /// </summary>
        /// <returns></returns>
        static MySqlConnection GetConnection()
        {
            DBConfig db_config = new DBConfig(); // read config file
            string connectStr = "server=" + db_config.server + 
                                ";port=" + db_config.port + 
                                ";user=" + db_config.user + 
                                ";password=" + db_config.password + 
                                ";database=" + db_config.db_name + ";";
            return new MySqlConnection(connectStr);
        }

        /// <summary>
        /// convert result set(MySqlDataReader) to Dictionary list
        /// </summary>
        /// <param name="rdr"></param>
        /// <returns>jason format string</returns>
        static List<Dictionary<string, object>> DataReaderToList(MySqlDataReader r)
        {
            List<Dictionary<string, object>> res = new List<Dictionary<string, object>>();
            while (r.Read())
            {
                Dictionary<string, object> d = new Dictionary<string, object>();
                for (int inc = 0; inc < r.FieldCount; inc++)
                {
                    d.Add(r.GetName(inc), r.GetValue(inc));
                }
                res.Add(d);
            }
            r.Close();
            return res;
        }

        /// <summary>
        /// For query (select sql command) using
        /// </summary>
        /// <param name="sql">select command</param>
        /// <returns> jason format string </returns>
        public static List<Dictionary<string, object>> Query(string sql)
        {
            MySqlConnection conn = GetConnection();
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                return DataReaderToList(cmd.ExecuteReader());
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return null;
        }


        /// <summary>
        /// For Insert/Delete/Update sql command suing
        /// </summary>
        /// <param name="sql"> I/D/U sql command</param>
        /// <returns> 0 : fail ; 1 : success </returns>
        public static int Insert_Delete_Update(string sql)
        {
            MySqlConnection conn = GetConnection();
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(sql, conn);

                return cmd.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return 0;
        }

      /*  static void Main(string[] args)
        {
            string sql = "SELECT * FROM clothes";
            List<Dictionary<string, object>> lst = Query(sql);



            Console.WriteLine();
        }*/
    }
}
