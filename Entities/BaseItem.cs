namespace GestaoProjetos.Entities
{
    public abstract class BaseItem : BaseContent
    {
        public int Index { get; }

        public BaseItem(int index, string type) : base(type)
        {
            Index = index;
        }

        public sealed override int GetHashCode()
        {
            return Index.GetHashCode() + Type.GetHashCode();
        }

        public sealed override bool Equals(object obj)
        {
            return obj is BaseItem other ? Index.Equals(other.Index) && Type.Equals(other.Type) : false;
        }
    }
}
