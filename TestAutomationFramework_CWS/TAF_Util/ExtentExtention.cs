using System;
using System.Configuration;
using System.Diagnostics;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;

namespace TestAutomationFramework_CWS.TAF_Util
{
    public class ExtentExtention
    {

        public static readonly ExtentReports _instance = new ExtentReports();

        private ExtentExtention() { }

        public static ExtentReports Instance
        {
            get
            {
                return _instance;
            }
        }

        //static constructor - to initialize report
        static ExtentExtention()
        {
            var location = System.Reflection.Assembly.GetExecutingAssembly().Location;
            var directory = System.IO.Path.GetDirectoryName(location);
            string Path = FileOperations.GetReportLocation();

            var htmlReporter = new ExtentHtmlReporter(Path + "TestReport.html");
            htmlReporter.LoadConfig(directory + @"/extent-config.xml");

            Instance.AttachReporter(htmlReporter);

            Instance.AddSystemInfo("Browser", ConfigurationManager.AppSettings["Browser"]);
            Instance.AddSystemInfo("OS Name", Environment.OSVersion.VersionString);
            Instance.AddSystemInfo("Host Name", Environment.UserName);

           
        }
    }
}
