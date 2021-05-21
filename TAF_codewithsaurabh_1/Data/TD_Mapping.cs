using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAF_codewithsaurabh_1.Utilities;

namespace TAF_codewithsaurabh_1.Data
{
    ///<summary>
    /// Class Description : Contains Mapping of Mapping of items in Data Collection to the IEnumerable Test Case Sources
    ///</summary>
    public class TD_Mapping 
    {

        static TD_Mapping()
        {
            LoadData.LoadDataFile();
        }
        public static IEnumerable DataSet1
        {
            get
            {
                TD_Initialization TD_Init;
                for(int i = 1; i < 2; i++)
                {
                    TD_Init = new TD_Initialization
                    {
                        ScenarioDescription = Excel.ReadData(i, "ScenarioDescription"),
                        FirstName = Excel.ReadData(i, "FirstName"),
                        LastName = Excel.ReadData(i, "LastName"),
                    };
                    string strScenario = TD_Init.ScenarioDescription;
                    yield return new TestCaseData(strScenario, TD_Init);
                }
            }
        }

        public static IEnumerable DataSet2
        {
            get
            {
                TD_Initialization TD_Init;
                for (int i = 2; i < 3; i++)
                {
                    TD_Init = new TD_Initialization
                    {
                        ScenarioDescription = Excel.ReadData(i, "ScenarioDescription"),
                        FirstName = Excel.ReadData(i, "FirstName"),
                        LastName = Excel.ReadData(i, "LastName"),
                    };
                    string strScenario = TD_Init.ScenarioDescription;
                    yield return new TestCaseData(strScenario, TD_Init);
                }
            }
        }


        public static IEnumerable DataSet3
        {
            get
            {
                TD_Initialization TD_Init;
                for (int i = 3; i < 4; i++)
                {
                    TD_Init = new TD_Initialization
                    {
                        ScenarioDescription = Excel.ReadData(i, "ScenarioDescription"),
                        FirstName = Excel.ReadData(i, "FirstName"),
                        LastName = Excel.ReadData(i, "LastName"),
                    };
                    string strScenario = TD_Init.ScenarioDescription;
                    yield return new TestCaseData(strScenario, TD_Init);
                }
            }
        }
    }
}
