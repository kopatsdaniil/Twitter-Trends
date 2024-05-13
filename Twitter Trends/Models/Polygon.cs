namespace Twitter_Trends.Models
{
    public class Polygon
    {
        public float X { get; set; }
        public float Y { get; set; }

        public Polygon(float x, float y)
        {
            X = x;
            Y = y;
        }

        public static Polygon Parse(string str)
        {
            str = str.Trim('[', ']');

            string[] parts = str.Split(',');
            if (parts.Length != 2)
            {
                throw new ArgumentException("Invalid coordinate format");
            }

            float x = float.Parse(parts[0].Trim());
            float y = float.Parse(parts[1].Trim());

            return new Polygon(x, y);
        }
    }
}
