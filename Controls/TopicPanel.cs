using System;
using System.Drawing;
using System.Windows.Forms;
using GestaoProjetos.Entities;

namespace GestaoProjetos.Controls
{
    public partial class TopicPanel : BaseControl
    {
        public string Title { get; set; }
        public Action<TopicPanel> Abrir { get; set; }

        Panel panel1 = new Panel()
        {
            Dock = DockStyle.Top,
            Height = 53,
            BorderStyle = BorderStyle.Fixed3D
        };

        Label lblTitle = new Label() { Font = new Font("Consolas", 12, FontStyle.Bold) };
        Label lblPercentage = new Label() { Font = new Font("Consolas", 12) };

        public TopicPanel(string title, Color selectionColor, Action<BaseControl> selecionar, Action<TopicPanel> abrir) : base(selectionColor, selecionar)
        {
            Title = title;
            Abrir = abrir;

            foreach (var lbl in new[] { lblPercentage, lblTitle })
            {
                lbl.AutoSize = false;
                lbl.Dock = DockStyle.Top;
                lbl.ForeColor = Color.White;
                lbl.Cursor = Cursors.Hand;
                lbl.Height = 24;
                lbl.Padding = new Padding(5, 0, 0, 0);
                lbl.TextAlign = ContentAlignment.MiddleLeft;
                lbl.Click += (s, e) => Selecionar(this);
                lbl.DoubleClick += (s, e) => Abrir(this);
                panel1.Controls.Add(lbl);
            }

            Height = 60;
            Controls.Add(panel1);
        }

        public override int GetHashCode()
        {
            return Title.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return obj is TopicPanel other ? Title.Equals(other.Title) : false;
        }

        public override void Atualizar(BaseContent content)
        {
            if (content is BaseTopic topic)
            {
                if (panel1.InvokeRequired) panel1.Invoke(SetBackground());
                else SetBackground().Invoke();

                if (lblTitle.InvokeRequired) lblTitle.Invoke(SetTitle(topic.Name));
                else SetTitle(topic.Name).Invoke();

                if (lblPercentage.InvokeRequired) lblPercentage.Invoke(SetPercentage(topic.Percentage));
                else SetPercentage(topic.Percentage).Invoke();
            }
        }

        private Action SetBackground() => new Action(() =>
        {
            panel1.BackColor = Selected ? SelectionColor : Color.Transparent;
        });

        private Action SetTitle(string title) => new Action(() =>
        {
            lblTitle.Text = title;
        });

        private Action SetPercentage(double percentage) => new Action(() =>
        {
            lblPercentage.Text = $"{percentage.FormatDouble()}% Completo";
        });
    }
}
