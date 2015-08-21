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
                ContactSprocs sproc = new ContactSprocs();
                return sproc.GetContacts();
            }
        }

        public Contact GetContact(int id)
        {
            ContactSprocs sproc = new ContactSprocs();
            return sproc.GetContact(id);

        }

        public void EditContact(Contact contact)
        {
            ContactSprocs sproc = new ContactSprocs();
            sproc.EditContacts(contact.Id.ToString(), contact.Name, contact.Email, contact.Mobile);

        }

        public int AddContact(Contact contact)
        {
            ContactSprocs sproc = new ContactSprocs();
            return sproc.AddContact(contact.Name, contact.Email, contact.Mobile);

        }

    }
}