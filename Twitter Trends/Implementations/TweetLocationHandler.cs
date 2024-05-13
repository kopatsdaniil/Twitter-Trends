using System.Drawing;
using Twitter_Trends.Models;

namespace Twitter_Trends.Implementations
{
    public class TweetLocationHandler
    {
        public static bool IsTweetInState(Tweet tweet, IEnumerable<Polygon> polygon)
        {
            var testPoint = new PointF(
                (float)Convert.ToDouble(tweet.Coordinate.X) * 15.0f + 2800.0f,
                (float)Convert.ToDouble(tweet.Coordinate.Y) * -15.0f + 1200.0f
            );

            bool result = false;
            var lastPolygon = polygon.Last();

            foreach (var poly in polygon)
            {
                if ((poly.X == testPoint.X) && (poly.Y == testPoint.Y))
                    return true;

                if ((poly.Y == lastPolygon.Y) && (testPoint.Y == lastPolygon.Y))
                {
                    if ((lastPolygon.X <= testPoint.X) && (testPoint.X <= poly.X))
                        return true;

                    if ((poly.X <= testPoint.X) && (testPoint.X <= lastPolygon.X))
                        return true;
                }

                if ((poly.Y < testPoint.Y) && (lastPolygon.Y >= testPoint.Y) || (lastPolygon.Y < testPoint.Y) && (poly.Y >= testPoint.Y))
                {
                    if (poly.X + (testPoint.Y - poly.Y) / (lastPolygon.Y - poly.Y) * (lastPolygon.X - poly.X) <= testPoint.X)
                        result = !result;
                }
                lastPolygon = poly;
            }

            return result;
        }

        public static Dictionary<State, List<Tweet>> HandleLocations(List<State> states, List<Tweet> tweets)
        {
            Dictionary<State, List<Tweet>> result = new Dictionary<State, List<Tweet>>();
            foreach (Tweet tweet in tweets)
            {
                foreach (State state in states)
                {
                    List<List<Polygon>> polygons = state.Location;
                    foreach (var polygon in polygons)
                    {
                        bool flag = IsTweetInState(tweet, polygon);
                        if (flag)
                        {
                            if (result.ContainsKey(state))
                            { result[state].Add(tweet); }
                            else
                            {
                                List<Tweet> tw = new List<Tweet>();
                                tw.Add(tweet);
                                result.Add(state, tw);
                            }
                        }
                    }
                }

            }

            return result;
        }
    }
}
