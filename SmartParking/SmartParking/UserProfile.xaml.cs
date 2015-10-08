using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.IO;
using System.IO.IsolatedStorage;
using SmartParking.API.Models;

namespace SmartParking
{
    public partial class UserProfile : PhoneApplicationPage
    {
        private readonly string _UserID = "SmartParking_UserIdentity";
        public UserProfile()
        {
            InitializeComponent();
        }
        private void LoadUserProfile()
        {
            string UserId;
            if (IsolatedStorageSettings.ApplicationSettings.Contains(_UserID ))
            {
                UserId = IsolatedStorageSettings.ApplicationSettings[_UserID].ToString();
                WebAPICaller myWebAPICaller = new WebAPICaller();
                myWebAPICaller.JsonSerializer = new System.Runtime.Serialization.Json.DataContractJsonSerializer(typeof(UserProfileModel));
                myWebAPICaller.ActionOnReadCompleted = new DlgtActionOnReadCompleted(ActionOn_LoadUserProfile);
                myWebAPICaller.GET("UserProfile/GetUserProfile/" + UserId);
            }
            else
            {
                txtUserName.Text = "";
                txtAddress.Text = "";
                txtMobileNo.Text = "";
                txtVehclNo.Text = "";
                txtCity.Text = "";
                txtPincode.Text = "";
                txtEmailID.Text = "";
                return;
            }
        }
        void ActionOn_LoadUserProfile(object usrProfl)
        {
            if (usrProfl == null) return;
            UserProfileModel model = (UserProfileModel)usrProfl;
            txtUserName.Text=model.FirstName ;
            txtAddress.Text=model.Address1 ;
            txtMobileNo.Text=model.MobileNumber ;
            txtVehclNo.Text=model.ActiveVehicleNumber ;
            txtCity.Text = model.City;
            txtPincode.Text = model.Pincode;
            txtEmailID.Text = model.EmailID; 
        }
        void ActionOn_SaveUserProfile(string userID)
        {
            if (userID  == null) return;
            IsolatedStorageSettings.ApplicationSettings[_UserID] = userID;
            IsolatedStorageSettings.ApplicationSettings.Save();
        }
        void Save()
        {
            if (!IsValidInput())
            {
                MessageBox.Show("User Name is mandatory.");
                return;
            }
            string UserId;
            WebAPICaller myWebAPICaller = new WebAPICaller();
            UserProfileModel model = new UserProfileModel();
            model.FirstName = txtUserName.Text;
            model.Address1 = txtAddress.Text;
            model.MobileNumber = txtMobileNo.Text;
            model.ActiveVehicleNumber = txtVehclNo.Text;
            model.City=txtCity.Text ;
            model.Pincode=txtPincode.Text ;
            model.EmailID=txtEmailID.Text ;
            if (IsolatedStorageSettings.ApplicationSettings.Contains(_UserID))
            {
                UserId = IsolatedStorageSettings.ApplicationSettings[_UserID].ToString();
                model.UserID = System .Convert.ToInt32(UserId);
                myWebAPICaller.POST("UserProfile/PostUpdateUserProfile/", model);
            }
            else
            {
                myWebAPICaller.ActionOnUploadCompleted = new DlgtActionOnUploadCompleted(ActionOn_SaveUserProfile);
                myWebAPICaller.POST("UserProfile/PostUserProfile/", model);
            }
            NavigationService.GoBack();
        }
        private bool IsValidInput()
        {
            if (txtUserName.Text == "")
                return false;
            else
                return true;
        }
        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
            LoadUserProfile();
        }
        private void imgBack_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            //NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
            ParkingBase.NavigateToParking(NavigationService);
        }
        private void imgSave_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            Save();
        }
        private void imgHome_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            NavigationService.Navigate(new Uri("/ParkingSel.xaml", UriKind.Relative));
        }
    }
}