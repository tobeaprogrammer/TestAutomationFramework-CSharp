using AventStack.ExtentReports;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TAF_codewithsaurabh_1.Utilities
{
    ///<summary>
    /// Class Description : Contains methods for File Handling
    ///</summary>
    class FileOperations
    {
        ///<summary>
        ///Method Name            : MoveDirectory
        ///Return Type            : void
        /// Method Description    : Move the directory from source to target
        /// Method Parameters     : string source, string target
        /// Parameter Description : Source and Destination Path in string format
        ///</summary>
        public static void MoveDirectory(string source, string target)
        {
            try
            {
                var sourcePath = source.TrimEnd('\\', ' ');
                var targetPath = target.TrimEnd('\\', ' ');
                var files = Directory.EnumerateFiles(sourcePath, "*", SearchOption.AllDirectories)
                                     .GroupBy(s => Path.GetDirectoryName(s));
                foreach (var folder in files)
                {
                    var targetFolder = folder.Key.Replace(sourcePath, targetPath);
                    Directory.CreateDirectory(targetFolder);
                    foreach (var file in folder)
                    {
                        var targetFile = Path.Combine(targetFolder, Path.GetFileName(file));
                        if (File.Exists(targetFile)) File.Delete(targetFile);
                        File.Move(file, targetFile);
                    }
                }
                Directory.Delete(source, true);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        ///<summary>
        ///Method Name            : CopyDirectory
        ///Return Type            : void
        /// Method Description    : Copy the directory from source to target
        /// Method Parameters     : string source, string target
        /// Parameter Description : Source and Destination Path in string format
        ///</summary>
        public static void CopyDirectory(string source, string target)
        {
            try
            {
                var sourcePath = source.TrimEnd('\\', ' ');
                var targetPath = target.TrimEnd('\\', ' ');
                var files = Directory.EnumerateFiles(sourcePath, "*", SearchOption.AllDirectories)
                                     .GroupBy(s => Path.GetDirectoryName(s));
                foreach (var folder in files)
                {
                    var targetFolder = folder.Key.Replace(sourcePath, targetPath);
                    Directory.CreateDirectory(targetFolder);
                    foreach (var file in folder)
                    {
                        var targetFile = Path.Combine(targetFolder, Path.GetFileName(file));
                        if (File.Exists(targetFile)) File.Delete(targetFile);
                        File.Copy(file, targetFile);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }


        ///<summary>
        ///Method Name            : GetReportPath
        ///Return Type            : string
        /// Method Description    : Return the report folder location
        /// Method Parameters     : N/A
        /// Parameter Description : N/A
        ///</summary>
        public static string GetReportPath()
        {
            string Path;
            var location = System.Reflection.Assembly.GetExecutingAssembly().Location;
            var directory = System.IO.Path.GetDirectoryName(location);
            Path = System.IO.Path.Combine(directory, @"..\..\TestReports\");
            return Path;

        }

        ///<summary>
        ///Method Name            : MoveFiles
        ///Return Type            : void
        /// Method Description    : Move the files in a directory from source to target
        /// Method Parameters     : string srcLocation, string destLocation
        /// Parameter Description : Source and Destination Path in string format
        ///</summary>
        public static void MoveFiles(string srcLocation, string destLocation)
        {
            List<String> files = Directory.GetFiles(srcLocation, "*.*", SearchOption.AllDirectories).ToList();

            foreach (string file in files)
            {
                FileInfo mFile = new FileInfo(file);
                mFile.MoveTo(destLocation + "\\" + mFile.Name);
            }
        }

    }
}
