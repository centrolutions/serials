namespace Serials
{
    public abstract class SerialNumberBase<T> : ISerialNumber
    {
        public T NumericValue { get; protected set; }

        public SerialNumberBase(T initialValue)
        {
            NumericValue = initialValue;
        }

        public abstract void DecreaseBy(int decrease);
        public abstract void IncreaseBy(int increase);
        public override abstract string ToString();

        public override bool Equals(object obj)
        {
            var serial = obj as SerialNumberBase<T>;
            if (obj == null)
                return false;

            return this.NumericValue.Equals(serial.NumericValue);
        }

        public override int GetHashCode()
        {
            return NumericValue.GetHashCode();
        }
    }
}
