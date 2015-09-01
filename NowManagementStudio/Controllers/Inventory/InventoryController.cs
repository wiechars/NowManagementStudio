using NowManagementStudio.Hubs;
using NowManagementStudio.Models.Inventory;
using NowManagementStudio.Controllers;
using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Http;

namespace NowManagementStudio.Inventory.Controllers
{
    [RoutePrefix("api/Inventory")]
    public class InventoryController : ApiControllerWithHub<ContactHub>
    {
        //Here is the once-per-class call to initialize the log object
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private InventoryContext _inventoryDB = new InventoryContext();

        [Authorize]
        [Route("")]
        public IHttpActionResult GetLots()
        {
            try
            {
                var lots = _inventoryDB.Inventory;
                return Ok(lots);
            }
            catch (Exception ex)
            {
                log.Error("Error Getting Lots : ", ex);
                return NotFound();
            }

        }

        [Authorize]
        [Route("Categories")]
        public IHttpActionResult GetMatCategories()
        {
            try
            {
                var categories = _inventoryDB.GetMatCategories();
                return Ok(categories);
            }
            catch (Exception ex)
            {
                log.Error("Error Getting Categories : ", ex);
                return NotFound();
            }

        }


        [Authorize]
        [Route("UpdateLot")]
        public IHttpActionResult UpdateLot(Lots _Lot)
        {
            try
            {

                _inventoryDB.UpdateLot(_Lot);
                return Ok();
            }
            catch (Exception ex)
            {
                log.Error("Error Editing Lot : ", ex);
                return NotFound();
            }
        }


        [Authorize]
        [Route("AddLot")]
        public IHttpActionResult AddLot(string _Lot)
        {
            int newID;
            try
            {
              // _inventoryDB.AddLot(_Lot);
                //var subscribed = Hub.Clients.Group("contact");
                //subscribed.addItem(_Contact);
                return Ok();
            }
            catch (Exception ex)
            {
                log.Error("Error Adding Lot : ", ex);
                return BadRequest();
            }
        }


        [Route("Locations")]
        public IHttpActionResult GetLocations()
        {
            try
            {
                var locations = _inventoryDB.Locations;
                return Ok(locations);
            }
            catch (Exception ex)
            {
                log.Error("Error Getting Lots : ", ex);
                return NotFound();
            }
        }

        [Route("MatTypes")]
        public IHttpActionResult GetMatTypes()
        {
            try
            {
                //Hardcoded brand Id of one, as there is currently only one for this demo
                var types = _inventoryDB.GetMatTypesByCategories(1);
                return Ok(types);
            }
            catch (Exception ex)
            {
                log.Error("Error Getting Material Types : ", ex);
                return NotFound();
            }
        }

      
    }
}