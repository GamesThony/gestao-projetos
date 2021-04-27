using GestaoProjetos.Controls;
using GestaoProjetos.Entities;
using GestaoProjetos.Overlays;
using GestaoProjetos.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;

namespace GestaoProjetos.Views
{
    public partial class MenuView : Form
    {
        List<GroupTopic> Projetos = new List<GroupTopic>();
        List<TopicPanel> Panels = new List<TopicPanel>();
        public GroupTopic ProjetoSelecionado = null;
        public TopicPanel PanelSelecionado = null;

        EditTopicOverlay EditOverlay;
        FloatingMenu menuAdicionar, menuConfig;
        RoundButton btnAbrir, btnEditar, btnExcluir, btnSalvar;

        public MenuView()
        {
            InitializeComponent();
            Design();
            ToolTips();
            Overlays();
        }

        private void Design()
        {
            btnAbrir = new RoundButton(Abrir, Resources.abrir_0, Resources.abrir_1);
            btnEditar = new RoundButton(Editar, Resources.editar);
            btnExcluir = new RoundButton(Excluir, Resources.excluir_0, Resources.excluir_1);
            btnSalvar = new RoundButton(Salvar, Resources.salvar);

            foreach (var btn in new[] { btnAbrir, btnEditar, btnExcluir, btnSalvar })
            {
                Controls.Add(btn);
                btn.BringToFront();
            }

            menuAdicionar = new FloatingMenu(0, pnlControle, btnAdicionar);
            menuConfig = new FloatingMenu(0, pnlControle, btnConfig, btnSalvar, btnExcluir, btnEditar, btnAbrir);
            
            SizeChanged += (s, e) => UpdateMenus();
            pnlControle.ControlAdded += (s, e) => UpdateMenus();
            pnlControle.ControlRemoved += (s, e) => UpdateMenus();

            btnConfig.Click += (s, e) => menuConfig.ChangeState(!btnAbrir.Visible);
            lblMinimizar.Click += (s, e) => WindowState = FormWindowState.Minimized;
            lblFechar.Click += (s, e) => Close();
            FormClosing += (s, e) =>
            {
                StringBuilder info = new StringBuilder();
                info.AppendLine("Deseja fechar o programa?");
                info.AppendLine("Projetos não salvos serão perdidos!");
                
                if (MessageBox.Show(info.ToString(), "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes) Environment.Exit(0);
                else e.Cancel = true;
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
            toolTip1.CreateToolTip(btnAbrir, "Abrir");
            toolTip1.CreateToolTip(btnEditar, "Editar");
            toolTip1.CreateToolTip(btnSalvar, "Salvar");
            toolTip1.CreateToolTip(btnExcluir, "Excluir");
        }

        private void Overlays()
        {
            EditOverlay = new EditTopicOverlay(Color.FromArgb(0, 0, 70), OK, Cancelar);
            EditOverlay.TopicType = 0;
            Controls.Add(EditOverlay);
        }

        private void Adicionar(object sender, EventArgs e)
        {
            if (PanelSelecionado != null) Selecionar(PanelSelecionado);

            menuConfig.ChangeState(false);
            EditOverlay.Exibir();
        }

        public void Adicionar(GroupTopic projeto)
        {
            if (!Projetos.Contains(projeto))
            {
                Projetos.Add(projeto);
                TopicPanel panel = new TopicPanel(projeto.Name, Color.FromArgb(0, 0, 50), Selecionar, Abrir);
                Atualizar(panel, projeto);
                Panels.Add(panel);
                pnlControle.Controls.Add(panel);
                panel.BringToFront();
            }
        }

        private void OK()
        {
            string nome = EditOverlay.Nome;
            if (nome.IsValidString() && nome.IndexOfAny(Path.GetInvalidFileNameChars()) == -1)
            {
                if (PanelSelecionado == null) Adicionar(new GroupTopic(nome));
                else
                {
                    if (!Projetos.Contains(new GroupTopic(nome)))
                    {
                        string anterior = PanelSelecionado.Title;
                        string src = Directory.GetCurrentDirectory() + $@"\Projetos\{anterior}.xml";
                        string dst = Directory.GetCurrentDirectory() + $@"\Projetos\{nome}.xml";
                        if (File.Exists(src)) File.Move(src, dst);

                        PanelSelecionado.Title = nome;
                        ProjetoSelecionado.Name = nome;
                    }

                    Atualizar(PanelSelecionado, ProjetoSelecionado);
                }
            }
        }

        private void Cancelar()
        {
            menuConfig.ChangeState(PanelSelecionado != null);
        }

        private void Selecionar(BaseControl panel)
        {
            if (PanelSelecionado != null)
            {
                PanelSelecionado.SetSelected();
                PanelSelecionado.Atualizar(ProjetoSelecionado);
            }

            if (PanelSelecionado != panel)
            {
                PanelSelecionado = panel as TopicPanel;
                ProjetoSelecionado = Projetos.Where(p => p.Name == PanelSelecionado.Title).First();
                PanelSelecionado.SetSelected();
                PanelSelecionado.Atualizar(ProjetoSelecionado);
            }
            else
            {
                PanelSelecionado = null;
                ProjetoSelecionado = null;
            }

            EditOverlay.Visible = false;
            menuConfig.ChangeState(false);
            menuAdicionar.Index = PanelSelecionado == null ? 0 : 1;
            UpdateMenus();
        }

        private void Abrir(TopicPanel panel)
        {
            if (!panel.Selected) Selecionar(panel);
            Abrir();
        }

        private void Abrir()
        {
            ProjectView project = new ProjectView(ProjetoSelecionado, this);
            project.Show();
            Hide();
        }

        private void Editar()
        {
            menuConfig.ChangeState(false);
            EditOverlay.Exibir(ProjetoSelecionado.Name);
        }

        private void Excluir()
        {
            string nome = ProjetoSelecionado.Name;
            string msg = $"Deseja excluir o projeto '{nome}'?";
            DialogResult result = MessageBox.Show(msg, "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                string path = Directory.GetCurrentDirectory() + $@"\Projetos\{nome}.xml";
                if (File.Exists(path)) File.Delete(path);

                pnlControle.Controls.Remove(PanelSelecionado);
                Panels.Remove(PanelSelecionado);
                Projetos.Remove(ProjetoSelecionado);
                Selecionar(PanelSelecionado);
            }

            Cancelar();
        }

        private void Salvar()
        {
            string nome = ProjetoSelecionado.Name;
            string path = Directory.GetCurrentDirectory() + $@"\Projetos\{nome}.xml";
            if (File.Exists(path)) File.Delete(path);

            XElement xml = new XElement("project");
            xml.Add(new XAttribute("name", nome));
            ProjetoSelecionado.Save(xml);
            xml.Save(path);
        }

        public void Atualizar(TopicPanel panel, GroupTopic topic)
        {
            panel.Atualizar(topic);
        }
    }
}
