using System;
using System.Collections.ObjectModel;
using System.Configuration;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Internal;
using OpenQA.Selenium.Support.UI;

namespace TestAutomationFramework_CWS.TAF_Util
{
    public class BaseDriver : IWebDriver, IWrapsDriver
    {
        private static BaseDriver _instance = new BaseDriver();
        
        public static TimeSpan ExplicitTimeOut = TimeSpan.FromSeconds(120);
        public static TimeSpan defaultExplicitWait = TimeSpan.FromSeconds(120);

        // To Initialize the browser
        public BaseDriver()
        {
            var defaultPageLoadWait = TimeSpan.FromSeconds(90);

            switch (ConfigurationManager.AppSettings["Browser"])
            {
                case "Chrome":
                    var ChromeDriverService = OpenQA.Selenium.Chrome.ChromeDriverService.CreateDefaultService();
                    ChromeDriverService.HideCommandPromptWindow = true;
                    var chromeOption = new ChromeOptions
                    {
                        PageLoadStrategy = PageLoadStrategy.Normal,
                    };
                    WrappedDriver = new ChromeDriver(ChromeDriverService, chromeOption, defaultPageLoadWait);
                    break;

                case "Safari":
                    break;

                case "Firefox":
                    break;
            }

            string url = ConfigurationManager.AppSettings["URL"];

            WebDriverWaitInterval = new WebDriverWait(WrappedDriver, defaultExplicitWait);
            WebDriverWaitInterval.IgnoreExceptionTypes(typeof(NoSuchElementException), typeof(ElementNotVisibleException),
                typeof(StaleElementReferenceException), typeof(InvalidOperationException));

            WrappedDriver.Manage().Timeouts().PageLoad = defaultPageLoadWait;
            WrappedDriver.Manage().Window.Maximize();
            WrappedDriver.SwitchTo().ActiveElement();

            //Clearing Cookies
            WrappedDriver.Manage().Cookies.DeleteAllCookies();
        }
        public IWebDriver WrappedDriver { get; }
        public WebDriverWait WebDriverWaitInterval { get; }

        public string Url { get => WrappedDriver.Url; set => WrappedDriver.Url = value; }
        string IWebDriver.Url
        {
            get { return WrappedDriver.Url; }
            set { WrappedDriver.Url = value; }
        }

        public string Title => WrappedDriver.Title;

        public string PageSource => WrappedDriver.PageSource;

        public static BaseDriver GetInstance()
        {
            return _instance ??= (new BaseDriver());
        }

        public string CurrentWindowHandle { get => WrappedDriver.CurrentWindowHandle; set => throw new NotImplementedException(); }

        public ReadOnlyCollection<string> WindowHandles => WrappedDriver.WindowHandles;

        public void Close()
        {
            WrappedDriver.Close();
        }

        public void Quit()
        {
            WrappedDriver.Quit();
        }

        public void Dispose()
        {
            WrappedDriver.Close();
            WrappedDriver.Quit();
            _instance = null;
        }

        public IOptions Manage()
        {
            return WrappedDriver.Manage();
        }

        public INavigation Navigate()
        {
            return WrappedDriver.Navigate();
        }

        public ITargetLocator SwitchTo()
        {
            return WrappedDriver.SwitchTo();
        }

        public IWebElement FindElement(By by)
        {
            return ((ISearchContext)_instance).FindElement(by);
        }

        public ReadOnlyCollection<IWebElement> FindElements(By by)
        {
            ReadOnlyCollection<IWebElement> elements = null;
            DoUntil(() => { elements = WrappedDriver.FindElements(by); }); 
            return ((ISearchContext)_instance).FindElements(by);
        }

        public void DoUntil(Action act)
        {
            try
            {
                WebDriverWaitInterval.Until(a =>
                {
                    act.Invoke();
                    return true;
                });
            }
            catch (Exception ex)
            {
                while(ex.InnerException != null)
                {
                    ex = ex.InnerException;
                }
                throw;
            }
        }
    }
}
