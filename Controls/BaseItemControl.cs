using System;
using System.Drawing;

namespace GestaoProjetos.Controls
{
    public abstract partial class BaseItemControl : BaseControl
    {
        public int Index { get; set; }
        public string Type { get; set; }

        public BaseItemControl(int index, string type, Color selectionColor, Action<BaseControl> selecionar) : base(selectionColor, selecionar)
        {
            Index = index;
            Type = type;
        }

        public sealed override int GetHashCode()
        {
            return Index.GetHashCode() + Type.GetHashCode();
        }

        public sealed override bool Equals(object obj)
        {
            return obj is BaseItemControl other ? Index.Equals(other.Index) && Type.Equals(other.Type) : false;
        }
    }
}
