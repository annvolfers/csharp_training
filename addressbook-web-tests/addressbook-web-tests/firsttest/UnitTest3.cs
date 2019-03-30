using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace addressbook_web_tests.firsttest
{
    [TestClass]
    public class UnitTest3
    {
        [TestMethod]
        public void TestMethodCycles()
        {
            //FOR
            /*string[] s = new string[] { "I", "want", "to", "sleep" };
            for (int i = 0; i < s.Length; i++)
            {
                System.Console.Out.Write(s[i] + "\n");
            }*/

            //FOREACH
            /*string[] s = new string[] { "I", "want", "to", "sleep" };
            foreach (string element in s)
            {
                System.Console.Out.Write(element + "\n");
            }*/

            //WHILE
            /*IWebElement driver = null;
            int attempt = 0;

            while (driver.FindElements(By.Id("test")).Count == 0 && attempt < 60)
            {
                System.Threading.Thread.Sleep(1000);
                attempt++;
            }*/

            //DO WHILE
            /*IWebElement driver = null;
            int attempt = 0;

            do
            {
                System.Threading.Thread.Sleep(1000);
                attempt++;
            } while (driver.FindElements(By.Id("test")).Count == 0 && attempt < 60);*/
        }
    }
}
