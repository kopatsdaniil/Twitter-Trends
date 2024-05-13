using System.Text.Json;
using Twitter_Trends.Interfaces;
using Twitter_Trends.Models;

namespace Twitter_Trends.Implementations.Parsers
{
    public class PolygonsParser : IParser<State>
    {
        public List<State> Parse(string obj)
        {
            var reader = new StreamReader("C:\\Users\\kopot\\source\\repos\\Twitter Trends\\Twitter Trends\\Data\\states.json");
            var data = JsonDocument.Parse(reader.ReadToEnd());

            var points = new List<Polygon>();
            var polygons = new List<List<Polygon>>();
            var states = new List<State>();

            foreach (var abbreviation in data.RootElement.EnumerateObject())
            {
                foreach (var form in abbreviation.Value.EnumerateArray())
                {
                    var polygon = form;
                    if (polygon[0][0].ValueKind != JsonValueKind.Number) polygon = polygon[0];

                    foreach (var point in polygon.EnumerateArray())
                    {
                        points.Add(new Polygon(
                            (float)point[0].GetDouble() * 15.0f + 2800.0f,
                            (float)point[1].GetDouble() * -15.0f + 1200.0f));
                    }

                    polygons.Add(points);
                    points = new List<Polygon>();
                }

                states.Add(new State(abbreviation.Name, polygons));
                polygons = new List<List<Polygon>>();
            }

            return states;
        }
    }
}
