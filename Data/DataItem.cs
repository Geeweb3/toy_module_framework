using System.IO;

namespace Database
{
    public class DataItem
    {
        public int Id { get; protected set; }

        public virtual void ParseByString(string data)
        { }
        public virtual void ParseByBytes(MemoryStream ms)
        { }
    }
}