using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactInformationTests : AuthTestBase
    {
        [Test]
        public void ContactInformationTest()
        {
            ContactDate fromTable = app.Contact.GetContactInformationFromTable(0);
            ContactDate fromForm = app.Contact.GetContactInformationFromEditForm(0);

            //verification
            Assert.AreEqual(fromTable, fromForm);
            Assert.AreEqual(fromTable.Address, fromForm.Address);
            Assert.AreEqual(fromTable.AllPhones, fromForm.AllPhones);
            Assert.AreEqual(fromTable.AllEmails, fromForm.AllEmails);
        }

        [Test]
        public void ConatctInformationEditAndViewTest()
        {
            string contactFromForm = app.Contact.GetTextContactInformationFromEditForm(0);
            string contactFromView = app.Contact.GetTextContactInformationFromViewForm(0);

            Assert.AreEqual(contactFromForm, contactFromView);
        }
    }
}
