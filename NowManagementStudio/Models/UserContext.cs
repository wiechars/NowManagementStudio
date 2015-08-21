using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using NowManagementStudio.DAL;
using NowManagementStudio;

namespace NowManagementStudio.Models
{
    public class UserContext : DbContext
    {
        public List<Contact> Contacts
        {
            get
            {
                ContactSprocs sproc = new ContactSprocs();
                return sproc.GetContacts();
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