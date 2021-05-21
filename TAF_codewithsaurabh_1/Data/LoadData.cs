using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAF_codewithsaurabh_1.Utilities;

namespace TAF_codewithsaurabh_1.Data
{
    ///<summary>
    /// Class Description : Class containing method to map and load data in DataCollection
    ///</summary>
    public class LoadData
    {
        ///<summary>
        ///Method Name            : LoadDataFile
        ///Return Type            : void
        /// Method Description    : Loads the datasheet excel file and stores it in the DataCollection.
        /// Method Parameters     : N/A
        /// Parameter Description : N/A
        ///</summary>
        public static void LoadDataFile()
        {
            var directory = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            var DataFilePath = directory + @"\..\Data\DataSheet.xlsx";
            Excel.PopulateInCollection(DataFilePath);
        }
    }
}
