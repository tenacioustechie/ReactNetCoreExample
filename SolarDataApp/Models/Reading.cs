using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SolarDataApp.Models
{
    public class ReadingModel
    {
        public int Id { get; set; }
        public int LocationId { get; set; }
        public DateTime Day { get; set; }
        public decimal SolarGenerated { get; set; }
        public decimal PowerUsed { get; set; }
    }
}
