using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SendGrid.Helpers.Mail;
using EliteRecruit.Models.Identity;


namespace EliteRecruit.Data
{
    public class EliteRecruitContext(DbContextOptions<EliteRecruitContext> options) : IdentityDbContext<ApplicationUser>(options)
    {
        public DbSet<Models.Student> Student { get; set; } = default!;
    }
}
