using GestaoProjetos.Entities;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace GestaoProjetos.Controls
{
    public partial class AutoFitLabel : BaseItemControl
    {
        Label lblContent = new Label()
        {
            Dock = DockStyle.Top,
            AutoSize = false,
            Cursor = Cursors.Hand
        };

        public AutoFitLabel(int index, Color selectionColor, Action<BaseControl> selecionar) : base(index, "text", selectionColor, selecionar)
        {
            lblContent.Click += (s, e) => Selecionar(this);
            Controls.Add(lblContent);
        }

        public override void Atualizar(BaseContent content)
        {
            if (content is TextItem text)
            {
                if (lblContent.InvokeRequired) lblContent.Invoke(SetText(text));
                else SetText(text).Invoke();

                if (InvokeRequired) Invoke(new Action(() => Height = lblContent.Height + 5));
                else Height = lblContent.Height + 5;
            }
        }

        private Action SetText(TextItem text) => new Action(() =>
        {
            FontStyle style = FontStyle.Regular;
            if (text.IsBold) style |= FontStyle.Bold;
            if (text.IsItalic) style |= FontStyle.Italic;
            if (text.IsUnderlined) style |= FontStyle.Underline;
            lblContent.Font = new Font("Consolas", text.FontSize, style);
            lblContent.Text = (text.IsParagraph ? "    " : "") + text.Content;
            lblContent.TextAlign = text.Alignment;
            lblContent.BackColor = Selected ? SelectionColor : Color.Transparent;

            lblContent.AutoSize = true;
            lblContent.MaximumSize = new Size(Width, 0);
            lblContent.MinimumSize = new Size(Width, 0);
            int DesiredHeight = lblContent.Height;

            lblContent.AutoSize = false;
            lblContent.MaximumSize = new Size(0, 0);
            lblContent.MinimumSize = new Size(0, 0);
            lblContent.Height = DesiredHeight;
        });
    }
}
