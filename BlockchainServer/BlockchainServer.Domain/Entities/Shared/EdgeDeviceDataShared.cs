using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockchainServer.Domain.Entities.Shared
{
    public class EdgeDeviceDataShared
    {
        public List<MeasurmentShared> Measurments { get; set; }
        public string Signature { get; set; }
        public string Name { get; set; }
    }
}
