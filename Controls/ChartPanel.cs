using GestaoProjetos.Entities;
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace GestaoProjetos.Controls
{
    public partial class ChartPanel : BaseItemControl
    {
        Chart chrChart = new Chart()
        {
            BackColor = Color.Transparent,
            Cursor = Cursors.Hand,
            Dock = DockStyle.Top,
            Height = 250
        };

        public ChartPanel(int index, Color selectionColor, Action<BaseControl> selecionar) : base(index, "chart", selectionColor, selecionar)
        {
            chrChart.Click += (s, e) => Selecionar(this);
            Controls.Add(chrChart);
            Height = 255;

            if (chrChart.ChartAreas.Count == 0) chrChart.ChartAreas.Add(new ChartArea());
            ChartArea area = chrChart.ChartAreas[0];
            foreach (Axis axis in area.Axes)
            {
                axis.LabelStyle = new LabelStyle() { ForeColor = Color.White };
                axis.LineColor = Color.White;
                axis.MajorGrid.LineColor = Color.Transparent;
                axis.MajorTickMark.LineColor = Color.White;
                axis.MinorGrid.LineColor = Color.Transparent;
                axis.MinorTickMark.LineColor = Color.White;
            }
            area.BackColor = Color.Transparent;
        }

        public override void Atualizar(BaseContent content)
        {
            if (content is ChartItem chart)
            {
                if (chrChart.InvokeRequired) chrChart.Invoke(SetChart(chart));
                else SetChart(chart).Invoke();
            }
        }

        private Action SetChart(ChartItem chart) => new Action(() =>
        {
            chrChart.BackColor = Selected ? SelectionColor : Color.Transparent;
            chrChart.Series.Clear();

            foreach (ChartData data in chart.Charts)
            {
                Series series = new Series()
                {
                    ChartType = data.Type,
                    IsVisibleInLegend = false,
                    LabelForeColor = Color.White
                };

                series.IsValueShownAsLabel = series.ChartType == SeriesChartType.Point || series.ChartType == SeriesChartType.Pie;
                foreach (Point point in data.Points) series.Points.AddXY(point.X, point.Y);
                chrChart.Series.Add(series);
            }
        });
    }
}
