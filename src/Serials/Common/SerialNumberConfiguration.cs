using System;
using System.Collections.Generic;
using System.Text;

namespace Serials.Common
{
    public class SerialNumberConfiguration
    {
        public IEncoder Encoder { get; private set; }

        public SerialNumberConfiguration(IEncoder encoder)
        {
            Encoder = encoder;
        }
    }
}
