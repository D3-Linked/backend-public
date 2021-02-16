using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Linked_backend.Models
{
    public class User
    {
        public int UserID { get; set; }
        public string Naam { get; set; }
        public string Email { get; set; }
        public string Paswoord { get; set; }
        [NotMapped]
        public string Token { get; set; }
        public int RoleID { get; set; }
        public Role Role { get; set; }
    }
}
