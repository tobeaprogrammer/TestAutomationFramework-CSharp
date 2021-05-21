using AventStack.ExtentReports;
using AventStack.ExtentReports.MarkupUtils;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TAF_codewithsaurabh_1.Utilities
{
    ///<summary>
    /// Class Description : Contains all the NUNit hook implementations to conduct executions
    ///</summary>
    public class Core
    {
        public static int TotalTests = 0;
        public static int TotalPass = 0;
        public static int TotalFail = 0;
        public static ExtentTest Test;
        public static ExtentReports extent;


        [SetUp]
        public void SetUp()
        {
            Test = extent.CreateTest(TestContext.CurrentContext.Test.Name);
            var ClassName = TestContext.CurrentContext.Test.ClassName;
            var Category = ClassName.Split('.');
            Test.AssignCategory(Category[Category.Length - 1]);
            Test.Info(MarkupHelper.CreateLabel("Test Initiated", ExtentColor.Blue));
            BaseDriver.Instance.Navigate().GoToUrl(ConfigurationManager.AppSettings["URL"]);
        }

        [TearDown]
        public void TearDown()
        {
            int fail = 0;
            var NunitStatus = TestContext.CurrentContext.Result.Outcome.Status;
            var TStatus = Test.Status;
            var errorMessage = TestContext.CurrentContext.Result.Message;

#pragma warning disable CS0219 // Variable is assigned but its value is never used
            Status logstatus;
#pragma warning restore CS0219 // Variable is assigned but its value is never used
            if (NunitStatus == TestStatus.Failed)
            {
                logstatus = Status.Fail;
                Test.FailX("Test Failed :: " + errorMessage);
                Test.Info(MarkupHelper.CreateLabel("Test Failed", ExtentColor.Red));
                TotalFail++;
                fail = 1;
            }
            else
            {
                switch (TStatus)
                {
                    case Status.Fail:
                        logstatus = Status.Fail;
                        Test.Fail("Test Failed");
                        if (fail != 1) TotalFail++;
                        break;
                    case Status.Warning:
                        logstatus = Status.Warning;
                        Test.Info(MarkupHelper.CreateLabel("Warning", ExtentColor.Orange));
                        break;
                    case Status.Skip:
                        logstatus = Status.Skip;
                        Test.Info(MarkupHelper.CreateLabel("Test Skipped", ExtentColor.Yellow));
                        break;
                    case Status.Pass:
                        logstatus = Status.Pass;
                        Test.Info(MarkupHelper.CreateLabel("Test Passed", ExtentColor.Green));
                        TotalPass++;
                        break;
                    default:
                        logstatus = Status.Pass;
                        break;
                }
            }

            BaseDriver.Instance.Dispose();
        }

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            // Will run once before the execution starts
            extent = Extent.Instance;
            
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            // Will run once after the execution ends
            var location = System.Reflection.Assembly.GetExecutingAssembly().Location;
            var directory = System.IO.Path.GetDirectoryName(location);
            string Path = FileOperations.GetReportPath();

            extent.Flush();

            // Debug Code
            var NewFileName = "Test_Report_" + DateTime.Now.ToString("dd-MM-yyyy_HHmmss") + ".html";
            string strTimeStamp = DateTime.Now.ToString("dd-MM-yyyy_HHmmss");
            string strReportFolderPath = System.IO.Path.Combine(Path + "Report_" + strTimeStamp + @"\");
            DirectoryInfo Validation = new DirectoryInfo(strReportFolderPath);
            Validation.Create();
            var NewPath = System.IO.Path.Combine(strReportFolderPath, NewFileName);
            System.IO.File.Move(Path + "Report.html", NewPath);

            //Move all the screenshots in the relevant reports folder
            FileOperations.MoveFiles(FileOperations.GetReportPath() + @"temp", strReportFolderPath);
            Directory.Delete(System.IO.Path.Combine(FileOperations.GetReportPath()+ @"temp"));

            Process.Start(NewPath);
        }

    }
}
