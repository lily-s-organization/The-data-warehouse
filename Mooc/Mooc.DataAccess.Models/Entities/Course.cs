using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mooc.DataAccess.Models.Entities
{
    public class Course
    {
        [Key]
        [DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity)]//添加时自动增长
        public int CourseID { get; set; }
        public string CourseDescription { get; set; }           //课程描述
        public string VideoUrl { get; set; }                    //视频资源路径
       // public int StudyUnit { get; set; }                      //记录学到哪个单元了
       // public long StudyTime { get; set; }                     //记录在某个单元学习了多久 便于下次播放视频定位时间
       // public long TotalLearningHours { get; set; }            //学生在该课程的总学习时长
        //public DateTime? PurchasedTime { get; set; }            //购买时间
       // public bool PurchasedFlag { get; set; }                 //是否购买的标志 如果没购买  则将该课程放入购物车
      //  public User User { get; set; }
    }
}
