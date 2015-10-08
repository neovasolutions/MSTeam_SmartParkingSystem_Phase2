using System;
using System.Collections.Generic;
using System.Linq;

namespace SmartParking.API.Models
{
    public class SlotModel
    {
        public int SlotID { get; set; }
        public int ParkingID { get; set; }
        public string SlotNumber { get; set; }
        public bool IsActive { get; set; }
        public bool IsAcquired { get; set; }
    }
}