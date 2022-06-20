using System;
using System.Collections.Generic;

namespace Amzaon_DataWarehouse_BackEnd.Models
{
    public partial class DirectorMovie
    {
        public string DirectorName { get; set; } = null!;
        public int MovieId { get; set; }

        public virtual Movie Movie { get; set; } = null!;
    }
}
