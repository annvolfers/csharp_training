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
    public class LoginHelper : HelperBase
    {
        public LoginHelper(ApplicationManager manager) : base(manager)
        {
        }

        public void Login(AccountData account)
        {
            if (IsLoggedIn())
            {
                if (IsLoggedIn(account))
                {
                    return;
                }

                Logout();
            }
            Type(By.Name("username"), account.Name);
            driver.FindElement(By
                .CssSelector("input.width-40.pull-right.btn.btn-success.btn-inverse.bigger-110")).Click();
            Type(By.Name("password"), account.Password);
            driver.FindElement(By
                .CssSelector("input.width-40.pull-right.btn.btn-success.btn-inverse.bigger-110")).Click();
        }

        public void Logout()
        {
            if (IsLoggedIn())
            {
                driver.FindElement(By.ClassName("user-info")).Click();
                driver.FindElement(By
                    .CssSelector("ul.user-menu.dropdown-menu.dropdown-menu-right.dropdown-yellow.dropdown-caret.dropdown-close"))
                    .FindElements(By.TagName("li"))[3]
                    .FindElement(By.TagName("a"))
                    .Click();
            }
        }

        public bool IsLoggedIn()
        {
            return IsElementPresent(By.ClassName("user-info"));
        }

        public bool IsLoggedIn(AccountData account)
        {
            return IsLoggedIn()
                && GetLoggetUserName() == account.Name;
            /*&& driver.FindElement(By.Name("logout")).FindElement(By.TagName("b")).Text
                //== "(" + account.Username + ")";
                == System.String.Format("(${0})", account.Username);*/
        }

        public string GetLoggetUserName()
        {
            string text = driver.FindElement(By.ClassName("user-info")).FindElement(By.TagName("span")).Text;
            return text;
        }
    }
}
