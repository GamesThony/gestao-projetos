using GestaoProjetos.Entities;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace GestaoProjetos.Controls
{
    public partial class CircularProgressBar : Label
    {
        private double _progress { get; set; }
        public double Progress
        {
            get => _progress;
            set
            {
                if (value < 0) _progress = 0;
                else if (value > 100) _progress = 100;
                else _progress = value;

                if (InvokeRequired) Invoke(new Action(() => Refresh()));
                else Refresh();
            }
        }

        public CircularProgressBar()
        {
            InitializeComponent();

            AutoSize = false;
            BackColor = Color.Transparent;
            DoubleBuffered = true;
            Font = new Font("Consolas", 10);
            ForeColor = Color.White;
            TextAlign = ContentAlignment.MiddleCenter;
            MinimumSize = new Size(0, 100);
            MaximumSize = new Size(0, 100);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Text = $"{Progress.FormatDouble()}%";
            base.OnPaint(e);

            using (var pen = new Pen(Color.White, 7f) { Alignment = PenAlignment.Inset })
                using (var path = GetProgressPath())
                    e.Graphics.DrawPath(pen, path);
        }

        private GraphicsPath GetProgressPath()
        {
            GraphicsPath gp = new GraphicsPath();
            double progress = Math.Min(Progress, 99.99);
            gp.AddArc((Width - Height + 20) / 2, 10, Height - 20, Height - 20, 269, (float)progress * 360 / 100);
            return gp;
        }
    }
}
