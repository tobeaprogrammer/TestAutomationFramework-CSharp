using NUnit.Framework;
using TestAutomationFramework_CWS.TAF_Util;

namespace TestAutomationFramework_CWS
{
    [TestFixture]
    public class Tests : FrameworkCore
    {
        [Test,Sequential]
        public void SampleTest()
        {
            Assert.Pass();
        }
    }
}