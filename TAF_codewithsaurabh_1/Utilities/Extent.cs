using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using System;
using System.Configuration;
using System.Diagnostics;
using System.Threading;

namespace TAF_codewithsaurabh_1.Utilities
{
    ///<summary>
    /// Class Description : Contains the Implementation for Extent Reporting
    ///</summary>
    public class Extent
    {

        public static readonly ExtentReports _instance = new ExtentReports();

        private Extent() { }

        public static ExtentReports Instance
        {
            get
            {
                return _instance;
            }
        }

        //Constructor for the class, creates an instance of the Extent Report
        static Extent()
        {
            var location = System.Reflection.Assembly.GetExecutingAssembly().Location;
            var directory = System.IO.Path.GetDirectoryName(location);
            string Path = FileOperations.GetReportPath();
            var htmlReporter = new ExtentSparkReporter(Path + "Report.html");
            htmlReporter.LoadConfig(directory + @"\extent-config.xml");

            Instance.AttachReporter(htmlReporter);

            Instance.AddSystemInfo("Browser", ConfigurationManager.AppSettings["Browser"]);
            Instance.AddSystemInfo("OS Name", Environment.OSVersion.VersionString);


            Process[] chromeDriverProcesses = Process.GetProcessesByName("chromedriver");
            try
            {
                foreach (var chromeDriverProcess in chromeDriverProcesses)
                {
                    chromeDriverProcess.Kill();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
