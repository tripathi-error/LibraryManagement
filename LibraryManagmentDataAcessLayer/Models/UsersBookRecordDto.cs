using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryManagmentDomainLayer.Models
{
    public class UsersBookRecordDto
    {
        public int BookId { get; set; }
        public bool IsFaviourite { get; set; }
        public bool IsRead { get; set; }
        public string UserId { get; set; }
    }
}
