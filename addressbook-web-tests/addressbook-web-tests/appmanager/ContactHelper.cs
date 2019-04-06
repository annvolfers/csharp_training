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
            //driver.FindElement(By.XPath("(//img[@alt='Edit'])[" + (index+1) + "]")).Click();
            driver.FindElements(By.Name("entry"))[index]
                .FindElements(By.TagName("td"))[7]
                .FindElement(By.TagName("a")).Click();
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

        public ContactDate GetContactInformationFromTable(int index)
        {
            manager.Navigator.GoToHomePage();

            IList<IWebElement> cells = driver.FindElements(By.Name("entry"))[index]
                .FindElements(By.TagName("td"));
            string lastName = cells[1].Text;
            string firstName = cells[2].Text;
            string address = cells[3].Text;
            string allPhones = cells[5].Text;

            return new ContactDate(firstName, lastName)
            {
                Address = address,
                AllPhones = allPhones
            };

        }

        public ContactDate GetContactInformationFromEditForm(int index)
        {
            manager.Navigator.GoToHomePage();
            GoToPageContactModification(index);

            string firstName = driver.FindElement(By.Name("firstname")).GetAttribute("value");
            string lastName = driver.FindElement(By.Name("lastname")).GetAttribute("value");
            string address = driver.FindElement(By.Name("address")).GetAttribute("value");

            string homePhone = driver.FindElement(By.Name("home")).GetAttribute("value");
            string mobilePhone = driver.FindElement(By.Name("mobile")).GetAttribute("value");
            string workPhone = driver.FindElement(By.Name("work")).GetAttribute("value");

            return new ContactDate(firstName, lastName)
            {
                Address = address,
                HomePhone = homePhone,
                MobilePhone = mobilePhone,
                WorkPhone = workPhone
            };
        }

        public int GetNumberOfSearchResults()
        {
            manager.Navigator.GoToHomePage();
            string text = driver.FindElement(By.TagName("label")).Text;
            Match m = new Regex(@"\d+").Match(text);
            return Int32.Parse(m.Value);
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
                        contactCache.Add(new ContactDate(words[1], words[0]) {
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

        public bool IsContactPresent()
        {
            return IsElementPresent(By.XPath("(//img[@alt='Edit'])[" + (1) + "]"));
        }
    }
}
