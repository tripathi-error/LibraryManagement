using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LibraryManagmentDomainLayer.Models
{
    public class BookCreationDto
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Author { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public DateTime PublishedDate { get; set; }
    }
}
