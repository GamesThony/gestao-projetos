using GestaoProjetos.Entities;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace GestaoProjetos.Controls
{
    public partial class ImageBox : BaseItemControl
    {
        PictureBox pcbImagem = new PictureBox()
        {
            Cursor = Cursors.Hand,
            Dock = DockStyle.Top,
            SizeMode = PictureBoxSizeMode.Zoom,
            MinimumSize = new Size(0, 20)
        };

        public ImageBox(int index, Color selectionColor, Action<BaseControl> selecionar) : base(index, "image", selectionColor, selecionar)
        {
            MinimumSize = new Size(0, 25);
            pcbImagem.Click += (s, e) => Selecionar(this);
            Controls.Add(pcbImagem);
        }

        public override void Atualizar(BaseContent content)
        {
            if (content is ImageItem image)
            {
                if (pcbImagem.InvokeRequired) pcbImagem.Invoke(SetImage(image));
                else SetImage(image).Invoke();

                if (InvokeRequired) Invoke(new Action(() => Height = pcbImagem.Height + 5));
                else Height = pcbImagem.Height + 5;
            }
        }

        private Action SetImage(ImageItem image) => new Action(() =>
        {
            pcbImagem.BackColor = Selected ? SelectionColor : Color.Transparent;
            pcbImagem.Height = image.Height;
            if (File.Exists(image.URL)) pcbImagem.Image = Image.FromFile(image.URL);
        });
    }
}
