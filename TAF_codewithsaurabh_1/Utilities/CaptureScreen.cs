using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TAF_codewithsaurabh_1.Utilities
{
    ///<summary>
    /// Class Description : Contains methods required for capturing a screenshot
    ///</summary>
    public class CaptureScreen
    {
        public static string strPat = @"\";
        public static string strReportFolderPath = null;

        public static int counter = 0;

        // Constructor for the class, will create a temporary location to store screenshots
        static CaptureScreen()
        {
            var directory = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            string Path = FileOperations.GetReportPath();
            strReportFolderPath = System.IO.Path.Combine(Path + "temp" + strPat);
            DirectoryInfo Validation = new DirectoryInfo(strReportFolderPath);
            Validation.Create();
        }


        ///<summary>
        ///Method Name            : TakeScreenPrint
        ///Return Type            : string
        /// Method Description    : Captures the current screen on the webdriver instance
        /// Method Parameters     : N/A
        /// Parameter Description : N/A
        ///</summary>
        public static string TakeScreenPrint()
        {
            var driver = BaseDriver.Instance;
            string FullFileName = null;
            counter++; 


            DirectoryInfo Validation = new DirectoryInfo(strReportFolderPath); //System IO object

            //New Code to capture File Name
            FullFileName = counter.ToString() + "_" + TestContext.CurrentContext.Test.MethodName + "_" + DateTime.Now.ToString("dd-MM-yyyy_HHmmss") + "." + System.Drawing.Imaging.ImageFormat.Jpeg;

            if (Validation.Exists == true) //Capture screen if the path is available
            {
                ((ITakesScreenshot)driver.WrappedDriver).GetScreenshot().SaveAsFile(strReportFolderPath + FullFileName);
            }
            else //Create the folder and then Capture the screen
            {
                Validation.Create();
                ((ITakesScreenshot)driver.WrappedDriver).GetScreenshot().SaveAsFile(strReportFolderPath + FullFileName);
            }
            
            return FullFileName;

        }
    }
}
