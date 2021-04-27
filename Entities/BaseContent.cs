using System.Xml.Linq;

namespace GestaoProjetos.Entities
{
    public abstract class BaseContent
    {
        public string Type { get; }

        public BaseContent(string type)
        {
            Type = type;
        }

        public abstract void Save(XElement item);
    }
}
