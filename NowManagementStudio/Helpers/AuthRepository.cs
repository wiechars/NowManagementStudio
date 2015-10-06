using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using NowManagementStudio.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using NowManagementStudio.Models.Security;

public class AuthRepository : IDisposable
{
    private AuthContext _ctx;

    private UserManager<IdentityUser> _userManager;
    private RoleManager<IdentityRole> _roleManager;

    //Here is the once-per-class call to initialize the log object
    private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    public AuthRepository()
    {
        _ctx = new AuthContext();
        _userManager = new UserManager<IdentityUser>(new UserStore<IdentityUser>(_ctx));
        _roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(_ctx));
    }

    /// <summary>
    /// Creates a new user in the system.
    /// </summary>
    /// <param name="userModel"></param>
    /// <returns></returns>
    public async Task<IdentityResult> RegisterUser(UserModel userModel)
    {
        IdentityUser user = new IdentityUser
        {
            UserName = userModel.UserName,
            Email = userModel.Email,
            PhoneNumber = userModel.PhoneNumber,
        };

        var result = await _userManager.CreateAsync(user, userModel.Password);
        return result;
    }

    /// <summary>
    /// Update the Identity user details
    /// </summary>
    /// <param name="userModel"></param>
    /// <returns></returns>
    public async Task<IdentityResult> EditUser(UserModel userModel)
    {
        var user = _userManager.FindById(userModel.Id);
        if (!string.IsNullOrEmpty(userModel.Password))
        {
            user.PasswordHash = _userManager.PasswordHasher.HashPassword(userModel.Password);
        }
        user.UserName = userModel.UserName;
        user.Email = userModel.Email;
        user.PhoneNumber = userModel.PhoneNumber;

        var result = await _userManager.UpdateAsync(user);
        return result;
    }

    /// <summary>
    /// Checks if logged in user exists in the system, and returns the found user
    /// </summary>
    /// <param name="userName"></param>
    /// <param name="password"></param>
    /// <returns></returns>
    public async Task<IdentityUser> FindUser(string userName, string password)
    {
        IdentityUser user = await _userManager.FindAsync(userName, password);
        return user;
    }


    /// <summary>
    /// Get List of User Roles
    /// </summary>
    /// <param name="userID"></param>
    /// <returns></returns>
    public IList<string> GetUserRoles(string userName)
    {

        IList<string> roles = new List<string>();

        try
        {
            IdentityUser user = _userManager.FindByName(userName);
            roles = _userManager.GetRoles(user.Id);
        }
        catch (Exception ex)
        {
            log.Error("Error fetching user roles : " + ex);
        }

        return roles;
    }


    /// <summary>
    /// Ret
    /// </summary>
    /// <param name="userName"></param>
    /// <returns></returns>
    public List<Roles> GetUserRoleMembership(List<Roles> roles, string userName)
    {
        List<Roles> result = new List<Roles>();
        try
        {
            IdentityUser user = _userManager.FindByName(userName);
            foreach (var role in roles)
            {
                if (_userManager.IsInRole(user.Id, role.Name))
                {
                    role.isMember = true;
                }
                result.Add(role);
            }
        }
        catch (Exception ex)
        {
            log.Error("Error fetching user role membership : " + ex);
        }

        return result;
    }

    /// <summary>
    /// Edits the identity users role membership
    /// </summary>
    /// <param name="userID"></param>
    /// <param name="roles"></param>
    /// <returns></returns>
    public async Task<IdentityResult> EditUserRoles(string userID, List<Roles> roles)
    {
        IdentityResult result = IdentityResult.Success;
        try
        {
            foreach (Roles role in roles)
            {
                if (role.isMember)
                {
                    if (!_userManager.IsInRole(userID, role.Name))
                    {
                        result = await _userManager.AddToRoleAsync(userID, role.Name);
                    }
                }
                else
                {
                    if (_userManager.IsInRole(userID, role.Name))
                    {
                        result = await _userManager.RemoveFromRoleAsync(userID, role.Name);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            log.Error("Error editing user role membership : " + ex);
            return result;
        }

        return result;
    }

    public void Dispose()
    {
        _ctx.Dispose();
        _userManager.Dispose();

    }
}