using System.Collections.Generic;
using SmartAC1.Core.Models;

namespace SmartAC1.Core.Interfaces
{
    public interface ISensorDataService
    {
        void AddSensorData(SensorData sensorData);
        void AddSensorDataBulk(IEnumerable<SensorData> sensorData);
    }
}