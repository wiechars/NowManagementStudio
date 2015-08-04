using NowManagementStudio.Helpers;
using NowManagementStudio.Models;
using NowManagementStudio.DAL;
using System.Web.Mvc;
using System.Net;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;

namespace NowManagementStudio.Models
{
    public class ContactHub : Hub
    {
        private static ContactContext _db = new ContactContext();
        private static ConcurrentDictionary<string, int> _locks = new ConcurrentDictionary<string, int>();
        private static object _lock = new object();

        public override async Task OnConnected()
        {

            System.Collections.Generic.List<Contact> result = new List<Contact>();
            NowManagementStudio.DAL.StoredProcedure sproc = new StoredProcedure();
            var contacts = _db.Contacts;
            await Clients.Caller.all( contacts);
        }

        public override async Task OnReconnected()
        {
            // Refresh as other users could update data while we were offline
            await OnConnected();
        }



        public void Add(Contact value)
        {
            _db.EditContact(value);

            var contact = _db.Contacts;
            Clients.All.add(contact, System.Web.Mvc.JsonRequestBehavior.AllowGet);
        }

      
    }
}