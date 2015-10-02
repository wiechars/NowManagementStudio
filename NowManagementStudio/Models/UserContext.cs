using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using NowManagementStudio.DAL;
using NowManagementStudio;

namespace NowManagementStudio.Models
{
    public class UserContext : DbContext
    {
        public List<UserModel> Users
        {
            get
            {
                UserSprocs sproc = new UserSprocs();
                return sproc.GetUsers();
            }
        }

        public void EditContact(Contact contact)
        {
            //StoredProcedure sproc = new StoredProcedure();
            //sproc.EditContacts(contact.Id, contact.Name, contact.Email, contact.Mobile);

        }

        public void AddContact(Contact contact)
        {
            //StoredProcedure sproc = new StoredProcedure();
            //sproc.AddContact(contact.Name, contact.Email, contact.Mobile);

        }

    }
}