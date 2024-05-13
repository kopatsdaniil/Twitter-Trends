using System.Drawing;
using System.Drawing.Imaging;
using Twitter_Trends.Implementations;
using Twitter_Trends.Implementations.Parsers;
using Twitter_Trends.Implementations.Services;
using Twitter_Trends.Models;

public class MapDrawer
{
    private int width;
    private int height;
    private PolygonsParser parser = new PolygonsParser();

    public MapDrawer(int width, int height)
    {
        this.width = width;
        this.height = height;
    }

    public void DrawMap(string outputPath, List<Tweet> tweets)
    {
        var states = parser.Parse(null);
        
        using (var bitmap = new Bitmap(width, height))
        {
            using (var graphics = Graphics.FromImage(bitmap))
            {
                graphics.Clear(Color.White);
                Dictionary<State, List<Tweet>> handledTweets = TweetLocationHandler.HandleLocations(states, tweets);

                foreach(var state in states)
                {
                    foreach (var polygons in state.Location)
                    {
                        var points = new List<PointF>();
                        foreach (var point in polygons)
                        {
                            points.Add(new PointF((float)point.X, (float)point.Y));
                        }

                        using (var brush = new SolidBrush(Color.Gray))
                        {
                            graphics.FillPolygon(brush, points.ToArray());
                            graphics.DrawPolygon(new Pen(Color.Black), points.ToArray());
                        }
                    }
                }

                foreach (var pair in handledTweets)
                {
                    var averageSentiment = SentimentsParser.GetAverageSentiment(pair.Value);
                    var state = pair.Key;

                    foreach(var polygons in state.Location)
                    {
                        var points = new List<PointF>();
                        foreach (var point in polygons)
                        {
                            points.Add(new PointF((float)point.X, (float)point.Y));
                        }

                        using (var brush = new SolidBrush(ColorService.GetColor(averageSentiment)))
                        {
                            graphics.FillPolygon(brush, points.ToArray());
                            graphics.DrawPolygon(new Pen(Color.Black), points.ToArray());
                        }
                    }
                }
            }

            bitmap.Save(outputPath, ImageFormat.Png);
        }
    }
}
