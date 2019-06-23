using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mooc.DataAccess.Models.Entities
{
    public class Section
    {
        [Key]
        [DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required]
        public string SectionName { get; set; }

        [Required]
        public Subject Subject { get; set; }

        public string Description { get; set; }
    }
}
