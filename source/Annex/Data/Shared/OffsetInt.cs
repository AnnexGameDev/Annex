namespace Annex_Old.Data.Shared
{
    public class OffsetInt : Int
    {
        public readonly Int Offset;
        public readonly Int Base;

        public override int Value { 
            get => this.Base.Value + this.Offset.Value; 
            set => this.Offset.Value = value; 
        }

        public OffsetInt(Int baseInt, Int offsetInt) {
            this.Base = baseInt;
            this.Offset = offsetInt;
        }
    }
}
