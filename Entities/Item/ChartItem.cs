using System.Collections.Generic;
using System.Xml.Linq;

namespace GestaoProjetos.Entities
{
    public class ChartItem : BaseItem
    {
        public List<ChartData> Charts { get; } = new List<ChartData>();

        public ChartItem(int index) : base(index, "chart")
        {
        }

        public override string ToString()
        {
            return $"Gráfico #{Index}";
        }

        public override void Save(XElement item)
        {
            foreach(var chart in Charts)
            {
                XElement chart_ = new XElement(chart.Type.ToString().ToLower());
                SaveChart(chart, chart_);
                item.Add(chart_);
            }
        }

        private static void SaveChart(ChartData chart, XElement chart_)
        {
            foreach (var point in chart.Points)
            {
                XElement point_ = new XElement("point");
                point_.Add(new XAttribute("x", point.X));
                point_.Add(new XAttribute("y", point.Y));
                chart_.Add(point_);
            }
        }
    }
}
