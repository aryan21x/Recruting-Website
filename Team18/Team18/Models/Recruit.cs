using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Team18.Models
{
    public class Recruit
    {
        public int Id { get; set; }

        [StringLength(30, MinimumLength = 2)]
        [Required]
        public string? fName { get; set; }

        [StringLength(30, MinimumLength = 2)]
        [Required]
        public string? lName { get; set; }

        [StringLength(60, MinimumLength = 3)]
        [Required]
        public string? school { get; set; }

        [Range(1, 4)]
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(1, 2)")]
        public decimal GPA { get; set; }

        [StringLength(30, MinimumLength = 3)]
        [Required]
        public string? major { get; set; }

        [StringLength(30, MinimumLength = 3)]
        public string? minor { get; set; }

        [StringLength(20, MinimumLength = 6)]
        [Required]
        public string? schoolYear { get; set; }

        //Work

        [StringLength(80, MinimumLength = 10)]
        public string? email { get; set; }

        [RegularExpression(@"^[0-9""'\s-]*$")]
        [StringLength(15)]
        public string? phoneNumber { get; set; }
    }
}
