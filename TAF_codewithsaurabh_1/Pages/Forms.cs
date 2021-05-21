using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TAF_codewithsaurabh_1.Utilities;

namespace TAF_codewithsaurabh_1.Pages
{
    /// Class Description : Page Class for homepage containing the locators and relevant action methods
    class Forms : POM_Setup
    {
        #region Objects

        #region NavigationLinks
        public IWebElement Link_PracticeForm => FindElement(By.XPath("//span[text()='Practice Form']"));
        #endregion

        #region FormObjects
        public IWebElement Input_FirstName => FindElement(By.Id("firstName"));
        public IWebElement Input_LastName => FindElement(By.Id("lastName"));
        #endregion

        #region PageHeaders
        public IWebElement PageHeader_PracticeForm => FindElement(By.XPath("//div[@class='main-header'][text()='Practice Form']"));
        #endregion

        #endregion

        #region ActionMethods

        ///<summary>
        ///Method Name            : EnterName
        ///Return Type            : boolean
        /// Method Description    : Enters the First and Last Name on the Form.
        /// Method Parameters     : string Fname,string Lname
        /// Parameter Description : "string" to be passed
        ///</summary>
        public bool EnterName(string Fname,string Lname)
        {
            try
            {
                Input_FirstName.SendKeys(Fname);
                Input_LastName.SendKeys(Lname);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        #endregion

        #region Navigations
        ///<summary>
        ///Method Name            : LaunchPracticeForm
        ///Return Type            : boolean
        /// Method Description    : Navigation method to navigate from Forms Page to Practice Form Page.
        /// Method Parameters     : N/A
        /// Parameter Description : N/A
        ///</summary>
        public bool LaunchPracticeForm()
        {
            try
            {
                Link_PracticeForm.Click();
                Thread.Sleep(5000);

                if (PageHeader_PracticeForm.Displayed == true) return true;
                else return false;
            }
            catch (Exception)
            {

                return false;
            }
        }
        #endregion

    }
}
