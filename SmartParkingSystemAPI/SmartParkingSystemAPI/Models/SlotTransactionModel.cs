using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartParkingSystemAPI.Models
{
    public class SlotTransactionModel
    {
        public int TransactionID { get; set; }
        public int SlotID { get; set; }
        public int UserID { get; set; }
        public Nullable<System.DateTime> ParkingInTime { get; set; }
        public Nullable<System.DateTime> ParkingOutTime { get; set; }
        public string VehicleNumber { get; set; }
        public string SlotName { get; set; }
        public string UserName { get; set; }
        public string ParkingName { get; set; }
    }
}