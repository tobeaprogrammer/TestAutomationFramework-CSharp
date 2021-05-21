using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TAF_codewithsaurabh_1.Utilities
{
    ///<summary>
    /// Class Description : Contains Methods related to Excel Operations
    ///</summary>
    public class Excel
    {
        private static List<Datacollection> _dataCol = new List<Datacollection>();


        ///<summary>
        ///Method Name            : PopulateInCollection
        ///Return Type            : void
        /// Method Description    : Extracts all the Values from Excel into a Data Collection
        /// Method Parameters     : string fileName
        /// Parameter Description : Full File Path in string variable
        ///</summary>
        public static void PopulateInCollection(string fileName)
        {
            System.Data.DataSet dataSet = ExtractExcelValues(fileName);

            foreach (System.Data.DataTable dt in dataSet.Tables)
            {
                //Iterate through the rows and columns of the Table
                for (int row = 1; row <= dt.Rows.Count; row++)
                {
                    for (int col = 0; col < dt.Columns.Count; col++)
                    {
                        Datacollection dtTable = new Datacollection()
                        {
                            rowNumber = row,
                            colName = dt.Columns[col].ColumnName,
                            colValue = dt.Rows[row - 1][col].ToString()
                        };
                        //Add all the details for each row
                        _dataCol.Add(dtTable);
                    }
                }

            }

        }

        ///<summary>
        ///Method Name            : ExtractExcelValues
        ///Return Type            : System.Data.DataSet
        /// Method Description    : Extracts the values from the Excel and return in a Set [DataSet]
        /// Method Parameters     : string fileName
        /// Parameter Description : Full File Path in string variable
        ///</summary>
        private static System.Data.DataSet ExtractExcelValues(string fileName)
        {
            using (var stream = File.Open(fileName, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    var result = reader.AsDataSet(new ExcelDataSetConfiguration()
                    {
                        ConfigureDataTable = (data) => new ExcelDataTableConfiguration()
                        {
                            UseHeaderRow = true
                        }
                    });
                    List<string> visibleWorksheetNames = new List<string>();
                    System.Data.DataTable resultTable = null;
                    DataTableCollection table = null;

                    // will fetch all the tables from the visible worksheets
                    for (int i = 0; i < reader.ResultsCount; i++)
                    {
                        if (reader.VisibleState == "visible")
                        {
                            visibleWorksheetNames.Add(reader.Name);
                            table = result.Tables;      
                            resultTable = table[visibleWorksheetNames.ElementAt(i)];
                        }

                        reader.NextResult();
                    }

                    return result;
                }
            }
        }

        ///<summary>
        ///Method Name            : ReadData
        ///Return Type            : string
        /// Method Description    : LINQ query to reduce iterations and extract values
        /// Method Parameters     : int rowNumber, string columnName
        /// Parameter Description : Co-ordinates of data in the collection in the form of "int"
        ///</summary>
        public static string ReadData(int rowNumber, string columnName)
        {
            try
            {
                string data = (from colData in _dataCol
                               where colData.colName == columnName && colData.rowNumber == rowNumber
                               select colData.colValue).FirstOrDefault();

                return data.ToString();
            }
            catch (Exception)
            {
                return null;
            }
        }


        public class Datacollection
        {
            public int rowNumber { get; set; }
            public string colName { get; set; }
            public string colValue { get; set; }
        }
    }
}
