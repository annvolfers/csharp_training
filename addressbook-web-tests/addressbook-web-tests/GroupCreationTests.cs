using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class GroupCreationTests : TestBase
    {
        [Test]
        public void GroupCreationTest()
        {
            GoToHomePage();
            Login(new AccountDate("admin", "secret"));
            GoToGroupsPage();
            InitGroupCreation();
            GroupDate group = new GroupDate("aaa");
            group.Header = "sss";
            group.Footer = "ddd";
            FillGroupForm(group);
            SubmitGroupCreation();
            ReturnToGroupsPage();
        }
    }
}
