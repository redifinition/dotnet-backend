using System;
using System.Collections.Generic;

namespace Amzaon_DataWarehouse_BackEnd.Models
{
    public partial class Time
    {
        public Time()
        {
            Movies = new HashSet<Movie>();
        }

        public int TimeId { get; set; }
        public short Year { get; set; }
        public sbyte Month { get; set; }
        public sbyte Day { get; set; }
        public sbyte Season { get; set; }
        public sbyte Weekday { get; set; }
        public DateTime TimeStr { get; set; }

        public virtual ICollection<Movie> Movies { get; set; }
    }
}
