using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using NowManagementStudio.Models.Inventory;
using System.Globalization;

namespace NowManagementStudio.DAL
{
    class InventorySprocs : StoredProcedure
    {
        //Here is the once-per-class call to initialize the log object
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Gets all the lots in the system
        /// </summary>
        /// <returns></returns>
        public List<Lots> GetLots()
        {
            StoredProcedure sproc = new StoredProcedure();
            MySqlCommand cmd = sproc.Command("INV_GetLots", null, null);
            MySqlDataReader rdr = null;
            double val;
            rdr = cmd.ExecuteReader();
            List<Lots> results = new List<Lots>();
            // iterate through results, printing each to console
            while (rdr.Read())
            {
                Lots lot = new Lots();
                lot.Id = Convert.ToInt32(rdr["id"]);
                lot.serialNo = Convert.ToString(rdr["serial_no"]);
                lot.brand = Convert.ToString(rdr["brand"]);
                lot.type = Convert.ToString(rdr["type"]);
                lot.typeId = Convert.ToString(rdr["typeID"]);
                lot.location = Convert.ToString(rdr["location"]);
                lot.locationId = Convert.ToString(rdr["locationID"]);
                lot.price = Double.TryParse(Convert.ToString(rdr["price"]), out val) ? val : Double.NaN;
                lot.weightId = Convert.ToString(rdr["weight_id"]);
                lot.weight = Double.TryParse(Convert.ToString(rdr["weight"]), out val) ? val : Double.NaN;
                lot.widthId = Convert.ToString(rdr["width_id"]);
                lot.width = Double.TryParse(Convert.ToString(rdr["width"]), out val) ? val : Double.NaN;
                lot.heightId = Convert.ToString(rdr["height_id"]);
                lot.height = Double.TryParse(Convert.ToString(rdr["height"]), out val) ? val : Double.NaN;
                lot.volumeId = Convert.ToString(rdr["volume_id"]);
                lot.volume = Double.TryParse(Convert.ToString(rdr["volume"]), out val) ? val : Double.NaN;
                lot.purchaseDate = Convert.ToString(rdr["purchase_date"]);
                lot.expirationDate = Convert.ToString(rdr["expiration_date"]);
                lot.nextInvDateId = Convert.ToString(rdr["last_inv_date_id"]);
                lot.nextInvDate = Convert.ToString(rdr["last_inv_date"]);
                lot.lastInvDateId = Convert.ToString(rdr["next_inv_date_id"]);
                lot.lastInvDate = Convert.ToString(rdr["next_inv_date"]);
                lot.notes = Convert.ToString(rdr["notes"]);
                results.Add(lot);
            }
            return results;
        }

        /// <summary>
        /// Gets a list of Lot storage locations from the database
        /// </summary>
        /// <returns></returns>
        public List<Location> GetLocations()
        {
            StoredProcedure sproc = new StoredProcedure();
            MySqlCommand cmd = sproc.Command("INV_GetLocations", null, null);
            MySqlDataReader rdr = null;
            double val;
            rdr = cmd.ExecuteReader();
            List<Location> results = new List<Location>();
            // iterate through results, printing each to console
            while (rdr.Read())
            {
                Location location = new Location();
                location.Id = Convert.ToInt32(rdr["id"]);
                location.location = Convert.ToString(rdr["location"]);
                results.Add(location);
            }
            return results;
        }

        /// <summary>
        /// Update values of a specific lot
        /// </summary>
        /// <param name="lot"></param>
        /// <param name="propValsId"></param>
        /// <param name="propVals"></param>
        public void UpdateLot(Lots lot,string propValsId, string propVals)
        {
            try
            {
                StoredProcedure sproc = new StoredProcedure();
                MySqlDataReader rdr = null;
                var list = new List<KeyValuePair<string, string>>();

                //Add Parameters
                list.Add(new KeyValuePair<string, string>("@lotID", Convert.ToString(lot.Id)));
                list.Add(new KeyValuePair<string, string>("@serialNo", lot.serialNo));
                list.Add(new KeyValuePair<string, string>("@uomID", "1"));
                list.Add(new KeyValuePair<string, string>("@quantity", "1"));
                list.Add(new KeyValuePair<string, string>("@price", Convert.ToString(lot.price)));
                list.Add(new KeyValuePair<string, string>("@dateAdded", lot.purchaseDate));
                list.Add(new KeyValuePair<string, string>("@expirationDate", lot.expirationDate));
                list.Add(new KeyValuePair<string, string>("@userName", "RSW"));
                list.Add(new KeyValuePair<string, string>("@matTypeID", lot.typeId));
                list.Add(new KeyValuePair<string, string>("@propValsID", propValsId));
                list.Add(new KeyValuePair<string, string>("@propVals", propVals));
                list.Add(new KeyValuePair<string, string>("@userNotes", lot.notes));


                MySqlCommand cmd = sproc.Command("INV_UpdateLot", list, null);

                // execute the command
                cmd.ExecuteNonQuery();

                //Update Lot Location in case there was a change
                MoveLot(lot, "RSW");
            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

        /// <summary>
        /// Procedure for moving lots to track lot movement history
        /// </summary>
        /// <param name="lot"></param>
        /// <param name="userName"></param>
        public void MoveLot(Lots lot, string userName)
        {
            try
            {
                StoredProcedure sproc = new StoredProcedure();
                MySqlDataReader rdr = null;
                var list = new List<KeyValuePair<string, string>>();

                //Add Parameters
                list.Add(new KeyValuePair<string, string>("@lotIDs", Convert.ToString(lot.Id)));
                list.Add(new KeyValuePair<string, string>("@newValue", lot.locationId));
                list.Add(new KeyValuePair<string, string>("@user", userName));
                MySqlCommand cmd = sproc.Command("INV_UpdateLotLocation", list, null);

                // execute the command
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

        /// <summary>
        /// Creates a new lot in the datasbase
        /// </summary>
        /// <param name="serialNo"></param>
        /// <param name="price"></param>
        /// <param name="dateAdded"></param>
        /// <param name="expirationDate"></param>
        /// <param name="propValsId"></param>
        /// <param name="propVals"></param>
        /// <param name="notes"></param>
        public void InsertLot(string serialNo, string price, string dateAdded, string expirationDate, string propValsId, string propVals, string notes)
        {
            try
            {
                StoredProcedure sproc = new StoredProcedure();
                MySqlDataReader rdr = null;
                var list = new List<KeyValuePair<string, string>>();

                //Add Parameters

                list.Add(new KeyValuePair<string, string>("@uomID", "1"));
                list.Add(new KeyValuePair<string, string>("@lotStatusID", "1"));

                list.Add(new KeyValuePair<string, string>("@locationID", "1"));
                list.Add(new KeyValuePair<string, string>("@brandID", "1"));
                list.Add(new KeyValuePair<string, string>("@typeID", "1"));
                list.Add(new KeyValuePair<string, string>("@serialNo", serialNo));
                list.Add(new KeyValuePair<string, string>("@quantity", "1"));
                list.Add(new KeyValuePair<string, string>("@price", price));
                list.Add(new KeyValuePair<string, string>("@dateAdded", "1982-08-09"));
                list.Add(new KeyValuePair<string, string>("@expirationDate", "1982-08-09"));
                list.Add(new KeyValuePair<string, string>("@userName", "RSW"));

                list.Add(new KeyValuePair<string, string>("@userNotes", "Added"));
                list.Add(new KeyValuePair<string, string>("@propValsID", propValsId));
                list.Add(new KeyValuePair<string, string>("@propVals", propVals));
                MySqlCommand cmd = sproc.Command("INV_UpdateLot", list, null);

                string outputParam = "@insertID";
                cmd = sproc.Command("INV_InsertLot", list, outputParam);
                // execute the command
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

        /// <summary>
        /// Returns a list of all the material categories
        /// </summary>
        /// <returns></returns>
        public List<MatCategory> GetMatCategories()
        {
            StoredProcedure sproc = new StoredProcedure();
            MySqlCommand cmd = sproc.Command("INV_GetMatCategories", null, null);
            MySqlDataReader rdr = null;

            rdr = cmd.ExecuteReader();
            List<MatCategory> results = new List<MatCategory>();

            // iterate through results, printing each to console
            while (rdr.Read())
            {
                MatCategory category = new MatCategory();
                category.Id = Convert.ToInt32(rdr["id"]);
                category.category = rdr["category"].ToString();
                results.Add(category);
            }
            return results;
        }

        /// <summary>
        /// Returns a list of all the material types within a category
        /// </summary>
        /// <returns></returns>
        public List<MatType> GetMatTypesByCategories(int brandID)
        {
            StoredProcedure sproc = new StoredProcedure();
            MySqlDataReader rdr = null;
            var list = new List<KeyValuePair<string, string>>();
            //Add Parameters
            //TODO - for the current implementation, we don't need the Brand Category
            //So its current hardcoded as a value of 1
            list.Add(new KeyValuePair<string, string>("@brandIDs", Convert.ToString(brandID)));
            MySqlCommand cmd = sproc.Command("INV_GetMatTypesInBrands", list, null);
            rdr = cmd.ExecuteReader();
            List<MatType> results = new List<MatType>();  

            // iterate through results, printing each to console
            while (rdr.Read())
            {
                MatType type = new MatType();
                type.Id = Convert.ToInt32(rdr["id"]);
                type.type = rdr["type"].ToString();
                results.Add(type);
            }
            return results;
        }




    }
}