using System;

namespace CS06_EF
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            Author eco = new Author() {FirstName = "Umberto", LastName = "Eco"};
            Book pendulum = new Book("Foucault's Pendulum", new DateTime(1988, 3, 15), 750)
                {BookAuthor = eco};
            Book rose = new Book("The Name of the Rose", new DateTime(1980, 6, 11), 630)
                { BookAuthor = eco };

            using BooksContext context = new BooksContext();
            context.Books.AddRange(pendulum, rose);
            context.SaveChanges();
        }
    }
}
