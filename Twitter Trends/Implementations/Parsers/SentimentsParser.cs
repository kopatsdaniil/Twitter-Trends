using CsvHelper;
using System.Globalization;
using Twitter_Trends.Models;

namespace Twitter_Trends.Implementations.Parsers
{
    class SentimentsParser
    {
        private static Dictionary<string, float> GetSentiments()
        {
            var sentiments = new Dictionary<string, float>();

            using (var reader = new StreamReader("C:\\Users\\kopot\\source\\repos\\Twitter Trends\\Twitter Trends\\Data\\sentiments.csv"))
            {
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    while (csv.Read())
                    {
                        try
                        {
                            string line = csv.GetField<string>(0);
                            float value = csv.GetField<float>(1);

                            sentiments.Add(line, value);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error reading CSV: {ex.Message}");
                        }
                    }
                }
            }

            return sentiments;
        }

        public static float GetAverageSentiment(List<string> words)
        {
            var sumSentiment = 0.0f;
            var count = words.Count;

            foreach (var word in words)
            {
                if (GetSentiments().ContainsKey(word))
                {
                    sumSentiment += GetSentiments()[word];
                    count++;
                }

                count--;
            }

            if (count == 0)
            {
                return 0;
            }

            return sumSentiment / count;
        }

        public static float GetAverageSentiment(List<Tweet> tweets)
        {
            float avg = 0;
            int count = 0;

            foreach (Tweet tweet in tweets)
            {
                if (tweet.Emotionality != 0)
                {
                    avg += tweet.Emotionality;
                    count++;
                }
            }

            if (count == 0)
            {
                return 0;
            }

            return avg / count;
        }
    }
}
