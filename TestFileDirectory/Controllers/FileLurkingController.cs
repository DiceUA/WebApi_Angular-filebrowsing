using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TestFileDirectory.Models;

namespace TestFileDirectory.Controllers
{
    public class FileLurkingController : ApiController
    {
        //Set path to current directory
        string path = Directory.GetCurrentDirectory();
        //string pathDebug = "I:\\Video\\";
        
        public FileLurkingController ()
        {

        }

        // GET api/<controller>

        public IEnumerable<Object> Get()
        {
            return new MyFileManager().GetAll(path);
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>

        public IEnumerable<Object> Post([FromBody]UpdateFolder folder)
        {
            return new MyFileManager().GetAll(folder.Path);
        }


        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }
    }
}
