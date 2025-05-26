using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace BlockchainServer.Domain.Entities
{
    public class Block
    {
        public int Index { get; set; }
        public DateTime TimeStamp { get; set; } = DateTime.UtcNow;
        public EdgeDeviceData Data { get; set; }
        public string PreviousHash { get; set; }
        public string Hash { get; set; }

        public Block() { }
        public Block(int index, EdgeDeviceData data, string previousHash)
        {
            Index = index;
            TimeStamp = DateTime.UtcNow;
            Data = data;
            PreviousHash = previousHash;
            Hash = ComputeHash();
        }

        public string ComputeHash()
        {
            string dataJson = JsonSerializer.Serialize(Data);
            string input = Index.ToString() + TimeStamp.ToString("O") + dataJson + PreviousHash;

            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));
                StringBuilder builder = new StringBuilder();
                foreach (byte b in bytes)
                    builder.Append(b.ToString("x2"));

                return builder.ToString();
            }
        }
    }
}
