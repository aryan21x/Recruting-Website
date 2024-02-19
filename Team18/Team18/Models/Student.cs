using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Team18.Models
{
    public class Student
    {
        public int Id { get; set; }

        [StringLength(30, MinimumLength = 2)]
        [Required]
        public string? FirstName { get; set; }

        [StringLength(30, MinimumLength = 2)]
        [Required]
        public string? LastName { get; set; }

        [StringLength(60, MinimumLength = 3)]
        [Required]
        public string? School { get; set; }

        [Range(1, 4)]
        [Column(TypeName = "decimal(3, 2)")]
        public decimal GPA { get; set; }

        [StringLength(30, MinimumLength = 3)]
        [Required]
        public string? Major { get; set; }

        [StringLength(30, MinimumLength = 3)]
        public string? Minor { get; set; }

        [Range(1, 4)]
        [Required]
        public string? SchoolYear { get; set; }

        [StringLength(80, MinimumLength = 10)]
        [EmailAddress]
        public string? Email { get; set; }

        [RegularExpression(@"^[0-9""'\s-]*$")]
        [StringLength(15)]
        public string? PhoneNumber { get; set; }
    }
}
