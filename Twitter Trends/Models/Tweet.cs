namespace Twitter_Trends.Models
{
    public class Tweet
    {
        public Polygon Coordinate { get; set; }
        public DateTime Date { get; set; }
        public string Text { get; set; }
        public float Emotionality { get; set; }


        public Tweet(Polygon coordinate, DateTime date, string text, float emotionality)
        {
            Coordinate = coordinate;
            Date = date;
            Text = text;
            Emotionality = emotionality;
        }
    }
}
