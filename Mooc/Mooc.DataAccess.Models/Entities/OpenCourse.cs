using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mooc.DataAccess.Models.Entities
{
    public class OpenCourse
    {
        [Key]
        [DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required]
        public long CourseId { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime CloseDate { get; set; }       



    }
}
