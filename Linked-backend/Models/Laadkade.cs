using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Linked_backend.Models
{
    public class Laadkade
    {
        public int LaadkadeID { get; set; }
        public int Nummer { get; set; }
        public bool IsBezet { get; set; }
        public String Locatie { get; set; }
    }
}
