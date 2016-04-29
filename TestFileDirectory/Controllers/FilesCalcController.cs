using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.IO;
using TestFileDirectory.Models;

namespace TestFileDirectory.Controllers
{
    public class FilesCalcController : ApiController
    {
        string path = Directory.GetCurrentDirectory();
        //string pathDebug = "I:\\Video\\";
        

        public FilesCalcController()
        {

        }

        // GET api/<controller>

        public Dictionary<string, int> Get()
        {            
            return new MyCalc().CalculateAll(path);
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public Dictionary<string, int> Post([FromBody]UpdateFolder folder)
        {
            return new MyCalc().CalculateAll(folder.Path);
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }
    }    
}
