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
            string allEmails = cells[4].Text;

            return new ContactDate(firstName, lastName)
            {
                Address = address,
                AllPhones = allPhones,
                AllEmails = allEmails
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

            string email_1 = driver.FindElement(By.Name("email")).GetAttribute("value");
            string email_2 = driver.FindElement(By.Name("email2")).GetAttribute("value");
            string email_3 = driver.FindElement(By.Name("email3")).GetAttribute("value");

            return new ContactDate(firstName, lastName)
            {
                Address = address,
                HomePhone = homePhone,
                MobilePhone = mobilePhone,
                WorkPhone = workPhone, 
                Email_1 = email_1,
                Email_2 = email_2,
                Email_3 = email_3
            };
        }

        public string GetTextContactInformationFromViewForm(int index)
        {
            manager.Navigator.GoToHomePage();
            driver.FindElements(By.Name("entry"))[index]
                .FindElements(By.TagName("td"))[6]
                .FindElement(By.TagName("a")).Click();
            string allInformaton = driver.FindElement(By.Id("content")).Text;
            return allInformaton;
        }

        public string GetTextContactInformationFromEditForm(int index)
        {
            manager.Navigator.GoToHomePage();
            GoToPageContactModification(index);

            string[] contact = new string[9];
            for (int i = 0; i < contact.Length; i++)
            {
                contact[i] = "";
            }
            contact[0] = driver.FindElement(By.Name("firstname")).GetAttribute("value");
            contact[1] = driver.FindElement(By.Name("lastname")).GetAttribute("value");
            contact[2] = driver.FindElement(By.Name("address")).GetAttribute("value");
            contact[3] = driver.FindElement(By.Name("home")).GetAttribute("value");
            contact[4] = driver.FindElement(By.Name("mobile")).GetAttribute("value");
            contact[5] = driver.FindElement(By.Name("work")).GetAttribute("value");
            contact[6] = driver.FindElement(By.Name("email")).GetAttribute("value");
            contact[7] = driver.FindElement(By.Name("email2")).GetAttribute("value");
            contact[8] = driver.FindElement(By.Name("email3")).GetAttribute("value");

            string name = "";
            string address = "";
            string phones = "";
            string emails = "";

            if (contact[0].Length > 0 || contact[1].Length > 0)
            {
                if (contact[0].Length > 0 && contact[1].Length > 0)
                {
                    name += contact[0] + " " + contact[1];
                }
                else
                {
                    if (contact[0].Length > 0)
                    {
                        name += contact[0];
                    }
                    if (contact[1].Length > 0)
                    {
                        name += contact[1];
                    }
                }
            }
            if (contact[2].Length > 0)
            {
                address += "\r\n" + contact[2];
            }
            if (contact[3].Length > 0 || contact[4].Length > 0 || contact[5].Length > 0)
            {
                phones += "\r\n";
                if (contact[3].Length > 0)
                {
                    phones += "\r\nH: " + contact[3];
                }
                if (contact[4].Length > 0)
                {
                    phones += "\r\nM: " + contact[4];
                }
                if (contact[5].Length > 0)
                {
                    phones += "\r\nW: " + contact[5];
                }
            }
            if (contact[6].Length > 0 || contact[7].Length > 0 || contact[8].Length > 0)
            {
                emails += "\r\n";
                if (contact[6].Length > 0)
                {
                    emails += "\r\n" + contact[6];
                }
                if (contact[7].Length > 0)
                {
                    emails += "\r\n" + contact[7];
                }
                if (contact[8].Length > 0)
                {
                    emails += "\r\n" + contact[8];
                }
            }
            
            return name + address + phones + emails;
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
                        string[] words = new string[element.FindElements(By.TagName("td")).Count];
                        for (int i = 0; i < words.Length; i++)
                        {
                            words[i] = element.FindElements(By.TagName("td"))[i].Text;
                        }
                        contactCache.Add(new ContactDate(words[2], words[1]) {
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
