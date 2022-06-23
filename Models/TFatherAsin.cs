using System;
using System.Collections.Generic;

namespace DataWarehouse.Models
{
    public partial class TFatherAsin
    {
        public string Asin { get; set; } = null!;
        public string? FatherAsin { get; set; }
    }
}
