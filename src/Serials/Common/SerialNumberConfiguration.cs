using System;
using System.Collections.Generic;
using System.Text;

namespace Serials.Common
{
    public class SerialNumberConfiguration
    {
        public IEncoder Encoder { get; private set; }
        public int? MinimumLength { get; set; }

        public SerialNumberConfiguration() : this(new Base36Encoder()) { }

        public SerialNumberConfiguration(IEncoder encoder)
        {
            Encoder = encoder;
        }
    }
}
