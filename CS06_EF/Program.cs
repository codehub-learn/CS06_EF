using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;

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
              {
                  BookAuthor = eco,
                  Genres = new() { semiology },
                  Synopsis = new Synopsis { WriterFirstName = "John", WriterLastName = "Smith" }
              };

            //Create context instance and persist entities
            
             using BooksContext context0 = new BooksContext();
              { 
                  context0.Books.AddRange(pendulum, rose, limits);
                  context0.SaveChanges();
              }

             //Queries
             using BooksContext context = new BooksContext();

             //Query to get all books
             IQueryable<Book> books = context.Books;
             List<Book> booksList = books.ToList();

             //Query to get books published in 1980
             var books1980 = context.Books
                 .Where(b => b.PubDate.Year == 1980)
                 .OrderBy(b => b.Title)
                 .ToList();

             //Query to get a single book
             Book nameOfTheRose = context.Books
                 .FirstOrDefault(b => b.Title == "The Name of the Rose");

             //Query to get a single book (Query syntax)
             Book nameOfTheRoseQ = (from b in context.Books
                                    select b)
                 .FirstOrDefault(b => b.Title == "The Name of the Rose");

            //Query to get books published in 1980 (Query Syntax)
            var books1980Q = 
                     (
                         from book in context.Books
                         where book.PubDate.Year == 1980
                         select book
                     )
                 .ToList();

            //Query to get books with title starting with The
            var booksStarWithThe = context.Books
                 .Where(b => b.Title.StartsWith("The"))
                 .ToList();

            //Using the Find() method
            Book findBook = context.Books.Find(1);

            //Update the Publisher Name
            var fixedHouseUpdate = context.Publishers
                .FirstOrDefault(p => p.Name == "Fixed House");

            if (fixedHouseUpdate != null)
            {
                fixedHouseUpdate.Name = "Galapagos Fixed House";
                context.SaveChanges();
            }

            //Delete a Publisher 
            Publisher findPublisher= context.Publishers.Find(1);
            if (findPublisher!= null) context.Remove(findPublisher);

            //Delete multiple publishers with RemoveRange()
            var fixedHouses = context.Publishers.Where(p => p.Name =="Fixed House");
            context.RemoveRange(fixedHouses);
            context.SaveChanges();

            //Adding a new book to an author
            Author author = context.Authors.First(a => a.FirstName == "Umberto" && a.LastName == "Eco");
            List<Genre> genres = context.Genres.ToList();
            Genre mysteryGenre = genres.First(g => g.Name == "Mystery");
            Genre novelGenre = genres.First(g => g.Name == "Novel");
            author.AuthoredBooks.Add(
                new Book()
                {
                    Title = "The Prague Cemetery",
                    Pages = 720,
                    Genres = new() { mysteryGenre, novelGenre }
                }
            );
            context.SaveChanges();

            var pragueBook = context.Books.Where(b => b.Title == "The Prague Cemetery").First();
            var removeBooks = author.AuthoredBooks.Remove(pragueBook);
            context.SaveChanges();

            //Query to get publishers of author eco
            var ecoPubllishers = context.Authors
                 .Where(a => a.Id==1)
                 .Select(e => e.Publishers)
                 .ToList();

            //Eager Loading
            var authorAndMore = context.Authors
                .Include(a => a.AuthoredBooks)
                .Include(a => a.Publishers)
                .ToList();

            //Anonymous Type (Projection Query)
            var projection = context.Authors.Select(
                    a => new
                    {
                        a.Id, a.FirstName, a.LastName, CountOfBooks = a.AuthoredBooks.Count
                    }
                )
                .ToList();

            var projectionReference = context.Authors.Select(
                    a => new
                    {
                        a = author,
                        CountOfBooks = a.AuthoredBooks.Count
                    }
                )
                .ToList();

            projectionReference[0].a.FirstName = "UmChanged";
            context.SaveChanges();
            context.ChangeTracker.Clear();

            //Explicit Loading
            var aBook = context.Books.First();
            context.Entry(aBook).Collection(b => b.Genres).Load();
            context.Entry(aBook).Reference(b => b.BookAuthor).Load();


            //Query to get all books with a specific genre name
            var mysteryBooks = context.Books
                 .Where(b => b.Genres.Any(g => g.Name.Contains("semiology")))
                 .Select(b => b)
                 .ToList();

             Console.WriteLine("--- Books in semiology: ---");
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
