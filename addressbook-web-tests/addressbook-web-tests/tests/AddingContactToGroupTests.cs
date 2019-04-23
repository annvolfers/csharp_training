using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    public class AddingContactToGroupTests : AuthTestBase
    {
        [Test]
        public void AddingContactToGroupTest()
        {
            GroupDate group = new GroupDate();
            ContactDate contact = new ContactDate();
            
            if (!app.Groups.IsGroupPresent())
            {
                group = new GroupDate("aaa");
                group.Header = "sss";
                group.Footer = "ddd";
                app.Groups.Create(group);
            }

            group = GroupDate.GetAll()[0];

            if (!app.Contact.IsContactPresent() || group.GetContactsNotInGroups().Count == 0) 
            {
                contact = new ContactDate("qqq", "www");
                app.Contact.Create(contact);
            }
            
            List<ContactDate> oldList = group.GetContacts();

            contact = ContactDate.GetAll().Except(oldList).First();

            app.Contact.AddContactToGroup(contact, group);

            List<ContactDate> newList = group.GetContacts();
            oldList.Add(contact);
            newList.Sort();
            oldList.Sort();

            Assert.AreEqual(oldList, newList);
        }
    }
}
