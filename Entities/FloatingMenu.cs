using GestaoProjetos.Controls;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace GestaoProjetos.Entities
{
    public class FloatingMenu
    {
        public int Index { get; set; }
        private Panel _mainPanel { get; set; }
        private RoundButton _mainButton { get; set; }
        private List<RoundButton> _buttons { get; set; } = new List<RoundButton>();

        public FloatingMenu(int index, Panel mainPanel, RoundButton mainButton, params RoundButton[] buttons)
        {
            Index = index;
            _mainPanel = mainPanel;
            _mainButton = mainButton;
            _buttons.AddRange(buttons);

            foreach (var btn in _buttons)
            {
                btn.Visible = false;
                btn.BackColor = Color.Transparent;
                btn.Size = new Size(50, 50);
            }
        }

        public void Update()
        {
            int X = _mainPanel.Width - _mainButton.Width - (62 * Index);
            if (_mainPanel.VerticalScroll.Visible) X -= SystemInformation.VerticalScrollBarWidth + 5;

            int Y = _mainButton.Location.Y;
            _mainButton.Location = new Point(X, Y);

            int i = 0;
            foreach(var btn in _buttons)
            {
                if (btn.Visible)
                {
                    int X_ = X + 3;
                    int Y_ = Y - (56 * (i + 1));
                    btn.Location = new Point(X_, Y_);
                    i++;
                }
            }
        }

        public void ChangeState(bool newState, params int[] ignore)
        {
            for (int i = 0; i < _buttons.Count; i++)
                if (!ignore.ToList().Contains(i))
                    _buttons[i].Visible = newState;
            Update();
        }
    }
}
