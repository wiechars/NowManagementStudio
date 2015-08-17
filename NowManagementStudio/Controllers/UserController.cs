using NowManagementStudio.Models;
using System;
using System.Collections.Generic;
using System.Web.Http;

[RoutePrefix("api/Users")]
public class UserController : ApiController
{
    private ContactContext ContactsDB = new ContactContext();

    /// <summary>
    /// Retrive list of users in the system for admin screen
    /// </summary>
    /// <returns></returns>
    [Authorize]
    [Route("")]
    public IHttpActionResult Get()
    {
        List<Contact> result = new List<Contact>();
        return Ok(ContactsDB.Contacts);
    }

}

