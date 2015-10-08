using System;
using System.Collections.Generic;
using System.Linq;

namespace SmartParking.API.Models
{
    public class UserProfileModel
    {
        public int UserID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Pincode { get; set; }
        public string MobileNumber { get; set; }
        public string EmailID { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public string ActiveVehicleNumber { get; set; }
    }
}