using System;
using System.Collections.Generic;

namespace Amzaon_DataWarehouse_BackEnd.Models
{
    public partial class ActorActor
    {
        public string FirstActorName { get; set; } = null!;
        public string SecondActorName { get; set; } = null!;
        public long MovieCount { get; set; }
    }
}
