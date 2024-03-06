using EliteRecruit.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EliteRecruit.ViewModels
{
    public class StudentViewModel
    {
        public StudentViewModel()
        {
            // Empty contructor.
        }

        public StudentViewModel(Student student)
        {
            if (student != null)
            {
                Id = student.Id;
                FirstName = student.FirstName;
                LastName = student.LastName;
                School = student.School;
                GPA = student.GPA;
                Major = student.Major;
                SchoolYear = student.SchoolYear;
                Email = student.Email;
                PhoneNumber = student.PhoneNumber;
            }
        }

        public int Id { get; set; }

        [Display(Name = "First Name")]
        [StringLength(30, MinimumLength = 2)]
        [Required]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [StringLength(30, MinimumLength = 2)]
        [Required]
        public string LastName { get; set; }

        [StringLength(60, MinimumLength = 3)]
        [Required]
        public string School { get; set; }

        [Range(1, 4)]
        [Column(TypeName = "decimal(3, 2)")]
        public decimal GPA { get; set; }

        [StringLength(30, MinimumLength = 3)]
        [Required]
        public string Major { get; set; }

        [Display(Name = "School Year")]
        [Required]
        public string SchoolYear { get; set; }

        [StringLength(80, MinimumLength = 10)]
        [EmailAddress]
        public string Email { get; set; }

        [Display(Name = "Phone Number")]
        [RegularExpression(@"^[0-9""'\s-]*$")]
        [StringLength(15, MinimumLength = 10, ErrorMessage = "The phone number must be minimum 10 digits long.")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Graduation Year")]
        public int GraduationYear
        {
            get
            {
                if (int.TryParse(SchoolYear, out int year))
                {
                    if (year > 4) // graduate 
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
        public string FullName
        {
            get { return string.Concat(FirstName, " ", LastName); }
        }

        public List<Student> Students { get; set; }
        public string SearchString { get; set; }

        public SelectList SchoolYear2 { get; set; }
        public string SchoolY { get; set; }
        public IEnumerable<SelectListItem> GraduationYearOptions { get; set; }

        public SelectList Major2 { get; set; }
        public String majorString { get; set; }

    }
}

