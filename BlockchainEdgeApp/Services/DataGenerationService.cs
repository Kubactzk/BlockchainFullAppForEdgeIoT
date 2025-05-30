﻿using BlockchainServerAppAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockchainEdgeApp.Services
{
    internal class DataGenerationService
    {
        public static string deviceName = "Edge_device_1";
        public string[] sensorTypes = ["Temperature", "Humidity", "Preassure", "Vibration"];
        public Measurment GenerateData()
        {
            Random random = new Random();
            int index = random.Next(sensorTypes.Length);

            var data = new Measurment()
            {
                IoTDeviceName = sensorTypes[index],
                timestamp = DateTime.Now,
                Value = random.NextDouble(),
            };

            return data;
        }
    }
}
