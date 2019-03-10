using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace WebAddressbookTests
{
    public class ContactHelper : HelperBase
    {
        public ContactHelper(ApplicationManager manager) : base(manager)
        {
        }

        public ContactHelper Create(ContactDate contact)
        {
            manager.Navigator.GoToGroupsPage();

            InitContactCreation();
            FillContactForm(contact);
            SubmitContactCreation();
            manager.Navigator.ReturnToHomePage();
            return this;
        }

        public ContactHelper Modify(string v, ContactDate newData)
        {
            manager.Navigator.GoToHomePage();
            GoToPageContactModification(v);
            FillContactForm(newData);
            SubmitContactModification();
            manager.Navigator.ReturnToHomePage();
            return this;
        }

        public ContactHelper Remove(int v)
        {
            manager.Navigator.GoToHomePage();
            SelectContactRemove(v);
            SubmitContactRemove();
            manager.Navigator.ReturnToHomePage();
            return this;
        }

        public ContactHelper InitContactCreation()
        {
            driver.FindElement(By.LinkText("add new")).Click();
            return this;
        }

        public ContactHelper FillContactForm(ContactDate contact)
        {
            driver.FindElement(By.Name("firstname")).Clear();
            driver.FindElement(By.Name("firstname")).SendKeys(contact.First_name);
            driver.FindElement(By.Name("lastname")).Clear();
            driver.FindElement(By.Name("lastname")).SendKeys(contact.Last_name);
            return this;
        }

        public ContactHelper SubmitContactCreation()
        {
            driver.FindElement(By.Name("submit")).Click();
            return this;
        }

        private ContactHelper SubmitContactModification()
        {
            driver.FindElement(By.Name("update")).Click();
            return this;
        }

        private ContactHelper GoToPageContactModification(string v)
        {
            string contacteditURL = "http://localhost/addressbook/edit.php?id=" + v;
            driver.Navigate().GoToUrl(contacteditURL);
            return this;
        }

        private ContactHelper SubmitContactRemove()
        {
            driver.FindElement(By.XPath("(.//*[normalize-space(text()) and normalize-space(.)='Select all'])[1]/following::input[2]")).Click();
            driver.SwitchTo().Alert().Accept();
            return this;
        }

        private ContactHelper SelectContactRemove(int v)
        {
            int i = 1;
            while (i == 1)
            {
                try
                {
                    string x = Convert.ToString(v);
                    driver.FindElement(By.Id(x)).Click();
                    i++;
                }
                catch
                {
                    v++;
                }
            }
            return this;
        }
    }
}
