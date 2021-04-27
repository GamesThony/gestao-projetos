using System.Xml.Linq;

namespace GestaoProjetos.Entities
{
    public class ImageItem : BaseItem
    {
        public string URL { get; set; }
        public int Height { get; set; }

        public ImageItem(int index, string url, int height) : base(index, "image")
        {
            URL = url;
            Height = height;
        }

        public override string ToString()
        {
            return $"Imagem #{Index}";
        }

        public override void Save(XElement item)
        {
            item.Value = URL;
            item.Add(new XAttribute("height", Height));
        }
    }
}
