using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Linked_backend.Models
{
    public class Bedrijf
    {
        public int BedrijfID { get; set; }
        public string Naam { get; set; }
        public string Adres { get; set; }
        public string BTWNummer { get; set; }
        public string Email { get; set; }
    }
}
