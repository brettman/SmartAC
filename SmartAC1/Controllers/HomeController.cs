using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SmartAC1.Core.Interfaces;
using SmartAC1.Models;

namespace SmartAC1.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAlertsService _alertsService;

        public HomeController(IAlertsService alertsService)
        {
            _alertsService = alertsService;
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpGet]
        public IActionResult Index()
        {
            var activeAlerts = _alertsService.GetUnresolvedAlerts();
            return View(activeAlerts);
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpGet]
        public IActionResult Resolve(int id)
        {
             _alertsService.ResolveAlert(id);
            var activeAlerts = _alertsService.GetUnresolvedAlerts();
            return View("Index",activeAlerts);
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpGet("privacy")]
        public IActionResult Privacy()
        {
            return View();
        }
        
        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpGet("Error")]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
