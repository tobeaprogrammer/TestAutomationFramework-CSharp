using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TAF_codewithsaurabh_1.Utilities
{
    ///<summary>
    /// Class Description : Contains the Initialisation and extentions methods for Page Object Model Setup
    ///</summary>
    public class POM_Setup
    {
        // Constructor for the calss, initalises all the Web Elements of the Page.
        public POM_Setup()
        {
#pragma warning disable CS0618 // Type or member is obsolete
            PageFactory.InitElements(BaseDriver.Instance, this);
#pragma warning restore CS0618 // Type or member is obsolete
        }

        public IWebElement FindElement(By by)
        {
            return BaseDriver.Instance.FindElement(by);
        }

        
        public ReadOnlyCollection<IWebElement> FindElements(By by)
        {
            return BaseDriver.Instance.FindElements(by);

        }
    }
}
