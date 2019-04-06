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
            if (!app.Groups.IsGroupPresent())
            {
                GroupDate group = new GroupDate("aaa");
                group.Header = "sss";
                group.Footer = "ddd";
                app.Groups.Create(group);
                oldGroups = app.Groups.GetGroupList();
            }
            GroupDate oldData = oldGroups[0];

            app.Groups.Modify(0, newData);

            Assert.AreEqual(oldGroups.Count, app.Groups.GetGroupCount());

            List<GroupDate> newGroups = app.Groups.GetGroupList();
            oldGroups[0].Name = newData.Name;
            oldGroups.Sort();
            newGroups.Sort();
            Assert.AreEqual(oldGroups, newGroups);

            foreach (GroupDate group in newGroups)
            {
                if (group.Id == oldData.Id)
                {
                    Assert.AreEqual(newData.Name, group.Name);
                }
            }
        }
    }
}
