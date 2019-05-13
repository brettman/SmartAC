using System;
using System.Collections.Generic;
using SmartAC1.Core.Interfaces;
using SmartAC1.Core.Models;

namespace SmartAC1.Core.Services
{
    public class DeviceService : IDeviceService
    {
        private readonly IDeviceRepository _repo;
        private readonly IAlertsRepository _alerts;

        public DeviceService(IDeviceRepository repo)
        {
            _repo = repo;
        }

        public Device GetDevice(string serialNr, TimeLimit filter = TimeLimit.today)
        {
            return _repo.GetDevice(serialNr, filter);
        }

        public IEnumerable<Device> SearchPartialSerialNr(string partialSerialNr)
        {
            return _repo.SearchPartialSerialNr(partialSerialNr);
        }

        public IEnumerable<Device> GetAllDevices()
        {
            return _repo.GetAllDevices();
        }

        public Device RegisterDevice(Device device)
        {
            return _repo.RegisterDevice(device);
        }
    }
}