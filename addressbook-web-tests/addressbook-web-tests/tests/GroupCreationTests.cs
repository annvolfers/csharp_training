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
using System.Linq;

namespace WebAddressbookTests
{
    [TestFixture]
    public class GroupCreationTests : GroupTestBase
    {
        public static IEnumerable<GroupDate> RandomGroupDataProvider()
        {
            List<GroupDate> groups = new List<GroupDate>();
            for (int i = 0; i < 5; i++)
            {
                groups.Add(new GroupDate(GenerateRandomString(30))
                {
                    Header = GenerateRandomString(100),
                    Footer = GenerateRandomString(100)
                });
            }
            return groups;
        }

        public static IEnumerable<GroupDate> GroupDataFromCsvFile()
        {
            List<GroupDate> groups = new List<GroupDate>();
            string [] lines = File.ReadAllLines(@"groups.csv");
            foreach (string l in lines)
            {
                string[] parts = l.Split(',');
                groups.Add(new GroupDate(parts[0])
                {
                    Header = parts[1],
                    Footer = parts[2]
                });
            }
            return groups;
        }

        public static IEnumerable<GroupDate> GroupDataFromXmlFile()
        {
            List<GroupDate> groups = new List<GroupDate>();
            return (List<GroupDate>)
                new XmlSerializer(typeof(List<GroupDate>))
                    .Deserialize(new StreamReader(@"groups.xml"));
        }

        public static IEnumerable<GroupDate> GroupDataFromJsonFile()
        {
            return JsonConvert.DeserializeObject<List<GroupDate>>(
                File.ReadAllText(@"groups.json"));
        }

        public static IEnumerable<GroupDate> GroupDataFromExcelFile()
        {
            List<GroupDate> groups = new List<GroupDate>();
            Excel.Application app = new Excel.Application();
            app.Visible = true;
            Excel.Workbook wb = app.Workbooks.Open(Path.Combine(Directory.GetCurrentDirectory(), @"groups.xlxs"));
            Excel.Worksheet sheet = wb.Sheets[1];
            Excel.Range range = sheet.UsedRange;
            for (int i = 1; i <= range.Rows.Count; i++)
            {
                groups.Add(new GroupDate()
                {
                    Name = range.Cells[i, 1].Value,
                    Header = range.Cells[i, 2].Value,
                    Footer = range.Cells[i, 3].Value
                });
            }
            wb.Close();
            app.Visible = false;
            app.Quit();
            return groups;
        }

        [Test, TestCaseSource("GroupDataFromJsonFile")]
        public void GroupCreationTest(GroupDate group)
        {
            /*GroupDate group = new GroupDate("aaa");
            group.Header = "sss";
            group.Footer = "ddd";*/

            List<GroupDate> oldGroups = GroupDate.GetAll();

            app.Groups.Create(group);
            
            Assert.AreEqual(oldGroups.Count + 1, app.Groups.GetGroupCount());

            List<GroupDate> newGroups = GroupDate.GetAll();
            oldGroups.Add(group);
            oldGroups.Sort();
            newGroups.Sort();
            Assert.AreEqual(oldGroups, newGroups);
        }

        [Test]
        public void TestDBConnectivityGroups()
        {
            DateTime start = DateTime.Now;
            List<GroupDate> fromUi = app.Groups.GetGroupList();
            DateTime end = DateTime.Now;
            System.Console.Out.WriteLine(end.Subtract(start));

            start = DateTime.Now;
            List<GroupDate> fromDb = GroupDate.GetAll();
            end = DateTime.Now;
            System.Console.Out.WriteLine(end.Subtract(start));
        }

        /*[Test]
        public void EmptyGroupCreationTest()
        {
            GroupDate group = new GroupDate("");
            group.Header = "";
            group.Footer = "";

            List<GroupDate> oldGroups = app.Groups.GetGroupList();

            app.Groups.Create(group);

            Assert.AreEqual(oldGroups.Count + 1, app.Groups.GetGroupCount());

            List<GroupDate> newGroups = app.Groups.GetGroupList();
            oldGroups.Add(group);
            oldGroups.Sort();
            newGroups.Sort();
            Assert.AreEqual(oldGroups, newGroups);
        }*/

        /*[Test]
        public void BadNameGroupCreationTest()
        {
            GroupDate group = new GroupDate("a'a");
            group.Header = "";
            group.Footer = "";

            List<GroupDate> oldGroups = app.Groups.GetGroupList();

            app.Groups.Create(group);

            Assert.AreEqual(oldGroups.Count + 1, app.Groups.GetGroupCount());

            List<GroupDate> newGroups = app.Groups.GetGroupList();
            oldGroups.Add(group);
            oldGroups.Sort();
            newGroups.Sort();
            Assert.AreEqual(oldGroups, newGroups);
        }*/
    }
}
