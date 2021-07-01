using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS06_EF
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime? PubDate { get; set; }
        public int? Pages { get; set; }
        public int? AuthorId { get; set; }
        public Author BookAuthor { get; set; }
        public List<Genre> Genres { get; set; } = new();
        
        public Book(string title, DateTime pubDate, int pages)
        {
            Title = title;
            PubDate = pubDate;
            Pages = pages;
        }

        public Book() { }
    }
}
