using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mooc.DataAccess.Models.Entities
{
    public class Teacher
    {
        [Key]
        [DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required]
        public string TeacherName { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string PhotoUrl { get; set;}

        [Required]
        [Range(1, 3)]//1=教授 2=副教授 3=讲师
        public int Title { get; set; }

        [Required]
        [Range(1, 3)]//1=清华大学 2=北京大学 3=同济大学
        public int Department { get; set; }

        [Required]//讲师个人简介
        public string Description { get; set; }

        public int UserState { get; set; }  //0为正常 1为异常

        public DateTime? AddTime { get; set; }       //用户注册时间

    }
}
