using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace NowManagementStudio.Hubs
{
    [HubName("contactHub")]
    public class ContactHub : Hub
    {
        public void Subscribe(string contactGroup)
        {
            Groups.Add(Context.ConnectionId, contactGroup);
        }

        public void Unsubscribe(string contactID)
        {
            Groups.Remove(Context.ConnectionId, contactID);
        }
    }
}