using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockchainServer.Domain.Entities
{
    public class Measurment
    {
        public int DeviceId { get; set; }
        public double Value { get; set; }
        public DateTime timestamp { get; set; }
    }
}
