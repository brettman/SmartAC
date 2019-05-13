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
}