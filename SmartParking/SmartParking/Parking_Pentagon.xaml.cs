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
    public partial class Parking_Pentagon : ParkingBase
    {
        public Parking_Pentagon()
        {
            InitializeComponent();
            InitMe(ContentPanel, ParkCommand, ParkHeader);
        }
        protected override void ClearAll_ParkingSlot()
        {
            ((ParkingSlot)ContentPanel.FindName("R0C0")).Parked = false;
            ((ParkingSlot)ContentPanel.FindName("R0C1")).Parked = false;

            ((ParkingSlot)ContentPanel.FindName("R1C1")).Parked = false;
            ((ParkingSlot)ContentPanel.FindName("R2C1")).Parked = false;
            ((ParkingSlot)ContentPanel.FindName("R3C1")).Parked = false;
            ((ParkingSlot)ContentPanel.FindName("R4C1")).Parked = false;
            ((ParkingSlot)ContentPanel.FindName("R5C1")).Parked = false;
            ((ParkingSlot)ContentPanel.FindName("R6C1")).Parked = false;

            ((ParkingSlot)ContentPanel.FindName("R1C2")).Parked = false;
            ((ParkingSlot)ContentPanel.FindName("R2C2")).Parked = false;
            ((ParkingSlot)ContentPanel.FindName("R3C2")).Parked = false;
            ((ParkingSlot)ContentPanel.FindName("R4C2")).Parked = false;
            ((ParkingSlot)ContentPanel.FindName("R5C2")).Parked = false;
            ((ParkingSlot)ContentPanel.FindName("R6C2")).Parked = false;
        }
        public void ParkSlot_Click(object sender, RoutedEventArgs e)
        {
            ParkSlot_Click(sender);
        }
    }
}