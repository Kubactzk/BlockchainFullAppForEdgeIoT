using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockchainServer.Domain.Entities.DatabaseModels
{
    public class Device
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string PublicKey { get; set; }
        public bool IsActive { get; set; }
        public bool IsAuthority { get; set; } 
    }
}
