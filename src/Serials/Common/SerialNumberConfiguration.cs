using System;
using System.Collections.Generic;
using System.Text;

namespace Serials.Common
{
    public class SerialNumberConfiguration
    {
        /// <summary>
        /// The encoder used to parse and create serial numbers
        /// </summary>
        public IEncoder Encoder { get; private set; }

        /// <summary>
        /// Optional minimum length of the serial number
        /// </summary>
        /// <remarks>
        /// When used, the serial number is left-padded with the <see cref="PadCharacter"/>. Note: the minimum length is applied before any prefix is added.
        /// </remarks>
        public int? MinimumLength { get; set; }

        /// <summary>
        /// The character to pad the serial number with
        /// </summary>
        /// <remarks>
        /// Only used if <see cref="MinimumLength"/> is set. Padding is performed before a prefix is prepended.
        /// </remarks>
        public char PadCharacter { get; set; }

        /// <summary>
        /// Optional prefix always attached before (in front of) the serial number
        /// </summary>
        public string Prefix { get; set; }

        public SerialNumberConfiguration() : this(new Base36Encoder()) { }

        public SerialNumberConfiguration(IEncoder encoder)
        {
            Encoder = encoder;
            PadCharacter = '0';
        }
    }
}
