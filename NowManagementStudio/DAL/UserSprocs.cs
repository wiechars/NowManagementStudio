using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using NowManagementStudio.Models;

namespace NowManagementStudio.DAL
{
    class UserSprocs : StoredProcedure
    {
        //Here is the once-per-class call to initialize the log object
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Returns all the Contacts in the system
        /// </summary>
        /// <returns></returns>
        public List<UserModel> GetUsers()
        {
            StoredProcedure sproc = new StoredProcedure();
            MySqlCommand cmd = sproc.Command("SEC_GetUsers", null, null, true);
            MySqlDataReader rdr = null;

            rdr = cmd.ExecuteReader();
            List<UserModel> results = new List<UserModel>();

            // iterate through results, printing each to console
            while (rdr.Read())
            {
                UserModel user = new UserModel();
               // user.Id = Convert.ToInt32(rdr["id"]);
                user.UserName = rdr["UserName"].ToString();
                user.Email = rdr["Email"].ToString();
                user.PhoneNumber = rdr["PhoneNumber"].ToString();
                results.Add(user);
            }
            return results;
        }
               


    }
}