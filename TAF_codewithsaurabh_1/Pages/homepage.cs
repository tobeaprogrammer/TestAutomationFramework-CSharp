using AventStack.ExtentReports;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TAF_codewithsaurabh_1.Utilities;

namespace TAF_codewithsaurabh_1.Pages
{
    ///<summary>
    /// Class Description : Page Class for homepage containing the locators and relevant action methods
    ///</summary>
    public class homepage : POM_Setup
    {

        #region Locators

        #region Tiles
        [FindsBy(How = How.XPath,Using = "//h5[text()='Elements']")]
        public IWebElement TileLink_Elements { get; set; }

        [FindsBy(How = How.XPath, Using = "//h5[text()='Elements']")]
        public IWebElement TileLink_Forms => FindElement(By.XPath("//h5[text()='Forms']"));
        public IWebElement TileLink_Alerts => FindElement(By.XPath("//h5[contains(text(),'Alerts')]"));
        public IWebElement TileLink_Widgets => FindElement(By.XPath("//h5[text()='Widgets']"));
        public IWebElement TileLink_Interactions => FindElement(By.XPath("//h5[text()='Interactions']"));
        public IWebElement TileLink_BookStore => FindElement(By.XPath("//h5[text()='Book Store Application']"));
        public IWebElement TileLink_Fail => FindElement(By.XPath("//h5[text()='Failure']"));
        #endregion

        #region PageHeaders
        public IWebElement PageHeader_Elements => FindElement(By.XPath("//div[@class='main-header'][text()='Elements']"));
        public IWebElement PageHeader_Forms => FindElement(By.XPath("//div[@class='main-header'][text()='Forms']"));
        #endregion
        #endregion

        #region ActionMethods
        ///<summary>
        ///Method Name            : VerifyTilesDisplayed
        ///Return Type            : boolean
        /// Method Description    : Verifies if the tile elements are displayed
        /// Method Parameters     : ExtentTest Test
        /// Parameter Description : Instance of the ExtentTest Object
        ///</summary>
        public bool VerifyTilesDisplayed(ExtentTest Test)
        {
            try
            {
                bool Breturn = true;

                Breturn &= LogVisibilityResult(TileLink_Elements, "Elements", Test);
                Breturn &= LogVisibilityResult(TileLink_Forms, "Forms", Test);
                Breturn &= LogVisibilityResult(TileLink_Alerts, "Alerts, Frame & Windows", Test);
                Breturn &= LogVisibilityResult(TileLink_Widgets, "Widgets", Test);
                Breturn &= LogVisibilityResult(TileLink_Interactions, "Interactions", Test);
                Breturn &= LogVisibilityResult(TileLink_BookStore, "Book Store Application", Test);
                
                return Breturn;
            }
            catch (Exception)
            {
                return false;
            }
        }

        ///<summary>
        ///Method Name            : VerifyTilesDisplayed_Fail
        ///Return Type            : boolean
        /// Method Description    : Failure induced tile element validation for demo
        /// Method Parameters     : ExtentTest Test
        /// Parameter Description : Instance of the ExtentTest Object
        ///</summary>
        public bool VerifyTilesDisplayed_Fail(ExtentTest Test)
        {
            try
            {
                bool Breturn = true;

                Breturn &= LogVisibilityResult(TileLink_Elements, "Elements", Test);
                Breturn &= LogVisibilityResult(TileLink_Forms, "Forms", Test);
                Breturn &= LogVisibilityResult(TileLink_Alerts, "Alerts, Frame & Windows", Test);
                Breturn &= LogVisibilityResult(TileLink_Widgets, "Widgets", Test);
                Breturn &= LogVisibilityResult(TileLink_Interactions, "Interactions", Test);
                Breturn &= LogVisibilityResult(TileLink_BookStore, "Book Store Application", Test);
                Breturn &= LogVisibilityResult(TileLink_Fail, "Failure Tile", Test);

                return Breturn;
            }
            catch (Exception)
            {
                return false;
            }
        }
        #endregion

        #region Navigation

        ///<summary>
        ///Method Name            : NavigateToFormsPage
        ///Return Type            : boolean
        /// Method Description    : Navigation method to navigate from homepage to Forms Page.
        /// Method Parameters     : N/A
        /// Parameter Description : N/A
        ///</summary>
        public bool NavigateToFormsPage()
        {
            try
            {
                TileLink_Forms.Click();
                Thread.Sleep(5000);

                if (PageHeader_Forms.Displayed == true) return true;
                else return false;
            }
            catch (Exception)
            {

                return false;
            }
        }

        #endregion

        #region GenericMethods
        ///<summary>
        ///Method Name            : LogVisibilityResult
        ///Return Type            : boolean
        /// Method Description    : Generic Method to Log isDisplayed Result in Report.
        /// Method Parameters     : N/A
        /// Parameter Description : N/A
        ///</summary>
        public bool LogVisibilityResult(IWebElement item,string strElementName,ExtentTest Test)
        {
            try
            {
                bool Breturn = true;
                if(item.Displayed == true)
                {
                    Test.Pass("<b>" + strElementName + "Tile </b> is displayed.");
                    Breturn &= true;
                }
                else
                {
                    Test.Fail("<b>" + strElementName + "Tile </b> : is NOT displayed.");
                    Breturn &= false;
                }

                return Breturn;
            }
            catch (Exception)
            {
                return false;
            }
        }
        #endregion
    }
}
