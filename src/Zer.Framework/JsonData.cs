namespace Zer.Framework
{
    public class JsonData
    {
        public int C { get; set; }
        public string Msg { get; set; }
        public object Data { get; set; }
    }

    public class JsonData<T> where T : class
    {
        public int C { get; set; }
        public string Msg { get; set; }
        public T Data { get; set; }
    }


    public class JsonPageData
    {
        public int C { get; set; }
        public string Msg { get; set; }
        public int PgIndex { get; set; }
        public int PgSize { get; set; }
        public int Count { get; set; }
        public object Data { get; set; }
    }
}