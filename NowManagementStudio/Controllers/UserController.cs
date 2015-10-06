using Microsoft.AspNet.Identity.EntityFramework;
using NowManagementStudio.Models;
using NowManagementStudio.Models.Security;
using System;
using System.Collections.Generic;
using System.Web.Http;

[RoutePrefix("api/Users")]
public class UserController : ApiController
{
    private SecurityContext securityDB = new SecurityContext();

    /// <summary>
    /// Retrive list of users in the system for admin screen
    /// </summary>
    /// <returns></returns>
    [Authorize]
    [Route("")]
    public IHttpActionResult GetUsers()
    {
        List<UserModel> result = new List<UserModel>();
        return Ok(securityDB.Users);
    }

    /// <summary>
    /// Returns all Security Roles in the system
    /// </summary>
    /// <returns></returns>
    [Authorize]
    [Route("Roles")]
    public IHttpActionResult GetRoles()
    {
        return Ok(securityDB.Roles);
    }

    [Route("RolesByUser/{userName}")]
    public IHttpActionResult GetRolesByUser(string userName)
    {
        List<Roles> result = new List<Roles>();
        result = securityDB.UserRoleMembership(securityDB.Roles, userName);
        return Ok(result);

    }


}

