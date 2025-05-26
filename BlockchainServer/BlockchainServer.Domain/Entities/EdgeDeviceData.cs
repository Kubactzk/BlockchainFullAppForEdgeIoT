using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockchainServer.Domain.Entities
{
    public class EdgeDeviceData
    {
        public List<Measurment> Measurments { get; set; }
        public string Signature { get; set; }
        public string Name { get; set; }
    }
}
