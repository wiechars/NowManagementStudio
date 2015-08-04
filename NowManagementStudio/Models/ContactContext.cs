using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using NowManagementStudio.DAL;

namespace NowManagementStudio.Models
{
    public class ContactContext : DbContext
    {
        public List<Contact> Contacts
        {
            get
            {
                StoredProcedure sproc = new StoredProcedure();
                return sproc.GetContacts();
            }
        }

        public void EditContact(Contact contact)
        {
            StoredProcedure sproc = new StoredProcedure();
            sproc.EditContacts(contact.Id, contact.Name, contact.Email, contact.Mobile);

        }

        public void AddContact(Contact contact)
        {
            StoredProcedure sproc = new StoredProcedure();
            sproc.AddContact(contact.Name, contact.Email, contact.Mobile);

        }

    }
}