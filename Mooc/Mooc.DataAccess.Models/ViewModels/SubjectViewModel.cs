
using Mooc.DataAccess.Models.Entities;

namespace Mooc.DataAccess.Models.ViewModels
{
    public class SubjectViewModel
    {
        public int TeacherId { get; set; }
        public string TeacherName { get; set; }
        public int Status { get; set; }
        public int Id { get; set; }
        public string CategoryName { set; get; }
        public string SubjectName { set; get; }

        public string ShowStatus
        {
            get
            {
                if (Status == 0)
                {
                    return "编辑中";
                }
                else if(Status == 1)
                {
                    return "上架";
                }
                else if (Status == 2)
                {
                    return "下架";
                }
                else
                {
                    return "课程状态错误";
                }

               
            }
        }
    }
}
