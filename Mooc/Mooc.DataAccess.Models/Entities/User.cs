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


        [Required]      
        [Display(Name = "邮箱")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(Name = "头像")]
        public string PhotoUrl { get; set; }

        public int UserState { get; set; }
        [Required]
        [Range(1, 3)]//1=管理员 2=老师 3=学生
        public int RoleType { get; set; }       //通过枚举来区别是学生还是老师
        public DateTime? AddTime { get; set; }       //用户注册时间
       

       

    }
}
