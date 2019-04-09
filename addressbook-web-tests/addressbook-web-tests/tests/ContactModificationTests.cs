using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactModificationTests : ContactTestBase
    {
        [Test]
        public void ContactModificationTest()
        {
            ContactDate newData = new ContactDate("eee", "rrr");
            
            List<ContactDate> oldContacts = new List<ContactDate>();
            if (!app.Contact.IsContactPresent())
            {
                ContactDate contact = new ContactDate("qqq", "www");
                app.Contact.Create(contact);
            }
            oldContacts = ContactDate.GetAll();
            ContactDate oldData = oldContacts[0];
            
            app.Contact.Modify(oldData, newData);

            Assert.AreEqual(oldContacts.Count, app.Contact.GetContactCount());
            
            List<ContactDate> newContacts = ContactDate.GetAll();
            oldContacts[0].First_name = newData.First_name;
            oldContacts[0].Last_name = newData.Last_name;
            oldContacts.Sort();
            newContacts.Sort();
            Assert.AreEqual(oldContacts, newContacts);

            foreach (ContactDate contact in newContacts)
            {
                if (contact.Id == oldData.Id)
                {
                    Assert.AreEqual(newData.First_name, contact.First_name);
                    Assert.AreEqual(newData.Last_name, contact.Last_name);
                }
            }
        }
    }
}
