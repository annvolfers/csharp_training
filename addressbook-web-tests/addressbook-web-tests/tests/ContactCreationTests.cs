using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using Newtonsoft.Json;
using Excel = Microsoft.Office.Interop.Excel;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactCreationTests : ContactTestBase
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

        public static IEnumerable<ContactDate> ContactDataFromCsvFile()
        {
            List<ContactDate> contacts = new List<ContactDate>();
            string[] lines = File.ReadAllLines(@"contacts.csv");
            foreach (string l in lines)
            {
                string[] parts = l.Split(',');
                contacts.Add(new ContactDate(parts[0], parts[1]));
            }
            return contacts;
        }

        public static IEnumerable<ContactDate> ContactDataFromXmlFile()
        {
            List<ContactDate> contacts = new List<ContactDate>();
            return (List<ContactDate>)
                new XmlSerializer(typeof(List<ContactDate>))
                    .Deserialize(new StreamReader(@"contacts.xml"));
        }

        public static IEnumerable<ContactDate> ContactDataFromJsonFile()
        {
            return JsonConvert.DeserializeObject<List<ContactDate>>(
                File.ReadAllText(@"contacts.json"));
        }

        public static IEnumerable<ContactDate> ContactDataFromExcelFile()
        {
            List<ContactDate> contacts = new List<ContactDate>();
            Excel.Application app = new Excel.Application();
            app.Visible = true;
            Excel.Workbook wb = app.Workbooks.Open(Path.Combine(Directory.GetCurrentDirectory(), @"contacts.xlxs"));
            Excel.Worksheet sheet = wb.Sheets[1];
            Excel.Range range = sheet.UsedRange;
            for (int i = 1; i <= range.Rows.Count; i++)
            {
                contacts.Add(new ContactDate()
                {
                    First_name = range.Cells[i, 1].Value,
                    Last_name = range.Cells[i, 2].Value
                });
            }
            wb.Close();
            app.Visible = false;
            app.Quit();
            return contacts;
        }

        [Test, TestCaseSource("ContactDataFromJsonFile")]
        public void ContactCreationTest(ContactDate contact)
        {
            //ContactDate contact = new ContactDate("qqq", "www");

            //List<ContactDate> oldContacts = app.Contact.GetContactList();
            List<ContactDate> oldContacts = ContactDate.GetAll();

            app.Contact.Create(contact);

            Assert.AreEqual(oldContacts.Count + 1, app.Contact.GetContactCount());

            //List<ContactDate> newContacts = app.Contact.GetContactList();
            List<ContactDate> newContacts = ContactDate.GetAll();
            oldContacts.Add(contact);
            oldContacts.Sort();
            newContacts.Sort();
            Assert.AreEqual(oldContacts, newContacts);
        }

        [Test]
        public void TestDBConnectivityContacts()
        {
            DateTime start = DateTime.Now;
            List<ContactDate> fromUi = app.Contact.GetContactList();
            DateTime end = DateTime.Now;
            System.Console.Out.WriteLine(end.Subtract(start));

            start = DateTime.Now;
            List<ContactDate> fromDb = ContactDate.GetAll();
            end = DateTime.Now;
            System.Console.Out.WriteLine(end.Subtract(start));
        }

        [Test]
        public void TestDBConnectivity()
        {
            foreach (ContactDate contact in GroupDate.GetAll()[1].GetContacts())
            {
                System.Console.Out.WriteLine(contact);
            }
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
