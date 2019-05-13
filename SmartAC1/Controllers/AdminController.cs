using Microsoft.AspNetCore.Mvc;
using SmartAC1.Core.Interfaces;
using SmartAC1.Core.Models;

namespace SmartAC1.Controllers
{
    public class AdminController : Controller
    {
        private readonly IDeviceService _deviceService;

        public AdminController(IDeviceService deviceService)
        {
            _deviceService = deviceService;
        }

        public IActionResult Index()
        {
            var devices = _deviceService.GetAllDevices();
            return View(devices);
        }

        public IActionResult Details(string serialNr)
        {
            var device = _deviceService.GetDevice(serialNr, TimeLimit.this_week);
            return View(device);
        }
    }
}