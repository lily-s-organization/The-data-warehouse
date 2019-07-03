using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mooc.DataAccess.Models.Entities
{
    public class Subject
    {
        [Key]
        [DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required]
        public string SubjectName { get; set; }

        [Required]
        public int Status { get; set; }  //初始默认为0（编辑中） 1为上架 2为下架

        
        [Required]
        public int SubjectCategoryId { get; set; }

        [Required]
        public int TeacherId {get;set;}

        public string Description { get; set; }

        public DateTime? AddTime { get; set; }      

        public string PhotoUrl { get; set; }


    }
}
