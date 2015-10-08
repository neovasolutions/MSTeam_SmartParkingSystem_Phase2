using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Windows;
using System.Windows.Controls;
using System.IO.IsolatedStorage;
using SmartParking.API.Models;

namespace SmartParking
{
    public delegate void ActionOnImgTap();
    public class ParkingBase: PhoneApplicationPage
    {
        private Grid _SlotContainer;
        private UCParkCommand _ParkCommand;
        private UCParkHeader _ParkHeader;
        private static readonly string _SelParking = "SmartParking_SelParking";
        private readonly string _UserID = "SmartParking_UserIdentity";
        public ParkingBase()
        {
            this.Loaded += ParkingBase_Loaded;
        }
        void ParkingBase_Loaded(object sender, RoutedEventArgs e)
        {
            imgRefresh_Tap();
            Camera.ActionAfterBCScanned = new TakeAction(AllocateParkingSlots);
        }
        public void InitMe(Grid slotContnr, UCParkCommand ParkCommand, UCParkHeader ParkHeader)
        {
            _SlotContainer = slotContnr;
            _ParkCommand = ParkCommand;
            _ParkHeader = ParkHeader;
            _ParkCommand.Home_Tap = new ActionOnImgTap(imgHome_Tap);
            _ParkCommand.Park_Tap = new ActionOnImgTap(imgPark_Tap);
            _ParkCommand.UnPark_Tap  = new ActionOnImgTap(imgUnPark_Tap );
            _ParkCommand.UserProf_Tap = new ActionOnImgTap(imgUsrProf_Tap );
            _ParkHeader.Refresh_Tap = new ActionOnImgTap(imgRefresh_Tap);
        }
        public void LoadParkingSlots()
        {
            WebAPICaller myWebAPICaller = new WebAPICaller();
            myWebAPICaller.JsonSerializer = new System.Runtime.Serialization.Json.DataContractJsonSerializer(typeof(SlotModel[]));
            myWebAPICaller.ActionOnReadCompleted = new DlgtActionOnReadCompleted(ActionOn_LoadParkingSlots);
            string selParking = IsolatedStorageSettings.ApplicationSettings[_SelParking].ToString();
            myWebAPICaller.GET("SlotMaster/GetSlots" , "id=" + selParking);
        }
        public static void NavigateToParking(System.Windows.Navigation.NavigationService NavgServ)
        {
            string selParking = IsolatedStorageSettings.ApplicationSettings[_SelParking].ToString();
            switch (selParking)
            {
                case "1":
                    NavgServ.Navigate(new Uri("/Parking_Pentagon.xaml", UriKind.Relative));
                    break;
                case "2":
                    NavgServ.Navigate(new Uri("/Parking_Amanora.xaml", UriKind.Relative));
                    break;
                default:
                    MessageBox.Show("App is not configured for selected Parking");
                    break;
            }
        }
        void ActionOn_LoadParkingSlots(object myList)
        {
            if (myList == null) return;
            SlotModel[] parkedSlot = (SlotModel[])myList;
            ParkingSlot slot;
            ClearAll_ParkingSlot();
            foreach (SlotModel item in parkedSlot)
            {
                slot = (ParkingSlot)_SlotContainer.FindName(item.SlotNumber);
                if (slot == null)
                    continue;
                else
                {
                    if(item.IsActive) 
                        slot.Parked = item.IsAcquired;
                    else
                        slot.MakeMeInActive();
                }
            }
        }
        protected virtual void ClearAll_ParkingSlot()
        {
            
        }
        private void AllocateParkingSlots(bool isCalledFromBarcodeScanner, string barcodeNo, bool isAlloc)
        {
            WebAPICaller myWebAPICaller = new WebAPICaller();
            string selParking = IsolatedStorageSettings.ApplicationSettings[_SelParking].ToString();
            if (!IsolatedStorageSettings.ApplicationSettings.Contains(_UserID))
                IsolatedStorageSettings.ApplicationSettings[_UserID] = 12;
            string userId = IsolatedStorageSettings.ApplicationSettings[_UserID].ToString();
            string urlParam = "userId=" + userId + "&parkingId=" + selParking + "&slotNo=" + barcodeNo + "&isAcquired=" + isAlloc.ToString();
            myWebAPICaller.POST_urlParam("SlotMaster/PostParkUnPark",  urlParam );
            if (isCalledFromBarcodeScanner)
            {
                object slot = _SlotContainer.FindName(barcodeNo);
                if (slot == null) return;
                ((ParkingSlot)slot).Parked = isAlloc;
            }
        }
        public void ParkSlot_Click(object sender)
        {
            ParkingSlot parkSlot = (ParkingSlot)sender;
            parkSlot.Parked = !parkSlot.Parked;
            AllocateParkingSlots(false, parkSlot.Name, parkSlot.Parked);
        }
        public void imgRefresh_Tap()
        {
            ClearAll_ParkingSlot();
            LoadParkingSlots();
        }
        public void imgHome_Tap()
        {
            NavigationService.Navigate(new Uri("/ParkingSel.xaml", UriKind.Relative));
        }
        public void imgPark_Tap()
        {
            Camera.IsForParked = true;
            NavigationService.Navigate(new Uri("/BarcodeScanner.xaml", UriKind.Relative));
        }
        public void imgUnPark_Tap()
        {
            Camera.IsForParked = false;
            NavigationService.Navigate(new Uri("/BarcodeScanner.xaml", UriKind.Relative));
        }
        public void imgUsrProf_Tap()
        {
            NavigationService.Navigate(new Uri("/UserProfile.xaml", UriKind.Relative));
        }
    }
}
