using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using NowManagementStudio.Models;
using System.Threading.Tasks;
using System.Web;
using System.IO;
using System.Net.Http.Headers;
using NowManagementStudio.Controllers;

namespace NowManagementStudio.Controllers
{
    [RoutePrefix("api/Reports")]
    public class ReportController : ApiController
    {
        //Here is the once-per-class call to initialize the log object
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        // GET api/<controller>
        [HttpGet]
        [Route("ContactsReport")]
        public async Task<HttpResponseMessage> GetContactsReportPDF()
        {
            try
            {
                string fileName = string.Concat("Contacts.pdf");
                string filePath = HttpContext.Current.Server.MapPath("~/Reports/" + fileName);

                ContactController contact = new ContactController();
                ContactContext ContactsDB = new ContactContext();
                List<Contact> contactList = new List<Contact>();
                contactList = ContactsDB.Contacts;

                await NowManagementStudio.Report.ReportGenerator.GeneratePDF(contactList, filePath);

                HttpResponseMessage result = null;
                result = Request.CreateResponse(HttpStatusCode.OK);
                result.Content = new StreamContent(new FileStream(filePath, FileMode.Open));
                result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
                result.Content.Headers.ContentDisposition.FileName = fileName;
                return result;
            }
            catch (Exception ex)
            {
                log.Error("Error Generating Contact Report PDF : " + ex);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        /// <summary>
        /// Inventory Report
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("InventoryReport")]
        public async Task<HttpResponseMessage> GetInventoryReportPDF()
        {
            try
            {
                string fileName = string.Concat("InventoryReport.pdf");
                string filePath = HttpContext.Current.Server.MapPath("~/Reports/" + fileName);

                ContactController contact = new ContactController();
                ContactContext ContactsDB = new ContactContext();
                List<Contact> contactList = new List<Contact>();
                contactList = ContactsDB.Contacts;

                await NowManagementStudio.Report.ReportGenerator.GeneratePDF(contactList, filePath);

                HttpResponseMessage result = null;
                result = Request.CreateResponse(HttpStatusCode.OK);
                result.Content = new StreamContent(new FileStream(filePath, FileMode.Open));
                result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
                result.Content.Headers.ContentDisposition.FileName = fileName;
                return result;
            }
            catch (Exception ex)
            {
                log.Error("Error Generating Contact Report PDF : " + ex);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }
    }
}