using System;
namespace TestAutomationFramework_CWS.TAF_Util
{
    public class FileOperations
    {
        public static string GetReportLocation()
        {
            string strPat = @"\";
            var location = System.Reflection.Assembly.GetExecutingAssembly().Location;
            var directory = System.IO.Path.GetDirectoryName(location);
            string Path = System.IO.Path.Combine(directory, @"..\..\..\Reports\");
            return Path;
        }
    }
}
