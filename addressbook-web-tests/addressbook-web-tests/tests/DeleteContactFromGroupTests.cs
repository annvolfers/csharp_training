using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    public class DeleteContactFromGroupTests : AuthTestBase
    {
        [Test]
        public void DeleteContactFromGroupTest()
        {
            GroupDate group = GroupDate.GetAll()[0];
            List<ContactDate> oldList = group.GetContacts();
            ContactDate contact = ContactDate.GetAll().First();
            if (oldList.Count == 0)
            {
                app.Contact.AddContactToGroup(contact, group);
                oldList = group.GetContacts();
            }
            else
            {
                contact = group.GetContacts().First();
            }
            
            app.Contact.DeleteContactFromGroup(contact, group.Id);

            List<ContactDate> newList = group.GetContacts();
            oldList.RemoveAt(0);
            newList.Sort();
            oldList.Sort();

            Assert.AreEqual(oldList, newList);
        }
    }
}
