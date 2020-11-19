using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagmentDomainLayer.Entities
{
    public class UserBooksRecord
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public bool IsFavorite { get; set; }
        public bool IsRead { get; set; }

        public virtual Book Book { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}
