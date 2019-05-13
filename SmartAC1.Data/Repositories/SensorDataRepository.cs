using System.Collections.Generic;
using System.Linq;
using SmartAC1.Core.Interfaces;
using SmartAC1.Core.Models;

namespace SmartAC1.Data.Repositories
{
    public class AlertsRepository : IAlertsRepository
    {
        private readonly SmartAc1DbContext _context;

        public AlertsRepository(SmartAc1DbContext context)
        {
            _context = context;
        }

        public IEnumerable<AlertItem> GetUnresolvedAlerts()
        {
            throw new System.NotImplementedException();

        }

        public bool ResolveAlert(int id)
        {
            throw new System.NotImplementedException();
        }
    }

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