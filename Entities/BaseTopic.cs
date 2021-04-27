using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Xml.Linq;

namespace GestaoProjetos.Entities
{
    public abstract class BaseTopic : BaseContent
    {
        public string Name { get; set; }
        public abstract double Percentage { get; set; }
        public abstract Color BackColor { get; }
        public abstract Color OverlayColor { get; }
        public List<BaseContent> Contents { get; } = new List<BaseContent>();

        public BaseTopic(string name, string type) : base(type)
        {
            Name = name;
        }

        public sealed override int GetHashCode()
        {
            return Name.GetHashCode();
        }

        public sealed override bool Equals(object obj)
        {
            return obj is BaseTopic other ? Name.Equals(other.Name) : false;
        }

        public sealed override string ToString()
        {
            return Name;
        }

        public sealed override void Save(XElement item)
        {
            foreach(var content in Contents)
            {
                XElement content_ = new XElement(content.Type);

                if (content is BaseTopic other)
                {
                    content_.Add(new XAttribute("name", other.Name));
                    content_.Add(new XAttribute("percentage", other.Percentage));
                }

                content.Save(content_);
                item.Add(content_);
            }
        }

        public BaseTopic GetItem(string name)
        {
            var topics = Contents.Where(p => p is BaseTopic).Select(p => p as BaseTopic);
            return topics.Where(p => p.Name == name).First();
        }

        public BaseItem GetItem(int index, string type)
        {
            var items = Contents.Where(p => p.Type == type).Select(p => p as BaseItem);
            return items.Where(p => p.Index == index).First();
        }

        public int GetFreeIndex(string type)
        {
            var items = Contents.Where(p => p.Type == type).Select(p => p as BaseItem);
            return items.Count() == 0 ? 0 : items.Last().Index + 1;
        }
    }
}
