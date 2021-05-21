using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Internal;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TAF_codewithsaurabh_1.Utilities
{
    ///<summary>
    /// Class Description : Contains Webdriver Instantiation and all selenium Webdriver methods
    ///</summary>
    public class BaseDriver : IWrapsDriver,IWebDriver
    {

        public static BaseDriver _instance = new BaseDriver();
        public static TimeSpan ExplicitTimeOut = TimeSpan.FromSeconds(int.Parse(ConfigurationManager.AppSettings["ExplicitWait"]));
        public static int interval = int.Parse(ConfigurationManager.AppSettings["ExplicitWait"]);
        public static int ElementLevelExplixcitWait = int.Parse(ConfigurationManager.AppSettings["ElementLevelExplixcitWait"]);
        
        //Constructor for the class will initiate a instance of the browser.
        public BaseDriver()
        {
            var defaultExplicitWait = TimeSpan.FromSeconds(int.Parse(ConfigurationManager.AppSettings["ExplicitWait"]));
            var defaultPageLoadWait = TimeSpan.FromSeconds(int.Parse(ConfigurationManager.AppSettings["PageLoadWait"]));

            switch (ConfigurationManager.AppSettings["Browser"])
            {
                case "Chrome":
                    var chromeDriverService = ChromeDriverService.CreateDefaultService();
                    chromeDriverService.HideCommandPromptWindow = true;
                    var chromeOption = new ChromeOptions
                    {
                        PageLoadStrategy = PageLoadStrategy.Normal,
                    };
                    WrappedDriver = new ChromeDriver(chromeDriverService, chromeOption, defaultExplicitWait);
                    break;

                case "Firefox":
                    var firefoxDriverservice = FirefoxDriverService.CreateDefaultService();
                    firefoxDriverservice.FirefoxBinaryPath = @"C:\Program Files\Mozilla Firefox\firefox.exe";

                    firefoxDriverservice.HideCommandPromptWindow = true;
                    var firefoxOption = new FirefoxOptions
                    {
                        PageLoadStrategy = PageLoadStrategy.Normal
                    };
                    firefoxOption.AcceptInsecureCertificates = true;
                    WrappedDriver = new FirefoxDriver(firefoxDriverservice, firefoxOption, defaultExplicitWait);
                    break;
            }
            WrappedDriver.Manage().Timeouts().PageLoad = defaultPageLoadWait;
            WrappedDriver.Manage().Window.Maximize();
            WrappedDriver.Manage().Cookies.DeleteAllCookies();
            WrappedDriver.SwitchTo().ActiveElement();
        }

        // check if the instance is null, If yes then assign a new instance.
        public static BaseDriver Instance => _instance ?? (_instance = new BaseDriver());


        //Native Selenium Methods from the IWebDriver Interface
        #region NativeSeleniumMethods
        public IWebDriver WrappedDriver { get; }

        public string Url { get => WrappedDriver.Url; set => WrappedDriver.Url = value; }

        public string Title => WrappedDriver.Title;

        public string PageSource => WrappedDriver.PageSource;

        public string CurrentWindowHandle => WrappedDriver.CurrentWindowHandle;

        public ReadOnlyCollection<string> WindowHandles => WrappedDriver.WindowHandles;

        public void Close()
        {
            WrappedDriver.Close();
        }

        public void Dispose()
        {
            WrappedDriver.Close();
            WrappedDriver.Quit();
            _instance = null;
        }

        public IWebElement FindElement(By by)
        {
            return FindElement_Override(by);
        }

        public IOptions Manage()
        {
            return WrappedDriver.Manage();
        }

        public INavigation Navigate()
        {
            return WrappedDriver.Navigate();
        }

        public void Quit()
        {
            WrappedDriver.Quit();
        }

        public ITargetLocator SwitchTo()
        {
            return WrappedDriver.SwitchTo();
        }
        #endregion

        //Custom Methods
        #region CustomSeleniumMethods
        public IWebElement FindElement_Override(By by, int interval = 400, int timeout = 7000)
        {
            if (TimeSpan.FromSeconds(interval) != TimeSpan.FromSeconds(int.Parse(ConfigurationManager.AppSettings["Interval"])))
            {
                interval = (ExplicitTimeOut.Seconds * 1000);
            }
            if (TimeSpan.FromSeconds(timeout) != TimeSpan.FromSeconds(int.Parse(ConfigurationManager.AppSettings["ExplicitWait"])))
            {
                timeout = (ExplicitTimeOut.Seconds * 1000);
            }

            IWebElement webElement = null;
            var tick = 0;
            try
            {
                do
                {
                    try
                    {
                        webElement = WrappedDriver.FindElement(by);
                    }
                    catch (Exception)
                    {
                        Thread.Sleep(interval);
                        tick += interval;
                    }

                } while (webElement == null && tick < timeout);
                if (webElement == null)
                {
                    throw new TimeoutException(string.Format("Element with By : {0} not found within {1} seconds", by.ToString(), (timeout / 1000).ToString()));
                }
                ElementIndicator(webElement);
                return webElement;
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void ElementIndicator(IWebElement webElement)
        {
            try
            {
                var javaScriptDriver = (IJavaScriptExecutor)WrappedDriver;
                string highlightJavascript = @"arguments[0].style.cssText = ""border-width: 2px; border-style: solid; border-color: red"";";
                javaScriptDriver.ExecuteScript(highlightJavascript, new object[] { webElement });
            }
            catch (Exception)
            {

                throw;
            }
        }


        public ReadOnlyCollection<IWebElement> FindElements(By by)
        {
            ReadOnlyCollection<IWebElement> element = null;
            element = WrappedDriver.FindElements(by);
            return element;
        }

        public bool IsElementPresent(By by)
        {
            try
            {
                var a = FindElement_Override(by, interval, ElementLevelExplixcitWait);
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
            catch (TimeoutException)
            {
                return false;
            }
        }
        #endregion


    }
}
