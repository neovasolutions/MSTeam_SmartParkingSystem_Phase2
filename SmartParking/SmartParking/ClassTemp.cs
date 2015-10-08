using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartParking
{
    class ClassTemp
    {
        //cam.Initialized -= cam_Initialized;
        //void cam_Initialized(object sender, Microsoft.Devices.CameraOperationCompletedEventArgs e)
        //        {
        //            if (e.Succeeded)
        //            {
        //                this.Dispatcher.BeginInvoke(delegate()
        //                {
        //                });
        //            }
        //        }
        //cam.CaptureThumbnailAvailable -= cam_CaptureThumbnailAvailable;
        //cam.CaptureCompleted -= cam_CaptureCompleted;
        //string fileName = "MyBarcode.jpg";
        //return;
        //                // Write message to the UI thread.
        //                Deployment.Current.Dispatcher.BeginInvoke(delegate()
        //                {
        //                    txtDebug.Text = "Captured image available, saving photo.";
        //                });

        //                // Save photo to the media library camera roll.
        //                using (IsolatedStorageFile isStore = IsolatedStorageFile.GetUserStoreForApplication())
        //                {
        //                    isStore.DeleteFile(fileName);
        //                }
        //                library.SavePictureToCameraRoll(fileName, e.ImageStream);

        //                // Write message to the UI thread.
        //                Deployment.Current.Dispatcher.BeginInvoke(delegate()
        //                {
        //                    txtDebug.Text = "Photo has been saved to camera roll.";

        //                });

        //                // Set the position of the stream back to start
        //                e.ImageStream.Seek(0, SeekOrigin.Begin);

        //                // Save photo as JPEG to the local folder.
        //                using (IsolatedStorageFile isStore = IsolatedStorageFile.GetUserStoreForApplication())
        //                {

        //                    using (IsolatedStorageFileStream targetStream = isStore.OpenFile(fileName, FileMode.Create, FileAccess.Write))
        //                    {
        //                        // Initialize the buffer for 4KB disk pages.
        //                        byte[] readBuffer = new byte[4096];
        //                        int bytesRead = -1;

        //                        // Copy the image to the local folder. 
        //                        while ((bytesRead = e.ImageStream.Read(readBuffer, 0, readBuffer.Length)) > 0)
        //                        {
        //                            targetStream.Write(readBuffer, 0, bytesRead);
        //                        }
        //                    }
        //                }

        //                // Write message to the UI thread.
        //                Deployment.Current.Dispatcher.BeginInvoke(delegate()
        //                {
        //                    txtDebug.Text = "Photo has been saved to the local folder.";

        //                });
        //int currSec = DateTime.Now.Second + 5;
        //                    while (true)
        //                    {
        //                        if (DateTime.Now.Second >= currSec)
        //                        {
        //                            ReadMyBarcode(_imageStream );
        //                            NavigationService.GoBack();
        //                            break;
        //                        }
        //                    }
        //private string ReadBarcode()
        //       {
        //           ZXing.IBarcodeReader reader = new BarcodeReader();
        //           BitmapImage bi = new BitmapImage();
        //           using (IsolatedStorageFile myIsolatedStorage = IsolatedStorageFile.GetUserStoreForApplication())
        //           {
        //               using (IsolatedStorageFileStream fileStream = myIsolatedStorage.OpenFile("MyBarcode.jpg", FileMode.Open, FileAccess.Read))
        //               {
        //                   bi.SetSource(fileStream);
        //                   MessageBox.Show(bi.PixelWidth.ToString());
        //               }
        //           }
        //           WriteableBitmap wb = new WriteableBitmap(bi);

        //           // detect and decode the barcode inside the bitmap
        //           var result = reader.Decode(wb);
        //           // do something with the result
        //           if (result != null)
        //           {
        //               return result.Text;
        //           }
        //           else
        //               return "";
        //       }
        //private void GetSavedImage()
        //        {
        //            BitmapImage bi = new BitmapImage();
        //            using (IsolatedStorageFile myIsolatedStorage = IsolatedStorageFile.GetUserStoreForApplication())
        //            {
        //                using (IsolatedStorageFileStream fileStream = myIsolatedStorage.OpenFile("MyBarcode.jpg", FileMode.Open, FileAccess.Read))
        //                {
        //                    bi.SetSource(fileStream);
        //                    MessageBox.Show(bi.PixelWidth.ToString () );
        //                }
        //            }
        //        }
        // Informs when thumbnail photo has been taken, saves to the local folder
        // User will select this image in the Photos Hub to bring up the full-resolution. 
        //public void cam_CaptureThumbnailAvailable(object sender, ContentReadyEventArgs e)
        //{
        //    string fileName = savedCounter + "_th.jpg";

        //    try
        //    {
        //        // Write message to UI thread.
        //        Deployment.Current.Dispatcher.BeginInvoke(delegate()
        //        {
        //            txtDebug.Text = "Captured image available, saving thumbnail.";
        //        });

        //        // Save thumbnail as JPEG to the local folder.
        //        using (IsolatedStorageFile isStore = IsolatedStorageFile.GetUserStoreForApplication())
        //        {
        //            using (IsolatedStorageFileStream targetStream = isStore.OpenFile(fileName, FileMode.Create, FileAccess.Write))
        //            {
        //                // Initialize the buffer for 4KB disk pages.
        //                byte[] readBuffer = new byte[4096];
        //                int bytesRead = -1;

        //                // Copy the thumbnail to the local folder. 
        //                while ((bytesRead = e.ImageStream.Read(readBuffer, 0, readBuffer.Length)) > 0)
        //                {
        //                    targetStream.Write(readBuffer, 0, bytesRead);
        //                }
        //            }
        //        }

        //        // Write message to UI thread.
        //        Deployment.Current.Dispatcher.BeginInvoke(delegate()
        //        {
        //            txtDebug.Text = "Thumbnail has been saved to the local folder.";

        //        });
        //    }
        //    finally
        //    {
        //        // Close image stream
        //        e.ImageStream.Close();
        //    }
        //}
        //private void ImageButton_Click(object sender, RoutedEventArgs e)
        //       {
        //           PhotoChooserTask pct = new Microsoft.Phone.Tasks.PhotoChooserTask();
        //           pct.Show();
        //           pct.Completed += pct_Completed;
        //       }

        //       void pct_Completed(object sender, Microsoft.Phone.Tasks.PhotoResult e)
        //       {
        //           try
        //           {
        //               if (e != null)
        //               {
        //                   MessageBox.Show(e.OriginalFileName);
        //               }
        //           }
        //           catch (Exception ex)
        //           {
        //               MessageBox.Show(ex.Message);
        //           }
        //       }
        // Ensure that the viewfinder is upright in LandscapeRight.
        //protected override void OnOrientationChanged(OrientationChangedEventArgs e)
        //{
        //    base.OnOrientationChanged(e);
        //    return;
        //    if (cam != null)
        //    {
        //        // LandscapeRight rotation when camera is on back of phone.
        //        int landscapeRightRotation = 180;

        //        // Change LandscapeRight rotation for front-facing camera.
        //        if (cam.CameraType == CameraType.FrontFacing) landscapeRightRotation = -180;

        //        // Rotate video brush from camera.
        //        if (e.Orientation == PageOrientation.LandscapeRight)
        //        {
        //            // Rotate for LandscapeRight orientation.
        //            viewfinderBrush.RelativeTransform =
        //                new CompositeTransform() { CenterX = 0.5, CenterY = 0.5, Rotation = landscapeRightRotation };
        //        }
        //        else
        //        {
        //            // Rotate for standard landscape orientation.
        //            viewfinderBrush.RelativeTransform =
        //                new CompositeTransform() { CenterX = 0.5, CenterY = 0.5, Rotation = 0 };
        //        }
        //    }


        //}
        //GetSavedImage();
        //string fileName = @"C:\Data\Users\Public\Pictures\Camera Roll\MyBarcode.jpg";
        //if (!File.Exists(fileName))
        //return;
        //PhotoChooserTask pct = new Microsoft.Phone.Tasks.PhotoChooserTask();
        //pct.Show();
        //pct.Completed += pct_Completed;
        //return;
        //ReadBarcode();
        //return;

        //void cam_CaptureImageAvailable(object sender, Microsoft.Devices.ContentReadyEventArgs e)
        //        {
        //            string fileName = "MyBarcode.jpg";
        //            _imageStream = e.ImageStream;
        //            this.Dispatcher.BeginInvoke(delegate()
        //            {
        //                // Write message.
        //                //txtDebug.Text = "Camera initialized.";
        //            });
        //            return;
        //            try
        //            {   // Write message to the UI thread.
        //                Deployment.Current.Dispatcher.BeginInvoke(delegate()
        //                {
        //                    txtDebug.Text = "Captured image available, saving photo.";
        //                });

        //                // Save photo to the media library camera roll.
        //                using (IsolatedStorageFile isStore = IsolatedStorageFile.GetUserStoreForApplication())
        //                {
        //                    isStore.DeleteFile(fileName);
        //                }
        //                library.SavePictureToCameraRoll(fileName, e.ImageStream);

        //                // Write message to the UI thread.
        //                Deployment.Current.Dispatcher.BeginInvoke(delegate()
        //                {
        //                    txtDebug.Text = "Photo has been saved to camera roll.";

        //                });

        //                // Set the position of the stream back to start
        //                e.ImageStream.Seek(0, SeekOrigin.Begin);

        //                // Save photo as JPEG to the local folder.
        //                using (IsolatedStorageFile isStore = IsolatedStorageFile.GetUserStoreForApplication())
        //                {

        //                    using (IsolatedStorageFileStream targetStream = isStore.OpenFile(fileName, FileMode.Create, FileAccess.Write))
        //                    {
        //                        // Initialize the buffer for 4KB disk pages.
        //                        byte[] readBuffer = new byte[4096];
        //                        int bytesRead = -1;

        //                        // Copy the image to the local folder. 
        //                        while ((bytesRead = e.ImageStream.Read(readBuffer, 0, readBuffer.Length)) > 0)
        //                        {
        //                            targetStream.Write(readBuffer, 0, bytesRead);
        //                        }
        //                    }
        //                }

        //                // Write message to the UI thread.
        //                Deployment.Current.Dispatcher.BeginInvoke(delegate()
        //                {
        //                    txtDebug.Text = "Photo has been saved to the local folder.";

        //                });
        //            }
        //            finally
        //            {
        //                // Close image stream
        //                e.ImageStream.Close();
        //            }
        //        }

        //        // Informs when thumbnail photo has been taken, saves to the local folder
        //        // User will select this image in the Photos Hub to bring up the full-resolution. 
        //        public void cam_CaptureThumbnailAvailable(object sender, ContentReadyEventArgs e)
        //        {
        //            string fileName = savedCounter + "_th.jpg";

        //            try
        //            {
        //                // Write message to UI thread.
        //                Deployment.Current.Dispatcher.BeginInvoke(delegate()
        //                {
        //                    txtDebug.Text = "Captured image available, saving thumbnail.";
        //                });

        //                // Save thumbnail as JPEG to the local folder.
        //                using (IsolatedStorageFile isStore = IsolatedStorageFile.GetUserStoreForApplication())
        //                {
        //                    using (IsolatedStorageFileStream targetStream = isStore.OpenFile(fileName, FileMode.Create, FileAccess.Write))
        //                    {
        //                        // Initialize the buffer for 4KB disk pages.
        //                        byte[] readBuffer = new byte[4096];
        //                        int bytesRead = -1;

        //                        // Copy the thumbnail to the local folder. 
        //                        while ((bytesRead = e.ImageStream.Read(readBuffer, 0, readBuffer.Length)) > 0)
        //                        {
        //                            targetStream.Write(readBuffer, 0, bytesRead);
        //                        }
        //                    }
        //                }

        //                // Write message to UI thread.
        //                Deployment.Current.Dispatcher.BeginInvoke(delegate()
        //                {
        //                    txtDebug.Text = "Thumbnail has been saved to the local folder.";

        //                });
        //            }
        //            finally
        //            {
        //                // Close image stream
        //                e.ImageStream.Close();
        //            }
        //        }


        // Event is fired when the capture sequence is complete.
        //cam.CaptureCompleted += new EventHandler<CameraOperationCompletedEventArgs>(cam_CaptureCompleted);

        // Event is fired when the capture sequence is complete and a thumbnail image is available.
        //cam.CaptureThumbnailAvailable += new EventHandler<ContentReadyEventArgs>(cam_CaptureThumbnailAvailable);


        //void cam_CaptureCompleted(object sender, CameraOperationCompletedEventArgs e)
        //        {
        //            // Increments the savedCounter variable used for generating JPEG file names.
        //            savedCounter++;
        //        }
        // Event is fired when the PhotoCamera object has been initialized.
        //cam.Initialized += new EventHandler<Microsoft.Devices.CameraOperationCompletedEventArgs>(cam_Initialized);
    }
}
