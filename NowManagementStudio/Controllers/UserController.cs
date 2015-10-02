using NowManagementStudio.Models;
using System;
using System.Collections.Generic;
using System.Web.Http;

[RoutePrefix("api/Users")]
public class UserController : ApiController
{
    private UserContext userDB = new UserContext();

    /// <summary>
    /// Retrive list of users in the system for admin screen
    /// </summary>
    /// <returns></returns>
    [Authorize]
    [Route("")]
    public IHttpActionResult Get()
    {
        List<UserModel> result = new List<UserModel>();
        return Ok(userDB.Users);
    }

}

