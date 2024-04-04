using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EliteRecruit.Models
{
    public class Watched
    {
        [Key]
        [ForeignKey("Student")]
        public int StudentID { get; set; }
        
        [Key]
        [ForeignKey("Student")]
        public int FirstName { get; set; }

        [Key]
        [ForeignKey("Student")]
        public int LastName { get; set; }

        [Key]
        [ForeignKey("Student")]
        public int School { get; set; }

        [Key]
        [ForeignKey("Student")]
        public int GPA { get; set; }

        [Key]
        [ForeignKey("Student")]
        public int Major { get; set; }

        [Key]
        [ForeignKey("Student")]
        public int SchoolYear { get; set; }

        public bool IsWatched { get; set; }
    }
}
