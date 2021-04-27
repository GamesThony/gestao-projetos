using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace GestaoProjetos.Controls
{
    public partial class GradientPanel : Panel
    {
        public float Angle { get; set; }
        public Color TopColor { get; set; }
        public Color BottomColor { get; set; }

        public GradientPanel()
        {
            InitializeComponent();

            BackColor = Color.Transparent;
            DoubleBuffered = true;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            LinearGradientBrush brush = new LinearGradientBrush(this.ClientRectangle, TopColor, BottomColor, Angle);
            e.Graphics.FillRectangle(brush, this.ClientRectangle);
        }
    }
}
