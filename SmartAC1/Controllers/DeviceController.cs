using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartAC1.Core.Interfaces;
using SmartAC1.Core.Models;

namespace SmartAC1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeviceController : ControllerBase
    {
        private static readonly Random _rnd = new Random(DateTime.UtcNow.Millisecond);
        private readonly IDeviceService _deviceService;

        // limit variance to +- 5%
        private Func<double, double> _variance = val =>
        {
            var spread = _rnd.Next(1, 5) * .01;

            if (_rnd.Next(100) % 3 == 0) return val;

            if (_rnd.Next(100) % 2 == 0)
                return val + (val * spread);
            return val - (val * spread);
        };

        public DeviceController(IDeviceService deviceService)
        {
            _deviceService = deviceService;
        }

        [HttpGet]
        public IEnumerable<Device> Get()
        {
            var result = _deviceService.GetAllDevices();
            return result;
        }

        [HttpGet("{serialNr}/{timeLimit}")]
        public Device GetBySerialNr(string serialNr, string timeLimit)
        {
            var limit = TimeLimit.today;
            Enum.TryParse(timeLimit, true, out limit);
            var result = _deviceService.GetDevice(serialNr, limit);
            return result;
        }

        [HttpPost]
        public Device AddDevice([FromBody]Device device)
        {
            var result = _deviceService.RegisterDevice(device);
            return result;
        }

        [HttpPut, Route("Seed")]
        public IActionResult Seed()
        {
            var devices = Enumerable
                .Range(1, 3)
                .Select(i => new Device
                {
                    FirmwareVersion = "1.0.0",
                    RegistrationDate = DateTime.Today.AddDays(_rnd.Next(90) * -1),
                    SerialNr = $"{_rnd.Next(100, 999)}-x1",
                });
            devices.ToList().ForEach(i =>
            {
                SeedSensorData(i);
                _deviceService.RegisterDevice(i);
            });

            return Ok();
        }

        private void SeedSensorData(Device device)
        {
            const int minutes_in_day = 1440;
            var days = 7;
            var eventsToGenerate = days * minutes_in_day;

            var sensorDatas = new List<SensorData>();

            SensorData prev = new SensorData
            {
                CarbonMonoxidePpm = 3.1,
                HumidityPercentage = .35,
                TemperatureInCelsius = 17.8,
                SubmissionTime = device.RegistrationDate.AddMinutes(1),
            };

            for (var i = 0; i < eventsToGenerate; i++)
            {
                var sensorData = new SensorData
                {
                    SerialNr = device.SerialNr,
                    CarbonMonoxidePpm = _variance(prev.CarbonMonoxidePpm),
                    HumidityPercentage = _variance(prev.HumidityPercentage),
                    TemperatureInCelsius = _variance(prev.TemperatureInCelsius),
                    SubmissionTime = prev.SubmissionTime.AddMinutes(1),
                    DeviceHealthStatus = _rnd.Next(100) % 33 == 0 ? "needs_service" : "all_good",
                };

                sensorDatas.Add(sensorData);
                prev = sensorData;
            }

            device.SensorData = sensorDatas;

        }
    }
}