using CSC_Assignment2.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CSC_Assignment2.Controllers
{
    [AllowAnonymous]
    public class TalentSearchController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET api/talentsearch
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/talentsearch/5
        [HttpGet]
        [Route("api/talentsearch/{searchterm}")]
        public object Get(string searchterm)
        {
            //object matches = db.Blobs
            //              .Select(oneBlob => oneBlob.TalentName.Equals(searchterm));

            DbSqlQuery<Blob> blobObject = db.Blobs.SqlQuery("Select * from imagetable where bio like '%image%'");
            foreach(var blob in blobObject)
            {
                Console.WriteLine(blob);
            }
            return blobObject;

            //return matches;
        }

        // POST api/talentsearch
        public void Post([FromBody]string value)
        {
        }

        // PUT api/talentsearch/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
