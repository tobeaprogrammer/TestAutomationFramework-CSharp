using AventStack.ExtentReports;
using AventStack.ExtentReports.MarkupUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TAF_codewithsaurabh_1.Utilities
{
    ///<summary>
    /// Class Description : Contains all the extention methods required in the framework.
    ///</summary>
    public static class Extensions
    {
        ///<summary>
        ///Method Name            : PassX
        ///Return Type            : void
        /// Method Description    : Logs a Pass Step along with a screenshot [Extent Report]
        /// Method Parameters     : string strTextMessage
        /// Parameter Description : Text Message to be logged with the screenshot
        ///</summary>
        public static void PassX(this ExtentTest Test, string strTextMessage, MediaEntityModelProvider provider = null)
        {
            try
            {
                string Screenshot = CaptureScreen.TakeScreenPrint();
                if (Screenshot != null)
                {
                    var m = MediaEntityBuilder.CreateScreenCaptureFromPath(Screenshot).Build();
                    Test.Pass(strTextMessage + "<br/>", m);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        ///<summary>
        ///Method Name            : FailX
        ///Return Type            : void
        /// Method Description    : Logs a Fail Step along with a screenshot [Extent Report]
        /// Method Parameters     : string strTextMessage
        /// Parameter Description : Text Message to be logged with the screenshot
        ///</summary>
        public static void FailX(this ExtentTest Test, string strTextMessage,  MediaEntityModelProvider provider = null)
        {
            try
            {
                if (strTextMessage.Length < 500 && BaseDriver.Instance.WindowHandles.Count != 0)
                {
                    string Screenshot = CaptureScreen.TakeScreenPrint();
                    if (Screenshot != null)
                    {
                        var m = MediaEntityBuilder.CreateScreenCaptureFromPath(Screenshot).Build();
                        Test.Fail(strTextMessage + "<br/>", m);
                    }
                }
                else
                {
                    Test.Fail(strTextMessage);
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
        }

        ///<summary>
        ///Method Name            : CodeBlock
        ///Return Type            : void
        /// Method Description    : Logs a Info Step with message as a codeblock an HTML extension[Extent Report]
        /// Method Parameters     : string strTextMessage
        /// Parameter Description : Text Message to be logged with the screenshot
        ///</summary>
        private static void CodeBlock(this ExtentTest Test, string strTextMessage)
        {
            try
            {
                var m = MarkupHelper.CreateCodeBlock(strTextMessage);
                Test.Info(m);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
