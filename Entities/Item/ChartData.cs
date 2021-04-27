using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms.DataVisualization.Charting;

namespace GestaoProjetos.Entities
{
    public class ChartData
    {
        public SeriesChartType Type { get; set; }
        public List<Point> Points { get; } = new List<Point>();

        public ChartData(SeriesChartType type)
        {
            Type = type;
        }

        public override int GetHashCode()
        {
            return Type.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return obj is ChartData other ? Type.Equals(other.Type) : false;
        }

        public override string ToString()
        {
            switch(Type)
            {
                case SeriesChartType.Point: return "Ponto";
                case SeriesChartType.Line: return "Linha";
                case SeriesChartType.Column: return "Coluna";
                default: return "Setor";
            }
        }
    }
}
