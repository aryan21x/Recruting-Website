using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace EliteRecruit.Data
{
    public class EliteRecruitContext(DbContextOptions<EliteRecruitContext> options) : IdentityDbContext(options)
    {
        public DbSet<Models.Student> Student { get; set; } = default!;
    }
}
