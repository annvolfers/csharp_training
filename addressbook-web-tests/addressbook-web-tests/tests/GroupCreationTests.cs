using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class GroupCreationTests : AuthTestBase
    {
        [Test]
        public void GroupCreationTest()
        {
            GroupDate group = new GroupDate("aaa");
            group.Header = "sss";
            group.Footer = "ddd";
            
            app.Groups.Create(group);
        }

        [Test]
        public void EmptyGroupCreationTest()
        {
            GroupDate group = new GroupDate("");
            group.Header = "";
            group.Footer = "";
            
            app.Groups.Create(group);
        }
    }
}
