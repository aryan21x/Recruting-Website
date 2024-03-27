using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using EliteRecruit.Models;


namespace EliteRecruit.Data
{
    public class EliteRecruitContext(DbContextOptions<EliteRecruitContext> options) : IdentityDbContext(options)
    {
        public DbSet<Models.Student> Student { get; set; } = default!;
    }
}
