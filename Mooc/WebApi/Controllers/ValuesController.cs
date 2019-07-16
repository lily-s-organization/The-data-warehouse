using Mooc.DataAccess.Models.Context;
using Mooc.DataAccess.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApi.Controllers
{
    [AllowAnonymous]
    public class ValuesController : ApiController
    {
        private DataContext db = new DataContext();
        // GET api/values
        public IEnumerable<Subject> Get()
        {
            var list = db.Subjects.ToList();
            return list;
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public string Post([FromBody]string value)
        {
            return "post test";
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
