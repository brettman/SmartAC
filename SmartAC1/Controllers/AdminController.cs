using System;
using Microsoft.AspNetCore.Mvc;
using SmartAC1.Core.Interfaces;
using SmartAC1.Core.Models;
using SmartAC1.Models;

namespace SmartAC1.Controllers
{
    public class AdminController : Controller
    {
        private readonly IDeviceService _deviceService;

        public AdminController(IDeviceService deviceService)
        {
            _deviceService = deviceService;
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpGet]
        public IActionResult Index()
        {
            var devices = _deviceService.GetAllDevices();
            return View(devices);
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpGet("Details")]
        public IActionResult Details(string serialNr, string timeLimit)
        {
            var limit = TimeLimit.today;
            Enum.TryParse(timeLimit, true, out limit);
            var device = _deviceService.GetDevice(serialNr, limit);
            return View(new DeviceViewModel(device));
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult Search(string searchString)
        {
            var found = _deviceService.SearchPartialSerialNr(searchString);
            return View("Index", found);
        }
    }
}