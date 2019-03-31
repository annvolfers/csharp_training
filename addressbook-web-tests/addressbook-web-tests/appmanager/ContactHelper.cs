using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
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

        public ContactHelper Modify(int v, ContactDate newData)
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
            Type(By.Name("firstname"), contact.First_name);
            Type(By.Name("lastname"), contact.Last_name);
            return this;
        }

        public ContactHelper SubmitContactCreation()
        {
            driver.FindElement(By.Name("submit")).Click();
            contactCache = null;
            return this;
        }

        public ContactHelper SubmitContactModification()
        {
            driver.FindElement(By.Name("update")).Click();
            contactCache = null;
            return this;
        }

        public ContactHelper GoToPageContactModification(int index)
        {
            driver.FindElement(By.XPath("(//img[@alt='Edit'])[" + (index+1) + "]")).Click();
            return this;
        }

        public ContactHelper SubmitContactRemove()
        {
            driver.FindElement(By.XPath("//input[@value='Delete']")).Click();
            driver.SwitchTo().Alert().Accept();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            IWebElement element = driver.FindElement(By.CssSelector("div.msgbox"));
            contactCache = null;
            return this;
        }

        public ContactHelper SelectContactRemove(int index)
        {
            driver.FindElement(By.XPath("(//input[@name='selected[]'])[" + (index+1) + "]")).Click();
            return this;
        }

        private List<ContactDate> contactCache = null;

        public List<ContactDate> GetContactList()
        {
            if (contactCache == null)
            {
                contactCache = new List<ContactDate>();
                manager.Navigator.GoToHomePage();
                ICollection<IWebElement> elements = driver.FindElements(By.Name("entry"));
                foreach (IWebElement element in elements)
                {
                    /*if (element.Text.Length > 0)
                    {
                        string[] words = element.Text.Split(new char[] { ' ' });
                        contactCache.Add(new ContactDate(words[1], words[0]));
                    }*/
                    if (element.Text.Length > 0)
                    {
                        string[] words = element.Text.Split(new char[] { ' ' });
                        contactCache.Add(new ContactDate(words[1], words[0])
                        {
                            Id = element.FindElement(By.TagName("input")).GetAttribute("value")
                        });
                    }
                    else
                    {
                        contactCache.Add(new ContactDate("", ""));
                    }
                }
            }
            return new List<ContactDate>(contactCache);
        }

        public int GetContactCount()
        {
            return driver.FindElements(By.Name("entry")).Count;
        }
    }
}
