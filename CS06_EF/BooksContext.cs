using Microsoft.EntityFrameworkCore;

namespace CS06_EF
{
    public class BooksContext : DbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) 
        {
            optionsBuilder.UseSqlServer
                ("Data Source = localhost; Initial Catalog = CS06_Data; User ID = User; Password = pass");
        }
    }
}