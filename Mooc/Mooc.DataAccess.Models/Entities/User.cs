using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Mooc.DataAccess.Models.Entities
{
    public class User
    {
        [Key]
        [DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity)]//添加时自动增长
                                                                                                          // public long Id { get; set; }
        public long Id { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "用户名长度不能超过100个字符")]
        [Display(Name = "用户名")]
        public string UserName { get; set; }

        [Required]
        [Display(Name = "密码")]
        [StringLength(20, ErrorMessage = "{0} 必须至少包含 {2} 个字符。", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string PassWord { get; set; }

        [Required(ErrorMessage = "Please re-type your password")]
        [Compare("PassWord", ErrorMessage = "password and re-type password must match")]
        [Display(Name = "确认密码")]
        [DataType(DataType.Password)]
        public string Confirm { get; set; }

        [Required]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9]+\.[A-Za-z]{2,4}", ErrorMessage = "{0}的格式不正确")]
        [Display(Name = "邮箱")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(Name = "昵称")]
        public string NickName { get; set; }





        public int UserState { get; set; }
        [Required]
        [Range(0, 1, ErrorMessage = " 字段 RoleType 必须在 0 和 1 之间,学生选0，老师选1")]
        public int RoleType { get; set; }       //通过枚举来区别是学生还是老师
        public DateTime? AddTime { get; set; }       //用户注册时间
        public int Credit { get; set; }             //用户积分
        public int LearningHours { get; set; }      //用户总学习时长
        public string PhotoUrl { get; set; }        //用户照片
        public int Phone { get; set; }

        public List<Follower> FollowingsList { get; set; }          //关注列表
        public List<Follower> FollowedList { get; set; }            //粉丝列表


        public List<Course> CourseList { get; set; }
        public List<Comment> CommentList { get; set; }         //在一门课程中，学生可以提问很多次

    }
}
