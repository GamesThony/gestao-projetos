using GestaoProjetos.Controls;
using GestaoProjetos.Entities;
using GestaoProjetos.Overlays;
using GestaoProjetos.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GestaoProjetos.Views
{
    public partial class ProjectView : Form
    {
        List<BaseControl> Controles = new List<BaseControl>();
        BaseControl ControlSelecionado = null;
        BaseContent ContentSelecionado = null;

        BaseTopic Origem = null;
        Form Form = null;

        EditTopicOverlay EditTopicOverlay;
        EditTextOverlay EditTextOverlay;
        EditImageOverlay EditImageOverlay;
        EditChartOverlay EditChartOverlay;
        EditPositionOverlay EditPositionOverlay;

        FloatingMenu menuAdicionar, menuConfig;
        RoundButton btnSecao, btnTopico, btnTexto, btnImagem, btnGrafico;
        RoundButton btnAbrirSecao, btnAbrirTopico, btnEditar, btnExcluir, btnMover;

        public ProjectView(BaseTopic origem, Form form)
        {
            InitializeComponent();

            Origem = origem;
            Form = form;

            Design();
            ToolTips();
            Overlays();
            Carregar();
        }

        private void Design()
        {
            BackColor = Origem.BackColor;

            btnSecao = new RoundButton(Adicionar(0), Resources.secao);
            btnTopico = new RoundButton(Adicionar(1), Resources.topico);
            btnTexto = new RoundButton(Adicionar(2), Resources.texto);
            btnImagem = new RoundButton(Adicionar(3), Resources.imagem);
            btnGrafico = new RoundButton(Adicionar(4), Resources.grafico);
            btnAbrirSecao = new RoundButton(Abrir, Resources.secao, Resources.abrir_secao);
            btnAbrirTopico = new RoundButton(Abrir, Resources.topico, Resources.abrir_topico);
            btnEditar = new RoundButton(Editar, Resources.editar);
            btnExcluir = new RoundButton(Excluir, Resources.excluir_0, Resources.excluir_1);
            btnMover = new RoundButton(Mover, Resources.mover);
            
            foreach (var btn in new[] { btnSecao, btnTopico, btnTexto, btnImagem, btnGrafico, btnAbrirSecao, btnAbrirTopico, btnEditar, btnExcluir, btnMover })
            {
                Controls.Add(btn);
                btn.BringToFront();
            }
            
            menuAdicionar = new FloatingMenu(0, pnlControle, btnAdicionar, btnGrafico, btnImagem, btnTexto, btnTopico, btnSecao);
            menuConfig = new FloatingMenu(0, pnlControle, btnConfig, btnMover, btnExcluir, btnEditar, btnAbrirTopico, btnAbrirSecao);
            
            SizeChanged += (s, e) => UpdateMenus();
            pnlControle.ControlAdded += (s, e) => UpdateMenus();
            pnlControle.ControlRemoved += (s, e) => UpdateMenus();

            lblMinimizar.Click += (s, e) => WindowState = FormWindowState.Minimized;
            lblFechar.Click += (s, e) => Close();
            FormClosing += (s, e) =>
            {
                if (Form is MenuView menu) menu.Atualizar(menu.PanelSelecionado, menu.ProjetoSelecionado);
                else (Form as ProjectView).Atualizar();
                Form.Show();
            };
        }

        private void UpdateMenus()
        {
            menuAdicionar.Update();
            menuConfig.Update();
        }

        private void ToolTips()
        {
            toolTip1.CreateToolTip(btnAdicionar, "Adicionar Novo");
            toolTip1.CreateToolTip(btnConfig, "Opções");
            toolTip1.CreateToolTip(btnSecao, "Seção");
            toolTip1.CreateToolTip(btnTopico, "Tópico");
            toolTip1.CreateToolTip(btnTexto, "Texto");
            toolTip1.CreateToolTip(btnImagem, "Imagem");
            toolTip1.CreateToolTip(btnGrafico, "Gráfico");
            toolTip1.CreateToolTip(btnAbrirSecao, "Abrir Seção");
            toolTip1.CreateToolTip(btnAbrirTopico, "Abrir Tópico");
            toolTip1.CreateToolTip(btnEditar, "Editar");
            toolTip1.CreateToolTip(btnExcluir, "Excluir");
            toolTip1.CreateToolTip(btnMover, "Mover");
        }

        private void Overlays()
        {
            EditTopicOverlay = new EditTopicOverlay(Origem.OverlayColor, OK_Topic, Cancelar);
            EditTextOverlay = new EditTextOverlay(Origem.OverlayColor, OK_Text, Cancelar);
            EditImageOverlay = new EditImageOverlay(Origem.OverlayColor, OK_Image, Cancelar);
            EditChartOverlay = new EditChartOverlay(Origem.OverlayColor, OK_Chart, Cancelar);
            EditPositionOverlay = new EditPositionOverlay(Origem.OverlayColor, OK_Position, Cancelar);

            Controls.Add(EditTopicOverlay);
            Controls.Add(EditTextOverlay);
            Controls.Add(EditImageOverlay);
            Controls.Add(EditChartOverlay);
            Controls.Add(EditPositionOverlay);
        }

        private void Carregar()
        {
            foreach (BaseContent content in Origem.Contents) Adicionar(content, true);
            Atualizar();
        }

        private void Adicionar(object sender, EventArgs e)
        {
            if (ControlSelecionado != null)
                Selecionar(ControlSelecionado);

            menuConfig.ChangeState(false);

            var ignore = Origem is SimpleTopic ? new[] { 3, 4 } : new int[] { };
            menuAdicionar.ChangeState(!btnGrafico.Visible, ignore);
        }

        private Action Adicionar(int type) => new Action(() => 
        {
            menuAdicionar.ChangeState(false);

            if (type == 0 || type == 1)
            {
                EditTopicOverlay.TopicType = type + 1;
                EditTopicOverlay.Exibir();
            }
            else if (type == 2) EditTextOverlay.Exibir();
            else if (type == 3) EditImageOverlay.Exibir();
            else if (type == 4) EditChartOverlay.Exibir();
        });

        private void Adicionar(BaseContent content, bool ignore = false)
        {
            if (!Origem.Contents.Contains(content) || ignore)
            {
                if (!ignore) Origem.Contents.Add(content);
                Color color = Origem.OverlayColor;

                BaseControl control = null;
                if (content is BaseTopic topic) control = new TopicPanel(topic.Name, color, Selecionar, Abrir);
                else if (content is BaseItem item)
                {
                    int index = item.Index;
                    if (item is TextItem) control = new AutoFitLabel(index, color, Selecionar);
                    else if (item is ImageItem) control = new ImageBox(index, color, Selecionar);
                    else if (item is ChartItem) control = new ChartPanel(index, color, Selecionar);
                }

                Controles.Add(control);
                pnlControle.Controls.Add(control);
                control.BringToFront();

                if (!ignore) control.Atualizar(content);
            }
        }

        private void OK_Topic()
        {
            double porcentagem = EditTopicOverlay.Porcentagem;
            string nome = EditTopicOverlay.Nome;
            int type = EditTopicOverlay.TopicType;

            if (nome.IsValidString())
            {
                if (ContentSelecionado == null)
                {
                    if (type == 1) Adicionar(new GroupTopic(nome));
                    else Adicionar(new SimpleTopic(nome, porcentagem));
                }
                else
                {
                    if (!Origem.Contents.Contains(new GroupTopic(nome)))
                    {
                        (ControlSelecionado as TopicPanel).Title = nome;
                        (ContentSelecionado as BaseTopic).Name = nome;
                    }

                    if (ContentSelecionado is SimpleTopic topic)
                        topic.Percentage = porcentagem;

                    ControlSelecionado.Atualizar(ContentSelecionado);
                }

                lblStatus.Text = $"{Origem.Name} [{Origem.Percentage.FormatDouble()}%]";
            }
        }

        private void OK_Text()
        {
            int index = Origem.GetFreeIndex("text");
            var text = EditTextOverlay.Content;
            var fontSize = EditTextOverlay.FontSize;
            var alignment = EditTextOverlay.Alignment;
            var bold = EditTextOverlay.IsBold;
            var italic = EditTextOverlay.IsItalic;
            var underlined = EditTextOverlay.IsUnderlined;
            var paragraph = EditTextOverlay.IsParagraph;

            if (text.Length != 0)
            {
                if (ControlSelecionado == null) Adicionar(new TextItem(index, text, fontSize, alignment, bold, italic, underlined, paragraph));
                else
                {
                    (ContentSelecionado as TextItem).Content = text;
                    (ContentSelecionado as TextItem).FontSize = fontSize;
                    (ContentSelecionado as TextItem).Alignment = alignment;
                    (ContentSelecionado as TextItem).IsBold = bold;
                    (ContentSelecionado as TextItem).IsItalic = italic;
                    (ContentSelecionado as TextItem).IsUnderlined = underlined;
                    (ContentSelecionado as TextItem).IsParagraph = paragraph;
                    
                    ControlSelecionado.Atualizar(ContentSelecionado);
                }
            }
        }

        private void OK_Image()
        {
            int index = Origem.GetFreeIndex("image");
            var url = EditImageOverlay.ImageLocation;
            var height = EditImageOverlay.DesiredHeight;
            
            if(File.Exists(url))
            {
                if (ControlSelecionado == null) Adicionar(new ImageItem(index, url, height));
                else
                {
                    (ContentSelecionado as ImageItem).URL = url;
                    (ContentSelecionado as ImageItem).Height = height;
                    
                    ControlSelecionado.Atualizar(ContentSelecionado);
                }
            }
        }
        
        private void OK_Chart()
        {
            int index = Origem.GetFreeIndex("chart");
            var charts = EditChartOverlay.Charts;
            ChartItem chart;

            for(int i = 0; i < charts.Count; i++)
            {
                if (charts[i].Points.Count == 0)
                {
                    charts.RemoveAt(i);
                    i--;
                }
            }
            
            if (charts.Count != 0)
            {
                if (ControlSelecionado == null)
                {
                    chart = new ChartItem(index);
                    foreach (var data in charts)
                        chart.Charts.Add(data);

                    Adicionar(chart);
                }
                else
                {
                    chart = ContentSelecionado as ChartItem;
                    chart.Charts.Clear();
                    foreach (var data in charts)
                        chart.Charts.Add(data);
                    
                    ControlSelecionado.Atualizar(ContentSelecionado);
                }
            }
        }

        private void OK_Position()
        {
            var antes = EditPositionOverlay.MoveAfter;
            var item = EditPositionOverlay.Selecionado;

            if (item != null)
            {
                Origem.Contents.Remove(ContentSelecionado);

                int index = Origem.Contents.FindIndex(p => p.ToString().Equals(item));
                Origem.Contents.Insert(index + (antes ? 0 : 1), ContentSelecionado);

                Atualizar();
            }
        }

        private void Cancelar()
        {
            if (ContentSelecionado == null)
            {
                var ignore = Origem is SimpleTopic ? new[] { 3, 4 } : new int[] { };
                menuAdicionar.ChangeState(true, ignore);
            }
            else
            {
                int[] ignore;
                if (ContentSelecionado is SimpleTopic) ignore = new[] { 4 };
                else if (ContentSelecionado is GroupTopic) ignore = new[] { 3 };
                else ignore = new[] { 3, 4 };

                menuConfig.ChangeState(true, ignore);
            }
        }

        private void Selecionar(BaseControl control)
        {
            if (ControlSelecionado != null)
            {
                ControlSelecionado.SetSelected();
                ControlSelecionado.Atualizar(ContentSelecionado);
            }
            
            if (ControlSelecionado != control && control != null)
            {
                ControlSelecionado = control;

                if (control is TopicPanel topic) ContentSelecionado = Origem.GetItem(topic.Title);
                else if (control is BaseItemControl item) ContentSelecionado = Origem.GetItem(item.Index, item.Type);

                ControlSelecionado.SetSelected();
                ControlSelecionado.Atualizar(ContentSelecionado);
            }
            else
            {
                ControlSelecionado = null;
                ContentSelecionado = null;
            }

            EditTopicOverlay.Visible = false;
            EditTextOverlay.Visible = false;
            EditImageOverlay.Visible = false;
            EditChartOverlay.Visible = false;
            EditPositionOverlay.Visible = false;

            menuAdicionar.ChangeState(false);
            menuConfig.ChangeState(false);
            menuAdicionar.Index = ControlSelecionado == null ? 0 : 1;
            UpdateMenus();
        }

        private void Abrir(TopicPanel panel)
        {
            if (!panel.Selected) Selecionar(panel);
            Abrir();
        }

        private void Abrir()
        {
            ProjectView project = new ProjectView(ContentSelecionado as BaseTopic, this);
            project.Show();
            Hide();
        }

        private void Excluir()
        {
            string tipo = null;
            if (ContentSelecionado is GroupTopic) tipo = "a seção";
            if (ContentSelecionado is SimpleTopic) tipo = "o tópico";
            if (ContentSelecionado is TextItem) tipo = "o texto";
            if (ContentSelecionado is ImageItem) tipo = "a imagem";
            if (ContentSelecionado is ChartItem) tipo = "o gráfico";

            string msg = $"Deseja excluir {tipo} '{ContentSelecionado}'?";
            DialogResult result = MessageBox.Show(msg, "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                pnlControle.Controls.Remove(ControlSelecionado);
                Controles.Remove(ControlSelecionado);
                Origem.Contents.Remove(ContentSelecionado);
                Selecionar(ControlSelecionado);

                lblStatus.Text = $"{Origem.Name} [{Origem.Percentage.FormatDouble()}%]";
            }
        }

        private void Editar()
        {
            menuConfig.ChangeState(false);

            if (ContentSelecionado is BaseTopic topic)
            {
                EditTopicOverlay.TopicType = topic is GroupTopic ? 1 : 2;
                EditTopicOverlay.Exibir(topic.Name, topic.Percentage);
            }
            else if (ContentSelecionado is TextItem text)
            {
                EditTextOverlay.Exibir(text.Content, text.FontSize, text.Alignment, text.IsBold, 
                    text.IsItalic, text.IsUnderlined, text.IsParagraph);
            }
            else if (ContentSelecionado is ImageItem image)
            {
                EditImageOverlay.Exibir(image.URL, image.Height);
            }
            else if (ContentSelecionado is ChartItem chart)
            {
                EditChartOverlay.Exibir(chart);
            }
        }

        private void Mover()
        {
            menuConfig.ChangeState(false);
            EditPositionOverlay.Exibir(ContentSelecionado, Origem.Contents);
        }

        private void Opcoes(object sender, EventArgs e)
        {
            int[] ignore;
            if (ContentSelecionado is SimpleTopic) ignore = new[] { 4 };
            else if (ContentSelecionado is GroupTopic) ignore = new[] { 3 };
            else ignore = new[] { 3, 4 };

            menuConfig.ChangeState(!btnEditar.Visible, ignore);
        }

        public async void Atualizar()
        {
            lblStatus.Text = $"{Origem.Name} [{Origem.Percentage.FormatDouble()}%]";
            btnAdicionar.Enabled = false;

            await Task.Run(new Action(() =>
            {
                foreach (BaseContent content in Origem.Contents)
                {
                    BaseControl control = null;

                    if (content is BaseTopic topic)
                    {
                        var panels = Controles.Where(p => p is TopicPanel).Select(p => p as TopicPanel);
                        control = panels.Where(p => p.Title == topic.Name).First();
                    }
                    else if (content is BaseItem item)
                    {
                        var controls = Controles.Where(p => p is BaseItemControl).Select(p => p as BaseItemControl);
                        control = controls.Where(p => p.Index == item.Index && p.Type == item.Type).First();
                    }

                    control.Atualizar(content);

                    if (IsHandleCreated)
                    {
                        control.Invoke(new Action(() =>
                        {
                            control.BringToFront();
                            control.Enabled = false;
                        }));
                    }
                }
            }));

            foreach (var control in Controles)
                control.Enabled = true;

            btnAdicionar.Enabled = true;
        }
    }
}
