using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using NowManagementStudio.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

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
            UserName = userModel.UserName
        };

        var result = await _userManager.CreateAsync(user, userModel.Password);
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
    public IList<string> GetUserRoles(IdentityUser user)
    {
        IList<string> roles = new List<string>();

        try
        {
             roles = _userManager.GetRoles(user.Id);
        }
        catch (Exception ex)
        {
            log.Error("Error fetching user roles : " + ex);
        }

        return roles;
    }


    public void Dispose()
    {
        _ctx.Dispose();
        _userManager.Dispose();

    }
}