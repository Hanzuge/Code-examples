using System;
using System.Runtime.Remoting.Messaging;
using Basler.Pylon;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace Scanner
{
    public partial class BaslerRacer
    {
        string returnMessage = "";
        Bitmap picture;

        // Setting camera parameters
        public string SetParameters(int pictureHeight, int pictureWidth)
        {
            // The exit code of the application.
            int exitCode = 0;
            try
            {
                // Create a camera object that selects the first camera device found.
                using (Camera camera = new Camera())
                {
                    // Print the model name of the camera.
                    returnMessage = "Using camera " + camera.CameraInfo[CameraInfoKey.ModelName];

                    // Set the acquisition mode to free running continuous acquisition when the camera is opened.
                    camera.CameraOpened += Configuration.AcquireContinuous;

                    // Open the connection to the camera device.
                    camera.Open();

                    // Changing Inter-Packet Delay
                    camera.Parameters[PLCamera.GevSCPD].SetValue(10000);
                    // The parameter MaxNumBuffer can be used to control the amount of buffers
                    // allocated for grabbing. The default value of this parameter is 10.
                    camera.Parameters[PLCameraInstance.MaxNumBuffer].SetValue(5);
                    // Set the height to 1
                    camera.Parameters[PLCamera.Height].SetValue(pictureHeight);
                    // Set the width to 4000
                    camera.Parameters[PLCamera.Width].SetValue(pictureWidth);
                    // Set the "raw" gain value to 1500
                    // If you want to know the resulting gain in dB, use the formula given in this topic
                    camera.Parameters[PLCamera.GainRaw].SetValue(1500);
                    // Set the black level to 500
                    camera.Parameters[PLCamera.BlackLevelRaw].SetValue(500);
                    // Set the exposure time to 2000 microseconds
                    camera.Parameters[PLCamera.ExposureTimeAbs].SetValue(4500.0);


                    // Close the connection to the camera device.
                    camera.Close();
                }
            }
            catch (Exception e)
            {
                Console.Error.WriteLine("Exception: {0}", e.Message);
                returnMessage = "Exception: " + e.Message;
                exitCode = 1;
            }
            return returnMessage;
        }
        
        // Lights on/off
        public string LightsOnOff(string Light, bool OnOff)
        {
            // The exit code of the sample application.
            int exitCode = 0;
            try
            {
                // Create a camera object that selects the first camera device found.
                using (Camera camera = new Camera())
                {
                    // Print the model name of the camera.
                    returnMessage = "Using camera " + camera.CameraInfo[CameraInfoKey.ModelName];

                    // Set the acquisition mode to free running continuous acquisition when the camera is opened.
                    camera.CameraOpened += Configuration.AcquireContinuous;

                    // Open the connection to the camera device.
                    camera.Open();

                    // The parameter MaxNumBuffer can be used to control the amount of buffers
                    // allocated for grabbing. The default value of this parameter is 10.
                    camera.Parameters[PLCameraInstance.MaxNumBuffer].SetValue(5);

                    if (Light == "Backlight")
                    {
                        // Select Line 1 (output line)
                        camera.Parameters[PLCamera.LineSelector].SetValue(PLCamera.LineSelector.Line1);
                        // Select the User Output 1 signal
                        camera.Parameters[PLCamera.UserOutputSelector].SetValue(PLCamera.UserOutputSelector.UserOutput1);
                        if (OnOff == true)
                        {
                            // Set the User Output Value for the User Output 1 signal to true.
                            // Because User Output 1 is set as the source signal for Line 1,
                            // the status of Line 1 is set to high.
                            camera.Parameters[PLCamera.UserOutputValue].SetValue(true);
                            returnMessage = "Light is on";
                        }
                        else
                        {
                            // Set the User Output Value for the User Output 1 signal to false.
                            // Because User Output 1 is set as the source signal for Line 1,
                            // the status of Line 1 is set to low.
                            camera.Parameters[PLCamera.UserOutputValue].SetValue(false);
                            returnMessage = "Light is off";
                        }
                    }
                    else if (Light == "LineLight")
                    {
                        // Select Line 2 (output line)
                        camera.Parameters[PLCamera.LineSelector].SetValue(PLCamera.LineSelector.Line2);
                        // Select the User Output 2 signal
                        camera.Parameters[PLCamera.UserOutputSelector].SetValue(PLCamera.UserOutputSelector.UserOutput2);
                        if (OnOff == true)
                        {
                            // Set the User Output Value for the User Output 2 signal to true.
                            // Because User Output 2 is set as the source signal for Line 2,
                            // the status of Line 2 is set to high.
                            camera.Parameters[PLCamera.UserOutputValue].SetValue(true);
                            returnMessage = "Light is on";
                        }
                        else
                        {
                            // Set the User Output Value for the User Output 2 signal to falce.
                            // Because User Output 2 is set as the source signal for Line 2,
                            // the status of Line 2 is set to low.
                            camera.Parameters[PLCamera.UserOutputValue].SetValue(false);
                            returnMessage = "Light is off";
                        }
                    }
                    else if (Light == "Both")
                    {
                        if (OnOff == true)
                        {
                            // Set all User Output Values for the User Output signals to true.
                            // the status of all Lines is set to high.
                            camera.Parameters[PLCamera.UserOutputValueAll].SetValue(0x3);
                            returnMessage = "Light is on";
                        }
                        else
                        {
                            // Set all User Output Values for the User Output signals to false.
                            // the status of all Lines is set to true.
                            camera.Parameters[PLCamera.UserOutputValueAll].SetValue(0x0);
                            returnMessage = "Light is off";
                        }
                    }
                    // Close the connection to the camera device.
                    camera.Close();
                }
            }
            catch (Exception e)
            {
                Console.Error.WriteLine("Exception: {0}", e.Message);
                returnMessage = "Exception: " + e.Message;
                exitCode = 1;
            }
            return returnMessage;
        }

        // Taking pictures of rows and join pixels into one table
        public Bitmap TakePictures(int rowCount, int pictureWidth)
        {

            // The exit code of the application.
            int exitCode = 0;
            try
            {
                // Create a camera object that selects the first camera device found.
                using (Camera camera = new Camera())
                {
                    // Set the acquisition mode to free running continuous acquisition when the camera is opened.
                    camera.CameraOpened += Configuration.AcquireContinuous;

                    // Open the connection to the camera device.
                    camera.Open();

                    // Changing Inter-Packet Delay
                    camera.Parameters[PLCamera.GevSCPD].SetValue(10000);

                    // The parameter MaxNumBuffer can be used to control the amount of buffers
                    // allocated for grabbing. The default value of this parameter is 10.
                    camera.Parameters[PLCameraInstance.MaxNumBuffer].SetValue(5);


                    // Start grabbing.
                    camera.StreamGrabber.Start();

                    int byteCount = rowCount * pictureWidth;
                    byte[] pictureArray = new byte[byteCount];

                    // Grab a number of images.
                    for (int i = 0; i < rowCount; ++i)
                    {
                        // Wait for an image and then retrieve it. A timeout of 5000 ms is used.
                        IGrabResult grabResult = camera.StreamGrabber.RetrieveResult(5000, TimeoutHandling.ThrowException);
                        using (grabResult)
                        {
                            // Image grabbed successfully?
                            if (grabResult.GrabSucceeded)
                            {

                                // Access the image data.
                                byte[] buffer = grabResult.PixelData as byte[];

                                for (int a = 0; a < buffer.Length; a++)
                                {
                                    // Saving row in pictureArray
                                    pictureArray[a + i * buffer.Length] = buffer[a];
                                }
                            }
                            else
                            {
                                // returnMessage += "\r\nError: " + grabResult.ErrorCode + grabResult.ErrorDescription;
                            }
                        }
                    }

                    // converting an image to a bitmap.
                    CreateBitmap(out picture, pictureWidth, rowCount, false);
                    UpdateBitmap(picture, pictureArray, pictureWidth, rowCount, false);

                    // Stop grabbing.
                    camera.StreamGrabber.Stop();

                    // Close the connection to the camera device.
                    camera.Close();
                }
            }
            catch (Exception e)
            {
                Console.Error.WriteLine("Exception: {0}", e.Message);
                returnMessage = "Exception: " + e.Message;
                exitCode = 1;
            }

            return picture;
        }

        // Saving picture to folder
        public void SaveImage(string imageName, Bitmap image, string folderPath)
        {
            // Save the image as a GIF.           
            string imagePath = folderPath + "\\" + imageName;
            image.Save( imagePath, System.Drawing.Imaging.ImageFormat.Gif);
        }

        /* Returns the corresponding pixel format of a bitmap. */
        private static PixelFormat GetFormat(bool color)
        {
            return color ? PixelFormat.Format32bppRgb : PixelFormat.Format8bppIndexed;
        }

        /* Calculates the length of one line in byte. */
        private static int GetStride(int width, bool color)
        {
            return color ? width * 4 : width;
        }

        /* Compares the properties of the bitmap with the supplied image data. Returns true if the properties are compatible. */
        public static bool IsCompatible(Bitmap bitmap, int width, int height, bool color)
        {
            if (bitmap == null
                || bitmap.Height != height
                || bitmap.Width != width
                || bitmap.PixelFormat != GetFormat(color)
             )
            {
                return false;
            }
            return true;
        }

        /* Creates a new bitmap object with the supplied properties. */
        public static void CreateBitmap(out Bitmap bitmap, int width, int height, bool color)
        {
            bitmap = new Bitmap(width, height, GetFormat(color));

            if (bitmap.PixelFormat == PixelFormat.Format8bppIndexed)
            {
                ColorPalette colorPalette = bitmap.Palette;
                for (int i = 0; i < 256; i++)
                {
                    colorPalette.Entries[i] = Color.FromArgb(i, i, i);
                }
                bitmap.Palette = colorPalette;
            }
        }

        /* Copies the raw image data to the bitmap buffer. */
        public static void UpdateBitmap(Bitmap bitmap, byte[] buffer, int width, int height, bool color)
        {
            /* Check if the bitmap can be updated with the image data. */
            if (!IsCompatible(bitmap, width, height, color))
            {
                throw new Exception("Cannot update incompatible bitmap.");
            }

            /* Lock the bitmap's bits. */
            BitmapData bmpData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadWrite, bitmap.PixelFormat);
            /* Get the pointer to the bitmap's buffer. */
            IntPtr ptrBmp = bmpData.Scan0;
            /* Compute the width of a line of the image data. */
            int imageStride = GetStride(width, color);
            /* If the widths in bytes are equal, copy in one go. */
            if (imageStride == bmpData.Stride)
            {
                System.Runtime.InteropServices.Marshal.Copy(buffer, 0, ptrBmp, bmpData.Stride * bitmap.Height);
            }
            else /* The widths in bytes are not equal, copy line by line. This can happen if the image width is not divisible by four. */
            {
                for (int i = 0; i < bitmap.Height; ++i)
                {
                    Marshal.Copy(buffer, i * imageStride, new IntPtr(ptrBmp.ToInt64() + i * bmpData.Stride), width);
                }
            }

            /* Unlock the bits. */
            bitmap.UnlockBits(bmpData);
        }
    }
}