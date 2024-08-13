using CPRG211_Final_Library.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace LibraryDAL.Data
{
    internal class LibraryDbContext : DbContext
    {
        public LibraryDbContext(DbContextOptions<LibraryDbContext> options) : base(options) { }

        public DbSet<Book> Books { get; set; }
        public DbSet<DVD> DVDs { get; set; }
        public DbSet<Videogame> Videogames { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Book>()
                .Property(b => b.ISBN)
                .IsRequired()
                .HasMaxLength(13);

            modelBuilder.Entity<Book>()
                .HasIndex(b => b.ISBN)
                .IsUnique();


            modelBuilder.Entity<DVD>()
                .Property(d => d.ISBN)
                .IsRequired()
                .HasMaxLength (13);

            modelBuilder.Entity<DVD>()
                .HasIndex(d => d.ISBN)
                .IsUnique();

            modelBuilder.Entity<Videogame>()
                .Property(v => v.ISBN)
                .IsRequired()
                .HasMaxLength (13);

            modelBuilder.Entity<Videogame>()
                .HasIndex (v => v.ISBN)
                .IsUnique();


        }
    }
}
