using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using GestaoProjetos.Entities;

namespace GestaoProjetos.Overlays
{
    public partial class EditChartOverlay : UserControl
    {
        private List<ChartData> _charts { get; set; } = new List<ChartData>();
        public List<ChartData> Charts => _charts;

        public EditChartOverlay(Color backColor, Action ok, Action cancelar)
        {
            InitializeComponent();
            this.InitializeDesign(backColor, txtX, txtY, cboGraficos, lstPontos);

            btnOK.Click += (s, e) => { Visible = false; ok(); };
            btnCancelar.Click += (s, e) => { Visible = false; cancelar(); };

            foreach (var txt in new[] { txtX, txtY })
            {
                txt.KeyDown += (s, e) =>
                {
                    if (e.KeyCode == Keys.Enter)
                    {
                        e.SuppressKeyPress = true;
                        e.Handled = true;

                        if (txtX.Text.Length == 0) txtX.Focus();
                        else if (txtY.Text.Length == 0) txtY.Focus();
                        else btnAddPonto.PerformClick();
                    }
                };
            }
        }

        private void AtualizarPontos()
        {
            txtX.Clear();
            txtY.Clear();
            lstPontos.Items.Clear();
            int index = cboGraficos.SelectedIndex;

            if (index != -1)
            {
                ChartData data = _charts[index];
                lstPontos.Items.AddRange(data.Points.Select(p => $"({p.X}, {p.Y})").ToArray());
            }
        }

        public void Exibir(ChartItem chart = null)
        {
            Visible = true;
            BringToFront();
            Focus();

            _charts.Clear();
            cboGraficos.Items.Clear();

            if (chart != null)
            {
                foreach (ChartData data in chart.Charts)
                {
                    _charts.Add(data);

                    string nome = data.ToString();
                    cboGraficos.Items.Add(nome);

                    btnAddGrafico.Enabled = nome != "Setor";
                    rdbS.Enabled = nome == "Setor";
                }
            }
            else
            {
                rdbL.Enabled = true;
                rdbP.Enabled = true;
                rdbC.Enabled = true;
                rdbS.Enabled = true;
            }
        }

        private void btnAddPonto_Click(object sender, EventArgs e)
        {
            int index = cboGraficos.SelectedIndex;

            if (index != -1 && txtX.Text.IsValidInt() && txtY.Text.IsValidInt())
            {
                int X = txtX.Text.ToInt();
                int Y = txtY.Text.ToInt();
                txtX.Focus();

                _charts[index].Points.Add(new Point(X, Y));
                AtualizarPontos();
            }
        }

        private void btnEdtPonto_Click(object sender, EventArgs e)
        {
            int indexGrafico = cboGraficos.SelectedIndex;
            int indexPonto = lstPontos.SelectedIndex;
            string X = txtX.Text;
            string Y = txtY.Text;

            if (indexGrafico != -1 && indexPonto != -1 && X.IsValidInt() && Y.IsValidInt())
            {
                _charts[indexGrafico].Points[indexPonto] = new Point(X.ToInt(), Y.ToInt());
                AtualizarPontos();
            }
        }

        private void btnDelPonto_Click(object sender, EventArgs e)
        {
            int indexGrafico = cboGraficos.SelectedIndex;
            int indexPonto = lstPontos.SelectedIndex;

            if (indexGrafico != -1 && indexPonto != -1)
            {
                _charts[indexGrafico].Points.RemoveAt(indexPonto);
                AtualizarPontos();
            }
        }

        private void btnDelGrafico_Click(object sender, EventArgs e)
        {
            int index = cboGraficos.SelectedIndex;
            if (index != -1)
            {
                _charts.RemoveAt(index);
                cboGraficos.Items.RemoveAt(index);
                lstPontos.Items.Clear();

                if (!_charts.Contains(new ChartData(SeriesChartType.Pie)))
                {
                    rdbP.Enabled = true;
                    rdbL.Enabled = true;
                    rdbC.Enabled = true;
                    rdbS.Enabled = _charts.Count == 0;
                    btnAddGrafico.Enabled = true;
                }
            }
        }

        private void btnAddGrafico_Click(object sender, EventArgs e)
        {
            SeriesChartType type;

            if (rdbS.Checked)
            {
                rdbL.Enabled = false;
                rdbP.Enabled = false;
                rdbC.Enabled = false;
                type = SeriesChartType.Pie;
            }
            else
            {
                if (rdbL.Checked) type = SeriesChartType.Line;
                else if (rdbC.Checked) type = SeriesChartType.Column;
                else type = SeriesChartType.Point;
            }

            ChartData data = new ChartData(type);
            _charts.Add(data);

            string nome = data.ToString();
            rdbS.Enabled = false;
            btnAddGrafico.Enabled = nome != "Setor";
            cboGraficos.Items.Add(nome);
        }

        private void cboGraficos_SelectedIndexChanged(object sender, EventArgs e)
        {
            AtualizarPontos();
        }

        private void lstPontos_SelectedIndexChanged(object sender, EventArgs e)
        {
            int indexGrafico = cboGraficos.SelectedIndex;
            int indexPonto = lstPontos.SelectedIndex;

            ChartData chart = _charts[indexGrafico];
            var point = chart.Points[indexPonto];

            txtX.Text = point.X.ToString();
            txtY.Text = point.Y.ToString();
        }
    }
}
