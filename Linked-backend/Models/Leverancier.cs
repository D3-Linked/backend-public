using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Linked_backend.Models
{
    public class Leverancier
    {
        public int LeverancierID { get; set; }
        public int Code { get; set; }
        public string Nummerplaat { get; set; }
        public int BedrijfID { get; set; }
        public Bedrijf Bedrijf { get; set; }
    }
}
