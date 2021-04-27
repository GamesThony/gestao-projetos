using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

namespace GestaoProjetos.Entities
{
    public static class ExtensionMethods
    {
        private static CultureInfo IC = CultureInfo.InvariantCulture;
        private static NumberStyles F = NumberStyles.Float;

        public static bool IsValidInt(this string value)
        {
            return int.TryParse(value, out int result);
        }

        public static bool IsValidDouble(this string value)
        {
            return double.TryParse(value, F, IC, out double result) ? result >= 0 && result <= 100 : false;
        }

        public static bool IsValidString(this string value)
        {
            return !string.IsNullOrEmpty(value) && !string.IsNullOrWhiteSpace(value);
        }

        public static int ToInt(this string value)
        {
            return value.IsValidInt() ? int.Parse(value) : 0;
        }

        public static double ToDouble(this string value)
        {
            return value.IsValidDouble() ? double.Parse(value, F, IC) : 0;
        }

        public static string FormatDouble(this double value)
        {
            return value.ToString("F2", IC);
        }

        public static void CreateToolTip(this ToolTip toolTip, Control control, string title)
        {
            control.MouseHover += (s, e) => toolTip.Show(title, control, 50, 50);
            control.MouseLeave += (s, e) => toolTip.Hide(control);
        }

        public static void InitializeDesign(this UserControl userControl, Color color, params Control[] controls)
        {
            userControl.Dock = DockStyle.Bottom;
            userControl.Visible = false;
            userControl.BackColor = color;

            foreach (var control in controls)
                control.BackColor = color;
        }
    }
}
