using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EliteRecruit.Models
{
    public class Student
    {
        public int Id { get; set; }

        [Display(Name = "First Name")]
        [StringLength(30, MinimumLength = 2)]
        [Required]
        public string? FirstName { get; set; }

        [Display(Name = "Last Name")]
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

        [Display(Name = "School Year")]
        [Required]
        public string? SchoolYear { get; set; }

        [StringLength(80, MinimumLength = 10)]
        [EmailAddress]
        public string? Email { get; set; }

        [Display(Name = "Phone Number")]
        [RegularExpression(@"^[0-9""'\s-]*$")]
        [StringLength(15)]
        public string? PhoneNumber { get; set; }

        public int GraduationYear
        {
            get
            {
                if (int.TryParse(SchoolYear, out int year))
                {
                    if( year > 4) // graduate 
                    {
                        return DateTime.Now.Year + 2;
                    }
                    return DateTime.Now.Year + (4 - year);
                }
                else
                {
                    return 0;
                }
            }
        }

    }
}
