using GestaoProjetos.Entities;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace GestaoProjetos.Overlays
{
    public partial class EditTopicOverlay : UserControl
    {
        private int _topicType { get; set; }
        public int TopicType
        {
            get => _topicType;
            set
            {
                _topicType = value;
                panel2.Visible = value == 2;

                var nomes = new[] { "o projeto", "a seção", "o tópico" };
                lblNome.Text = $"Nome d{nomes[value]}:";
            }
        }

        public string Nome => txtNome.Text.Trim();
        public double Porcentagem => txtPorcentagem.Text.ToDouble();

        public EditTopicOverlay(Color color, Action ok, Action cancelar)
        {
            InitializeComponent();
            this.InitializeDesign(color, txtNome, txtPorcentagem);

            foreach (var txt in new[] { txtNome, txtPorcentagem })
            {
                txt.KeyDown += (s, e) =>
                {
                    if (e.KeyCode == Keys.Enter)
                    {
                        e.Handled = true;
                        e.SuppressKeyPress = true;

                        if (TopicType == 2 && s == txtNome) txtPorcentagem.Focus();
                        else btnOK.PerformClick();
                    }
                    if (e.KeyCode == Keys.Escape) btnCancelar.PerformClick();
                };
            }

            btnOK.Click += (s, e) => { ok(); Visible = false; };
            btnCancelar.Click += (s, e) => { cancelar(); Visible = false; };
        }

        public void Exibir(string nome = "", double porcentagem = 0)
        {
            Visible = true;
            BringToFront();
            Focus();

            txtNome.Text = nome;
            txtPorcentagem.Text = porcentagem.FormatDouble();
        }
    }
}
