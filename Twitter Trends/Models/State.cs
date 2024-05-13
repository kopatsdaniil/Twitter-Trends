namespace Twitter_Trends.Models
{
    public class State
    {
        public string Name;
        public List<List<Polygon>> Location;

        public State(string name, List<List<Polygon>> location)
        {
            Name = name;
            Location = location;
        }
    }
}
