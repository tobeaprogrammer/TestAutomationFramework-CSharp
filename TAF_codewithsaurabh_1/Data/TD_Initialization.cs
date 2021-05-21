using AventStack.ExtentReports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TAF_codewithsaurabh_1.Data
{
    ///<summary>
    /// Class Description : Contains Data Variables Declarations
    ///</summary>
    public class TD_Initialization
    {
        public ExtentTest Test { get; set; }
        public string ScenarioDescription { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public override string ToString()
        {
            return "Test Data";
        }

    }
}
