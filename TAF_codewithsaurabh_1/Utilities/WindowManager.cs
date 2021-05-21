using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TAF_codewithsaurabh_1.Utilities
{
    ///<summary>
    /// Class Description : Contains Methods related to Selenium Window Management
    ///</summary>
    public class WindowManager
    {
        public static List<string> WindowHandleCollection = new List<string>();
        public static string BaseWindowHandle { get; set; }

        ///<summary>
        ///Method Name            : RegisterHandles
        ///Return Type            : void
        /// Method Description    : Registers all the Window Handles triggered by selenium
        /// Method Parameters     : N/A
        /// Parameter Description : N/A
        ///</summary>
        public static void RegisterHandles()
        {
            if (WindowManager.WindowHandleCollection != null)
            {
                WindowManager.BaseWindowHandle = BaseDriver.Instance.CurrentWindowHandle;
                WindowManager.WindowHandleCollection.Add(WindowManager.BaseWindowHandle);
            }
            else
            {
                new WindowManager();
            }
        }

        ///<summary>
        ///Method Name            : ActiveHandle
        ///Return Type            : string
        /// Method Description    : Return the Active Handle in string format
        /// Method Parameters     : N/A
        /// Parameter Description : N/A
        ///</summary>
        public static string ActiveHandle()
        {
            try
            {
                var WindowHandles = BaseDriver.Instance.WindowHandles;
                string CurrentWindowHandle = "";
                foreach (var item in WindowHandles)
                {
                    if (!WindowHandleCollection.Contains(item))
                    {
                        WindowHandleCollection.Add(item);
                        CurrentWindowHandle = item;
                        break;
                    }
                }
                return CurrentWindowHandle;
            }
            catch (Exception)
            {

                throw;
            }
        }

        ///<summary>
        ///Method Name            : GoToHandle
        ///Return Type            : IWebDriver
        /// Method Description    : Transfer the control to the given Widnow Handle
        /// Method Parameters     : string HandleName
        /// Parameter Description : Name of the Window handle in string format
        ///</summary>
        public static IWebDriver GoToHandle(string HandleName)
        {
            try
            {
                return BaseDriver.Instance.SwitchTo().Window(HandleName);
            }
            catch (Exception)
            {

                throw;
            }
        }

        ///<summary>
        ///Method Name            : CloseWindowHandle
        ///Return Type            : void
        /// Method Description    : Closes the window for the webdriver instance
        /// Method Parameters     : IWebDriver webDriver
        /// Parameter Description : Current Webdriver Instance Object
        ///</summary>
        public static void CloseWindowHandle(IWebDriver webDriver)
        {
            try
            {
                webDriver.Close();

            }
            catch (Exception)
            {

                throw;
            }
        }

        ///<summary>
        ///Method Name            : GoToNewTab
        ///Return Type            : IWebdriver
        /// Method Description    : Switches the control to the newly opened tab from selenium action methods
        /// Method Parameters     : N/A
        /// Parameter Description : N/A
        ///</summary>
        public static IWebDriver GoToNewTab()
        {
            string CurrentWindowHandle = WaitTillHandleLoads();
            var ErrorPageTab = BaseDriver.Instance.SwitchTo().Window(CurrentWindowHandle);
            ErrorPageTab.Manage().Window.Maximize();
            return ErrorPageTab;
        }

        ///<summary>
        ///Method Name            : WaitTillHandleLoads
        ///Return Type            : string
        /// Method Description    : Extention methods to wait and return the expected window handle
        /// Method Parameters     : N/A
        /// Parameter Description : N/A
        ///</summary>
        private static string WaitTillHandleLoads()
        {
            try
            {
                int counter = 0;
                int timeOut = 5000;
                int interval = 200;
                int Handles = WindowHandleCollection.Count;
                string Handle = "";
                do
                {
                    Handle = WindowManager.ActiveHandle();
                    if (Handle == "")
                    {
                        Thread.Sleep(interval);
                        counter += interval;
                    }
                    else
                    {
                        return Handle;
                    }
                }
                while (counter < timeOut);
                return "";
            }
            catch (Exception)
            {
                throw;
            }
        }


    }
}
