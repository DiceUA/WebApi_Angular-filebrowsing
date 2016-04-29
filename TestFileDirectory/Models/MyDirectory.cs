using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

namespace TestFileDirectory.Models
{
    public class MyDirectory
    {
        //simple realisation
        public string Path { get; set; }
        public string Parent { get; set; }
        public string Name { get; set; }
        
        public MyDirectory ()
        {

        }
        public MyDirectory(string path, string parent, string name)
        {
            Path = path;
            Name = name;
            Parent = parent;
        }

    }
}