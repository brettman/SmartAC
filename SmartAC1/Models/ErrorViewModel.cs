using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using SmartAC1.Core.Models;

namespace SmartAC1.Models
{
    public class DeviceViewModel
    {
        private readonly Device _device;

        public DeviceViewModel(Device device)
        {
            _device = device;
            SensorData = device.SensorData
                .Select(i => new SensorDataViewModel(i))
                .ToList();
        }

        public string SerialNr => _device.SerialNr;
        public string RegistrationDate => _device.RegistrationDate.ToString("u");
        public string FirmwareVersion => _device.FirmwareVersion;

        public List<SensorDataViewModel> SensorData { get; }
    }

    public class SensorDataViewModel
    {
        private readonly SensorData _data;

        public SensorDataViewModel(SensorData data) { _data = data; }
        public string Timestamp => _data.SubmissionTime.ToString("yyyy-MM-dd hh:mm");
        public string Temperature => Math.Round(_data.TemperatureInCelsius, 2).ToString(CultureInfo.InvariantCulture);
        public string Humidity => Math.Round(_data.HumidityPercentage, 2).ToString(CultureInfo.InvariantCulture);
        public string CO => Math.Round(_data.CarbonMonoxidePpm, 2).ToString(CultureInfo.InvariantCulture);
    }

    public class ErrorViewModel
    {
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}