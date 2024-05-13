using Twitter_Trends.Interfaces;

namespace Twitter_Trends.Implementations
{
    class TextParser : IParser<string>
    {
        public static string[] GetFilenames()
        {
            var filenames = Directory.GetFiles("C:\\Users\\kopot\\source\\repos\\Twitter Trends\\Twitter Trends\\Data\\Tweets");

            return filenames;
        }

        public List<string> Parse(string filename)
        {
            return File.ReadAllLines(filename).ToList();
        }
    }
}
