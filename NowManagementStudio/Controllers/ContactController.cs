using NowManagementStudio.Models;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace NowManagementStudio.Controllers
{
    [RoutePrefix("api/Contacts")]
    public class ContactController : ApiController
    {
        private ContactContext ContactsDB = new ContactContext();

        /// <summary>
        /// Returns a list of contacts in the system
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [Route("")]
        public IHttpActionResult GetContacts()
        {

            var contacts = ContactsDB.Contacts;
            return Ok(ContactsDB.Contacts);
            //return Json(contacts, JsonRequestBehavior.AllowGet);
        }

        ///// <summary>
        ///// Add a contact to the system
        ///// </summary>
        ///// <param name="_Contact"></param>
        ///// <returns></returns>
        //public ActionResult AddContact(Contact _Contact)
        //{
        //    try
        //    {
        //        //ContactsDB.Contacts.Add(_Contact);
        //        //ContactsDB.SaveChanges();
        //        ContactsDB.AddContact(_Contact);
        //        return new HttpStatusCodeResult(HttpStatusCode.Created);  // OK = 200
        //    }
        //    catch (Exception ex)

        //    {
        //        //TODO : Error handling
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //}

        ///// <summary>
        ///// Update the current contact
        ///// </summary>
        ///// <param name="_Contact"></param>
        ///// <returns></returns>
        //public ActionResult UpdateContact(Contact _Contact)
        //{
        //    try
        //    {

        //        ContactsDB.EditContact(_Contact);
        //        return new HttpStatusCodeResult(HttpStatusCode.Accepted);
        //    }
        //    catch
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //}

        ///// <summary>
        ///// Delete a contact form the system
        ///// </summary>
        ///// <param name="id"></param>
        ///// <returns></returns>
        //public ActionResult DeleteContact(int id)
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
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //}
    }
}