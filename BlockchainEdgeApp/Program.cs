using BlockchainEdgeApp.Security;
using BlockchainEdgeApp.Services;
using BlockchainServerAppAPI.Models;

namespace BlockchainEdgeApp
{
    internal class Program
    {
        
        static async Task Main(string[] args)
        {
            List<Measurment> list = new List<Measurment>();
            DataGenerationService dataGenerationService = new DataGenerationService();

            for (int i=0; i<3; i++)
            {
                Measurment value = dataGenerationService.GenerateData();
                list.Add(value);
            }

            EdgeDeviceData edgeDeviceData = new EdgeDeviceData()
            {
                Signature = "1212",
                Measurments = list,
            };
            
            //KeyGenerator keyGenerator = new KeyGenerator();
            //Console.WriteLine(keyGenerator.GetPublicKey());
            //await new SendingdataService().SendNameToServer();
            await new SendingdataService().SendDataToServer(edgeDeviceData);
        }
    }
}
