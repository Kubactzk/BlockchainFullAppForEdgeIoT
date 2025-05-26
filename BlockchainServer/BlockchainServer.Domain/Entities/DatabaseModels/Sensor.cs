using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockchainServer.Domain.Entities.DatabaseModels
{
    public class Sensor
    {
        [Key]
        public int Id { get; set; }
        public string SensorType { get; set; }
        public int DeviceId { get; set; }
    }
}
