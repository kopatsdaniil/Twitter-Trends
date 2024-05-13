namespace Twitter_Trends.Implementations
{
    class TextSplitter
    {
        public static List<string[]> SplitText(string filename)
        {
            var parser = new TextParser();

            var tweets = parser.Parse(filename);
            var tweetData = new List<string[]>();

            foreach (string line in tweets)
            {
                tweetData.Add(line.Split("\t"));
            }
            
            return tweetData;
        }

    }
}
