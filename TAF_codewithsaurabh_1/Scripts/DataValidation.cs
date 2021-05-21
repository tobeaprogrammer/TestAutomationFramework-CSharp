using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAF_codewithsaurabh_1.Data;
using TAF_codewithsaurabh_1.Pages;
using TAF_codewithsaurabh_1.Utilities;

namespace TAF_codewithsaurabh_1.Scripts
{
    [TestFixture]
    public class DataValidation : Core
    {

        ///<summary>
        ///TestCase ID :
        ///Description : Validates if the Tiles are navigating to the respective pages.
        ///</summary>
        [Test, Sequential, Category("Validate Tile Page")]
        [Author("Saurabh Kulkarni")]
        [TestCaseSource(typeof(TD_Mapping), "DataSet3")]
        public void TestScript_3(string strScenario, ref TD_Initialization data)
        {
            try
            {
                bool Breturn = false;
                homepage homepage = new homepage();
                Forms forms = new Forms();

                Breturn = homepage.NavigateToFormsPage();
                Assert.That(Breturn, Is.True, "Failed to Navigate to Forms Page.");
                Test.Info("Navigated to Forms Page");

                Breturn = forms.LaunchPracticeForm();
                Assert.That(Breturn, Is.True, "Failed to Launch Practice Forms Page.");
                Test.PassX("Launched Practice Form Page");

                Breturn = forms.EnterName(data.FirstName, data.LastName);
                Assert.That(Breturn, Is.True, "Failed to Enter Name.");
                Test.PassX("Name Entered.");

            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
