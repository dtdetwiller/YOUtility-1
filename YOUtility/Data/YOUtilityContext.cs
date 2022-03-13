using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using YOUtility.Models;

namespace YOUtility.Data
{
    public class YOUtilityContext : DbContext
    {
        public YOUtilityContext (DbContextOptions<YOUtilityContext> options)
            : base(options)
        {
        }

        public DbSet<Bill> Bill { get; set; }
        public DbSet<YOUtility.Models.Friendship> Friendship { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Bill>().ToTable("Bill");
            modelBuilder.Entity<Friendship>().ToTable("Friendship").Property(f => f.accepted).HasDefaultValue(true);
        }
    }
}
