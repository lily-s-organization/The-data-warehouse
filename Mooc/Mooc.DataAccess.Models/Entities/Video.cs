using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mooc.DataAccess.Models.Entities
{
    public class Video
    {
        [Key]
        [DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required]
        public string VideoTitle { get; set; }

        [Required]
        public Section Section { get; set; }

        [Required]
        public string FileId { get; set; }  //视频在服务器中保存的文件名 由时间日期转化得到

        
        public string OriginalFileName { get; set; }  //原始的文件名

        public string Description { get; set; }


    }
}
