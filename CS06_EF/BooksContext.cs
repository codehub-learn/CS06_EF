using System;
using Microsoft.EntityFrameworkCore;

namespace CS06_EF
{
    public class BooksContext : DbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Publisher> Publishers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) 
        {
            optionsBuilder.UseSqlServer
                ("Data Source = localhost; Initial Catalog = CS06_Data; User ID = User; Password = pass");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Publisher>()
                .HasKey(p => p.PublisherKey);
            modelBuilder.Entity<Author>()
                .HasMany(a => a.Publishers)
                .WithMany(p => p.Authors)
                .UsingEntity<AuthorPublisher>(
                    ap => ap.HasOne<Publisher>().WithMany(),
                    ap => ap.HasOne<Author>().WithMany()
                )
                .Property(ap => ap.StartDate)
                .HasDefaultValue("getdate()");

        }
    }
}