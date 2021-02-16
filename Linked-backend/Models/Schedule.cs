using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Linked_backend.Models
{
    public class Schedule
    {
        public int ScheduleID { get; set; }
        public int Code { get; set; }
        public DateTime Datum { get; set; }
        public string Opmerking { get; set; }
        public int UserID { get; set; }
        public User User { get; set; }
    }
}
