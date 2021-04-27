using System.Drawing;
using System.Xml.Linq;

namespace GestaoProjetos.Entities
{
    public class TextItem : BaseItem
    {
        public string Content { get; set; }
        public int FontSize { get; set; }
        public ContentAlignment Alignment { get; set; }
        public bool IsBold { get; set; }
        public bool IsItalic { get; set; }
        public bool IsUnderlined { get; set; }
        public bool IsParagraph { get; set; }

        public TextItem(int index, string content, int fontSize, ContentAlignment alignment, bool isBold, bool isItalic, bool isUnderlined, bool isParagraph) : base(index, "text")
        {
            Content = content;
            FontSize = fontSize;
            Alignment = alignment;
            IsBold = isBold;
            IsItalic = isItalic;
            IsUnderlined = isUnderlined;
            IsParagraph = isParagraph;
        }

        public override string ToString()
        {
            return $"Texto #{Index}";
        }

        public override void Save(XElement item)
        {
            int style = 0;
            if (IsBold) style += 1;
            if (IsItalic) style += 2;
            if (IsUnderlined) style += 4;
            if (IsParagraph) style += 8;

            item.Value = Content;
            item.Add(new XAttribute("font-size", FontSize));
            item.Add(new XAttribute("alignment", Alignment));
            item.Add(new XAttribute("style", style));
        }
    }
}
