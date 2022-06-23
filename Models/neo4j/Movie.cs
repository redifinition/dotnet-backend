namespace DataWarehouse.Models.neo4j
{
    public class Movie
    {
        public int commentNum { get; set; }
        public float score { get; set; }
        public float negative { get; set; }
        public int asinCount { get; set; }
        public string format { get; set; }
        public int? month { get; set; }
        public int? year { get; set; }
        public int? day { get; set; }
        public string title { get; set; }
        public string asin { get; set; }
        public float neutral { get; set; }
        public float positive { get; set; }

    }
}
