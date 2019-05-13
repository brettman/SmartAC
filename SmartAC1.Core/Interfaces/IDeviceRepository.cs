using System.Collections.Generic;
using SmartAC1.Core.Models;

namespace SmartAC1.Core.Interfaces
{
    public interface IDeviceService
    {
        Device RegisterDevice(Device device);

        /// <summary>
        /// Returns the device with sensor data within submitted time constraint.
        /// </summary>
        /// <param name="serialNr"></param>
        /// <returns></returns>
        Device GetDevice(string serialNr, TimeLimit filter);

        IEnumerable<Device> SearchPartialSerialNr(string partialSerialNr);
        IEnumerable<Device> GetAllDevices();
    }

    public interface ISensorDataService
    {
        void AddSensorData(SensorData sensorData);
        void AddSensorDataBulk(IEnumerable<SensorData> sensorData);
    }

    public interface IDeviceRepository
    {
        Device GetDevice(string serialNr, TimeLimit filter);
        IEnumerable<Device> SearchPartialSerialNr(string partialSerialNr);
        IEnumerable<Device> GetAllDevices();

        Device RegisterDevice(Device device);
    }

    public interface ISensorDataRepository
    {
        bool AddSensorData(SensorData sensorData);
        bool AddSensorDataBulk(IEnumerable<SensorData> sensorData);
    }
}