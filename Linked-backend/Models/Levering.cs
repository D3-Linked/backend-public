using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Linked_backend.Models
{
    public class Levering
    {
        public int LeveringID { get; set; }
        public string Omschrijving { get; set; }
        public bool IsCompleet { get; set; }
        public int LaadkadeID { get; set; }
        public int ScheduleID { get; set; }
        public int LeverancierID { get; set; }
        public Laadkade Laadkade { get; set; }
        public Schedule Schedule { get; set; }
        public Leverancier Leverancier { get; set; }
    }
}
