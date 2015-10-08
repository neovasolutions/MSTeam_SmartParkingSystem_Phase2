using System;
using System.Collections.Generic;
using System.Linq;

namespace SmartParking.API.Models
{
    public class ParkingModel
    {
        public int ParkingID { get; set; }
        public string ParkingName { get; set; }
        public string ParkingAddress { get; set; }
        public string ParkingTemplateName { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public int NumberOfSlots { get; set; }
    }
}