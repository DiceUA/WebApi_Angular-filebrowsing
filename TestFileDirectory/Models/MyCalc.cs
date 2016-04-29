using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

namespace TestFileDirectory.Models
{
    class MyCalc
    {
        public Dictionary<string, int> Condition { get; set; }

        public MyCalc()
        {

        }

        public Dictionary<string, int> CalculateAll(string path)
        {
            Condition = new Dictionary<string, int>();
            Condition.Add("lessThan10", 0);
            Condition.Add("from10to50", 0);
            Condition.Add("moreThan100", 0);
            if (path == "root")
                return Condition;
            ProcessDirectory(path);
            return Condition;
        }

        void ProcessDirectory(string targetDirectory)
        {
            //Simple check if path is incorrect or directory access denied
            if (!Readable(targetDirectory))
                return;
            // Process the list of files found in the directory.
            string[] fileEntries = Directory.GetFiles(targetDirectory);
            foreach (string fileName in fileEntries)
                ProcessFile(fileName);

            // Recurse into subdirectories of this directory.
            string[] subdirectoryEntries = Directory.GetDirectories(targetDirectory);
            foreach (string subdirectory in subdirectoryEntries)
                ProcessDirectory(subdirectory);
        }

        //Check file length and increase counter
        void ProcessFile(string path)
        {
            FileInfo f = new FileInfo(path);
            Console.WriteLine(f.Name + " : " + f.Length.ToString());
            if (f.Length / 1024f / 1024f < 10)
                Condition["lessThan10"]++;
            if (f.Length / 1024f / 1024f > 10 && f.Length / 1024f / 1024f < 50)
                Condition["from10to50"]++;
            if (f.Length / 1024f / 1024f > 100)
                Condition["moreThan100"]++;
        }
        
        bool Readable(string path)
        {
            try
            {
                Directory.GetFiles(path);
                Directory.GetDirectories(path);
            }
            catch (UnauthorizedAccessException) { return false; }
            catch (DirectoryNotFoundException) { return false; }
            return true;
        }


    }
}