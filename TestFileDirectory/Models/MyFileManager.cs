using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

namespace TestFileDirectory.Models
{
    public class MyFileManager
    {
        
        public string CurrentPath { get; set; }
        public string Parent { get; set; }
        public List<MyFile> Files { get; set; }
        public List<MyDirectory> Subs { get; set; }

        public MyFileManager()
        {
            Files = new List<MyFile>();
            Subs = new List<MyDirectory>();
        }


        public List<Object> GetAll()
        {

            return null;
        }

        // Gets folders and files in one list
        public List<Object> GetAll(string path)
        {
            
            
            List<Object> lstFolder = new List<Object>();
            //If user need to see Drives
            if (path == "root")
            {
                lstFolder.AddRange(GetDrives());
                return lstFolder;
            }                
            CurrentPath = path;
            
            //Check if we see drive then parent will become "root"
            DirectoryInfo din = new DirectoryInfo(CurrentPath);            
            if (din.Parent != null)
                Parent = din.Parent.FullName;
            else
                Parent = "root";
          
            lstFolder.Add(new MyDirectory { Name = "..", Path = CurrentPath, Parent = this.Parent });                                                        
            lstFolder.AddRange(GetFolders(CurrentPath));
            lstFolder.AddRange(GetFiles(CurrentPath));
            return lstFolder;
        }

        public List<MyFile> GetFiles(string path)
        {
            if (!Readable(path))
                return Files;
            string[] fileEntries = Directory.GetFiles(path);
            foreach (string fileName in fileEntries)
            {
                FileInfo fInfo = new FileInfo(fileName);
                Files.Add(new MyFile { Name = fInfo.Name, Length=fInfo.Length, Path=fileName });
            }
            return Files;
        }

        public List<MyDirectory> GetFolders(string path)
        {            
            if (!Readable(path))
                return Subs;
            foreach (var item in Directory.GetDirectories(path))
            {
                DirectoryInfo dInfo = new DirectoryInfo(item);
                Subs.Add(new MyDirectory { Path = path, Name = dInfo.Name, Parent=dInfo.Parent.Name });
            }
            return Subs;
        }

        //Gets all drive letters
        public List<MyDirectory> GetDrives()
        {
            
            List<MyDirectory> drives = new List<MyDirectory>();
            foreach (var item in DriveInfo.GetDrives())
            {
                drives.Add(new MyDirectory { Name = item.Name, Parent=null, Path = "root"});
            }
            CurrentPath = "root";
            return drives;
        }

        //Simple check if path is incorrect or directory access denied
        bool Readable(string path)
        {
            try
            {
                Directory.GetFiles(path);
                Directory.GetDirectories(path);
            }
            catch (UnauthorizedAccessException)
            { return false; }
            catch (DirectoryNotFoundException) { return false; }
            return true;
        }

        bool UpperLevel(string path)
        {
            if (new DirectoryInfo(path).Parent != null)
                return true;
            else
                return false;
        }

    }
}