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
            return _context.AlertItems.Where(i => i.IsResolved == false);
        }

        public bool ResolveAlert(int id)
        {
            var item = _context.AlertItems.FirstOrDefault(i => i.Id == id);
            if (item == null) return false;
            item.IsResolved = true;
            _context.AlertItems.Update(item);
            return _context.SaveChanges() == 1;
        }

        public bool AddAlert(AlertItem item)
        {
            _context.AlertItems.Add(item);
            return _context.SaveChanges() == 1;
        }
    }
}