using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryManagmentDomainLayer.Models
{
    public class BookReviewDto
    {
        public int BookId { get; set; }
        public string ReviewDescription { get; set; }
    }
}
