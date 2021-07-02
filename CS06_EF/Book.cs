using System;
using System.Collections.Generic;

namespace CS06_EF
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime PubDate { get; set; }
        public int? Pages { get; set; }
        public int? AuthorId { get; set; }
        public Author BookAuthor { get; set; }
        public List<Genre> Genres { get; set; } = new();
        public Synopsis Synopsis { get; set; }

        public Book(string title, DateTime pubDate, int pages)
        {
            Title = title;
            PubDate = pubDate;
            Pages = pages;
        }

        public Book() { }
    }
}
