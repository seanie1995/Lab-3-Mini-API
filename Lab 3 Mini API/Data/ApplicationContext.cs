using Lab_3_Mini_API.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace Lab_3_Mini_API.Data
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Persons> Persons { get; set; }
        public DbSet<Interests> Interests { get; set; }
        public DbSet<InterestUrl> InterestUrls { get; set; }
        
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<InterestLink>()
        //        .HasKey(il => new { il.PersonId, il.InterestId });

        //    modelBuilder.Entity<InterestLink>()
        //        .HasOne(il => il.Person)
        //        .WithMany(p => p.InterestLinks)
        //        .HasForeignKey(il => il.PersonId);

        //    modelBuilder.Entity<InterestLink>()
        //        .HasOne(il => il.Interest)
        //        .WithMany(i => i.InterestLinks)
        //        .HasForeignKey(il => il.InterestId);
        //}
    }
}
