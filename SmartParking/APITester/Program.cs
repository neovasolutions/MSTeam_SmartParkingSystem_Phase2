using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APITester
{
    class Program
    {
        static void Main(string[] args)
        {
            AllocateParkingSlots("R0C0", true );
            Console.ReadKey();
        }
        private static  void AllocateParkingSlots(string barcodeNo, bool isAlloc)
        {
            WebAPICaller myWebAPICaller = new WebAPICaller();
            string selParking = "2";
            string userId = "12";
            string urlParam = "userId=" + userId + "&parkingId=" + selParking + "&slotNo=" + barcodeNo + "&isAcquired=" + isAlloc.ToString();
            myWebAPICaller.POST_urlParam("SlotMaster/PostParkUnPark", urlParam);
        }
    }
}
