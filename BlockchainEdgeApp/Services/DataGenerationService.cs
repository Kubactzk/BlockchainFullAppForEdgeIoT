using BlockchainServerAppAPI.Models;

internal class DataGenerationService
{
    public static string deviceName = "Edge_device_1";
    public string[] sensorTypes = ["Temperature", "Humidity", "Preassure", "Vibration", "Precision_unit"];
    private readonly Random random = new Random();

    public Measurment GenerateData()
    {
        int index = random.Next(sensorTypes.Length);
        string sensor = sensorTypes[index];
        double value = GenerateValueForSensor(sensor);

        return new Measurment()
        {
            IoTDeviceName = sensor,
            timestamp = DateTime.Now,
            Value = value
        };
    }

    private double GenerateValueForSensor(string sensorType)
    {
        return sensorType switch
        {
            "Precision_unit" => random.NextDouble(),
            "Temperature" => Math.Round(random.NextDouble() * 60 - 10, 2),
            "Humidity" => Math.Round(random.NextDouble() * 100, 2),
            "Preassure" => Math.Round(950 + random.NextDouble() * 100, 2),
            "Vibration" => Math.Round(random.NextDouble() * 5, 2),
            _ => 0.0
        };
    }
}
