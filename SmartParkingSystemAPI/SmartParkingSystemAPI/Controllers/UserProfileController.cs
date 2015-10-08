using SmartParkingSystemAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace SmartParkingSystemAPI.Controllers
{
    public class UserProfileController : ApiController
    {
        public List<UserProfileModel> GetUserProfileList()
        {
            List<UserProfileModel> result = new List<UserProfileModel>();
            using (SmartParkingEntities context = new SmartParkingEntities())
            {
                var qry = context.UserProfiles.ToList();

                if (qry.Count() > 0)
                {
                    result = qry.Select(u => new UserProfileModel()
                    {
                        ActiveVehicleNumber = u.ActiveVehicleNumber,
                        Address1 = u.Address1,
                        Address2 = u.Address2,
                        City = u.City,
                        EmailID = u.EmailID,
                        FirstName = u.FirstName,
                        IsActive = u.IsActive,
                        LastName = u.LastName,
                        MobileNumber = u.MobileNumber,
                        Pincode = u.Pincode,
                        State = u.State,
                        UserID = u.UserID
                    }).ToList();
                }
            }
            return result;
        }

        public int PostUserProfile(UserProfileModel usr)
        {
            int result = 0;
            using (SmartParkingEntities context = new SmartParkingEntities())
            {
                UserProfile usrProfile = new UserProfile()
                {
                    FirstName = usr.FirstName,
                    LastName = usr.LastName,
                    EmailID = usr.EmailID,
                    MobileNumber = usr.MobileNumber,
                    Address1 = usr.Address1,
                    Address2 = usr.Address2,
                    City = usr.City,
                    State = usr.State,
                    Pincode = usr.Pincode,
                    ActiveVehicleNumber = usr.ActiveVehicleNumber,
                    IsActive = usr.IsActive
                };

                context.UserProfiles.Add(usrProfile);
                context.SaveChanges();
                result = usrProfile.UserID;
            }
            return result;
        }

        public UserProfileModel GetUserProfile(int inputId)
        {
            UserProfileModel result = new UserProfileModel();
            using (SmartParkingEntities context = new SmartParkingEntities())
            {
                var usr = context.UserProfiles.Where(u => u.UserID == inputId).SingleOrDefault();
                if (usr != null)
                {
                    result = ConvertDBToModelObject(usr);
                }
            }
            return result;
        }

        public bool PostUpdateUserProfile(UserProfileModel usr)
        {
            bool result = false;
            using (SmartParkingEntities context = new SmartParkingEntities())
            {
                UserProfile usrProfile = context.UserProfiles.Where(u => u.UserID == usr.UserID).SingleOrDefault();
                usrProfile.FirstName = usr.FirstName;
                usrProfile.LastName = usr.LastName;
                usrProfile.EmailID = usr.EmailID;
                usrProfile.MobileNumber = usr.MobileNumber;
                usrProfile.Address1 = usr.Address1;
                usrProfile.Address2 = usr.Address2;
                usrProfile.City = usr.City;
                usrProfile.State = usr.State;
                usrProfile.Pincode = usr.Pincode;
                usrProfile.ActiveVehicleNumber = usr.ActiveVehicleNumber;
                usrProfile.IsActive = usr.IsActive;

                context.SaveChanges();

                result = true;
            }
            return result;
        }

        public UserProfileModel ConvertDBToModelObject(UserProfile usr)
        {
            return new UserProfileModel()
             {
                 ActiveVehicleNumber = usr.ActiveVehicleNumber,
                 Address1 = usr.Address1,
                 Address2 = usr.Address2,
                 City = usr.City,
                 EmailID = usr.EmailID,
                 FirstName = usr.FirstName,
                 IsActive = usr.IsActive,
                 LastName = usr.LastName,
                 MobileNumber = usr.MobileNumber,
                 Pincode = usr.Pincode,
                 State = usr.State,
                 UserID = usr.UserID
             };
        }

        public List<UserProfileModel> SearchUserProfile(UserSearchModel model)
        {
            List<UserProfileModel> result = new List<UserProfileModel>();
            using (SmartParkingEntities context = new SmartParkingEntities())
            {
                var qry = context.UserProfiles.Where(s => s.IsActive == true).ToList();

                if (model != null)
                {
                    if (model.UserName != null)
                        qry = qry.Where(u => u.FirstName.Contains(model.UserName)).ToList();
                    if (model.City != null)
                        qry = qry.Where(u => u.City.Contains(model.City)).ToList();
                    if (model.Email != null)
                        qry = qry.Where(u => u.EmailID.Contains(model.Email)).ToList();
                }

                if (qry.Count() > 0)
                {
                    result = qry.Select(u => new UserProfileModel()
                    {
                        ActiveVehicleNumber = u.ActiveVehicleNumber,
                        Address1 = u.Address1,
                        Address2 = u.Address2,
                        City = u.City,
                        EmailID = u.EmailID,
                        FirstName = u.FirstName,
                        IsActive = u.IsActive,
                        LastName = u.LastName,
                        MobileNumber = u.MobileNumber,
                        Pincode = u.Pincode,
                        State = u.State,
                        UserID = u.UserID
                    }).ToList();
                }
            }
            return result;
        }
    }
}
