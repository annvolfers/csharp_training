using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace addressbook_web_tests.firsttest
{
    [TestClass]
    public class UnitTest2
    {
        [TestMethod]
        public void TestMethodIfThenElse()
        {
            double total = 1200;
            bool vipClient = true;

            if (total > 1000 || vipClient)
            {
                total = total * 0.9;
                System.Console.Out.Write("Скидка 10%, общая сумма " + total);
            }
            /*else
            {
                System.Console.Out.Write("Скидки нет, общая сумма " + total);
            }*/
        }
    }
}
