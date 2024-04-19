using EliteRecruit.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static EliteRecruit.Helpers.Enums;
using EliteRecruit.Models.Identity;
using System.Xml.Linq;
using System.ComponentModel;

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
                ImagePath = student.ImagePath;
                Comments = student.Comments;
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

        [DisplayName("Comment")]
        public string CommentText { get; set; } = string.Empty;

        [DisplayName("Date")]
        public DateTime CommentEnteredOn { get; set; }

        [DisplayName("User")]
        public ApplicationUser CommentEnteredBy { get; set; }
        public string FullName
        {
            get { return string.Concat(FirstName, " ", LastName); }
        }

        public IEnumerable<Student> Students { get; set; }
        public IEnumerable<Comment> Comments { get; set; }
        public string searchString { get; set; }

        public SelectList SchoolYearList { get; set; }
        public string SchoolYearString { get; set; }

        public SelectList MajorList { get; set; }
        public string majorString { get; set; }

        public IEnumerable<SelectListItem> GraduationYearOptions = new List<SelectListItem>
            {
                new SelectListItem { Value = "1", Text = "2027" },
                new SelectListItem { Value = "2", Text = "2026" },
                new SelectListItem { Value = "3", Text = "2025" },
                new SelectListItem { Value = "4", Text = "2024" },
                new SelectListItem { Value = "5", Text = "Graduate" }
            };

        public string FilterBy { get; set; }
        public SortByParameter SortBy { get; set; }
        public SortByParameter SortByFirstName { get; set; }
        public SortByParameter SortByLastName { get; set; }
        public SortByParameter SortByGPA { get; set; }

        [Display(Name = "Image")]
        public string ImagePath { get; set; }
        public IFormFile Image { get; set; }
        public bool ClearImagePath { get; set; }
        public List<Student> Top5Students { get; set; }
    }
}

