using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using SmartAC1.Core.Interfaces;
using SmartAC1.Core.Models;

namespace SmartAC1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SensorDataController : ControllerBase
    {
        private readonly ISensorDataService _dataService;

        public SensorDataController(ISensorDataService dataService)
        {
            _dataService = dataService;
        }

        [HttpPost]
        public IActionResult Post(IEnumerable<SensorData> data)
        {
            try
            {
                _dataService.AddSensorDataBulk(data);
                return Ok();
            }
            catch (Exception ex)
            {
                return new StatusCodeResult(500);
            }
        }
    }
}