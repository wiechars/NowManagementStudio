using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using NowManagementStudio.Models;

namespace NowManagementStudio.DAL
{

    class StoredProcedure
    {
            private MySqlConnection nmsConnection = new MySqlConnection();
            private MySqlConnection nmsSecurity = new MySqlConnection();

            //Here is the once-per-class call to initialize the log object
            private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private void Connection()
        {
            string constr = ConfigurationManager.ConnectionStrings["NMS"].ToString();
            nmsConnection = new MySqlConnection(constr);

        }

        /// <summary>
        /// Method for Generated the MySQL Command for the reader
        /// </summary>
        /// <param name="sprocName"></param>
        /// <param name="inputParameters"></param>
        /// <returns></returns>
        public MySqlCommand Command(string sprocName, List<KeyValuePair<string,string>> inputParameters, string outputParameter)
        {
            MySqlCommand cmd = new MySqlCommand();
            try
            {
                if (nmsConnection != null)
                {
                    Connection();
                }
                nmsConnection.Open();
                cmd = new MySqlCommand(sprocName, nmsConnection);
                cmd.CommandType = CommandType.StoredProcedure;
                //Add parameters to Sproc 

                if (inputParameters != null)
                {
                    foreach (var item in inputParameters)
                    {
                        cmd.Parameters.Add(
                           new MySqlParameter(item.Key.ToString(), item.Value.ToString()));
                        
                    }
                }
                if (outputParameter != null)
                {
                    cmd.Parameters.AddWithValue(outputParameter, MySqlDbType.Int32);
                    cmd.Parameters[outputParameter].Direction = ParameterDirection.Output;
                }
            }
            catch (Exception e)
            {
                log.Error("Error Creating Stored Procedure : " + e);
            }
            return cmd;
        }

    }
}