using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using System.Reflection;
using System.IO;
using Levyskanneri;
using System.Security.Cryptography.X509Certificates;

namespace Scanner
{
    public partial class UserInterface : Form
    {
        #region Variables

        // Defining Motor numbers and Ip addresses
        // (Motor number, motors IP address, ...)
        private readonly MotorControls Motor1 = new MotorControls(1, "192.168.178.1", false, null, null, false, false, false);
        private readonly MotorControls Motor2 = new MotorControls(2, "192.168.178.2", false, null, null, false, false, false);
        private readonly BaslerRacer Camera = new BaslerRacer();

        // Recordnumber for move commands
        private readonly byte[] recordNumber1 = new byte[] { 0x00, 0x00, 0x00, 0x01 };
        private readonly byte[] recordNumber2 = new byte[] { 0x00, 0x00, 0x00, 0x02 };
        private readonly byte[] recordNumber3 = new byte[] { 0x00, 0x00, 0x00, 0x03 };
        private readonly byte[] recordNumber4 = new byte[] { 0x00, 0x00, 0x00, 0x04 };
        private readonly byte[] recordNumber5 = new byte[] { 0x00, 0x00, 0x00, 0x05 };
        private readonly byte[] recordNumber6 = new byte[] { 0x00, 0x00, 0x00, 0x06 };
        private readonly byte[] recordNumber7 = new byte[] { 0x00, 0x00, 0x00, 0x07 };
        private readonly byte[] recordNumber8 = new byte[] { 0x00, 0x00, 0x00, 0x08 };
        private readonly byte[] recordNumber9 = new byte[] { 0x00, 0x00, 0x00, 0x09 };
        
        string barcode = "EmptyBarcode";
        string imageName = "";
        int pictureNumber = 1;
        string folderPath = "";
        string logPath = "";
        Bitmap image;
        string lineLightState = ""; // Empty if not on
        string backlightState = ""; // Empty if not on
        string locationMotor1 = "";
        string locationMotor2 = "";
        bool error = false;
        bool stopAll = false;
        bool stackerConnected = false;
        #endregion

        #region Userinterface + Textboxes etc

        public UserInterface()
        {
            InitializeComponent();

            // Adding new folder for this use
            string date = DateTime.Now.ToString("yyyy'_'MM'_'dd'_'HH'_'mm");
            string folderName = "PlateScannerData_" + date;
            // Specify the directory you want to manipulate.
            folderPath = @"c:\PlateScanner\" + folderName;
            try
            {
                // Determine whether the directory exists. If exists, add seconds.
                if (Directory.Exists(folderPath))
                {
                    folderPath = @"c:\PlateScanner\" + folderName + DateTime.Now.ToString("'_'ss");
                }
                // Determine whether the directory exists. If exist, ask to restart.
                if (Directory.Exists(folderPath))
                {
                    textBox.Text = DateTime.Now.ToString("HH:mm:ss ") + "Error, invalid directory, restart" + Environment.NewLine + textBox.Text;
                    // Adding message to log file
                    using (StreamWriter sw = File.AppendText(logPath))
                    {
                        sw.WriteLine(DateTime.Now.ToString("HH:mm:ss ") + "Error, invalid directory, restart");
                    }
                    return;
                }

                // Try to create the directory.
                DirectoryInfo di = Directory.CreateDirectory(folderPath);               
            }
            catch (Exception e)
            {
                textBox.Text = DateTime.Now.ToString("HH:mm:ss ") + "The process failed: " + e.ToString() + Environment.NewLine + textBox.Text;
                // Adding message to log file
                using (StreamWriter sw = File.AppendText(logPath))
                {
                    sw.WriteLine(DateTime.Now.ToString("HH:mm:ss ") + "The process failed: " + e.ToString());
                }
            }
            finally { }
            //Adding new log file to folder
            logPath = folderPath + "\\Log_" + date + ".txt";
            // Create a new file     
            using (FileStream fs = File.Create(logPath))
            {
                // Add header to file    
                Byte[] title = new UTF8Encoding(true).GetBytes("Log " + date + " :\n\n");
                fs.Write(title, 0, title.Length);
            }
            // Writing messages to textbox
            textBox.Text = DateTime.Now.ToString("HH:mm:ss ") + "The directory was created successfully: " + folderPath + Environment.NewLine + textBox.Text;
            textBox.Text = DateTime.Now.ToString("HH:mm:ss ") + "Start by pressing the connect button" + Environment.NewLine + textBox.Text;
            // Adding message to log file
            using (StreamWriter sw = File.AppendText(logPath))
            {
                sw.WriteLine(DateTime.Now.ToString("HH:mm:ss ") + "The directory was created successfully: \n" + DateTime.Now.ToString("HH:mm:ss ") + "Start by pressing the connect button");
            }
        }

        private void UI_Load(object sender, EventArgs e)
        {

        }

        // Textbox for log messages
        public void TextBox(object sender, EventArgs e)
        {

        }

        // Textbox for location of motor 1
        private void Motor1Location(object sender, EventArgs e)
        {

        }

        // Textbox for location of motor 2
        private void Motor2Location(object sender, EventArgs e)
        {

        }

        // Textbox for barcode
        private void Barcode(object sender, EventArgs e)
        {

        }

        // Picturebox for plate picture
        private void PictureBox(object sender, EventArgs e)
        {

        }

        // Textbox for pictures name
        private void PictureName(object sender, EventArgs e)
        {

        }
        #endregion

        #region Motor connect / disconnect

        // Connecting button for both motors
        private void ConnectMotors(object sender, EventArgs e)
        {
            // Using another thread so that the UI remains available for use.
            Thread Connection = new Thread(t =>
            {
                ButtonPushStart();
                // Connecting both motors
                string returnMessage1 = Motor1.Connect();
                string returnMessage2 = Motor2.Connect();

                // showing messages in textBox
                WriteToTextbox("Motor 1: ", returnMessage1);
                WriteToTextbox("Motor 2: ", returnMessage2);

                // Homing if connecting was succesfull
                if (returnMessage1 == "Motor is connected" && returnMessage2 == "Motor is connected")
                {
                    StartHomingMotor1();
                    StartHomingMotor2();
                }
            })
            { IsBackground = true };
            Connection.Start();
        }

        // Connect only motor 1
        private void ConnectMotor1(object sender, EventArgs e)
        {
            // Using another thread so that the UI remains available for use.
            Thread Connection = new Thread(t =>
            {
                ButtonPushStart();
                // Connecting motor 1
                string returnMessage1 = Motor1.Connect();

                // showing messages in textBox
                WriteToTextbox("Motor 1: ", returnMessage1);
            })
            { IsBackground = true };
            Connection.Start();
        }

        // Connect only motor 2
        private void ConnectMotor2(object sender, EventArgs e)
        {
            // Using another thread so that the UI remains available for use.
            Thread Connection = new Thread(t =>
            {
                ButtonPushStart();
                // Connecting motor 2
                string returnMessage2 = Motor2.Connect();

                // showing messages in textBox
                WriteToTextbox("Motor 2: ", returnMessage2);
            })
            { IsBackground = true };
            Connection.Start();
        }

        // Disconnecting button
        private void DisconnectMotors(object sender, EventArgs e)
        {
            Thread Disconnect = new Thread(t =>
            {
                ButtonPushStart();
                // Disconnecting both motors
                string returnMessage1 = Motor1.Disconnect();
                string returnMessage2 = Motor2.Disconnect();

                // showing messages in textBox
                WriteToTextbox("Motor 1: ", returnMessage1);
                WriteToTextbox("Motor 2: ", returnMessage2);
            })
            { IsBackground = true };
            Disconnect.Start();
        }

        #endregion

        #region Arrow keys

        // Up button, moving when holding button down
        private void UpButtonHolding(object sender, MouseEventArgs e)
        {
            Thread Up = new Thread(t =>
            {
                ButtonPushStart();

                // While button is pushed, move to min location
                string returnMessage1 = Motor1.StartRecord(recordNumber2);
            
                // showing messages in textBox
                WriteToTextbox("Motor 1: ", returnMessage1);
            })
            { IsBackground = true };
            Up.Start();
        }
        private void UpButtonUp(object sender, MouseEventArgs e)
        {
            Thread Up = new Thread(t =>
            {
                // Stopping when releasing the button
                string returnMessage1 = Motor1.Stop();

                // showing messages in textBox
                WriteToTextbox("Motor 1: ", returnMessage1);
                ShowLocationAndErrorsMotor1();
            })
            { IsBackground = true };
            Up.Start();
        }

        // Down button, moving when holding button down
        private void DownButtonHolding(object sender, MouseEventArgs e)
        {
            Thread Down = new Thread(t =>
            {
                ButtonPushStart();

                // While button is pushed, move to max location
                string returnMessage1 = Motor1.StartRecord(recordNumber1); // Max location
            
                // showing messages in textBox
                WriteToTextbox("Motor 1: ", returnMessage1);
            })
            { IsBackground = true };
            Down.Start();
        }
        private void DownButtonUp(object sender, MouseEventArgs e)
        {
            Thread Down = new Thread(t =>
            {
                // Stopping when releasing the button
                string returnMessage1 = Motor1.Stop();

                // showing messages in textBox
                WriteToTextbox("Motor 1: ", returnMessage1);

                ShowLocationAndErrorsMotor1();
            })
            { IsBackground = true };
            Down.Start();
        }

        // Left button, moving when holding button down
        private void LeftButtonHolding(object sender, MouseEventArgs e)
        {
            Thread Left = new Thread(t =>
            {
                ButtonPushStart();

                // While button is pushed, move to min location
                string returnMessage2 = Motor2.StartRecord(recordNumber2); // Min location
            
                // showing messages in textBox
                WriteToTextbox("Motor 2: ", returnMessage2);
            })
            { IsBackground = true };
            Left.Start();
        }
        private void LeftButtonUp(object sender, MouseEventArgs e)
        {
            Thread Left = new Thread(t =>
            {
                // Stopping when releasing the button
                string returnMessage2 = Motor2.Stop();

                // showing messages in textBox
                WriteToTextbox("Motor 2: ", returnMessage2);
                ShowLocationAndErrorsMotor2();
            })
            { IsBackground = true };
            Left.Start();
        }

        // Right button, moving when holding button down
        private void RightButtonHolding(object sender, MouseEventArgs e)
        {
            Thread Right = new Thread(t =>
            {
                ButtonPushStart();

                // While button is pushed, move to max location
                string returnMessage2 = Motor2.StartRecord(recordNumber1);

                // showing messages in textBox
                WriteToTextbox("Motor 2: ", returnMessage2);
            })
            { IsBackground = true };
            Right.Start();
        }
        private void RightButtonUp(object sender, MouseEventArgs e)
        {
            Thread Right = new Thread(t =>
            {
                // Stopping when releasing the button
                string returnMessage2 = Motor2.Stop();

                // showing messages in textBox
                WriteToTextbox("Motor 2: ", returnMessage2);
                ShowLocationAndErrorsMotor2();
            })
            { IsBackground = true };
            Right.Start();
        }

        #endregion

        #region Motor moving and stop buttons

        // Homing button
        private void HomingButton(object sender, EventArgs e)
        {
            // Using another thread so that the UI remains available for use.
            Thread Homing = new Thread(t =>
            {
                StartHomingMotor1();
                StartHomingMotor2();
            })
            { IsBackground = true };
            Homing.Start();

        }

        // Homing button for motor 1
        private void HomingMotor1(object sender, EventArgs e)
        {
            // Using another thread so that the UI remains available for use.
            Thread Homing = new Thread(t =>
            {
                StartHomingMotor1();
            })
            { IsBackground = true };
            Homing.Start();
        }

        // Homing button for motor 2
        private void HomingMotor2(object sender, EventArgs e)
        {
            // Using another thread so that the UI remains available for use.
            Thread Homing = new Thread(t =>
            {
                StartHomingMotor2();
            })
            { IsBackground = true };
            Homing.Start();
        }

        // Go to 1st stacker button
        private void First(object sender, EventArgs e)
        {
            // Using another thread so that the UI remains available for use.
            Thread First = new Thread(t =>
            {
                MovingCommand(recordNumber3, recordNumber1); 
            })
            { IsBackground = true };
            First.Start();

        }

        // Go to 2nd stacker button
        private void Second(object sender, EventArgs e)
        {
            // Using another thread so that the UI remains available for use.
            Thread Second = new Thread(t =>
            {
                MovingCommand(recordNumber4, recordNumber1);
            })
            { IsBackground = true };
            Second.Start();

        }

        // Go to 3rd stacker button
        private void Third(object sender, EventArgs e)
        {
            // Using another thread so that the UI remains available for use.
            Thread Third = new Thread(t =>
            {
                MovingCommand(recordNumber5, recordNumber1);
            })
            { IsBackground = true };
            Third.Start();

        }

        // Go to camera button
        private void CameraLocation(object sender, EventArgs e)
        {
            // Using another thread so that the UI remains available for use.
            Thread Camera = new Thread(t =>
            {
                MovingCommand(recordNumber7, recordNumber5);
            })
            { IsBackground = true };
            Camera.Start();

        }

        // Go to barcode reader button
        private void BarcodeReaderLocation(object sender, EventArgs e)
        {
            // Using another thread so that the UI remains available for use.
            Thread Barcode = new Thread(t =>
            {
                MovingCommand(recordNumber6, recordNumber4);
            })
            { IsBackground = true };
            Barcode.Start();

        }

        // Stop both motors button
        private void Stop(object sender, EventArgs e)
        {
            Thread Stop = new Thread(t =>
            {
                ButtonPushStart();
                string returnMessage1 = Motor1.Stop();
                string returnMessage2 = Motor2.Stop();
                // showing messages in textBox
                WriteToTextbox("Motor 1: ", returnMessage1);
                WriteToTextbox("Motor 2: ", returnMessage2);
                ShowLocationAndErrorsMotor1();
                ShowLocationAndErrorsMotor2();
            })
            { IsBackground = true };
            Stop.Start();
        }

        // Moving in to stacker
        private void ToStacker(object sender, EventArgs e)
        {
            // Using another thread so that the UI remains available for use.
            Thread Stacker = new Thread(t =>
            {
                MovingToStacker();
            })
            { IsBackground = true };
            Stacker.Start();            
        }

        // Moving out from stacker
        private void FromStacker(object sender, EventArgs e)
        {
            // Using another thread so that the UI remains available for use.
            Thread Stacker = new Thread(t =>
            {
                MovingFromStacker();
            })
            { IsBackground = true };
            Stacker.Start();
            
        }

        #endregion

        #region Others

        // Start homing motor 1
        private void StartHomingMotor1()
        {
            ButtonPushStart();

            // Start homing motor 1
            string returnMessage1 = Motor1.Homing();
            // showing messages in textBox
            textBox.Invoke((Action)delegate
            {
                WriteToTextbox("Motor 1: ", returnMessage1);
            });
            Thread.Sleep(500);

            // asking, if homing is ready
            string returnMessage2 = Motor1.ReadIfReady(0x00, 0x01);
            // showing messages in textBox
            textBox.Invoke((Action)delegate
            {
                WriteToTextbox("Motor 1: ", returnMessage2);
            });

            // Asking motor locations
            string location1 = Motor1.ReadLocation();
            // If location is 0 & 0, homing is completed, in other cases, asking to try again
            if (location1 == "0.00 mm")
            {
                Motor1.HomingCompleted = true;
            }
            else
            {
                Motor1.HomingCompleted = false;
                textBox.Invoke((Action)delegate
                {
                    WriteToTextbox("Motor 1: ", "Homing failed, try again");
                });
            }
            // Showing motor locations in UI
            ShowLocationAndErrorsMotor1();
        }

        // Start homing motor 2
        private void StartHomingMotor2()
        {
            ButtonPushStart();

            // Start homing motor 2
            string returnMessage3 = Motor2.Homing();
            // showing messages in textBox
            textBox.Invoke((Action)delegate
            {
                WriteToTextbox("Motor 2: ", returnMessage3);
            });
            Thread.Sleep(500);

            // asking, if homing is ready
            string returnMessage4 = Motor2.ReadIfReady(0x00, 0x01);
            // showing messages in textBox
            textBox.Invoke((Action)delegate
            {
                WriteToTextbox("Motor 2: ", returnMessage4);
            });
            // Asking motor locations
            string location2 = Motor2.ReadLocation();
            // If location is 0 & 0, homing is completed, in other cases, asking to try again
            if (location2 == "0.00 mm")
            {
                Motor2.HomingCompleted = true;
            }
            else
            {
                Motor2.HomingCompleted = false;
                textBox.Invoke((Action)delegate
                {
                    WriteToTextbox("Motor 2: ", "Homing failed, try again");
                });
            }
            // Showing motor locations in UI
            ShowLocationAndErrorsMotor2();
        }

        // Start moving to location
        private void MovingCommand(byte[] recordNumberMotor2, byte[] recordNumberMotor1)
        {
            ButtonPushStart();

            // Start movin to right location
            string returnMessage1 = Motor1.StartRecord(recordNumberMotor1);
            string returnMessage2 = Motor2.StartRecord(recordNumberMotor2);
            // showing messages in textBox
            textBox.Invoke((Action)delegate
            {
                WriteToTextbox("Motor 1: ", returnMessage1);
                WriteToTextbox("Motor 2: ", returnMessage2);
            });

            // asking, if record is ready
            string returnMessage3 = Motor1.ReadIfReady(0x00, 0x01);
            string returnMessage4 = Motor2.ReadIfReady(0x00, 0x01);
            // showing messages in textBox
            textBox.Invoke((Action)delegate
            {
                WriteToTextbox("Motor 1: ", returnMessage3);
                WriteToTextbox("Motor 2: ", returnMessage4);
            });

            // Showing motor location and errors
            ShowLocationAndErrorsMotor1();
            ShowLocationAndErrorsMotor2();
        }

        // Start moving in to stacker
        private void MovingToStacker()
        {
            ButtonPushStart();
            string returnMessage = "";
            // Reading motor 2 location
            String motor2Location = Motor2.ReadLocation();

            // Checking if motor 2 is in the right location
            if (motor2Location == "33.00 mm" || motor2Location == "345.00 mm" || motor2Location == "655.00 mm")
            {
                // Locking Motor 2 location
                Motor2.LockMotor();
                if (Motor2.MotorLocationLocked == true)
                {
                    // Moving in to stacker
                    returnMessage = Motor1.StartRecord(recordNumber3);
                    Thread.Sleep(100);
                    // Reading if ready
                    returnMessage += Motor1.ReadIfReady(0x00, 0x01);
                    //returnMessage = "Motor 2 is in the right location for this function";
                    Motor1.LockMotor();
                }
                else
                {
                    returnMessage = "Motor 2 is in the wrong location for this function";
                    Motor2.UnlockMotor();
                }
            }
            else
            {
                returnMessage = "Motor 2 is in the wrong location for this function, motor 2 location is " + motor2Location;
            }
            // Writing message to textbox
            WriteToTextbox("Motor 1: ", returnMessage);

            if (!(returnMessage == "Motor 2 is in the wrong location for this function"))
            {
                // if location was ok, give instructions to continue
                WriteToTextbox("", "Push \"Come out from the stacker\" to continue ");
                ShowLocationAndErrorsMotor1();
            }
        }

        // Start moving out from stacker
        private void MovingFromStacker()
        {
            ButtonPushStart();
            string returnMessage = "";
            // Checking if motor 2 location is locked
            if (Motor2.MotorLocationLocked == true)
            {
                Motor1.UnlockMotor();
                // Moving out from stacker and unlocking motor 2location
                returnMessage = Motor1.StartRecord(recordNumber6);
                Thread.Sleep(100);
                returnMessage += Motor1.ReadIfReady(0x00, 0x01);
                String motor1Location = Motor1.ReadLocation();
                // If motor is out from stacker
                if (motor1Location == "200.00 mm")
                {
                    Motor2.UnlockMotor();
                }
                else
                {
                    returnMessage = "Error, try again";
                    Motor1.LockMotor();
                }
            }
            else
            {
                // Message, if motor is already out
                returnMessage = "Motor 2 is free to move";
            }
            WriteToTextbox("Motor 1: ", returnMessage);
            ShowLocationAndErrorsMotor1();
        }

        // Showing motor 1 current locations and errors in messageboxes
        private void ShowLocationAndErrorsMotor1()
        {
            // Showing location
            locationMotor1 = Motor1.ReadLocation();
            textBox1.Invoke((Action)delegate
            {
                textBox1.Text = locationMotor1;
            });
            using (StreamWriter sw = File.AppendText(logPath))
            {
                sw.WriteLine(DateTime.Now.ToString("HH:mm:ss ") + " Motor 1 location: " + locationMotor1);
            }

            // Showing errors and warnings
            // Reading Warnings
            string returnMessage3 = Motor1.ReadErrors();
            // showing messages in textBox
            if (!(returnMessage3 == "0 Errors, 0 Warnings" || returnMessage3 == "0 Errors0 Errors, 0 Warnings"))
            {
                WriteToTextbox("Motor 1: ", returnMessage3);
                error = true;
            }
        }

        // Showing motor 2 current locations and errors in messageboxes
        private void ShowLocationAndErrorsMotor2()
        {
            // Showing location
            locationMotor2 = Motor2.ReadLocation();
            textBox2.Invoke((Action)delegate
            {
                textBox2.Text = locationMotor2;
            });
            using (StreamWriter sw = File.AppendText(logPath))
            {
                sw.WriteLine(DateTime.Now.ToString("HH:mm:ss ") + " Motor 2 location: " + locationMotor2);
            }

            // Showing errors and warnings
            // Reading Warnings
            string returnMessage4 = Motor2.ReadErrors();
            // showing messages in textBox
            if (!(returnMessage4 == "0 Errors, 0 Warnings" || returnMessage4 == "0 Errors0 Errors, 0 Warnings"))
            {
                WriteToTextbox("Motor 2: ", returnMessage4);
                error = true;
            }
        }

        // Thing to do before starting buttons
        private void ButtonPushStart()
        {
            // Stopping everything
            Motor1.Stop();
            Motor2.Stop();
            // Allowing moving again
            Motor1.CancellingStop();
            Motor2.CancellingStop();            
        }

        // Take 6000 pictures and moving plate
        private void TakePictures()
        {
            ButtonPushStart();
            int imageWidth = 4000; // Row width in pixels
            int imageHeight = 6000; // Row count of picture (1 row = 1 pixel)           

            Motor2.StartRecord(recordNumber9);

            // Taking pictures and mowing plate 1 row at time.
            image = Camera.TakePictures(imageHeight, imageWidth); // Taking x pictures, x = images height.

            pictureBox1.Invoke((Action)delegate
            {
                // Showing picture
                pictureBox1.Image = image;
            });

            // Forming an image name
            imageName = "Picture" + pictureNumber.ToString("000") + "_Plate_" + barcode + backlightState + lineLightState + ".png";
            pictureNumber += 1;

            // Showing message in messagebox
            WriteToTextbox("Camera ", "Picture taken, pictures name: " + imageName);

            // Showing pictures name above picture
            textBox4.Invoke((Action)delegate
            {
                textBox4.Text = imageName;
            });
        }

        // Messagebox writer
        private void WriteToTextbox(string motor, string message)
        {
            textBox.Invoke((Action)delegate
            // Showing message in messagebox
            {
                textBox.Text = DateTime.Now.ToString("HH:mm:ss ") + motor + message + Environment.NewLine + textBox.Text;
            });
            
            // Adding message to log file
            using (StreamWriter sw = File.AppendText(logPath))
            {
                sw.WriteLine(DateTime.Now.ToString("HH:mm:ss ") + motor + message);
            }
        }


        #endregion

        #region Camera

        // Setting camera parameters
        private void SetParameters(object sender, EventArgs e)
        {
            Thread Parameters = new Thread(t =>
            {
                int imageWidth = 4000; // Row width in pixels
                int imageHeight = 1; // Row height in pixels
                string returnMessage = Camera.SetParameters(imageHeight, imageWidth); // Taking 1 picture.      
                // Writing message to textbox
                WriteToTextbox("Camera: ", returnMessage + ", parameters set");

                // Turning off the light
                Camera.LightsOnOff("Upper", false);
                Camera.LightsOnOff("Lower", false);
            })
            { IsBackground = true };
            Parameters.Start();
        }

        // Taking pictures button
        private void TakePictures(object sender, EventArgs e)
        {
            // Using another thread so that the UI remains available for use.
            Thread TakePicturesAll = new Thread(t =>
            {
                TakePictures();
            })
            { IsBackground = true };
            TakePicturesAll.Start();
        }

        // Put on the upper light
        private void BacklightOn(object sender, EventArgs e)
        {
            Thread Light = new Thread(t =>
            {
                string returnMessage = Camera.LightsOnOff("Backlight", true);
                // Writing message to textbox
                WriteToTextbox("Backlight: ", returnMessage);
                backlightState = "Backlight";
            })
            { IsBackground = true };
            Light.Start();
        }

        // Put off the upper light
        private void BacklightOff(object sender, EventArgs e)
        {
            Thread Light = new Thread(t =>
            {
                string returnMessage = Camera.LightsOnOff("Backlight", false);
                // Writing message to textbox
                WriteToTextbox("Backlight: ", returnMessage);
                backlightState = "";
            })
            { IsBackground = true };
            Light.Start();
        }

        // Put on the lower light
        private void LineLightOn(object sender, EventArgs e)
        {
            Thread Light = new Thread(t =>
            {
                string returnMessage = Camera.LightsOnOff("LineLight", true);
                // Writing message to textbox
                WriteToTextbox("LineLight: ", returnMessage);
                lineLightState = "LineLight";
            })
            { IsBackground = true };
            Light.Start();
        }

        // Put off the lower light
        private void LineLightOff(object sender, EventArgs e)
        {
            Thread Light = new Thread(t =>
            {
                string returnMessage = Camera.LightsOnOff("LineLight", false);
                // Writing message to textbox
                WriteToTextbox("LineLight: ", returnMessage);
                lineLightState = "";
            })
            { IsBackground = true };
            Light.Start();
        }

        // Save last picture
        private void SavePicture(object sender, EventArgs e)
        {
            Thread Save = new Thread(t =>
            {
                Camera.SaveImage(imageName, image, folderPath);
                WriteToTextbox("Camera: ", "Picture saved");
            })
            { IsBackground = true };
            Save.Start();
        }

        #endregion

        #region Stackers

        // Connect stackers button
        private void ConnectPutton(object sender, EventArgs e)
        {
            Thread Connect = new Thread(t =>
            {
                // If stackers are not connected yet, connect them
                if (stackerConnected == false)
                {
                    // Conenct stackers
                    string returnMessage = Essu.GatherNodes();
                    // Write returnmessage to messagebox
                    WriteToTextbox("Stacker: ", returnMessage);
                    // Change stackers state to "connected"
                    if (returnMessage == "Connected")
                    {
                        stackerConnected = true;
                    }
                }
                // If stackers are connected
                else
                {
                    WriteToTextbox("Stacker: ", "Stacker is already connected");
                }
            })
            { IsBackground = true };
            Connect.Start();
        }

        // Reading barcode from the plate
        private void ReadBarcode(object sender, EventArgs e)
        {
            Thread Barcode = new Thread(t =>
            {
                // Read barcode
                string returnMessage = Essu.ReadBarcodeOutput();
                // Writing message to textbox
                WriteToTextbox("Stacker: ", returnMessage);
                // Taking only the barcode from the return message
                barcode = returnMessage.Substring(returnMessage.IndexOf(",") +2 );
                // Writing barcode to textbox3
                textBox3.Invoke((Action)delegate
                {
                    textBox3.Text = barcode;
                });               
            })
            { IsBackground = true };
            Barcode.Start();
        }

        // Push button for stacker 1
        private void Stacker1Push(object sender, EventArgs e)
        {
            Thread Push = new Thread(t =>
            {
                // Push plate
                string returnMessage = Essu.PushOutput();
                // Writing message to textbox
                WriteToTextbox("Stacker: ", returnMessage);
            })
            { IsBackground = true };
            Push.Start();
        }

        // Pop button for stacker 1
        private void Stacker1Pop(object sender, EventArgs e)
        {
            Thread Pop = new Thread(t =>
            {
                // Pop plate
                string returnMessage = Essu.PopOutput();
                // Writing message to textbox
                WriteToTextbox("Stacker: ", returnMessage);
            })
            { IsBackground = true };
            Pop.Start();
        }

        // Reset button for stacker 1
        private void Stacker1Reset(object sender, EventArgs e)
        {
            Thread Reset = new Thread(t =>
            {
                // Reset stacker
                string returnMessage = Essu.ResetOutput();
                // Writing message to textbox
                WriteToTextbox("Stacker: ", returnMessage);
            })
            { IsBackground = true };
            Reset.Start();
        }

        // Peek button for stacker 1
        private void Stacker1Peek(object sender, EventArgs e)
        {
            Thread Peek = new Thread(t =>
            {
                // Peek plate
                string returnMessage = Essu.PeekOutput();
                // Writing message to textbox
                WriteToTextbox("Stacker: ", returnMessage);
            })
            { IsBackground = true };
            Peek.Start();
        }

        // Push button for stacker 2
        private void Stacker2Push(object sender, EventArgs e)
        {
            Thread Push = new Thread(t =>
            {
                // Push plate
                string returnMessage = Essu.PushInput();
                // Writing message to textbox
                WriteToTextbox("Stacker: ", returnMessage);
            })
            { IsBackground = true };
            Push.Start();
        }

        // Pop button for stacker 2
        private void Stacker2Pop(object sender, EventArgs e)
        {
            Thread Pop = new Thread(t =>
            {
                // Pop plate
                string returnMessage = Essu.PopInput();
                // Writing message to textbox
                WriteToTextbox("Stacker: ", returnMessage);
            })
            { IsBackground = true };
            Pop.Start();
        }

        // Reset button for stacker 2
        private void Stacker2Reset(object sender, EventArgs e)
        {
            Thread Reset = new Thread(t =>
            {
                // Reset stacker
                string returnMessage = Essu.ResetInput();
                // Writing message to textbox
                WriteToTextbox("Stacker: ", returnMessage);
            })
            { IsBackground = true };
            Reset.Start();
        }

        // Peek button for stacker 2
        private void Stacker2Peek(object sender, EventArgs e)
        {
            Thread Peek = new Thread(t =>
            {
                // Peek plate
                string returnMessage = Essu.PeekInput();
                // Writing message to textbox
                WriteToTextbox("Stacker: ", returnMessage);
            })
            { IsBackground = true };
            Peek.Start();
        }

        // Stacker 3 is not in use yet (When done, remove the note from the UI)
        // Push button for stacker 3
        private void Stacker3Push(object sender, EventArgs e)
        {
            Thread Push = new Thread(t =>
            {

            })
            { IsBackground = true };
            Push.Start();
        }

        // Pop button for stacker 3
        private void Stacker3Pop(object sender, EventArgs e)
        {
            Thread Pop = new Thread(t =>
            {

            })
            { IsBackground = true };
            Pop.Start();
        }

        // Reset button for stacker 3
        private void Stacker3Reset(object sender, EventArgs e)
        {
            Thread Reset = new Thread(t =>
            {

            })
            { IsBackground = true };
            Reset.Start();
        }

        // Peek button for stacker 3
        private void Stacker3Peek(object sender, EventArgs e)
        {
            Thread Peek = new Thread(t =>
            {
                
            })
            { IsBackground = true };
            Peek.Start();
        }

        #endregion

    }
}
