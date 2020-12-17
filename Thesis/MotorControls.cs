using System;
using System.Net.Sockets;
using System.Threading;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using Basler.Pylon;
using System.Linq;
using System.Text;

namespace Scanner
{
    public partial class MotorControls
    {
        #region variables and objects

        public int Motor { get; set; } // Motors number
        public string IPAddress { get; set; } // Motors IP address
        public bool Connected { get; set; } // Is motor connected already
        public TcpClient Client { get; set; }
        public NetworkStream Stream { get; set; }
        public bool StopActive { get; set; } // Is stop button pushed
        public bool MotorLocationLocked { get; set; } // Is motor 1 inside stacker
        public bool HomingCompleted { get; set; } // Is homing completed after pushing homing button

        public MotorControls(int motor, string ipAddress, bool connected, TcpClient client, NetworkStream stream, bool stopActive, bool motorLocationLocked, bool homingCompleted)
        {
            IPAddress = ipAddress;
            Motor = motor;
            Connected = connected;
            Client = client;
            Stream = stream;
            StopActive = stopActive;
            MotorLocationLocked = motorLocationLocked;
            HomingCompleted = homingCompleted;
        }

        string returnMessage = "";
        string errorMessage = "";

        #endregion

        #region Connect/Disconnect

        // Connecting motor
        public string Connect()
        {
            try
            {
                // Create a TcpClient.
                Int32 port = 49700;
                Client = new TcpClient();
                Client.Connect(IPAddress, port);

                // Get a client stream for reading and writing.
                Stream = Client.GetStream();

                byte[] message = { };

                void sendMessage()
                {
                    // Send the message to the connected TcpServer.
                    Stream.Write(message, 0, message.Length);

                    // Buffer to store the response bytes.
                    message = new Byte[256];

                    // Read the first batch of the TcpServer response bytes.
                    Int32 bytes = Stream.Read(message, 0, message.Length);

                    // writing the acnowledgement code and description, if there is error
                    byte ack = message[9];
                    string acknowledge = ack.ToString();

                    // Checking if there is any acknowledges 
                    if (!(acknowledge == "0"))
                    {
                        returnMessage = Acknowledges(acknowledge);
                    }
                }

                // Request “Read CVE object"
                byte[] writeCVE1 = ReadMessage(/*Object index*/0x00, 0x01);
                message = writeCVE1;
                sendMessage();

                // Comparing, when ready message == same as message from motorcontroller (two options)
                if (message[18] == 96 && message[19] == 132 && message[20] == 130 && message[21] == 0)
                {
                    returnMessage = "Motor is connected";
                    Connected = true;
                }
                else if (message[18] == 96 && message[19] == 4 && message[20] == 130 && message[21] == 0)
                {
                    returnMessage = "Motor is connected";
                    Connected = true;
                }
                else
                {
                    returnMessage = "Connecting failed, try again";
                    Connected = false;
                }
            }
            catch (ArgumentNullException e)
            {
                errorMessage += "\r\n\t\tArgumentNullException: " + e;
            }
            catch (SocketException e)
            {
                errorMessage += "\r\n\t\tSocketException: " + e + "\r\n\t\tTry connect again";
                Stream.Close();
                Client.Close();
            }
            catch (System.NullReferenceException e)
            {

                errorMessage += "\r\n\t\tArgumentNullException: " + e;
            }
            if (!(errorMessage == ""))
            {
                // if there is errors, return error message
                returnMessage = errorMessage;
                errorMessage = "";
                return returnMessage;

            }
            else
            {
                return returnMessage;
            }
        }

        // Closing connections
        public string Disconnect()
        {
            try
            {
                // Closing client and stream
                Stream.Close();
                Client.Close();
                returnMessage = "Connection closed";
                Connected = false;
            }
            catch (System.NullReferenceException e)
            {
                errorMessage += "\r\n\t\tArgumentNullException: " + e;
            }

            catch (ArgumentNullException e)
            {
                errorMessage += "\r\n\t\tArgumentNullException: " + e;
            }
            catch (SocketException e)
            {
                errorMessage += "\r\n\t\tSocketException: " + e;
            }
            // If there is errors, return error message
            if (!(errorMessage == ""))
            {
                returnMessage = errorMessage;
                errorMessage = "";
                return returnMessage;
            }
            else
            {
                return returnMessage;
            }
        }

        #endregion

        #region MovingAndStopping

        // Homing
        public string Homing()
        {
            if (Connected == true && StopActive == false && MotorLocationLocked == false)
            {
                try
                {
                    byte[] message = { };
                    void sendMessage()
                    {
                        // Send the message to the connected TcpServer.
                        Stream.Write(message, 0, message.Length);

                        // Buffer to store the response bytes.
                        message = new Byte[256];

                        // Read the first batch of the TcpServer response bytes.
                        Int32 bytes = Stream.Read(message, 0, message.Length);

                        // writing the acnowledgement code and description, if there is error
                        byte ack = message[9];
                        string acknowledge = ack.ToString();

                        // Checking if there is any acknowledges 
                        if (!(acknowledge == "0"))
                        {
                            returnMessage = Acknowledges(acknowledge);
                        }
                    }

                    //  Activate the higher-order controller for the CVE connection
                    byte[] writeCVE1 = WriteMessage(/*Object index*/0x00, 0x03, /*data type*/0x04, /*Data bytes*/new byte[] { 0x00, 0x00, 0x00, 0x02 });
                    message = writeCVE1;
                    sendMessage();

                    // Activate the “Ready to switch on” status 
                    byte[] writeCVE2 = WriteMessage(/*Object index*/0x00, 0x02, /*data type*/0x02, /*Data bytes*/new byte[] { 0x00, 0x00, 0x00, 0x06 });
                    message = writeCVE2;
                    sendMessage();

                    // Activate the “Switched on” status
                    byte[] writeCVE3 = WriteMessage(/*Object index*/0x00, 0x02, /*data type*/0x02, /*Data bytes*/new byte[] { 0x00, 0x00, 0x00, 0x07 });
                    message = writeCVE3;
                    sendMessage();

                    //  Activate the “Operation enabled” status 
                    byte[] writeCVE4 = WriteMessage(/*Object index*/0x00, 0x02, /*data type*/0x02, /*Data bytes*/new byte[] { 0x00, 0x00, 0x00, 0x0F });
                    message = writeCVE4;
                    sendMessage();

                    //  Select the “homing” drive function 
                    byte[] writeCVE5 = WriteMessage(/*Object index*/0x00, 0x78, /*data type*/0x08, /*Data bytes*/new byte[] { 0x00, 0x00, 0x00, 0x06 });
                    message = writeCVE5;
                    sendMessage();

                    // Start the homing run
                    byte[] writeCVE6 = WriteMessage(/*Object index*/0x00, 0x02, /*data type*/0x02, /*Data bytes*/new byte[] { 0x00, 0x00, 0x00, 0x1F });
                    message = writeCVE6;
                    sendMessage();

                    returnMessage = "Homing";

                }
                catch (ArgumentNullException e)
                {
                    errorMessage += "\r\n\t\tArgumentNullException: " + e;
                }
                catch (SocketException e)
                {
                    errorMessage += "\r\n\t\tSocketException: " + e;
                }
            }
            else
            {
                if (Connected == false)
                {
                    returnMessage = "Make connection first";
                }
                else if (StopActive == true)
                {
                    returnMessage = "Error, try again (Stop is active)";
                }
                else if (MotorLocationLocked == true)
                {
                    returnMessage = "Press \"Come out from the stacker\" button to continue";
                }
            }

            if (!(errorMessage == ""))
            {
                returnMessage = errorMessage;
                errorMessage = "";
                return returnMessage;
            }
            else
            {
                return returnMessage;
            }
        }

        // Using records to moving
        public string StartRecord(byte[] recordNumber)
        {
            if (Connected == true && StopActive == false && MotorLocationLocked == false && HomingCompleted == true)
            {
                try
                {
                    byte[] message = { };

                    void sendMessage()
                    {
                        // Send the message to the connected TcpServer.
                        Stream.Write(message, 0, message.Length);

                        // Buffer to store the response bytes.
                        message = new Byte[256];

                        // Read the first batch of the TcpServer response bytes.
                        Int32 bytes = Stream.Read(message, 0, message.Length);

                        // writing the acnowledgement code and description, if there is error
                        byte ack = message[9];
                        string acknowledge = ack.ToString();

                        // Checking if there is any acknowledges 
                        if (!(acknowledge == "0"))
                        {
                            returnMessage = Acknowledges(acknowledge);
                        }
                    }

                    //  Activate the higher-order controller for the CVE connection
                    byte[] writeCVE1 = WriteMessage(/*Object index*/0x00, 0x03, /*data type*/0x04, /*Data bytes*/new byte[] { 0x00, 0x00, 0x00, 0x02 });
                    message = writeCVE1;
                    sendMessage();

                    // Activate the “Ready to switch on” status 
                    byte[] writeCVE2 = WriteMessage(/*Object index*/0x00, 0x02, /*data type*/0x02, /*Data bytes*/new byte[] { 0x00, 0x00, 0x00, 0x06 });
                    message = writeCVE2;
                    sendMessage();

                    // Activate the “Switched on” status
                    byte[] writeCVE3 = WriteMessage(/*Object index*/0x00, 0x02, /*data type*/0x02, /*Data bytes*/new byte[] { 0x00, 0x00, 0x00, 0x07 });
                    message = writeCVE3;
                    sendMessage();

                    //  Activate the “Operation enabled” status 
                    byte[] writeCVE4 = WriteMessage(/*Object index*/0x00, 0x02, /*data type*/0x02, /*Data bytes*/new byte[] { 0x00, 0x00, 0x00, 0x0F });
                    message = writeCVE4;
                    sendMessage();

                    //  Select the “position record” drive function 
                    byte[] writeCVE5 = WriteMessage(/*Object index*/0x00, 0x78, /*data type*/0x08, /*Data bytes*/new byte[] { 0x00, 0x00, 0x00, 0x01 });
                    message = writeCVE5;
                    sendMessage();

                    // Select the desired record
                    byte[] writeCVE6 = WriteMessage(/*Object index*/0x00, 0x1F, /*data type*/0x04, /*Data bytes*/recordNumber);
                    message = writeCVE6;
                    sendMessage();

                    // Start the record by writing the control word
                    byte[] writeCVE7 = WriteMessage(/*Object index*/0x00, 0x02, /*data type*/0x02, /*Data bytes*/new byte[] { 0x00, 0x00, 0x00, 0x1F });
                    message = writeCVE7;
                    sendMessage();

                    returnMessage = "Moving";
                }
                catch (ArgumentNullException e)
                {
                    errorMessage += "\r\n\t\tArgumentNullException: " + e;
                }
                catch (SocketException e)
                {
                    errorMessage += "\r\n\t\tSocketException: " + e;
                }
            }
            else
            {
                if (Connected == false)
                {
                    returnMessage = "Make connection first";
                }
                else if (StopActive == true)
                {
                    returnMessage = "Error, try again (Stop is active)";
                }
                else if (MotorLocationLocked == true)
                {
                    returnMessage = "Press \"Come out from the stacker\" button to continue";
                }
                else if (HomingCompleted == false)
                {
                    returnMessage = "Press \"Homing\" button to continue";
                }
            }

            if (!(errorMessage == ""))
            {
                returnMessage = errorMessage;
                errorMessage = "";
                return returnMessage;
            }
            else
            {
                return returnMessage;
            }
        }

        // Stop
        public string Stop()
        {
            if (Connected == true)
            {
                try
                {
                    byte[] message = { };

                    void sendMessage()
                    {
                        // Send the message to the connected TcpServer.
                        Stream.Write(message, 0, message.Length);

                        // Buffer to store the response bytes.
                        message = new Byte[256];

                        // Read the first batch of the TcpServer response bytes.
                        Int32 bytes = Stream.Read(message, 0, message.Length);

                        // writing the acnowledgement code and description, if there is error
                        byte ack = message[9];
                        string acknowledge = ack.ToString();

                        // Checking if there is any acknowledges 
                        if (!(acknowledge == "0"))
                        {
                            returnMessage = Acknowledges(acknowledge);
                        }
                    }

                    //  Quick Stop activated
                    byte[] writeCVE1 = WriteMessage(/*Object index*/0x00, 0x03, /*data type*/0x04, /*Data bytes*/new byte[] { 0x00, 0x00, 0x00, 0x02 });
                    message = writeCVE1;
                    sendMessage();

                    byte[] writeCVE2 = WriteMessage(/*Object index*/0x00, 0x02, /*data type*/0x02, /*Data bytes*/new byte[] { 0x00, 0x00, 0x00, 0x02 });
                    message = writeCVE2;
                    sendMessage();

                    returnMessage = "Stop";
                    StopActive = true;

                }
                catch (ArgumentNullException e)
                {
                    errorMessage += "\r\n\t\tArgumentNullException: " + e;
                }
                catch (SocketException e)
                {
                    errorMessage += "\r\n\t\tSocketException: " + e;
                }
                catch (ObjectDisposedException e)
                {
                    errorMessage += "\r\n\t\tObjectDisposedException: " + e;
                }
                catch (System.IO.IOException e)
                {
                    errorMessage += "\r\n\t\tIOException: " + e;
                }
            }
            else
            {
                if (Connected == false)
                {
                    returnMessage = "Make connection first";
                }
                else if (StopActive == true)
                {
                    returnMessage = "Error, try again (Stop is active)";
                }
                else if (MotorLocationLocked == true)
                {
                    returnMessage = "Press \"Come out from the stacker\" button to continue";
                }
                
            }

            if (!(errorMessage == ""))
            {
                returnMessage = errorMessage;
                errorMessage = "";
                return returnMessage;
            }
            else
            {
                return returnMessage;
            }
        }

        // Cancelling stop button
        public void CancellingStop()
        {
            StopActive = false;
        }

        // Locks the motor location
        public void LockMotor()
        {
            // locked, when plate carrier is inside stacker
            MotorLocationLocked = true;
        }

        // Unlocks the motor location
        public void UnlockMotor()
        {
            // locked, when plate carrier is inside stacker
            MotorLocationLocked = false;
        }

        #endregion

        #region Reading

        // Read motor message
        public string Read(byte objectIndex1, byte objectIndex2)
        {
            if (Connected == true)
            {
                try
                {
                    byte[] message = { };

                    void sendMessage()
                    {
                        // Send the message to the connected TcpServer.
                        Stream.Write(message, 0, message.Length);

                        // Buffer to store the response bytes.
                        message = new Byte[256];

                        // Read the first batch of the TcpServer response bytes.
                        Int32 bytes = Stream.Read(message, 0, message.Length);

                        // writing the acnowledgement code and description, if there is error
                        byte ack = message[9];
                        string acknowledge = ack.ToString();

                        // Checking if there is any acknowledges 
                        if (!(acknowledge == "0"))
                        {
                            returnMessage = Acknowledges(acknowledge);
                        }
                    }

                    // Request “Read CVE object"
                    byte[] writeCVE1 = ReadMessage(/*Object index*/objectIndex1, objectIndex2);

                    message = writeCVE1;
                    sendMessage();

                    returnMessage = message[18] + " " + message[19] + " " + message[20] + " " + message[21];

                }
                catch (ArgumentNullException e)
                {
                    errorMessage += "\r\n\t\tArgumentNullException: " + e;
                }
                catch (SocketException e)
                {
                    errorMessage += "\r\n\t\tSocketException: " + e;
                }
            }
            else
            {
                if (Connected == false)
                {
                    returnMessage = "Make connection first";
                }
                else if (StopActive == true)
                {
                    returnMessage = "Error, try again (Stop is active)";
                }
                else if (MotorLocationLocked == true)
                {
                    returnMessage = "Press \"Come out from the stacker\" button to continue";
                }
            }
            if (!(errorMessage == ""))
            {
                returnMessage = errorMessage;
                errorMessage = "";
                return returnMessage;
            }
            else
            {
                return returnMessage;
            }
        }

        // Read motor message, until gets ready answer
        public string ReadIfReady(byte objectIndex1, byte objectIndex2)
        {
            if (Connected == true && StopActive == false && MotorLocationLocked == false)
            {
                try
                {
                    byte[] message = { };

                    void sendMessage()
                    {
                        // Send the message to the connected TcpServer.
                        Stream.Write(message, 0, message.Length);

                        // Buffer to store the response bytes.
                        message = new Byte[256];

                        // Read the first batch of the TcpServer response bytes.
                        Int32 bytes = Stream.Read(message, 0, message.Length);

                        // writing the acnowledgement code and description, if there is error
                        byte ack = message[9];
                        string acknowledge = ack.ToString();

                        // Checking if there is any acknowledges 
                        if (!(acknowledge == "0"))
                        {
                            returnMessage = Acknowledges(acknowledge);
                        }
                    }

                    // Request “Read CVE object"
                    byte[] writeCVE1 = ReadMessage(/*Object index*/objectIndex1, objectIndex2);

                    message = writeCVE1;
                    sendMessage();

                    // Comparing, when ready message == same as message from motorcontroller

                    for (int i = 0; i < 150; i++)
                    {
                        if (!(message[18] == 39 && message[19] == 196 && message[20] == 6 && message[21] == 0) && StopActive == false)
                        {
                            Thread.Sleep(100);
                            message = writeCVE1;
                            sendMessage();   
                            returnMessage = "Tiemout, try again";
                        }
                        else
                        {
                            returnMessage = "Ready";
                        }
                    }
                }
                catch (ArgumentNullException e)
                {
                    errorMessage += "\r\n\t\tArgumentNullException: " + e;
                }
                catch (SocketException e)
                {
                    errorMessage += "\r\n\t\tSocketException: " + e;
                }
                catch (ObjectDisposedException e)
                {
                    errorMessage += "\r\n\t\tObjectDisposedException: " + e;
                }
            }
            else
            {
                if (Connected == false)
                {
                    returnMessage = "Make connection first";
                }
                else if (StopActive == true)
                {
                    returnMessage = "Error, try again (Stop is active)";
                }
                else if (MotorLocationLocked == true)
                {
                    returnMessage = "Press \"Come out from the stacker\" button to continue";
                }
            }
            if (!(errorMessage == ""))
            {
                returnMessage = errorMessage;
                errorMessage = "";
                return returnMessage;
            }
            else
            {
                return returnMessage;
            }
        }

        // Read motors location from motor
        public string ReadLocation() // 
        {
            if (Connected == true)
            {
                try
                {
                    byte[] message = { };

                    void sendMessage()
                    {
                        // Send the message to the connected TcpServer.
                        Stream.Write(message, 0, message.Length);

                        // Buffer to store the response bytes.
                        message = new Byte[256];

                        // Read the first batch of the TcpServer response bytes.
                        Int32 bytes = Stream.Read(message, 0, message.Length);

                        // writing the acnowledgement code and description, if there is error
                        byte ack = message[9];
                        string acknowledge = ack.ToString();

                        // Checking if there is any acknowledges 
                        if (!(acknowledge == "0"))
                        {
                            returnMessage = Acknowledges(acknowledge);
                        }
                    }

                    // Request “Read CVE object"
                    byte[] writeCVE1 = ReadMessage(/*Object index*/0x00, 0x38);

                    message = writeCVE1;
                    sendMessage();

                    // Converting a message to hexadecimal
                    int first = message[21];
                    string firstString = first.ToString("X").PadLeft(2, '0');
                    int second = message[20];
                    string secondString = second.ToString("X").PadLeft(2, '0');
                    int third = message[19];
                    string thirdString = third.ToString("X").PadLeft(2, '0');
                    int fourth = message[18];
                    string fourthString = fourth.ToString("X").PadLeft(2, '0');
                    string hex = firstString + secondString + thirdString + fourthString;
                    uint intVal = Convert.ToUInt32(hex, 16);

                    // Showing location, when it is positive
                    if (Motor == 1)
                    {
                        double locationMm = intVal / 1000.0;
                        string location = string.Format("{0:0.00}", locationMm);
                        returnMessage = location + " mm";
                    }
                    // Taking two's complement and showing location, if location is negative
                    else if (Motor == 2)
                    {
                        uint twosComp = ~intVal + 1;
                        double locationMm = twosComp / 1000.0;
                        string location = string.Format("{0:0.00}", locationMm);
                        returnMessage = location + " mm";
                    }
                    else
                    {
                        returnMessage = "Error";
                    }
                }
                catch (ArgumentNullException e)
                {
                    errorMessage += "\r\n\t\tArgumentNullException: " + e;
                }
                catch (SocketException e)
                {
                    errorMessage += "\r\n\t\tSocketException: " + e;
                }
            }
            else
            {
                if (Connected == false)
                {
                    returnMessage = "Make connection first";
                }
                else if (StopActive == true)
                {
                    returnMessage = "Error, try again (Stop is active)";
                }
                else if (MotorLocationLocked == true)
                {
                    returnMessage = "Press \"Come out from the stacker\" button to continue";
                }
            }
            if (!(errorMessage == ""))
            {
                returnMessage = errorMessage;
                errorMessage = "";
                return returnMessage;
            }
            else
            {
                return returnMessage;
            }
        }

        // Read error messages from motor
        public string ReadErrors()
        {
            if (Connected == true)
            {
                try
                {
                    byte[] message = { };

                    void sendMessage()
                    {
                        // Send the message to the connected TcpServer.
                        Stream.Write(message, 0, message.Length);

                        // Buffer to store the response bytes.
                        message = new Byte[256];

                        // Read the first batch of the TcpServer response bytes.
                        Int32 bytes = Stream.Read(message, 0, message.Length);

                        // writing the acnowledgement code and description, if there is error
                        byte ack = message[9];
                        string acknowledge = ack.ToString();

                        // Checking if there is any acknowledges 
                        if (!(acknowledge == "0"))
                        {
                            returnMessage = Acknowledges(acknowledge);
                        }
                    }

                    // Request “Read CVE object"
                    byte[] writeCVE1 = ReadMessage(/*Object index*/0x00, 0xD5);

                    message = writeCVE1;
                    sendMessage();

                    if (message[18] == 255 && message[19] == 255)
                    {
                        returnMessage = ", 0 Warnings";
                    }

                    else
                    {
                        // Converting a message to hexadecimal
                        int first = message[21];
                        string firstString = first.ToString("X").PadLeft(2, '0');
                        int second = message[20];
                        string secondString = second.ToString("X").PadLeft(2, '0');
                        int third = message[19];
                        string thirdString = third.ToString("X").PadLeft(2, '0');
                        int fourth = message[18];
                        string fourthString = fourth.ToString("X").PadLeft(2, '0');
                        string hex = firstString + secondString + thirdString + fourthString;

                        returnMessage = ", Warning: " + hex;
                    }

                    // Request “Read CVE object"
                    byte[] writeCVE2 = ReadMessage(/*Object index*/0x00, 0xC2);

                    message = writeCVE2;
                    sendMessage();

                    if (message[18] == 0)
                    {
                        returnMessage = "Error: The error cannot be acknowledged." + returnMessage;
                    }

                    else if (message[18] == 1)
                    {
                        returnMessage = "Error: The malfunction is still active; the error can be deleted only after the malfunction is eliminated." + returnMessage;
                    }

                    else if (message[18] == 2)
                    {
                        returnMessage = "Error: The error can be eliminated immediately" + returnMessage;
                    }

                    else if (message[18] == 255)
                    {
                        returnMessage = "0 Errors" + returnMessage;
                    }
                }
                catch (ArgumentNullException e)
                {
                    errorMessage += "\r\n\t\tArgumentNullException: " + e;
                }
                catch (SocketException e)
                {
                    errorMessage += "\r\n\t\tSocketException: " + e;
                }
            }
            else
            {
                if (Connected == false)
                {
                    returnMessage = "Make connection first";
                }
                else if (StopActive == true)
                {
                    returnMessage = "Error, try again (Stop is active)";
                }
                else if (MotorLocationLocked == true)
                {
                    returnMessage = "Press \"Come out from the stacker\" button to continue";
                }
            }
            if (!(errorMessage == ""))
            {
                returnMessage = errorMessage;
                errorMessage = "";
                return returnMessage;
            }
            else
            {
                return returnMessage;
            }
        }
        #endregion

        #region Messages

        // Message, when readin message from motor
        private byte[] ReadMessage(byte objectIndex1, byte objectIndex2)
        {
            byte[] write = new byte[]
            {
                0x10, // Service ID (UINT08), 0x10 = Read.
                0x01, 0x0, 0x00, 0x00, // Message ID (UINT32), freely assignable (not mandatory).
                0x04, 0x00, 0x00, 0x00, // Always 4 for this request.
                0x00, // Acknowledge (UINT08), initialise with 0.
                0x00, 0x00, 0x00, 0x00, // Reserved (UINT32), Placeholder, initialise with 0.
                objectIndex2, objectIndex1, // Object index (UINT 16), Index of the CVE object to be written. (List of CVE objects: Festo CMMO­ST-...-DION/DIOP manual, page 165)
                0x00, // Object subindex (UINT08), Always 0.
                0x00 // Reserved (UINT08), Placeholder (initialise with 0).
            };
            return write;
        }

        // Message, when writing message for motor
        private byte[] WriteMessage(byte objectIndex1, byte objectIndex2, byte dataType, byte[] dataBytes)
        {
            byte[] write = new byte[] { };
            // Message, when there is 1 byte in the message
            if (dataType == 0x04 || dataType == 0x08)
            {
                byte dataByte1 = dataBytes[3];
                byte[] message1Byte = new byte[]
                {
                    0x11, // Service ID (UINT08), 0x11 = Write.
                    0x02, 0x0, 0x00, 0x00, // Message ID (UINT32), freely assignable (not mandatory).
                    0x05, 0x00, 0x00, 0x00, // Data length (UINT32), Data length = 4 bytes + data type length. ( 4 bytes UINT32 and SINT32, 2 bytes UINT16 and SINT16, 1 byte UINT08 and SINT08 )
                    0x00, // Acknowledge (UINT08), initialise with 0.
                    0x00, 0x00, 0x00, 0x00, // Reserved (UINT32), Placeholder, initialise with 0.
                    objectIndex2, objectIndex1, // Object index (UINT 16), Index of the CVE object to be written. (List of CVE objects: Festo CMMO­ST-...-DION/DIOP manual, page 165)
                    0x00, // Object subindex (UINT08), Always 0.
                    dataType, // Data type (UINT08), Data type of the CVE object to be written. (Look at Festo CMMO­ST-...-DION/DIOP manual, page 166 - 171, which type to use) 0x02 = UINT32, 0x03 = UINT16, 0x04 = UINT08, 0x06 = SINT32, 0x07 = SINT16, 0x08 = SINT08
                    dataByte1 // Data bytes (Corresponding to data type of the CVE object), object value.    
                };
                write = message1Byte;
            }

            // Message, when there is 2 bytes in the message
            else if (dataType == 0x03 || dataType == 0x07)
            {
                byte dataByte1 = dataBytes[3];
                byte dataByte2 = dataBytes[2];
                byte[] message2Byte = new byte[]
                {
                    0x11, // Service ID (UINT08), 0x11 = Write.
                    0x03, 0x0, 0x00, 0x00, // Message ID (UINT32), freely assignable (not mandatory).
                    0x06, 0x00, 0x00, 0x00, // Data length (UINT32), Data length = 4 bytes + data type length. ( 4 bytes UINT32 and SINT32, 2 bytes UINT16 and SINT16, 1 byte UINT08 and SINT08 )
                    0x00, // Acknowledge (UINT08), initialise with 0.
                    0x00, 0x00, 0x00, 0x00, // Reserved (UINT32), Placeholder, initialise with 0.
                    objectIndex2, objectIndex1, // Object index (UINT 16), Index of the CVE object to be written. (List of CVE objects: Festo CMMO­ST-...-DION/DIOP manual, page 165)
                    0x00, // Object subindex (UINT08), Always 0.
                    dataType, // Data type (UINT08), Data type of the CVE object to be written. (Look at Festo CMMO­ST-...-DION/DIOP manual, page 166 - 171, which type to use) 0x02 = UINT32, 0x03 = UINT16, 0x04 = UINT08, 0x06 = SINT32, 0x07 = SINT16, 0x08 = SINT08
                    dataByte1, dataByte2 // Data bytes (Corresponding to data type of the CVE object), object value.    
                };
                write = message2Byte;
            }

            // Message, when there is 4 bytes in the message
            else if (dataType == 0x02 || dataType == 0x06)
            {
                byte dataByte1 = dataBytes[3];
                byte dataByte2 = dataBytes[2];
                byte dataByte3 = dataBytes[1];
                byte dataByte4 = dataBytes[0];
                byte[] message4Byte = new byte[]
                {
                    0x11, // Service ID (UINT08), 0x11 = Write.
                    0x04, 0x0, 0x00, 0x00, // Message ID (UINT32), freely assignable (not mandatory).
                    0x08, 0x00, 0x00, 0x00, // Data length (UINT32), Data length = 4 bytes + data type length. ( 4 bytes UINT32 and SINT32, 2 bytes UINT16 and SINT16, 1 byte UINT08 and SINT08 )
                    0x00, // Acknowledge (UINT08), initialise with 0.
                    0x00, 0x00, 0x00, 0x00, // Reserved (UINT32), Placeholder, initialise with 0.
                    objectIndex2, objectIndex1, // Object index (UINT 16), Index of the CVE object to be written. (List of CVE objects: Festo CMMO­ST-...-DION/DIOP manual, page 165)
                    0x00, // Object subindex (UINT08), Always 0.
                    dataType, // Data type (UINT08), Data type of the CVE object to be written. (Look at Festo CMMO­ST-...-DION/DIOP manual, page 166 - 171, which type to use) 0x02 = UINT32, 0x03 = UINT16, 0x04 = UINT08, 0x06 = SINT32, 0x07 = SINT16, 0x08 = SINT08
                    dataByte1, dataByte2, dataByte3, dataByte4 // Data bytes (Corresponding to data type of the CVE object), object value.    
                };
                write = message4Byte;
            }
            return write;
        }

        // Acknowledges, that motorcontroller gives if message is not in correct form
        private string Acknowledges(string acknowledge)
        {
            switch (acknowledge)
            {
                case "1":
                    errorMessage += "\r\n\t\tAcknowledge: 0x01, service is not supported, check the service ID of the request.";
                    break;
                case "3":
                    errorMessage += "\r\n\t\tAcknowledge: 0x03, user data length of the request is invalid, check the structure of the request.";
                    break;
                case "160":
                    errorMessage += "\r\n\t\tAcknowledge: 0xA0, range of values ​​of another CVE object violated, writing the CVE object would cause the range of values of another CVE object to be violated. (The other object uses this CVE object as a minimum or maximum).";
                    break;
                case "162":
                    errorMessage += "\r\n\t\tAcknowledge: 0xA2, invalid object index, correct the object index.";
                    break;
                case "163":
                    errorMessage += "\r\n\t\tAcknowledge: 0xA3, invalid object subindex, correct the object subindex.";
                    break;
                case "164":
                    errorMessage += "\r\n\t\tAcknowledge: 0xA4, the CVE object cannot be read.";
                    break;
                case "165":
                    errorMessage += "\r\n\t\tAcknowledge: 0xA5, the CVE object cannot be written.";
                    break;
                case "166":
                    errorMessage += "\r\n\t\tAcknowledge: 0xA6, the CVE object cannot be written while the drive is in an \"Operation enabled status\", quit the \"Operation enabled\" status .";
                    break;
                case "167":
                    errorMessage += "\r\n\t\tAcknowledge: 0xA7, the CVE object must not be written without master control, assign master control to the CVE interface. Use CVE object #3 for this purpose.";
                    break;
                case "169":
                    errorMessage += "\r\n\t\tAcknowledge: 0xA9, the CVE object cannot be written, as the value is lower than the minimum value, correct the value.";
                    break;
                case "170":
                    errorMessage += "\r\n\t\tAcknowledge: 0xAA, the CVE object cannot be written, as the value is greater than the maximum value, correct the value.";
                    break;
                case "171":
                    errorMessage += "\r\n\t\tAcknowledge: 0xAB, the CVE object cannot be written, as the value is not within the valid value set, correct the value.";
                    break;
                case "172":
                    errorMessage += "\r\n\t\tAcknowledge: 0xAC, the CVE object cannot be written, as the specified data type is incorrec, correct the data type.";
                    break;
                case "173":
                    errorMessage += "\r\n\t\tAcknowledge: 0xAD, the CVE object cannot be written, as it is password-protected, cancel the password protection (Festo CMMO­ST-...-DION/DIOP manual, chapter 2.3.3) .";
                    break;

            }
            return errorMessage;
        }

        #endregion
    }
}