using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MONKEY5.BusinessObjects
{
    public class CompletionImage
    {
        [Key]
        public Guid ImageId { get; set; } = Guid.NewGuid();

        [Required]
        public Guid AssignmentId { get; set; }

        [ForeignKey("AssignmentId")]
        public TaskAssignment? TaskAssignment { get; set; }

        [Required]
        public string? ImageUrl { get; set; }
    }
}
