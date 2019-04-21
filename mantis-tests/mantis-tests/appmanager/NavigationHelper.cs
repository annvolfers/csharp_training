using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace mantis_tests
{
    public class NavigationHelper : HelperBase
    {
        private string baseURL;

        public NavigationHelper(ApplicationManager manager, string baseURL) : base(manager)
        {
            this.baseURL = baseURL;
        }

        public void GoToManageProjectPage()
        {
            if (driver.Url == baseURL + "manage_proj_page.php")
            {
                return;
            }
            int count = driver.FindElement(By.CssSelector("ul.nav.nav-list"))
                .FindElements(By.TagName("li")).Count;
            driver.FindElement(By.CssSelector("ul.nav.nav-list"))
                .FindElements(By.TagName("li"))[count-1].Click();
            driver.FindElement(By.CssSelector("ul.nav.nav-tabs.padding-18"))
                .FindElements(By.TagName("li"))[2].Click();
        }
    }
}
