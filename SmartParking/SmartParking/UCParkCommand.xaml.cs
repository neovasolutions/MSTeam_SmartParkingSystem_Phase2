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
    public partial class UCParkCommand : UserControl
    {
        public ActionOnImgTap Home_Tap { set; get; }
        public ActionOnImgTap Park_Tap { set; get; }
        public ActionOnImgTap UnPark_Tap { set; get; }
        public ActionOnImgTap UserProf_Tap { set; get; }
        
        public UCParkCommand()
        {
            InitializeComponent();
        }
        public void BtnRefresh_Click(object sender, RoutedEventArgs e)
        {
        }
        public void imgHome_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            Home_Tap();
        }
        public void imgPark_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            Park_Tap();
        }
        public void imgUnPark_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            UnPark_Tap();
        }
        public void imgUsrProf_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            UserProf_Tap();
        }
    }
}
