﻿using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Collections.Generic;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactCreationTests : AuthTestBase
    {
        public static IEnumerable<ContactDate> RandomContactDataProvider()
        {
            List<ContactDate> contacts = new List<ContactDate>();
            for (int i = 0; i < 5; i++)
            {
                contacts.Add(new ContactDate(GenerateRandomString(20), GenerateRandomString(20)));
            }
            return contacts;
        }

        [Test, TestCaseSource("RandomContactDataProvider")]
        public void ContactCreationTest(ContactDate contact)
        {
            //ContactDate contact = new ContactDate("qqq", "www");

            List<ContactDate> oldContacts = app.Contact.GetContactList();

            app.Contact.Create(contact);

            Assert.AreEqual(oldContacts.Count + 1, app.Contact.GetContactCount());

            List<ContactDate> newContacts = app.Contact.GetContactList();
            oldContacts.Add(contact);
            oldContacts.Sort();
            newContacts.Sort();
            Assert.AreEqual(oldContacts, newContacts);
        }

        /*[Test]
        public void EmptyContactCreationTest()
        {
            ContactDate contact = new ContactDate("", "");

            List<ContactDate> oldContacts = app.Contact.GetContactList();

            app.Contact.Create(contact);

            Assert.AreEqual(oldContacts.Count + 1, app.Contact.GetContactCount());

            List<ContactDate> newContacts = app.Contact.GetContactList();
            oldContacts.Add(contact);
            oldContacts.Sort();
            newContacts.Sort();
            Assert.AreEqual(oldContacts, newContacts);
        }*/

        /*[Test]
        public void BadNameContactCreationTest()
        {
            ContactDate contact = new ContactDate("a'a", "www");

            List<ContactDate> oldContacts = app.Contact.GetContactList();

            app.Contact.Create(contact);

            Assert.AreEqual(oldContacts.Count + 1, app.Contact.GetContactCount());

            List<ContactDate> newContacts = app.Contact.GetContactList();
            oldContacts.Add(contact);
            oldContacts.Sort();
            newContacts.Sort();
            Assert.AreEqual(oldContacts, newContacts);
        }*/
    }
}
