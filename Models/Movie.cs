using System;
using System.Collections.Generic;

namespace Amzaon_DataWarehouse_BackEnd.Models
{
    public partial class Movie
    {
        public Movie()
        {
            ActorMovies = new HashSet<ActorMovie>();
            Categories = new HashSet<Category>();
            DirectorMovies = new HashSet<DirectorMovie>();
            Reviews = new HashSet<Review>();
        }

        public int MovieId { get; set; }
        public string MovieName { get; set; } = null!;
        /// <summary>
        /// 电影的版本数量
        /// </summary>
        public sbyte MovieEditionNum { get; set; }
        public float MovieScore { get; set; }
        public int? TimeId { get; set; }
        public int? FormatId { get; set; }
        public string MovieAsin { get; set; } = null!;
        public string MovieEdition { get; set; } = null!;
        public int CommentNum { get; set; }
        public DateTime? TimeStr { get; set; }

        public virtual Format? Format { get; set; }
        public virtual Time? Time { get; set; }
        public virtual MovieScore MovieScoreNavigation { get; set; } = null!;
        public virtual TimeMovie TimeMovie { get; set; } = null!;
        public virtual ICollection<ActorMovie> ActorMovies { get; set; }
        public virtual ICollection<Category> Categories { get; set; }
        public virtual ICollection<DirectorMovie> DirectorMovies { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
    }
}
