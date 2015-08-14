using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using NowManagementStudio.Helpers;


namespace NowManagementStudio.Models
{
    [DbConfigurationType(typeof(MySql.Data.Entity.MySqlEFConfiguration))]
    public class AuthContext : IdentityDbContext<IdentityUser>
    {
        public AuthContext()
            : base("AuthContext", throwIfV1Schema: false)
        {
            Database.SetInitializer(new MySqlInitializer());
        }
    }
}