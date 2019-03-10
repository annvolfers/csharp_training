using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactCreationTests : TestBase
    {
        [Test]
        public void ContactCreationTest()
        {
            ContactDate contact = new ContactDate("qqq", "www");

            app.Contact.Create(contact);
        }

        [Test]
        public void EmptyContactCreationTest()
        {
            ContactDate contact = new ContactDate("", "");

            app.Contact.Create(contact);
        }
    }
}
