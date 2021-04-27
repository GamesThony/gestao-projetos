using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace GestaoProjetos.Controls
{
    public partial class RoundButton : PictureBox
    {
        public RoundButton()
        {
            InitializeComponent();

            Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            BackColor = Color.Transparent;
            Cursor = Cursors.Hand;
            SizeMode = PictureBoxSizeMode.Zoom;
        }

        public RoundButton(Action action, Image img) : this()
        {
            Image = img;
            Click += (s, e) => action();
        }

        public RoundButton(Action action, Image img1, Image img2) : this(action, img1)
        {
            MouseHover += (s, e) => Image = img2;
            MouseLeave += (s, e) => Image = img1;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            using (var path = GetRegionPath())
                Region = new Region(path);
        }

        private GraphicsPath GetRegionPath()
        {
            GraphicsPath gp = new GraphicsPath();
            gp.AddEllipse(this.ClientRectangle);
            return gp;
        }
    }
}
