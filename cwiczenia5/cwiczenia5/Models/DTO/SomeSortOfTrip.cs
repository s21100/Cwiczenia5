using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cwiczenia5.Models.DTO
{
    public class SomeSortOfTrip
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public int MaxPeople { get; set; }
        public IEnumerable<SomeSortOfCountry> Countries { get; set; }
        public IEnumerable<SomeSortOfClient> Clients { get; set; }
    }
}
