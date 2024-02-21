using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Team18.Models;

namespace Team18.Data
{
    public class Team18Context : IdentityDbContext
    {
        public Team18Context (DbContextOptions<Team18Context> options)
            : base(options)
        {
        }

        public DbSet<Team18.Models.Student> Student { get; set; } = default!;
    }
}
