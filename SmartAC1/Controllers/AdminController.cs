using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using SmartAC1.Core.Interfaces;
using SmartAC1.Core.Models;
using SmartAC1.Models;

namespace SmartAC1.Controllers
{
    public class AdminController : Controller
    {
        private readonly IDeviceService _deviceService;
        private readonly ISensorDataService _sensorDataService;

        public AdminController(IDeviceService deviceService, ISensorDataService sensorDataService)
        {
            _deviceService = deviceService;
            _sensorDataService = sensorDataService;
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

        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpGet]
        public IActionResult RegisterDevice()
        {
            return View();
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpPost]
        public IActionResult RegisterDevice(Device device)
        {
            if (device != null)
                _deviceService.RegisterDevice(device);
            var devices = _deviceService.GetAllDevices();

            return View("Index", devices);
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpGet]
        public IActionResult SubmitData()
        {
            return View();
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpPost]
        public IActionResult SubmitData(SensorData data)
        {
            if(data != null)
                _sensorDataService.AddSensorDataBulk(new []{data});

            var devices = _deviceService.GetAllDevices();
            return View("Index", devices);
        }
    }
}