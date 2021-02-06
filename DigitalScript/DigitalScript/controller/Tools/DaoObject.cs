using MySql.Data.MySqlClient;
using System;

namespace DigitalScript.controller.Tools
{
    class DaoObject
    {
        MySqlConnection conn;

        /// <summary>
        /// After done with DB access remember to close the connection
        /// </summary>
        public DaoObject()
        {
            conn = DBTools.GetConnection();
            try
            {
                conn.Open();
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public MySqlConnection GetConnection()
        {
            return conn;
        }

        /// <summary>
        /// Close the connecetion
        /// </summary>
        public void CloseConnection()
        {
            conn.Close();
        }
    }
}
