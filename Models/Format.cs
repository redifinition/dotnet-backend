using System;
using System.Collections.Generic;

namespace Amzaon_DataWarehouse_BackEnd.Models
{
    public partial class Format
    {
        public Format()
        {
            Movies = new HashSet<Movie>();
        }

        public int FormatId { get; set; }
        public string FormatName { get; set; } = null!;

        public virtual ICollection<Movie> Movies { get; set; }
    }
}
