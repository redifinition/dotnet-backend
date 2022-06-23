using System;
using System.Collections.Generic;

namespace DataWarehouse.Models
{
    public partial class TMovieWithRemovingTv
    {
        public string Asin { get; set; } = null!;
        public string? MovieTitle { get; set; }
        public string? MovieEdition { get; set; }
        public string? MovieFormat { get; set; }
        public float? MovieScore { get; set; }
        public string? Director { get; set; }
        public string? MainActor { get; set; }
        public string? Actor { get; set; }
        public string? MovieCategory { get; set; }
        public string? MovieReleaseDate { get; set; }
        public string? NearAsin { get; set; }
        public int? CommentNum { get; set; }
        public float? DbRaringScore { get; set; }
    }
}
