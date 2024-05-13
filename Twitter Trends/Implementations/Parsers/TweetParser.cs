using System.Text.RegularExpressions;
using Twitter_Trends.Interfaces;
using Twitter_Trends.Models;

namespace Twitter_Trends.Implementations.Parsers
{
    internal class TweetParser : IParser<Tweet>
    {
        public static readonly List<Tweet> Tweets = new();

        public List<Tweet> Parse(string filename)
        {
            var tweets = TextSplitter.SplitText(filename);

            foreach (var tweet in tweets)
            {
                var coordinates = Polygon.Parse(tweet[0]);
                Tweets.Add(new Tweet(new Polygon(coordinates.Y, coordinates.X), DateTime.Parse(tweet[2]), tweet[3], SentimentsParser.GetAverageSentiment(ParseOnWords(tweet[3]))));
            }

            return Tweets;
        }

        private static List<string> ParseOnWords(string tweet)
        {
            List<string> words = new List<string>();
            Regex regex = new Regex(@"\w{1,}");
            foreach (Match match in regex.Matches(tweet)) { words.Add(match.Value.ToLower()); }
            return words;
        }
    }
}
