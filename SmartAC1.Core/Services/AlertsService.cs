using System.Collections.Generic;
using SmartAC1.Core.Interfaces;
using SmartAC1.Core.Models;

namespace SmartAC1.Core.Services
{
    public class AlertsService : IAlertsService
    {
        private readonly IAlertsRepository _repo;

        public AlertsService(IAlertsRepository repo)
        {
            _repo = repo;
        }

        public IEnumerable<AlertItem> GetUnresolvedAlerts()
        {
            var alerts = _repo.GetUnresolvedAlerts();
            return alerts;
        }

        public bool AddAlert(AlertItem item)
        {
            return _repo.AddAlert(item);
        }

        public bool ResolveAlert(int id)
        {
            return _repo.ResolveAlert(id);
        }
    }
}