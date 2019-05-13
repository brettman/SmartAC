using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace SmartAC1.Core.Models
{
    public class Device
    {
        public string SerialNr { get; set; }
        public DateTime RegistrationDate { get; set; }
        public string FirmwareVersion { get; set; }

        public IEnumerable<SensorData> SensorData { get; set; }
    }
}
