using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace SmartParking
{
    public  class ParkingSlot: System.Windows.Controls.Button    
    {
        private bool _Parked = false;
        private bool _IsActive = true;
        public bool Parked 
        { 
            set{
                if (!_IsActive) return;
                _Parked = value ;
                 if(_Parked)
                    Background = new SolidColorBrush(Colors.Green);
                else
                    Background = new SolidColorBrush(Colors.White);
            }
            get
            {
                if (!_IsActive) return false;
                return _Parked;
            }
        }
        public void MakeMeInActive()
        {
            _IsActive = false;
                _Parked = false;
                Background = new SolidColorBrush(Colors.Gray);
        }
    }
}
