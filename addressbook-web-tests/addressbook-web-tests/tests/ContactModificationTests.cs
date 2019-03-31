using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactModificationTests : AuthTestBase
    {
        [Test]
        public void ContactModificationTest()
        {
            ContactDate newData = new ContactDate("eee", "rrr");

            List<ContactDate> oldContacts = app.Contact.GetContactList();

            app.Contact.Modify(0, newData);

            List<ContactDate> newContacts = app.Contact.GetContactList();
            oldContacts[0].First_name = newData.First_name;
            oldContacts[0].Last_name = newData.Last_name;
            oldContacts.Sort();
            newContacts.Sort();
            Assert.AreEqual(oldContacts, newContacts);
        }
    }
}
