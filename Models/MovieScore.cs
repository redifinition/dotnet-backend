using System;
using System.Collections.Generic;

namespace Amzaon_DataWarehouse_BackEnd.Models
{
    public partial class MovieScore
    {
        public int MovieId { get; set; }
        public float MovieScore1 { get; set; }
        public float PositiveCommentRating { get; set; }
        public float NegativeCommentRating { get; set; }

        public virtual Movie Movie { get; set; } = null!;
    }
}
