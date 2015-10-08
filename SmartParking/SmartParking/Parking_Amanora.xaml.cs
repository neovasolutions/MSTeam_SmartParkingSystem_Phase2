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
    public partial class Parking_Amanora : ParkingBase
    {
        public Parking_Amanora()
        {
            InitializeComponent();
            InitMe(ContentPanel, ParkCommand, ParkHeader);
        }
        protected override void ClearAll_ParkingSlot()
        {
            ((ParkingSlot)ContentPanel.FindName("R0C0")).Parked = false;
            ((ParkingSlot)ContentPanel.FindName("R0C1")).Parked = false;
            ((ParkingSlot)ContentPanel.FindName("R0C2")).Parked = false;
            ((ParkingSlot)ContentPanel.FindName("R0C3")).Parked = false;

            ((ParkingSlot)ContentPanel.FindName("R1C0")).Parked = false;
            ((ParkingSlot)ContentPanel.FindName("R1C1")).Parked = false;
            ((ParkingSlot)ContentPanel.FindName("R1C2")).Parked = false;
            ((ParkingSlot)ContentPanel.FindName("R1C3")).Parked = false;

            ((ParkingSlot)ContentPanel.FindName("R2C0")).Parked = false;
            ((ParkingSlot)ContentPanel.FindName("R2C1")).Parked = false;
            ((ParkingSlot)ContentPanel.FindName("R2C2")).Parked = false;
            ((ParkingSlot)ContentPanel.FindName("R2C3")).Parked = false;

            ((ParkingSlot)ContentPanel.FindName("R3C0")).Parked = false;
            ((ParkingSlot)ContentPanel.FindName("R3C1")).Parked = false;
            ((ParkingSlot)ContentPanel.FindName("R3C2")).Parked = false;
            ((ParkingSlot)ContentPanel.FindName("R3C3")).Parked = false;
        }
        public void ParkSlot_Click(object sender, RoutedEventArgs e)
        {
            ParkSlot_Click(sender);
        }
    }
}