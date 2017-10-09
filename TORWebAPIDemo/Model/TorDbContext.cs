using System;
using Microsoft.EntityFrameworkCore;

namespace TORWebAPIDemo.Model
{
    public class TorDbContext : DbContext
    {
        public TorDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Level>().HasAlternateKey(c => c.Name).HasName("AlternateKey_Name");
        }

        public DbSet<Level> Levels { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<CncMember> CncMembers { get; set; }
    }
}