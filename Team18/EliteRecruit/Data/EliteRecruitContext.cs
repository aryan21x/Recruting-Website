using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using EliteRecruit.Models;

namespace EliteRecruit.Data
{
    public class EliteRecruitContext : IdentityDbContext
    {
        public EliteRecruitContext(DbContextOptions<EliteRecruitContext> options)
            : base(options)
        {
        }

        public DbSet<EliteRecruit.Models.Student> Student { get; set; } = default!;
    }
}
