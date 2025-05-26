using BlockchainServer.Domain.Entities;
using BlockchainServer.Domain.Entities.DatabaseModels;
using BlockchainServer.Application.Services.Interfaces;
using BlockchainServer.Infrastructure.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using NBitcoin;
using NBitcoin.Crypto;
using NBitcoin.DataEncoders;
using System.Text.Json;
using System.Text;
using Block = BlockchainServer.Domain.Entities.Block;
using BlockchainServer.Domain.Entities.Shared;

namespace BlockchainServer.Infrastructure.Services
{
    public class BlockchainService : IBlockchainService
    {
        private readonly List<Block> _chain;
        private readonly string _filePath;
        private readonly AppDbContext _appDbContext;

        public BlockchainService(AppDbContext dbContext)
        {
            _appDbContext = dbContext;

            _filePath = "D:\\Studia\\ISA-magister\\Magisterka\\OfficialApplication\\BlockchainServer\\BlockchainServer.Domain\\Blockchain\\blockchain.json";
            _chain = new List<Block>();

            InitializeBlockchain();
        }

        private void InitializeBlockchain()
        {
            if (File.Exists(_filePath))
            {
                try
                {
                    string json = File.ReadAllText(_filePath);
                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };

                    var loadedChain = JsonSerializer.Deserialize<List<Block>>(json, options);
                    if (loadedChain != null && loadedChain.Count > 0)
                    {
                        _chain.AddRange(loadedChain);
                        Console.WriteLine("Blockchain loaded successfully.");
                        return;
                    }

                    Console.WriteLine("Blockchain file exists but was empty or invalid. Creating new chain.");
                }
                catch (JsonException ex)
                {
                    Console.WriteLine($"Error reading blockchain file: {ex.Message}. Creating new chain.");
                }
            }
            else
            {
                Console.WriteLine("Blockchain file not found. Creating new chain.");
            }

            _chain.Add(CreateGenesisBlock());
            SaveBlockchain();
        }

        private Block CreateGenesisBlock()
        {
            var genesisData = new EdgeDeviceData
            {
                Measurments = new List<Measurment>
                {
                    new Measurment { DeviceId = 0, timestamp = DateTime.UtcNow, Value = 0 }
                },
                Signature = "0",
                Name = "Genesis Block"
            };

            return new Block(0, genesisData, "0");
        }

        public Block GetLatestBlock() => _chain.Last();

        public void AddBlock(EdgeDeviceData data)
        {
            var device = _appDbContext.Devices.FirstOrDefault(d => d.Name == data.Name && d.IsAuthority);
            if (device == null)
            {
                Console.WriteLine("Device is not authorized (PoA failed). Block rejected.");
                return;
            }

            var sharedData = new EdgeDeviceDataShared
            {
                Name = data.Name,
                Signature = data.Signature,
                Measurments = data.Measurments.Select(m => new MeasurmentShared
                {
                    Name = data.Name, // Jeśli każdy pomiar pochodzi od tego samego urządzenia
                    Value = m.Value,
                    timestamp = m.timestamp
                }).ToList()
            };

            if (!VerifySignature(sharedData))
            {
                Console.WriteLine("❌ Signature invalid. Block rejected.");
                return;
            }

            var latestBlock = GetLatestBlock();
            Block newBlock = new Block(latestBlock.Index + 1, data, latestBlock.Hash);
            _chain.Add(newBlock);

            SaveBlockchain();
            Console.WriteLine("Block accepted and saved.");
        }

        private bool VerifySignature(EdgeDeviceDataShared data)
        {
            try
            {
                // Serializacja z wymuszeniem camelCase, jak na Edge
                var payload = JsonSerializer.Serialize(new
                {
                    measurments = data.Measurments.Select(m => new {
                        name = m.Name,
                        value = m.Value,
                        timestamp = m.timestamp
                    }),
                    name = data.Name
                }, new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                });

                var payloadBytes = Encoding.UTF8.GetBytes(payload);
                var hash = SHA256.HashData(payloadBytes);

                var publicKeyHex = _appDbContext.Devices
                    .Where(x => x.Name == data.Name)
                    .Select(x => x.PublicKey)
                    .FirstOrDefault();

                if (publicKeyHex == null)
                {
                    Console.WriteLine("Public key not found in database.");
                    return false;
                }

                Console.WriteLine("SERVER PAYLOAD: " + payload);
                Console.WriteLine("SERVER HASH: " + Convert.ToHexString(hash));

                var pubKey = new PubKey(publicKeyHex);
                var signatureBytes = Encoders.Hex.DecodeData(data.Signature);
                var signature = ECDSASignature.FromDER(signatureBytes);

                return pubKey.Verify(new uint256(hash), signature);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Signature verification failed: {ex.Message}");
                return false;
            }
        }

        private void SaveBlockchain()
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            string json = JsonSerializer.Serialize(_chain, options);
            File.WriteAllText(_filePath, json);
            Console.WriteLine("Blockchain saved to file.");
        }

        public List<Block> GetBlockchain()
        {
            return _chain;
        }
    }
}
