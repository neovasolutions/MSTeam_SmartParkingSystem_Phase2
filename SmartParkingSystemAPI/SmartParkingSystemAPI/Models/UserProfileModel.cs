using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SmartParkingSystemAPI.Models
{
    public class UserProfileModel
    {
        public int UserID { get; set; }

        [DisplayName("First Name")]        
        public string FirstName { get; set; }

        [DisplayName("Last Name")]        
        public string LastName { get; set; }

        [DisplayName("Address one")]
        public string Address1 { get; set; }

        [DisplayName("Address two")]
        public string Address2 { get; set; }

        [DisplayName("City")]
        public string City { get; set; }

        [DisplayName("State")]
        public string State { get; set; }

        [DisplayName("Pincode")]
        public string Pincode { get; set; }

        [DisplayName("Mobile No")]
        public string MobileNumber { get; set; }

        [DisplayName("Email ID")]
        public string EmailID { get; set; }

        
        public Nullable<bool> IsActive { get; set; }

        [DisplayName("Active Vehicle No")]
        public string ActiveVehicleNumber { get; set; }
    }
}