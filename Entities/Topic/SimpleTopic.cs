using System.Drawing;

namespace GestaoProjetos.Entities
{
    public class SimpleTopic : BaseTopic
    {
        private double _percentage { get; set; }

        public SimpleTopic(string name, double percentage) : base(name, "topic")
        {
            Percentage = percentage;
        }

        public override double Percentage
        {
            get => _percentage;
            set => _percentage = value;
        }

        public override Color BackColor => Color.FromArgb(30, 0, 30);
        public override Color OverlayColor => Color.FromArgb(70, 0, 70);
    }
}
