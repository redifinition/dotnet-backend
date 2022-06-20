using System;
using System.Collections.Generic;

namespace Amzaon_DataWarehouse_BackEnd.Models
{
    public partial class TimeMovie
    {
        public int MovieId { get; set; }
        public DateTime? TimeStr { get; set; }

        public virtual Movie Movie { get; set; } = null!;
    }
}
