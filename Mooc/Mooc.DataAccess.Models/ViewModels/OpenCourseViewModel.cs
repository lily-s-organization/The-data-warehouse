
using System;
using Mooc.DataAccess.Models.Entities;

namespace Mooc.DataAccess.Models.ViewModels
{
    public class OpenCourseViewModel
    {
       
        public long Id { get; set; }

       
     

        public DateTime StartDate { get; set; }

        public DateTime CloseDate { get; set; }
        public string SubjectName { get; set; }

        public string ShowStatus
        {
            get
            {
                if (StartDate > DateTime.Now.Date)
                {
                    return "未开课";
                }
                else if (DateTime.Now.Date >= StartDate && DateTime.Now.Date<= CloseDate)
                {
                    return "正在进行";
                }
                else if (DateTime.Now.Date > CloseDate)
                {
                    return "已结束";
                }
                else
                {
                    return "课程状态错误";
                }


            }
        }
    }
}
