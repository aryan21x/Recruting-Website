
using EliteRecruit.Models.Identity;

namespace EliteRecruit.Models
{
    public class Comment
    {
        public Comment()
        {
            Text = string.Empty;
        }
        public int Id { get; set; }

        public DateTime EnteredOn { get; set; }

        public string Text { get; set; }

        public virtual Student Student { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }
        public string EnteredBy
        {
            get
            {
                return ApplicationUser == null ? "Not Set" : string.Concat(ApplicationUser.FirstName, " ", ApplicationUser.LastName);
            }
        }
    }
}