using System;
using System.Collections.Generic;

namespace Amzaon_DataWarehouse_BackEnd.Models
{
    public partial class ViewActorDirector
    {
        public int MovieId { get; set; }
        public string ActorName { get; set; } = null!;
        public string DirectorName { get; set; } = null!;
    }
}
