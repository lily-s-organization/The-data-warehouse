using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mooc.DataAccess.Models.Entities
{
    public class Comment
    {
        public int CommentId { get; set; }
        public int AccodringCourseId { get; set; }          //记录该条评论相关的课程

        public string Content { get; set; }                 //评论内容
        public DateTime PostTime { get; set; }              //发布时间
        public string Answer { get; set; }                  //老师的回答
        public DateTime? AnsweredTime { get; set; }         //回答的时间

        public User User { get; set; }

    }
}
