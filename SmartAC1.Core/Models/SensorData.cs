using System;

namespace SmartAC1.Core.Models
{
    public class SensorData
    {
        public int Id { get; set; }
        public string SerialNr { get; set; }
        public DateTime SubmissionTime { get; set; }
        public double TemperatureInCelsius { get; set; }
        public double HumidityPercentage { get; set; }
        public double CarbonMonoxidePpm { get; set; }
        public string DeviceHealthStatus { get; set; }
        
        // I think this is not a good idea... we want to allow for very fast inserts, and enforcing this join 
        //  doesn't seem to offer much benefit, but will cost a lot of lookups for device id's on bulk updates.
        // Can be revisited later.

        //[JsonIgnore]
        //public virtual Device Device { get; set; }
    }
}