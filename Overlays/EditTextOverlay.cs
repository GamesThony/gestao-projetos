using GestaoProjetos.Entities;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace GestaoProjetos.Overlays
{
    public partial class EditTextOverlay : UserControl
    {
        public string Content => txtConteudo.Text.Trim();
        public int FontSize => txtTamanho.Text.ToInt();
        public ContentAlignment Alignment
        {
            get
            {
                int index = cboAlinhamento.SelectedIndex;
                if (index < 0) index = 0;
                return _alignments[index];
            }
        }
        public bool IsBold => chkN.Checked;
        public bool IsItalic => chkI.Checked;
        public bool IsUnderlined => chkS.Checked;
        public bool IsParagraph => chkP.Checked;

        private ContentAlignment[] _alignments = new[]
        {
            ContentAlignment.MiddleLeft,
            ContentAlignment.MiddleCenter,
            ContentAlignment.MiddleRight
        };

        public EditTextOverlay(Color backColor, Action ok, Action cancelar)
        {
            InitializeComponent();
            this.InitializeDesign(backColor, txtConteudo, txtTamanho, cboAlinhamento);

            btnOK.Click += (s, e) => { Visible = false; ok(); };
            btnCancelar.Click += (s, e) => { Visible = false; cancelar(); };
        }

        public void Exibir(string content = "", int fontSize = 12, ContentAlignment alignment = ContentAlignment.MiddleLeft, bool bold = false, bool italic = false, bool underlined = false, bool paragraph = false)
        {
            Visible = true;
            BringToFront();
            Focus();

            txtConteudo.Text = content;
            txtTamanho.Text = fontSize.ToString();
            cboAlinhamento.SelectedIndex = _alignments.ToList().IndexOf(alignment);
            chkN.Checked = bold;
            chkI.Checked = italic;
            chkS.Checked = underlined;
            chkP.Checked = paragraph;
        }
    }
}
