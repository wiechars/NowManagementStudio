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
        static void Main()
        {
            StoredProcedure spd = new StoredProcedure();

            // run a simple stored procedure
            spd.RunStoredProc();

            // run a stored procedure that takes a parameter
            spd.RunStoredProcParams();
        }

        public List<Contact> GetContacts()
        {

            MySqlConnection conn = null;
            MySqlDataReader rdr = null;

            try
            {

                // create and open a connection object
                conn = new
                        MySqlConnection("Server=10.1.1.60;uid=root;pwd=Glasgow931;database=nms;");
                //SqlConnection(ConfigurationManager.AppSettings["conn"].ToString());
                conn.Open();

                // 1. create a command object identifying
                // the stored procedure
                MySqlCommand cmd = new MySqlCommand(
                    "NMS_GetContacts", conn);

                // 2. set the command object so it knows
                // to execute a stored procedure
                cmd.CommandType = CommandType.StoredProcedure;

                // 3. add parameter to command, which
                // will be passed to the stored procedure
                // cmd.Parameters.Add(
                //     new SqlParameter("@CustomerID", custId));

                // execute the command

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
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
                if (rdr != null)
                {
                    rdr.Close();
                }
            }

        }

        // run a stored procedure that takes a parameter
        public void EditContacts(int id, string name, string email, string phoneNumber)
        {

            MySqlConnection conn = null;
            MySqlDataReader rdr = null;

            try
            {

                // create and open a connection object
                conn = new
                        MySqlConnection("Server=10.1.1.60;uid=root;pwd=Glasgow931;database=nms;");
                conn.Open();

                // 1. create a command object identifying
                // the stored procedure
                MySqlCommand cmd = new MySqlCommand(
                    "NMS_UpdateContact", conn);

                // 2. set the command object so it knows
                // to execute a stored procedure
                cmd.CommandType = CommandType.StoredProcedure;

                // 3. add parameter to command, which
                // will be passed to the stored procedure
                cmd.Parameters.Add(
                    new MySqlParameter("@contactID", id));
                cmd.Parameters.Add(
    new MySqlParameter("@name", name));
                cmd.Parameters.Add(
    new MySqlParameter("@email", email));
                cmd.Parameters.Add(
    new MySqlParameter("@phoneNumber", phoneNumber));

                // execute the command
               cmd.ExecuteNonQuery();

            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
                if (rdr != null)
                {
                    rdr.Close();
                }
            }
        }

        // run a stored procedure that takes a parameter
        public void AddContact(string name, string email, string phoneNumber)
        {

            MySqlConnection conn = null;
            MySqlDataReader rdr = null;

            try
            {

                // create and open a connection object
                conn = new
                        MySqlConnection("Server=10.1.1.60;uid=root;pwd=Glasgow931;database=nms;");
                conn.Open();

                // 1. create a command object identifying
                // the stored procedure
                MySqlCommand cmd = new MySqlCommand(
                    "NMS_InsertContact", conn);

                // 2. set the command object so it knows
                // to execute a stored procedure
                cmd.CommandType = CommandType.StoredProcedure;

                // 3. add parameter to command, which
                // will be passed to the stored procedure
                cmd.Parameters.Add(
    new MySqlParameter("@name", name));
                cmd.Parameters.Add(
    new MySqlParameter("@email", email));
                cmd.Parameters.Add(
    new MySqlParameter("@phoneNumber", phoneNumber));

                // execute the command
                cmd.ExecuteNonQuery();

            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
                if (rdr != null)
                {
                    rdr.Close();
                }
            }
        }
        // run a simple stored procedure
        public void RunStoredProc()
        {
            SqlConnection conn = null;
            SqlDataReader rdr = null;

            Console.WriteLine("\nTop 10 Most Expensive Products:\n");

            try
            {
                // create and open a connection object
                conn = new
                    SqlConnection("Server=(local);DataBase=Northwind;Integrated Security=SSPI");
                conn.Open();

                // 1. create a command object identifying
                // the stored procedure
                SqlCommand cmd = new SqlCommand(
                    "Ten Most Expensive Products", conn);

                // 2. set the command object so it knows
                // to execute a stored procedure
                cmd.CommandType = CommandType.StoredProcedure;

                // execute the command
                rdr = cmd.ExecuteReader();

                // iterate through results, printing each to console
                while (rdr.Read())
                {
                    Console.WriteLine(
                        "Product: {0,-25} Price: ${1,6:####.00}",
                        rdr["TenMostExpensiveProducts"],
                        rdr["UnitPrice"]);
                }
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
                if (rdr != null)
                {
                    rdr.Close();
                }
            }
        }

        // run a stored procedure that takes a parameter
        public void RunStoredProcParams()
        {
            SqlConnection conn = null;
            SqlDataReader rdr = null;

            // typically obtained from user
            // input, but we take a short cut
            string custId = "FURIB";

            Console.WriteLine("\nCustomer Order History:\n");

            try
            {
                // create and open a connection object
                conn = new
                    SqlConnection("Server=(local);DataBase=Northwind;Integrated Security=SSPI");
                conn.Open();

                // 1. create a command object identifying
                // the stored procedure
                SqlCommand cmd = new SqlCommand(
                    "CustOrderHist", conn);

                // 2. set the command object so it knows
                // to execute a stored procedure
                cmd.CommandType = CommandType.StoredProcedure;

                // 3. add parameter to command, which
                // will be passed to the stored procedure
                cmd.Parameters.Add(
                    new SqlParameter("@CustomerID", custId));

                // execute the command
                rdr = cmd.ExecuteReader();

                // iterate through results, printing each to console
                while (rdr.Read())
                {
                    Console.WriteLine(
                        "Product: {0,-35} Total: {1,2}",
                        rdr["ProductName"],
                        rdr["Total"]);
                }
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
                if (rdr != null)
                {
                    rdr.Close();
                }
            }
        }
    }
}