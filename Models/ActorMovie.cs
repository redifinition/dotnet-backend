using System;
using System.Collections.Generic;

namespace Amzaon_DataWarehouse_BackEnd.Models
{
    public partial class ActorMovie
    {
        public string ActorName { get; set; } = null!;
        public int MovieId { get; set; }
        public sbyte IsMainActor { get; set; }

        public virtual Movie Movie { get; set; } = null!;
    }
}
