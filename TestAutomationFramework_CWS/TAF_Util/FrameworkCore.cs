using System;
using System.Configuration;
using AventStack.ExtentReports;
using AventStack.ExtentReports.MarkupUtils;
using NUnit.Framework;
using NUnit.Framework.Interfaces;

namespace TestAutomationFramework_CWS.TAF_Util
{
    public class FrameworkCore
    {
        public static int TotalTests = 0;
        public static int TotalPass = 0;
        public static int TotalFail = 0;
        public static ExtentTest Test;
        public static ExtentReports report;


        [SetUp] // Run before each Test
        public void SetUp()
        {
            TotalTests++;
            //Clean Temp Folder if Windows

            var testname = TestContext.CurrentContext.Test.Name;
            Test = report.CreateTest(testname);

            var ClassName = TestContext.CurrentContext.Test.ClassName;
            var Category = ClassName.Split('.');

            var markup = MarkupHelper.CreateLabel("Test Started", ExtentColor.Blue);
            Test.Info(markup);

            string url = ConfigurationManager.AppSettings["URL"];
            string projectName = ConfigurationManager.AppSettings["ApplicationtName"];
            BaseDriver.Instance.Navigate().GoToUrl(url);

            Test.Info("Opening URL for " + projectName + " :: " + url.AsLink());
        }


        [TearDown] // Run after each Test
        public void TearDown()
        {
            int fail = 0;
            var NunitStatus = TestContext.CurrentContext.Result.Outcome.Status;
            var status = Test.Status;
            var stackTrace = " " + TestContext.CurrentContext.Result.StackTrace + " ";
            var errorMessage = TestContext.CurrentContext.Result.Message;
            Status logstatus;
            if(NunitStatus == TestStatus.Failed)
            {
                logstatus = Status.Fail;
                Test.Fail("Test.Failed :: " + errorMessage);
                TotalFail++;
                fail = 1;
            }
            else
            {
                switch(status)
                {
                    case Status.Fail:
                        logstatus = Status.Fail;
                        Test.Fail("Test Failed");
                        if (fail != 1) TotalFail++;
                        break;
                    case Status.Warning:
                        logstatus = Status.Warning;
                        break;
                    case Status.Pass:
                        logstatus = Status.Pass;
                        TotalPass++;
                        break;
                    default:
                        logstatus = Status.Pass;
                        break;
                }
            }
            if (BaseDriver.Instance.WindowHandles.Count != 0) BaseDriver.Instance.Dispose();
        }
    }
}
