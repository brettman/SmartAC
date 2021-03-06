﻿using System.Collections.Generic;
using SmartAC1.Core.Interfaces;
using SmartAC1.Core.Models;

namespace SmartAC1.Data.Repositories
{
    public class SensorDataRepository : ISensorDataRepository
    {
        private readonly SmartAc1DbContext _context;

        public SensorDataRepository(SmartAc1DbContext context)
        {
            _context = context;
        }

        public bool AddSensorData(SensorData sensorData)
        {
            _context.SensorData.Add(sensorData);
            return _context.SaveChanges() == 1;
        }

        public bool AddSensorDataBulk(IEnumerable<SensorData> sensorData)
        {
            _context.SensorData.AddRange(sensorData);
            return _context.SaveChanges() > 0;
        }
    }
}