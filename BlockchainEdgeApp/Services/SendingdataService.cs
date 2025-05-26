using BlockchainEdgeApp.Models;
using BlockchainEdgeApp.Security;
using BlockchainServerAppAPI.Models;
using NBitcoin;
using NBitcoin.DataEncoders;
using System;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BlockchainEdgeApp.Services
{
    internal class SendingdataService
    {
        private readonly string name = "Edge_device_1";
        private readonly string ip = "localhost";
        private readonly KeyGenerator keyGen;

        public SendingdataService()
        {
            keyGen = new KeyGenerator();
        }

        public async Task SendDataToServer(EdgeDeviceData data)
        {
            await SignData(data);

            using var http = new HttpClient();
            var json = JsonSerializer.Serialize(data);
            var response = await http.PostAsync($"http://{ip}:5238/api/block", new StringContent(json, Encoding.UTF8, "application/json"));
            Console.WriteLine(await response.Content.ReadAsStringAsync());
        }

        private async Task SignData(EdgeDeviceData data)
        {
            data.Name = name;

            var payload = JsonSerializer.Serialize(new
            {
                data.Measurments,
                data.Name
            }, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });


            var payloadBytes = Encoding.UTF8.GetBytes(payload);
            var hash = SHA256.HashData(payloadBytes);

            var privateKey = keyGen.GetPrivateKey();
            var signature = privateKey.Sign(new uint256(hash)).ToDER();

            data.Signature = Encoders.Hex.EncodeData(signature);
            Console.WriteLine("EDGE PAYLOAD: " + payload);
            Console.WriteLine("EDGE HASH: " + Convert.ToHexString(hash));

        }

        public async Task SendNameToServer()
        {
            using var http = new HttpClient();
            AuthorityData authorityData = new AuthorityData()
            {
                Name = name,
                PublicKey = keyGen.GetPublicKeyHex(),
            };
            var json = JsonSerializer.Serialize(authorityData);
            var response = await http.PostAsync($"http://{ip}:5238/api/authority", new StringContent(json, Encoding.UTF8, "application/json"));
            Console.WriteLine(await response.Content.ReadAsStringAsync());
        }
    }
}
