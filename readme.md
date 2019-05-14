# SmartAC Proof of Concept

SmartAC is a set of API's and management pages for smart air conditioners.  The deployed air conditioners will submit a range of sensor readings at regular intervals, as well as data about hardware/software status of the AC unit itself.

## Quick links
The full site is deployed here:
http://theorem-smartac.azurewebsites.net/

API documentation is here:
http://theorem-smartac.azurewebsites.net/swagger

---

## Technical specs
* Tech stack: ASP.NET Core 2.2 with MVC and WebAPI.
* Database: SQL Server.
* ORM: EntityFramework Core
* Identity management:  standard MS identity  (not fully implemented, see below!)
* IOC / DI:  Unity (Microsoft)
* Hosting:  Azure

---

## API's

### Device
#### [GET] /api/device
Returns list of all devices.

#### [POST] /api/device
Submit new device for registration.

#### [GET] /api/Device/{serialNr}/{timeLimit}
Get's a device by serial number, and its associated sensor data within the given time limit.
Accepted time limit values are:
- today
- this_week
- this_month
- this_year

#### [PUT] /api/device/seed
This is a kludge to seed the development database with some pseudo random values.  It is explicitly excluded from the API documentation, and should be removed from any production deployment.

### Sensor Data
#### [POST] /api/sensorData
Accepts and array of sensor data values from a specific device.  
Temperature, humidity and carbon monoxide values are decimal values, e.g.:  19.03 or 0.25.
Humidity value submitted as percentage and should be less than one: 0.21

```
[
  {
    "serialNr": "string",
    "submissionTime": "2019-05-14T11:26:24.468Z",
    "temperatureInCelsius": 0,
    "humidityPercentage": 0,
    "carbonMonoxidePpm": 0,
    "deviceHealthStatus": "string"
  }
]
```

---
## Admin pages
These are extremely basic administrative pages allowing a user to view and search devices, along with related sensor data.  The home screen additionally lists actionable alerts in the following cases:
1. Carbon monoxide level submitted as greater than 9.0
2. Device health status is any of the following string values:
    * gas_leak
    * needs_new_filter
    * needs_service

The actionable alerts can be marked as resolved by the logged in administrator.
