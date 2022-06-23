using System;
using System.Collections.Generic;

namespace DataWarehouse.Models
{
    public partial class TComment
    {
        public string? Verified { get; set; }
        public float? Overall { get; set; }
        public string? ReviewTime { get; set; }
        public string? ReviewerId { get; set; }
        public string? Asin { get; set; }
        public string? ReviewText { get; set; }
        public string? Style { get; set; }
        public string? ReviewerName { get; set; }
        public string? Summary { get; set; }
        public string? UnixReviewTime { get; set; }
        public string? Vote { get; set; }
        public string? Image { get; set; }
        public int CommentId { get; set; }
    }
}
