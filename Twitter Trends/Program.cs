using Twitter_Trends.Implementations;
using Twitter_Trends.Implementations.Parsers;

class Program
{
    static private void Main()
    {
        MapDrawer mapDrawer = new MapDrawer(2000, 1000);

        var parser = new TweetParser();
        parser.Parse(TextParser.GetFilenames()[0]);

        mapDrawer.DrawMap("C:\\Users\\kopot\\source\\repos\\Twitter Trends\\Twitter Trends\\Maps\\cali_map.png", TweetParser.Tweets);
    }
}