using System;
using System.Collections.Generic;

namespace Amzaon_DataWarehouse_BackEnd.Models
{
    public partial class ViewMovieFact
    {
        public string MovieAsin { get; set; } = null!;
        public string MovieName { get; set; } = null!;
        public string FormatName { get; set; } = null!;
        public string MovieEdition { get; set; } = null!;
        public float MovieScore { get; set; }
        public int CommentNum { get; set; }
        public short Year { get; set; }
        public sbyte Month { get; set; }
        public sbyte Day { get; set; }
        public string ActorName { get; set; } = null!;
        public sbyte IsMainActor { get; set; }
        public string DirectorName { get; set; } = null!;
        public string CategoryName { get; set; } = null!;
    }
}
