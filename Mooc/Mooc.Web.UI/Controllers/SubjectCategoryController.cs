using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using log4net;
using Mooc.DataAccess.Models.Context;

namespace Mooc.Web.UI.Controllers
{
    public class SubjectCategoryController : Controller
    {

        private DataContext db = new DataContext();
        private ILog logger = LogManager.GetLogger(typeof(SubjectCategoryController));
        // GET: SubjectCategory
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult GetList()
        {
            var categoryList = db.SubjectCategorys.Where(x => x.Id > 0).OrderByDescending(x => x.Id).Take(7).ToList();

            ArrayList subjectList = new ArrayList();
            ArrayList resultList = new ArrayList();
           
            foreach (var item in categoryList)
            {
                
                subjectList.Add(db.Subjects.Where(x => x.SubjectCategoryId == item.Id).ToList());
                resultList.Add(item.CategoryName);
                resultList.Add(subjectList);
                
            }
           
            return Json(categoryList);
        }
    }
}