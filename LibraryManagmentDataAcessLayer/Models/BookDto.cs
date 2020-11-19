using LibraryManagmentDomainLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryManagmentDomainLayer.Models
{
    public class BookDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public DateTime PublishedDate { get; set; }
        public List<BookReview> BookReviews { get; set; }

    }
}
