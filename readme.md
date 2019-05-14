# SmartAC Proof of Concept

---

SmartAC is a set of API's and management pages for smart air conditioners.  The deployed air conditioners will submit a range of sensor readings at regular intervals, as well as data about hardware/software status of the AC unit itself.

## Technical specs
* Tech stack: ASP.NET Core 2.2 with MVC and WebAPI.
* Database: SQL Server.
* ORM: EntityFramework Core
* Identity management:  standard MS identity  (not fully implemented, see below!)
* IOC / DI:  Unity (Microsoft)
* Hosting:  Azure
---

## Quick links
The full site is deployed here:
http://theorem-smartac.azurewebsites.net/

API documentation is here:
http://theorem-smartac.azurewebsites.net/swagger
---

## Remaining work
The spec has not yet been fully implemented.  There are several items to address in the next sprint:

1) Security.
Neither the admin pages, nor the API's have been secured.  We need to ensure that all traffic is submitted over SSL, then at very least add basic authentication tokens to the API submission requests.  
User security and logins have been implemented internally.  The database is seeded with a default user in the 'admin' role with:
```
username:  admintest@theorem.com
password:  pass_word0
```
Unfortunately there was not enough time in the first round to complete the UI and allow users to login/logout.

2) UI.
No effort has yet been made to implement graphic design or UI styling to the admin pages.  These are ASP.NET default templates.  A UI specialist should be engaged to improve the look and usability of the site.

3) Sensor data display.
While the device page does allow a user to view a device's sensor data in tabular format, there are a number of required improvements here.  First, I was not able to complete the implementation of a graph display.  This work should probably be assigned to the UI specialist in phase two.  Second, there is a larger concern given the amount of data to display in the graph itself.  With one minute submission intervals, we achieve 1440 datapoints each day.  Some thought should be given to decide on an appropriate way to summarise and display this data over different time periods.

4) Performance.
This API and site are for POC only and have not been designed for performance.  You will notice this if you try to load a year of sensor data in the Device page...
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
---

## Misc
* There is a database init class that may or may not be useful in a production scenario. It ensures the creation of the database, and seeds the db with a default admin user.  This can create issues with the regular EF 'add-migration' - 'update-database' workflow.  I usually just bring it in because it speeds up my work in the initial stages of development.  I've left it here for reference.

