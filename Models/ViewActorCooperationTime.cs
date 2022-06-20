using System;
using System.Collections.Generic;

namespace Amzaon_DataWarehouse_BackEnd.Models
{
    public partial class ViewActorCooperationTime
    {
        public string ActorName1 { get; set; } = null!;
        public string ActorName2 { get; set; } = null!;
        public long CooperTime { get; set; }
    }
}
