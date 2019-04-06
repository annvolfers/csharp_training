using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Collections.Generic;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class GroupCreationTests : AuthTestBase
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

        [Test, TestCaseSource("RandomGroupDataProvider")]
        public void GroupCreationTest(GroupDate group)
        {
            /*GroupDate group = new GroupDate("aaa");
            group.Header = "sss";
            group.Footer = "ddd";*/

            List<GroupDate> oldGroups = app.Groups.GetGroupList();

            app.Groups.Create(group);
            
            Assert.AreEqual(oldGroups.Count + 1, app.Groups.GetGroupCount());

            List<GroupDate> newGroups = app.Groups.GetGroupList();
            oldGroups.Add(group);
            oldGroups.Sort();
            newGroups.Sort();
            Assert.AreEqual(oldGroups, newGroups);
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
