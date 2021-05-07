using System;
using NUnit.Framework;
using OpenQA.Selenium;
using TestAutomationFramework_CWS.TAF_Util;

namespace TestAutomationFramework_CWS
{
    [TestFixture]
    public class Tests : FrameworkCore
    {
        [Test,Sequential]
        public void SampleTest()
        {
            try
            {
                IWebElement Courses = BaseDriver.GetInstance().FindElement(By.XPath("(//a[text()='Courses'])[1]"));

                Courses.Click();
                if(BaseDriver.GetInstance().FindElement(By.XPath("//a[text()='All Courses']")).Displayed == true)
                {
                    Test.Pass("Test Successful.");
                }
                else
                {
                    Test.Fail("Test Fail");
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}