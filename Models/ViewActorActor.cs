using System;
using System.Collections.Generic;

namespace Amzaon_DataWarehouse_BackEnd.Models
{
    public partial class ViewActorActor
    {
        public int MovieId { get; set; }
        public string Actor1 { get; set; } = null!;
        public string Actor2 { get; set; } = null!;
    }
}
