using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Microsoft.Devices;
using System.IO;
using System.IO.IsolatedStorage;
using Microsoft.Xna.Framework.Media;
using System.Windows.Media;
using ZXing;
using System.Windows.Media.Imaging;
using Windows.Storage;
using Microsoft.Phone.Tasks;

namespace SmartParking
{
    public delegate void TakeAction(bool param1, string param2, bool param3);
    public partial class Camera : PhoneApplicationPage
    {
        PhotoCamera cam;
        MediaLibrary library = new MediaLibrary();
        public static bool IsForParked { set; get; }
        public static TakeAction ActionAfterBCScanned;

        public Camera()
        {
            InitializeComponent();
        }
        //Code for initialization, capture completed, image availability events; also setting the source for the viewfinder.
        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            // Check to see if the camera is available on the phone.
            if ((PhotoCamera.IsCameraTypeSupported(CameraType.Primary) == true) ||
                 (PhotoCamera.IsCameraTypeSupported(CameraType.FrontFacing) == true))
            {
                // Initialize the camera, when available.
                if (PhotoCamera.IsCameraTypeSupported(CameraType.Primary))
                {
                    // Use front-facing camera if available.
                    cam = new Microsoft.Devices.PhotoCamera(CameraType.Primary);
                }
                else
                {
                    // Otherwise, use standard camera on back of phone.
                    cam = new Microsoft.Devices.PhotoCamera(CameraType.FrontFacing);
                }
                // Event is fired when the capture sequence is complete and an image is available.
                cam.CaptureImageAvailable += new EventHandler<Microsoft.Devices.ContentReadyEventArgs>(cam_CaptureImageAvailable);
                //Set the VideoBrush source to the camera.
                viewfinderBrush.SetSource(cam);
                viewfinderBrush.RelativeTransform = new CompositeTransform() { CenterX = 0.5, CenterY = 0.5, Rotation = 90 };
            }
            else
            {
                // The camera is not supported on the phone.
                this.Dispatcher.BeginInvoke(delegate()
                {
                    // Write message.
                    txtDebug.Text = "A Camera is not available on this phone.";
                });
                // Disable UI.
                ShutterButton.IsEnabled = false;
            }
        }
        protected override void OnNavigatingFrom(System.Windows.Navigation.NavigatingCancelEventArgs e)
        {
            if (cam != null)
            {
                // Dispose camera to minimize power consumption and to expedite shutdown.
                cam.Dispose();
                // Release memory, ensure garbage collection.
                cam.CaptureImageAvailable -= cam_CaptureImageAvailable;
            }
        }
        // Update the UI if initialization succeeds.
        protected override void OnOrientationChanged(OrientationChangedEventArgs e)
        {
            base.OnOrientationChanged(e);
            return;
        }
        private void ShutterButton_Click(object sender, RoutedEventArgs e)
        {
            if (cam != null)
            {
                try
                {
                    // Start image capture.
                    cam.CaptureImage();
                }
                catch (Exception ex)
                {
                    this.Dispatcher.BeginInvoke(delegate()
                    {
                        // Cannot capture an image until the previous capture has completed.
                        txtDebug.Text = ex.Message;
                    });
                }
            }
        }
        void cam_CaptureImageAvailable(object sender, Microsoft.Devices.ContentReadyEventArgs e)
        {
            try
            {
                this.Dispatcher.BeginInvoke(delegate()
                {
                    ReadMyBarcode(e.ImageStream);
                });
            }
            finally
            {
                // Close image stream
                //e.ImageStream.Close();
            }
        }
        private void ReadMyBarcode(Stream imageStream)
        {
            ZXing.IBarcodeReader reader = new BarcodeReader();
            BitmapImage bi = new BitmapImage();
            bi.SetSource(imageStream );
            WriteableBitmap wb = new WriteableBitmap(bi);
            var result = reader.Decode(wb);
            if (result != null)
            {
                //MessageBox.Show(result.Text);
                ActionAfterBCScanned(true, result.Text, IsForParked);
                imageStream.Close();
                NavigationService.GoBack();
            }
            else
            {
                MessageBox.Show("Unable to Decode the Barcode, Try again.");
            }
        }
    }
}
