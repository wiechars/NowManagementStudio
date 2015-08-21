using NowManagementStudio.Hubs;
using NowManagementStudio.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Http;

namespace NowManagementStudio.Controllers
{
    [RoutePrefix("api/Contacts")]
    public class ContactController : ApiControllerWithHub<ContactHub>
    {
        //Here is the once-per-class call to initialize the log object
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private ContactContext ContactsDB = new ContactContext();

        /// <summary>
        /// Returns a list of contacts in the system
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [Route("")]
        public IHttpActionResult GetContacts()
        {
            try
            {
                var contacts = ContactsDB.Contacts;
                var subscribed = Hub.Clients.Group("Contact");
                subscribed.getContacts(ContactsDB.Contacts);
                return Ok(ContactsDB.Contacts);
            }
            catch (Exception ex)
            {
                log.Error("Error Getting Contacts : ", ex);
                return NotFound();
            }

        }

        /// <summary>
        /// Add a contact to the system
        /// </summary>
        /// <param name="_Contact"></param>
        /// <returns></returns>
        [Authorize]
        [Route("AddContact")]
        public IHttpActionResult AddContact(Contact _Contact)
        {
            int newID;
            try
            {
                _Contact.Id = ContactsDB.AddContact(_Contact); 
                var subscribed = Hub.Clients.Group("contact");
                subscribed.addItem(_Contact);
                return Ok();
            }
            catch (Exception ex)
            {
                log.Error("Error Adding Contact : ", ex);
                return BadRequest();
             }
        }

        /// <summary>
        /// Update the current contact
        /// </summary>
        /// <param name="_Contact"></param>
        /// <returns></returns>
        [Authorize]
        [Route("UpdateContact")]
        public IHttpActionResult UpdateContact(Contact _Contact)
        {
            try
            {

                ContactsDB.EditContact(_Contact);
                var subscribed = Hub.Clients.Group("contact");
                subscribed.updateItem(_Contact);
                return Ok();
            }
            catch (Exception ex)
            {
                log.Error("Error Editing Contact : ", ex);
                return NotFound();
            }
        }

        /// <summary>
        /// Delete a contact form the system
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize]
        [Route("DeleteContact")]
        public IHttpActionResult DeleteContact(int id)
        {
        //{
        //    var _Contact = ContactsDB.Contacts.First(r => r.Id == id);
        //    try
        //    {
        //        ContactsDB.Contacts.Remove(_Contact);
        //        ContactsDB.SaveChanges();
        //        return new HttpStatusCodeResult(HttpStatusCode.OK);  // OK = 200
        //    }
        //    catch
        //    {
            return NotFound();
        //    }
        }
    }
}