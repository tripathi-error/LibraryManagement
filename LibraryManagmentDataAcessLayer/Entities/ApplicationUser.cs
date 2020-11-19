using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagmentDomainLayer.Entities
{
    public class ApplicationUser : IdentityUser
    {
        
        
        [Required]
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }
        public string Password { get; set; }
        public UserType Type { get; set; }

        public virtual ICollection<UserBooksRecord> UserBooksRecords { get; set; }

        public ApplicationUser() {
            UserBooksRecords = new List<UserBooksRecord>();
        }


    }
}
