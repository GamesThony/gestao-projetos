using GestaoProjetos.Entities;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace GestaoProjetos.Controls
{
    public abstract partial class BaseControl : Panel
    {
        public bool Selected { get; set; }
        public Color SelectionColor { get; set; }
        public Action<BaseControl> Selecionar { get; set; }

        protected BaseControl(Color selectionColor, Action<BaseControl> selecionar)
        {
            InitializeComponent();
            SelectionColor = selectionColor;
            Selecionar = selecionar;
            Dock = DockStyle.Top;
        }

        public void SetSelected()
        {
            Selected = !Selected;
        }

        public abstract override int GetHashCode();
        public abstract override bool Equals(object obj);
        public abstract void Atualizar(BaseContent content);
    }
}
