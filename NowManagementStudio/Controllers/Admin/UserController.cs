using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using NowManagementStudio.Models;
using NowManagementStudio.Models.Security;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

[RoutePrefix("api/Users")]
public class UserController : ApiController
{
    private SecurityContext securityDB = new SecurityContext();
    private AuthRepository _repo = new AuthRepository();

    /// <summary>
    /// Register a new user in the system
    /// </summary>
    /// <param name="userModel"></param>
    /// <returns></returns>
    [Authorize(Roles = "Administrator")]
    [Route("Register")]
    public async Task<IHttpActionResult> Register(UserModel userModel)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        IdentityResult result = await _repo.RegisterUser(userModel);

        IHttpActionResult errorResult = GetErrorResult(result);

        if (errorResult != null)
        {
            return errorResult;
        }

        return Ok();
    }

    /// <summary>
    /// Edits a user in the system
    /// </summary>
    /// <param name="userModel"></param>
    /// <returns></returns>
    [Authorize(Roles = "Administrator")]
    [Route("EditUser")]
    public async Task<IHttpActionResult> EditUser(UserModel userModel)
    {
        IdentityResult result = await _repo.EditUser(userModel);

        IHttpActionResult errorResult = GetErrorResult(result);

        if (errorResult != null)
        {
            return errorResult;
        }

        return Ok();
    }

    /// <summary>
    /// Edit user role membership
    /// </summary>
    /// <param name="userID"></param>
    /// <param name="roles"></param>
    /// <returns></returns>
    [Authorize(Roles = "Administrator")]
    [Route("EditRoles/{userID}/{roles}")]
    public async Task<IHttpActionResult> EditUserRoles(string userID, List<Roles> roles)
    {

        IdentityResult result = await _repo.EditUserRoles(userID, roles);
        IHttpActionResult errorResult = GetErrorResult(result);

        if (errorResult != null)
        {
            return errorResult;
        }
        return Ok();
    }

    /// <summary>
    /// Retrive list of users in the system for admin screen
    /// </summary>
    /// <returns></returns>
    [Authorize(Roles = "Administrator")]
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
    [Authorize(Roles = "Administrator")]
    [Route("Roles")]
    public IHttpActionResult GetRoles()
    {
        return Ok(securityDB.Roles);
    }

    /// <summary>
    /// Given a user id, shows role membership
    /// </summary>
    /// <param name="userName"></param>
    /// <returns></returns>
    [Authorize(Roles = "Administrator")]
    [Route("RolesByUser/{userName}")]
    public IHttpActionResult GetRolesByUser(string userName)
    {
        List<Roles> result = new List<Roles>();
        result = securityDB.UserRoleMembership(securityDB.Roles, userName);
        return Ok(result);

    }


    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            _repo.Dispose();
        }

        base.Dispose(disposing);
    }

    private IHttpActionResult GetErrorResult(IdentityResult result)
    {
        if (result == null)
        {
            return InternalServerError();
        }

        if (!result.Succeeded)
        {
            if (result.Errors != null)
            {
                foreach (string error in result.Errors)
                {
                    ModelState.AddModelError("", error);
                }
            }

            if (ModelState.IsValid)
            {
                // No ModelState errors are available to send, so just return an empty BadRequest.
                return BadRequest();
            }

            return BadRequest(ModelState);
        }

        return null;
    }


}

