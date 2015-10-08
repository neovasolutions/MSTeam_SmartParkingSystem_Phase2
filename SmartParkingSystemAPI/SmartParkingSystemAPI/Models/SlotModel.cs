using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace SmartParkingSystemAPI.Models
{
    public class SlotModel
    {
        [DisplayName("Slot ID")]
        public int SlotID { get; set; }
        [DisplayName("Parking ID")]
        public int ParkingID { get; set; }
        [DisplayName("Slot Number")]
        public string SlotNumber { get; set; }
        [DisplayName("Is Active")]
        public Nullable<bool> IsActive { get; set; }
        [DisplayName("Is Acquired")]
        public bool IsAcquired { get; set; }
    }
}