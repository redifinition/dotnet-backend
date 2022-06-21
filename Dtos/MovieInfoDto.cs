using Google.Protobuf.WellKnownTypes;

namespace DataWarehouse.Dtos
{
    public class MovieInfoDto
    {
        public String? movieName { get; set; }
        public String? category { get; set; } 
        public List<string>? directorNames { get; set; }
        public List<string>? mainActors { get; set; }
        public List<string>? actors { get; set; }
        public string minScore { get; set; }
        public string maxScore { get; set; }
        public int? minYear { get; set; }
        public int? minMonth { get; set; }
        public int? minDay { get; set; }
        public int? maxYear { get; set; }
        public int? maxMonth { get; set; }
        public int? maxDay { get; set; }
        public int positive { get; set; }

        public DateTime? minDate { get; set; }
        public DateTime? maxDate { get; set; }
    }
}
