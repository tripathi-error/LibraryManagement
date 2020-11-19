using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagmentDomainLayer.Entities
{
    public class BookReview
    {
        public int Id { get; set; }
        public string ReviewDescription { get; set; }

        public int BookId { get; set; }

        public virtual Book Book { get; set; }
    }
}
