using System.Drawing;
using System.Linq;

namespace GestaoProjetos.Entities
{
    public class GroupTopic : BaseTopic
    {
        public GroupTopic(string name) : base(name, "section")
        {
        }

        public override double Percentage
        {
            get
            {
                var topics = Contents.Where(p => p is BaseTopic).Select(p => p as BaseTopic);
                return topics.Count() == 0 ? 0 : topics.Average(p => p.Percentage);
            }
            set { }
        }

        public override Color BackColor => Color.FromArgb(30, 0, 0);
        public override Color OverlayColor => Color.FromArgb(70, 0, 0);
    }
}
