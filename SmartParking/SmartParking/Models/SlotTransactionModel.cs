using System;
using System.Collections.Generic;
using System.Linq;

namespace SmartParking.API.Models
{
    public class SlotTransactionModel
    {
        public int TransactionID { get; set; }
        public int SlotID { get; set; }
        public int UserID { get; set; }
        public Nullable<System.DateTime> ParkingInTime { get; set; }
        public Nullable<System.DateTime> ParkingOutTime { get; set; }
        public string VehicleNumber { get; set; }
    }
}