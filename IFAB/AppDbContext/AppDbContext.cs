﻿using IFAB.Models;
using IFAB.Models.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace IFAB.AppDbContext
{
    public class IFABDbContext : IdentityDbContext<AppUser>
    {
        public IFABDbContext(DbContextOptions<IFABDbContext> options) : base(options)
        {
        }

        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Match> Matches { get; set; }
        public DbSet<MatchReport> MatchReports { get; set; }
        public DbSet<Recusal> Recusals { get; set; }
        public DbSet<TrainingMaterial> TrainingMaterials { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); 

            modelBuilder.Entity<IdentityUserLogin<string>>().HasKey(p => new { p.LoginProvider, p.ProviderKey });

            modelBuilder.Entity<Feedback>()
                .HasOne(f => f.Match)
                .WithOne(m => m.Feedback)
                .HasForeignKey<Feedback>(f => f.MatchId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Match>()
                .HasOne(m => m.User)
                .WithMany(u => u.Matches)
                .HasForeignKey(m => m.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<MatchReport>()
                .HasOne(r => r.Match)
                .WithOne(m => m.Report)
                .HasForeignKey<MatchReport>(r => r.MatchId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Recusal>()
                .HasOne(r => r.User)
                .WithMany(u => u.Recusals)
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
