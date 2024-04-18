using EliteRecruit.Models.Identity;
using EliteRecruit.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace EliteRecruit.ViewModels
{
    public class CommentViewModel
    {
        public CommentViewModel()
        {
            // Empty contructor.
        }

        public CommentViewModel(Comment comment)
        {
            if (comment == null)
            {
                Id = comment.Id;
                CommentText = comment.Text;
                CommentEnteredOn = comment.EnteredOn;
                CommentEnteredBy = comment.ApplicationUser;
                StudentId = comment.Student.Id;
                StudentFirstName = comment.Student.FirstName;
                StudentLastName = comment.Student.LastName;
            }
        }

        public int Id { get; set; }

        [Required]
        [DisplayName("Comment")]
        public string CommentText { get; set; } = string.Empty;

        [DisplayName("Date")]
        public DateTime CommentEnteredOn { get; set; }

        [DisplayName("User")]
        public ApplicationUser CommentEnteredBy { get; set; }

        public int StudentId { get; set; }

        public string StudentFirstName { get; set; }

        public string StudentLastName { get; set; }

        public string FullName
        {
            get { return string.Concat(StudentFirstName, " ", StudentLastName); }
        }
    }
}