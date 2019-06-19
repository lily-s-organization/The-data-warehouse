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
      
        public string Email { get; set; }
      
        public string PhotoUrl { get; set;}
   
        public string Level { get; set; }

        public string Department { get; set; }

        public string Description { get; set; }

        [Required]
        public int State { get; set; }  //0为正常 1为异常

        [Required]
        public DateTime? AddTime { get; set; }       //用户注册时间

    }
}
