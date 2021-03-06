﻿using System.Collections.Generic;
using SmartAC1.Core.Models;

namespace SmartAC1.Core.Interfaces
{
    public interface IAlertsService
    {
        IEnumerable<AlertItem> GetUnresolvedAlerts();
        bool AddAlert(AlertItem item);
        bool ResolveAlert(int id);
    }
}