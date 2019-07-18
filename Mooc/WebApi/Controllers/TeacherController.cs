using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Mooc.DataAccess.Models.Context;
using Mooc.DataAccess.Models.Entities;
using WebApi.Attributes;

namespace WebApi.Controllers
{
    [ApiAuthorize]
    public class TeacherController : ApiController
    {
        private DataContext db = new DataContext();

        [SwaggerConfig.HiddenApi]
        [HttpGet]
        public IEnumerable<Teacher> Get()
        {
            List<Teacher> list = db.Teachers.ToList();

            return list;
        }

        /// <summary>
        /// 获取老师信息
        /// </summary>
        /// <param name="id">主键id</param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/t/g")]
        public Teacher Get(int id)
        {

            Teacher teacher = db.Teachers.Find(id);
            return teacher;
        }



    }
}
