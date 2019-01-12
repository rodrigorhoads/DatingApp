using DatingApp.API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DatingApp.API.Data
{
    public class DataContext:DbContext
    {
        public DataContext(DbContextOptions<DataContext> options):base(options){}

            public DbSet<Values> Values { get; set; }        
            public DbSet<User> Users {get;set;}
            public DbSet<Photo> Photos { get; set; }
            public DbSet<Like> Likes { get; set; }

        /// <summary>
        /// u = User
        /// l= Like
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Like>()
                .HasKey(k => new { k.LikerId, k.LikeeId });
            
            modelBuilder.Entity<Like>()
                .HasOne(l => l.Likee)
                .WithMany(u => u.Likers)
                .HasForeignKey(l => l.LikeeId)
                .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<Like>()
                .HasOne(l => l.Liker)
                .WithMany(u => u.Likees)
                .HasForeignKey(l => l.LikerId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<User>()
                .HasKey(u => new { u.Id });
        }
    }
}
