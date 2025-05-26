using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockchainServer.Domain.Entities.Shared
{
    public class MeasurmentShared
    {
        public string Name { get; set; }
        public double Value { get; set; }
        public DateTime timestamp { get; set; }
    }
}
