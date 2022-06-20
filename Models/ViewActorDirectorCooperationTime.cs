using System;
using System.Collections.Generic;

namespace Amzaon_DataWarehouse_BackEnd.Models
{
    public partial class ViewActorDirectorCooperationTime
    {
        public string ActorName { get; set; } = null!;
        public string DirectorName { get; set; } = null!;
        public long MovieCount { get; set; }
    }
}
