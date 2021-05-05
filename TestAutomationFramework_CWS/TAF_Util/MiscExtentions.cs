using System;
namespace TestAutomationFramework_CWS.TAF_Util
{
    public static class MiscExtentions
    {
        public static string AsLink(this string strURL)
        {
            try
            {
                return "<a href = '" + strURL + "' target='_blank'>" + strURL + "</a>";
            }
            catch (Exception ex)
            {
                throw;
            }
        }

    }
}
