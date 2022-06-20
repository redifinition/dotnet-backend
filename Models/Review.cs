using System;
using System.Collections.Generic;

namespace Amzaon_DataWarehouse_BackEnd.Models
{
    public partial class Review
    {
        public int ReviewId { get; set; }
        public string ReviewerName { get; set; } = null!;
        public decimal? ReviewScore { get; set; }
        public DateOnly? ReviewTime { get; set; }
        public string? ReviewSummary { get; set; }
        public string? ReviewText { get; set; }
        public sbyte? Helpfulness { get; set; }
        public int MovieId { get; set; }
        public string? MovieAsin { get; set; }
        public sbyte? IsActive { get; set; }

        public virtual Movie Movie { get; set; } = null!;
    }
}
