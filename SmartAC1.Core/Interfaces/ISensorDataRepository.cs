using System.Collections.Generic;
using SmartAC1.Core.Models;

namespace SmartAC1.Core.Interfaces
{
    public interface ISensorDataRepository
    {
        bool AddSensorData(SensorData sensorData);
        bool AddSensorDataBulk(IEnumerable<SensorData> sensorData);
    }
}