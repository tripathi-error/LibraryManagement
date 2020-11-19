using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagmentDomainLayer.Entities
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public DateTime PublishedDate { get; set; }
        public virtual ICollection<BookReview> BookReviews { get; set; }

        public virtual ICollection<UserBooksRecord> UserBooksRecords { get; set; }

        public Book() {
            BookReviews = new List<BookReview>();
            UserBooksRecords = new List<UserBooksRecord>();
        }
    }
}
