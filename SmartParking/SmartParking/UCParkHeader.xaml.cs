using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace SmartParking
{
    public partial class UCParkHeader : UserControl
    {
        public ActionOnImgTap Refresh_Tap { set; get; }
        public string ParkingName
        {
            set { txbParkingTitle.Text = value; }
            get { return txbParkingTitle.Text ; }
        }
        public UCParkHeader()
        {
            InitializeComponent();
        }
        public void BtnRefresh_Click(object sender, RoutedEventArgs e)
        {
            Refresh_Tap();
            //ClearAll_ParkingSlot();
            //LoadParkingSlots();
        }
    }
}
