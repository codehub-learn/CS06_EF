using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace CS06_EF
{
    class Program
    {
        static void Main(string[] args)
        {
             Console.WriteLine("Hello World!");
            
            //Create entities

             Genre novel = new Genre() {Name = "Novel", Fiction = true};
             Genre mystery = new Genre() { Name = "Mystery", Fiction = true };
             Genre semiology = new Genre() {Name = "Semiology", Fiction = false};

             Publisher fixedHouse = new Publisher() {Name = "Fixed House", Language = "English"};
             Publisher dolceVita = new Publisher() {Name = "Dolce Vita", Language = "Italian"};

             Author eco = new Author() {FirstName = "Umberto", LastName = "Eco", Publishers = {fixedHouse, dolceVita}};

             Book pendulum = new Book("Foucault's Pendulum", new DateTime(1988, 3, 15), 750)
                 {BookAuthor = eco, Genres = new(){novel, mystery}};
             Book rose = new Book("The Name of the Rose", new DateTime(1980, 6, 11), 630)
                 { BookAuthor = eco, Genres = new() { novel, mystery }};
             Book limits = new Book("The Limits of Interpretation", new DateTime(1991, 7, 2), 304)
                 { BookAuthor = eco, Genres = new() { semiology } };

            //Create context instance and persist entities

             using BooksContext context = new BooksContext();
             context.Books.AddRange(pendulum, rose);
             context.SaveChanges();


            //Queries

            //Query to get all books with a specific genre name
            var mysteryBooks = context.Books
                .Where(b => b.Genres.Any(g => g.Name.Contains("Mystery")))
                .Select(b => b)
                .ToList();

            Console.WriteLine("--- Books in mystery: ---");
            foreach (var book in mysteryBooks)
            {
                Console.WriteLine(book.Title);
            }
            Console.WriteLine("--- ---");

            //Query to get all authors and their publisher
            var authorsPublishers = context.Authors
                .Select(a => a)
                .Include(a => a.Publishers)
                .ToList();

            Console.WriteLine(authorsPublishers[0]);

            Console.WriteLine("--- Authors and Publishers: ---");
            foreach (var auth in authorsPublishers)
            {
                Console.WriteLine(auth.FirstName + " " + auth.LastName);
                foreach (var pub in auth.Publishers)
                {
                    Console.WriteLine(pub.Name);
                }
            }

            Console.WriteLine("--- ---");

            //Query to edit the payload
            var author1_dolceVita =
                context.Set<AuthorPublisher>()
                    .Where(ap => ap.AuthorId == 1 && ap.PublisherPublisherKey == 2)
                    .SingleOrDefault();

            
            
                author1_dolceVita.StartDate = new DateTime(1980, 2, 1);
                context.SaveChanges();
            




        }
    }
}
