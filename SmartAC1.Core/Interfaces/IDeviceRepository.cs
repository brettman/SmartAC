﻿using System.Collections.Generic;
using SmartAC1.Core.Models;

namespace SmartAC1.Core.Interfaces
{
    public interface IDeviceRepository
    {
        Device GetDevice(string serialNr, TimeLimit filter);
        IEnumerable<Device> SearchPartialSerialNr(string partialSerialNr);
        IEnumerable<Device> GetAllDevices();

        Device RegisterDevice(Device device);
    }
}