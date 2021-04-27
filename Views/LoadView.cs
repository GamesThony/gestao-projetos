using GestaoProjetos.Entities;
using System;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Xml;

namespace GestaoProjetos.Views
{
    public partial class LoadView : Form
    {
        private MenuView _menu { get; set; } = new MenuView();

        public LoadView()
        {
            InitializeComponent();

            _menu.Shown += (s, e) => Hide();
            Carregar();
        }

        private async void Carregar()
        {
            string path = Directory.GetCurrentDirectory() + @"\Projetos";
            if (!Directory.Exists(path)) Directory.CreateDirectory(path);
            string[] projetos = Directory.GetFiles(path, "*.xml");
            int total = projetos.Length, feitos = 0;

            await Task.Run(new Action(() =>
            {
                foreach (string projeto in projetos)
                {
                    XmlDocument doc = new XmlDocument();
                    doc.Load(projeto);

                    string nome = doc.DocumentElement.GetAttribute("name");
                    GroupTopic projeto_novo = new GroupTopic(nome);
                    Carregar(projeto_novo, doc.DocumentElement.ChildNodes);
                    _menu.Adicionar(projeto_novo);
                    feitos++;

                    if (IsHandleCreated)
                        pgbProgresso.Progress = feitos / total * 100.0;
                }

                if (IsHandleCreated)
                    pgbProgresso.Progress = 100;
            }));

            _menu.Show();
        }

        private void Carregar(BaseTopic topic, XmlNodeList nodes)
        {
            foreach (XmlNode node in nodes)
            {
                BaseContent content = null;
                string tipo = node.Name;
                var attr = node.Attributes;

                if (tipo == "topic" || tipo == "section")
                {
                    string nome = attr[0].Value;
                    if (tipo == "topic") content = new SimpleTopic(nome, attr[1].Value.ToDouble());
                    else content = new GroupTopic(nome);
                    Carregar(content as BaseTopic, node.ChildNodes);
                }
                else
                {
                    int index = topic.GetFreeIndex(tipo);

                    if (tipo == "text")
                    {
                        int style = attr[2].Value.ToInt();
                        var styles = new[] { false, false, false, false };

                        for (int i = 3; i >= 0; i--)
                        {
                            int num = (int)Math.Pow(2, i);
                            styles[i] = style >= num;
                            style %= num;
                        }

                        content = new TextItem(index, node.InnerText, attr[0].Value.ToInt(), ToEnum<ContentAlignment>(attr[1].Value), styles[0], styles[1], styles[2], styles[3]);
                    }
                    else if (tipo == "image")
                    {
                        content = new ImageItem(index, node.InnerText, attr[0].Value.ToInt());
                    }
                    else if (tipo == "chart")
                    {
                        content = new ChartItem(index);
                        
                        foreach (XmlNode chart in node.ChildNodes)
                        {
                            string chartType = chart.Name;
                            ChartData data = new ChartData(ToEnum<SeriesChartType>(chartType));
                            foreach(XmlNode points in chart.ChildNodes)
                            {
                                int x = points.Attributes["x"].ToString().ToInt();
                                int y = points.Attributes["y"].ToString().ToInt();
                                data.Points.Add(new Point(x, y));
                            }

                            (content as ChartItem).Charts.Add(data);
                        }
                    }
                }

                topic.Contents.Add(content);
            }
        }

        private T ToEnum<T>(string value)
        {
            return (T)Enum.Parse(typeof(T), value, true);
        }
    }
}
