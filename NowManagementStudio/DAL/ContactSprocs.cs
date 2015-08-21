using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using NowManagementStudio.Models;

namespace NowManagementStudio.DAL
{
    class ContactSprocs : StoredProcedure
    {
        //Here is the once-per-class call to initialize the log object
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Returns all the Contacts in the system
        /// </summary>
        /// <returns></returns>
        public List<Contact> GetContacts()
        {
            StoredProcedure sproc = new StoredProcedure();
            MySqlCommand cmd = sproc.Command("NMS_GetContacts", null);
            MySqlDataReader rdr = null;

            rdr = cmd.ExecuteReader();
            List<Contact> results = new List<Contact>();

            // iterate through results, printing each to console
            while (rdr.Read())
            {
                Contact contact = new Contact();
                contact.Id = Convert.ToInt32(rdr["id"]);
                contact.Email = rdr["email"].ToString();
                contact.Mobile = rdr["phone_number"].ToString();
                contact.Name = rdr["name"].ToString();
                results.Add(contact);
            }
            return results;
        }


        /// <summary>
        /// Edits an Individual Contact
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="email"></param>
        /// <param name="phoneNumber"></param>
        public void EditContacts(string id, string name, string email, string phoneNumber)
        {
            try
            {
                StoredProcedure sproc = new StoredProcedure();
                MySqlDataReader rdr = null;
                var list = new List<KeyValuePair<string, string>>();
                //Add Parameters
                list.Add(new KeyValuePair<string, string>("@contactID", id));
                list.Add(new KeyValuePair<string, string>("@name", name));
                list.Add(new KeyValuePair<string, string>("@email", email));
                list.Add(new KeyValuePair<string, string>("@phoneNumber", phoneNumber));
                MySqlCommand cmd = sproc.Command("NMS_UpdateContact", list);
                // execute the command
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                log.Error("Error Editing Contacts : " + ex);
            }

        }

        /// <summary>
        /// Adds Contact to the databse
        /// </summary>
        /// <param name="name"></param>
        /// <param name="email"></param>
        /// <param name="phoneNumber"></param>
        public void AddContact(string name, string email, string phoneNumber)
        {

            try
            {
                StoredProcedure sproc = new StoredProcedure();
                var list = new List<KeyValuePair<string, string>>();
                //Add Parameters
                list.Add(new KeyValuePair<string, string>("@name", name));
                list.Add(new KeyValuePair<string, string>("@email", email));
                list.Add(new KeyValuePair<string, string>("@phoneNumber", phoneNumber));
                MySqlCommand cmd = sproc.Command("NMS_InsertContact", list);

                // execute the command
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                log.Error("Error Editing Contacts : " + ex);
            }

        }


    }
}