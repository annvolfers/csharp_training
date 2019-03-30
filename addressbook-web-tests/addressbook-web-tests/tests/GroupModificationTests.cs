using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class GroupModificationTests : AuthTestBase
    {
        [Test]
        public void GroupModificationTest()
        {
            GroupDate newData = new GroupDate("zzz");
            newData.Header = null;
            newData.Footer = null;
            
            List<GroupDate> oldGroups = app.Groups.GetGroupList();
            if (oldGroups.Count == 0)
            {
                GroupDate group = new GroupDate("aaa");
                group.Header = "sss";
                group.Footer = "ddd";
                app.Groups.Create(group);
                oldGroups = app.Groups.GetGroupList();
            }

            app.Groups.Modify(0, newData);

            List<GroupDate> newGroups = app.Groups.GetGroupList();
            oldGroups[0].Name = newData.Name;
            oldGroups.Sort();
            newGroups.Sort();
            Assert.AreEqual(oldGroups, newGroups);
        }
    }
}
