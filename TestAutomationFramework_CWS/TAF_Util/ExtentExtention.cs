using System;
namespace TestAutomationFramework_CWS.TAF_Util
{
    public class ExtentExtention
    {

        public static readonly ExtentExtention _instance = new ExtentExtention();

        private ExtentExtention() { }

        public static ExtentExtention Instance
        {
            get
            {
                return _instance;
            }
        }

        //static constructor - to initialize report
        static ExtentExtention()
        {
            var location = System.Reflection.Assembly.GetExecutingAssembly().Location;
            var directory = System.IO.Path.GetDirectoryName(location);
            string Path = 
        }

    }
}
