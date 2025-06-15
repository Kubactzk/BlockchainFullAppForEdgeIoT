using BlockchainEdgeApp.Security;
using BlockchainEdgeApp.Services;
using BlockchainServerAppAPI.Models;
using System.Diagnostics;

namespace BlockchainEdgeApp
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            DataGenerationService dataGenerationService = new DataGenerationService();
            KeyGenerator keyGenerator = new KeyGenerator();

            int[] sizes = { 3, 10, 20, 30, 50, 100, 150, 200, 500, 1000 };

            foreach (int size in sizes)
            {
                for (int repetition = 0; repetition < 100; repetition++)
                {
                    Console.WriteLine($"============================= {size} ({repetition + 1}/100) =============================");

                    List<Measurment> list = new();

                    for (int i = 0; i < size; i++)
                    {
                        Measurment value = dataGenerationService.GenerateData();
                        list.Add(value);
                    }

                    EdgeDeviceData edgeDeviceData = new()
                    {
                        Measurments = list
                    };

                    var stopwatch = new Stopwatch();
                    stopwatch.Start();

                    await new SendingdataService().SendDataToServer(edgeDeviceData);

                    stopwatch.Stop();
                    long elapsedMs = stopwatch.ElapsedMilliseconds;

                    // Ścieżka pełna jak na serwerze
                    string dir = @"D:/Studia/ISA-magister/Magisterka/OfficialApplication/logs_edge";
                    string path = Path.Combine(dir, $"{size}.txt");
                    Directory.CreateDirectory(dir);
                    await File.AppendAllTextAsync(path, $"{elapsedMs}\n");

                    await Task.Delay(1000);
                }
            }
        }
    }
}
