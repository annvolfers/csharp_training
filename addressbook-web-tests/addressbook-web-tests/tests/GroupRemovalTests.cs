using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Collections.Generic;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class GroupRemovalTests : AuthTestBase
    {
        [Test]
        public void GroupRemovalTest()
        {
            List<GroupDate> oldGroups = app.Groups.GetGroupList();
            if (oldGroups.Count == 0)
            {
                GroupDate group = new GroupDate("aaa");
                group.Header = "sss";
                group.Footer = "ddd";
                app.Groups.Create(group);
                oldGroups = app.Groups.GetGroupList();
            }

            app.Groups.Remove(0);

            List<GroupDate> newGroups = app.Groups.GetGroupList();

            oldGroups.RemoveAt(0);
            Assert.AreEqual(oldGroups, newGroups);
        }
    }
}
