namespace DataWarehouse.Dtos
{
    public class MovieResult
    {
        public string asin;
        public string title;
        public string format;
        public string edition;
        public float score;
        public int commentNum;
        public DateTime? date;

        public MovieResult(string asin, string title,string format, string edition,float score, int commentNum,DateTime? date)
        {
            this.asin = asin;
            this.title = title;
            this.format = format;
            this.edition = edition;
            this.score = score;
            this.date = date;
            this.commentNum = commentNum;
        }
    }
}
