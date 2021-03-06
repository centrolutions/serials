using Serials.Common;

namespace Serials
{
    public abstract class SerialNumberBase<T> : ISerialNumber
    {
        public T NumericValue { get; protected set; }
        protected SerialNumberConfiguration Configuration { get; set; }

        public SerialNumberBase(T initialValue, SerialNumberConfiguration configuration)
        {
            NumericValue = initialValue;
            Configuration = configuration;
        }

        /// <summary>
        /// Decrement the serial number by the value of the decrease parameter
        /// </summary>
        /// <param name="decrease">The value to decrease the serial number by</param>
        public abstract void DecreaseBy(int decrease);

        /// <summary>
        /// Increment the serial number by the value of the increase parameter
        /// </summary>
        /// <param name="increase">The value to increase the serial number by</param>
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


        protected string ApplyPrefix(string encoded)
        {
            if (!string.IsNullOrWhiteSpace(Configuration.Prefix))
                encoded = Configuration.Prefix + encoded;
            return encoded;
        }

        protected string ApplyMinimumLength(string encoded)
        {
            if (Configuration.MinimumLength.HasValue && encoded.Length < Configuration.MinimumLength.Value)
                encoded = encoded.PadLeft(Configuration.MinimumLength.Value, Configuration.PadCharacter);
            return encoded;
        }

        protected static string RemovePrefix(string encoded, SerialNumberConfiguration config)
        {
            if (!string.IsNullOrWhiteSpace(config.Prefix) && encoded.StartsWith(config.Prefix))
                encoded = encoded.Substring(config.Prefix.Length);
            return encoded;
        }
    }
}
