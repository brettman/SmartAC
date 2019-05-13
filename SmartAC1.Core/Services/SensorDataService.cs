using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SmartAC1.Core.Interfaces;
using SmartAC1.Core.Models;

namespace SmartAC1.Core.Services
{
    public class SensorDataService : ISensorDataService
    {
        private readonly ISensorDataRepository _repo;
        private static readonly Func<IEnumerable<SensorData>, IEnumerable<SensorData>> HealthAlertData = data
            => data.Where(i => i.DeviceHealthStatus == "needs_service"
                               || i.DeviceHealthStatus == "needs_new_filter"
                               || i.DeviceHealthStatus == "gas_leak");

        public SensorDataService(ISensorDataRepository repo)
        {
            _repo = repo;
        }

        public void AddSensorData(SensorData sensorData)
        {
            if(!_repo.AddSensorData(sensorData))
                throw new ArgumentException();
        }

        public void AddSensorDataBulk(IEnumerable<SensorData> sensorData)
        {
            var enumerated = sensorData.ToList();
            var healthAlerts = HealthAlertData(enumerated).ToList();

            // health alerts
            if (healthAlerts.Any())
                RaiseDeviceHealthAlert(healthAlerts);

            // CO alerts
            if (enumerated.Any(i => i.CarbonMonoxidePpm > 9))
                RaiseCoAlert(enumerated.Where(i => i.CarbonMonoxidePpm > 9));

            // submit to db
            if(!_repo.AddSensorDataBulk(enumerated))
                throw new ArgumentException();
        }

        private void RaiseDeviceHealthAlert(IEnumerable<SensorData> alertData)
        {
            // todo:  make this data available to the logged in Admin
            alertData.ToList().ForEach(i => 
                Console.WriteLine($"Alert:  Device Health Status: {i.SerialNr} | {i.DeviceHealthStatus}"));
        }

        private void RaiseCoAlert(IEnumerable<SensorData> alertData)
        {
            // todo:  raise alerts here...
            alertData.ToList().ForEach(i => 
                Console.WriteLine($"Alert:  Carbon monoxide level > 9ppm: {i.SerialNr} | {i.CarbonMonoxidePpm}ppm | {i.SubmissionTime}"));
        }
    }
}
