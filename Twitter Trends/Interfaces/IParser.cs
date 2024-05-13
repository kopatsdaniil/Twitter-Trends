namespace Twitter_Trends.Interfaces
{
    interface IParser<T> where T : class
    {
        public abstract List<T> Parse(string obj);
    }
}
