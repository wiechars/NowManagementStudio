using NowManagementStudio.Helpers;
using NowManagementStudio.Models;
using NowManagementStudio.DAL;
using System.Web.Mvc;
using System.Linq;
using System.Net;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace NowManagementStudio.Controllers
{
    public class ContactController : JsonController
    {
        private ContactContext ContactsDB = new ContactContext();

        /// <summary>
        /// Returns a list of contacts in the system
        /// </summary>
        /// <returns></returns>
        public ActionResult GetContacts()
        {
            List<Contact> result = new List<Contact>();
            StoredProcedure sproc = new StoredProcedure();
            var contacts = ContactsDB.Contacts;
            return Json(contacts, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Add a contact to the system
        /// </summary>
        /// <param name="_Contact"></param>
        /// <returns></returns>
        public ActionResult AddContact(Contact _Contact)
        {
            try
            {
                //ContactsDB.Contacts.Add(_Contact);
                //ContactsDB.SaveChanges();
                ContactsDB.AddContact(_Contact);
                return new HttpStatusCodeResult(HttpStatusCode.Created);  // OK = 200
            }
            catch
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
        }

        /// <summary>
        /// Update the current contact
        /// </summary>
        /// <param name="_Contact"></param>
        /// <returns></returns>
        public ActionResult UpdateContact(Contact _Contact)
        {
            try
            {

                ContactsDB.EditContact(_Contact);
                return new HttpStatusCodeResult(HttpStatusCode.Accepted);
            }
            catch
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
        }

        /// <summary>
        /// Delete a contact form the system
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult DeleteContact(int id)
        {
            var _Contact = ContactsDB.Contacts.First(r => r.Id == id);
            try
            {
                ContactsDB.Contacts.Remove(_Contact);
                ContactsDB.SaveChanges();
                return new HttpStatusCodeResult(HttpStatusCode.OK);  // OK = 200
            }
            catch
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
        }
    }
}