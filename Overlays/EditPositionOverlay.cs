using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using GestaoProjetos.Entities;

namespace GestaoProjetos.Overlays
{
    public partial class EditPositionOverlay : UserControl
    {
        public bool MoveAfter => rdbA.Checked;
        public string Selecionado => cboItens.SelectedItem.ToString();

        public EditPositionOverlay(Color backColor, Action ok, Action cancelar)
        {
            InitializeComponent();
            this.InitializeDesign(backColor, cboItens);

            btnOK.Click += (s, e) => { Visible = false; ok(); };
            btnCancelar.Click += (s, e) => { Visible = false; cancelar(); };
        }

        public void Exibir(BaseContent content, List<BaseContent> contents)
        {
            Visible = true;
            BringToFront();
            Focus();

            lblItem.Text = content.ToString();
            cboItens.Items.Clear();
            cboItens.Items.AddRange(contents.Where(p => !p.Equals(content)).Select(p => p.ToString()).ToArray());
        }
    }
}
