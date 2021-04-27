using System;
using System.Drawing;
using System.Windows.Forms;
using GestaoProjetos.Entities;
using System.IO;

namespace GestaoProjetos.Overlays
{
    public partial class EditImageOverlay : UserControl
    {
        private string _location { get; set; }
        public string ImageLocation => _location;
        public int DesiredHeight => txtAltura.Text.ToInt();

        public EditImageOverlay(Color backColor, Action ok, Action cancelar)
        {
            InitializeComponent();
            this.InitializeDesign(backColor, txtAltura);

            btnOK.Click += (s, e) => { Visible = false; ok(); };
            btnCancelar.Click += (s, e) => { Visible = false; cancelar(); };
            pcbImagem.Click += (s, e) =>
            {
                OpenFileDialog file = new OpenFileDialog()
                {
                    Title = "Selecione a imagem...",
                    Filter = "Arquivos de Imagem|*.png;*.jpg;*.jpeg"
                };

                if (file.ShowDialog() == DialogResult.OK)
                {
                    string path = Path.GetFullPath(file.FileName);
                    _location = path;
                    pcbImagem.Image = Image.FromFile(path);
                }
            };
        }

        public void Exibir(string location = "", int height = 20)
        {
            Visible = true;
            BringToFront();
            Focus();

            _location = location;
            if (File.Exists(_location)) pcbImagem.Image = Image.FromFile(_location);
            else pcbImagem.Image = null;
            txtAltura.Text = height >= 20 ? height.ToString() : "20";
        }
    }
}
