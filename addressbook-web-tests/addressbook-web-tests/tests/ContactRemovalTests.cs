﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactRemovalTests : GroupTestBase
    {
        [Test]
        public void ContactRemovalTest()
        {
            //List<ContactDate> oldContacts = app.Contact.GetContactList();
            List<ContactDate> oldContacts = ContactDate.GetAll();
            if (!app.Contact.IsContactPresent())
            {
                ContactDate contact = new ContactDate("qqq", "www");
                app.Contact.Create(contact);
                oldContacts = app.Contact.GetContactList();
            }
            ContactDate toBeRemoved = oldContacts[0];

            app.Contact.Remove(toBeRemoved);

            Assert.AreEqual(oldContacts.Count - 1, app.Contact.GetContactCount());
            

            //List<ContactDate> newContacts = app.Contact.GetContactList();
            List<ContactDate> newContacts = ContactDate.GetAll();

            oldContacts.RemoveAt(0);
            Assert.AreEqual(oldContacts, newContacts);

            foreach (ContactDate contact in oldContacts)
            {
                Assert.AreNotEqual(contact.Id, toBeRemoved.Id);
            }
        }
    }
}
