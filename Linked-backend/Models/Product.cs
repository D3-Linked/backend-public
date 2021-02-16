using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Linked_backend.Models
{
    public class Product
    {
        public int ProductID { get; set; }
        public string Naam { get; set; }
        public int LeveringID { get; set; }
        public Levering Levering { get; set; }
    }
}
