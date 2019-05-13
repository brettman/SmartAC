using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using Microsoft.EntityFrameworkCore;
using SmartAC1.Core.Interfaces;
using SmartAC1.Core.Models;

namespace SmartAC1.Data.Repositories
{
    public class DeviceRepository : IDeviceRepository
    {
        private readonly SmartAc1DbContext _context;

        private static readonly Func<DateTime> FirstOfWeek = () =>
        {
            var diff = (7 + (DateTime.Today.DayOfWeek - DayOfWeek.Monday)) % 7;
            return DateTime.Today.AddDays(-1 * diff).Date;
        };

        private static readonly Func<TimeLimit, DateTime> StartDate = limit =>
        {
            switch (limit)
            {
                case TimeLimit.today: return DateTime.Today;
                case TimeLimit.this_week: return FirstOfWeek();
                case TimeLimit.this_month: return new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
                case TimeLimit.this_year: return new DateTime(DateTime.Today.Year, 01, 01);
                default: return DateTime.Today;
            }
        };

        public DeviceRepository(SmartAc1DbContext context)
        {
            _context = context;
        }

        public Device GetDevice(string serialNr, TimeLimit filter)
        {
            var device = _context
                .Devices
                .FirstOrDefault(i => i.SerialNr == serialNr);
            if (device == null)
                throw new ArgumentException("Serial Number not found.");

            // I guess this is less elegant than using .Include on the device, but we can fix that later if we need.  I think this is more performant anyway...
            var startDate = StartDate(filter);
            var sensorData = _context
                .SensorData
                .Where(i => i.SerialNr == serialNr)
                .Where(i => i.SubmissionTime >= startDate)
                .OrderBy(i => i.SubmissionTime)
                .ToArray();

            device.SensorData = sensorData;
            return device;
        }

        public IEnumerable<Device> SearchPartialSerialNr(string partialSerialNr)
        {
            var devices = _context
                .Devices
                .Where(i => i.SerialNr.StartsWith(partialSerialNr))
                .ToList();

            return devices;
        }

        public IEnumerable<Device> GetAllDevices()
        {
            // this feels like a bad idea...
            return _context.Devices.ToList();
        }

        public Device RegisterDevice(Device device)
        {
            _context.Devices.Add(device);
            _context.SaveChanges();
            return device;
        }
    }
}
