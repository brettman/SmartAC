using System;

namespace SmartAC1.Core.Models
{
    public class AlertItem
    {
        public int Id { get; set; }
        public DateTime TimeStamp { get; set; }
        public string SerialNr { get; set; }
        public string AlertMessage { get; set; }
        public bool IsResolved { get; set; }
    }
}