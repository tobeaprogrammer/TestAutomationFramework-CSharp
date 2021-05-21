using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using System;
using TAF_codewithsaurabh_1.Data;
using TAF_codewithsaurabh_1.Utilities;
using TAF_codewithsaurabh_1.Pages;
using Assert = NUnit.Framework.Assert;

namespace TAF_codewithsaurabh_1.Scripts
{
    [TestFixture]
    public class UIValidation : Core
    {

        ///<summary>
        ///TestCase ID :
        ///Description : Validates if the UI Element is present
        ///</summary>
        [Test, Sequential, Category("Validate_UI")]
        [Author("Saurabh Kulkarni")]
        [TestCaseSource(typeof(TD_Mapping),"DataSet1")]
        public void TestScript_1(string strScenario,ref TD_Initialization data)
        {
            try
            {
                bool Breturn = false;
                homepage homepage = new homepage();

                Breturn = homepage.VerifyTilesDisplayed(Test);
                Assert.That(Breturn, Is.True, "Tiles NOT Displayed as Expected.");
                Test.PassX("Tiles Displayed as excpected.");
            }
            catch (Exception)
            {
                throw;
            }
        }

        ///<summary>
        ///TestCase ID :
        ///Description : Sample Intentional Failing Test
        ///</summary>
        [Test, Sequential, Category("Validate_UI")]
        [Author("Saurabh Kulkarni")]
        [TestCaseSource(typeof(TD_Mapping), "DataSet2")]
        public void TestScript_2(string strScenario, ref TD_Initialization data)
        {
            try
            {
                bool Breturn = false;
                homepage homepage = new homepage();

                Breturn = homepage.VerifyTilesDisplayed_Fail(Test);
                Assert.That(Breturn, Is.True, "Tiles NOT Displayed as Expected.");
                Test.PassX("Tiles Displayed as excpected.");
            }
            catch (Exception)
            {
                throw;
            }
        }


        
    }
}
