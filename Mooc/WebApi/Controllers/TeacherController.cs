using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Mooc.DataAccess.Models.Context;
using Mooc.DataAccess.Models.Entities;

namespace WebApi.Controllers
{
    public class TeacherController : ApiController
    {
        private DataContext db = new DataContext();
        [HttpGet]
        public IEnumerable<Teacher> Get()
        {
            List<Teacher> list = db.Teachers.ToList();

            return list;
        }

        [HttpGet]
        public Teacher Get(int id)
        {
            Teacher teacher = db.Teachers.Find(id);
            return teacher;
        }
    }
}
