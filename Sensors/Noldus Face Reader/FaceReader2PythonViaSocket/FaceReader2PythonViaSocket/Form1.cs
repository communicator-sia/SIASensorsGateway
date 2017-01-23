using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using FaceReaderAPI;
using FaceReaderAPI.Data;
using System.Threading;

namespace WindowsFormsTestSocket_01
{
    public partial class Form1 : Form
    {

        //delegate void SetTextCallback(string text);
        delegate void SetTextCallback(string text, Label label);


        Socket sender=null;
        // global instance of the FaceReaderController
        private FaceReaderController mFaceReaderController;


        private bool _isConnectedToPython = false;
        private int sentPacketsToPython = 0;

        private int receivedInSession = 0;
        private int receivedInSessionInLastInterval = 0;

        private int sentPacketsToPythonInLastInterval = 0;
        DateTime interval = DateTime.Now;
        DateTime observingStart;

        int observingIntervalSec = 3;
        private Thread labelReceivedInSessionUpdaterThread;

        //public Thread demoThread { get; private set; }
        private BackgroundWorker backgroundWorker1;

        // have this set up to update labels in thread.
        private Label label2update;
        private String text2Update;

        private Boolean autostart = false;


        public Form1(Boolean autostart)
        {
            this.autostart = autostart;
            InitializeComponent();
            //autostart = true;
            if (autostart) autostartAll();
        }

        private void buttonConnectSocket_Click(object sender, EventArgs e)
        {
            createSocket();

        }

        private void buttonSendMessage_Click(object sender, EventArgs e)
        {
            Console.WriteLine("Send message");
            sendMessageAndDisplayResponse("Ali tole deluje?");
        }

        private void buttonDisconnectSocket_Click(object sender, EventArgs e)
        {
            Console.WriteLine("Disconnect socket");
            destroySocket();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Console.WriteLine("Doing it old fashioned way....StartClient");
            StartClient("Ppipi");
        }



        private void createSocket()
        {
            if (sender == null)
            {
                Boolean socketConnectionSuccessful = false;
                Console.WriteLine("Creating new sender socket.");

                // Establish the remote endpoint for the socket.
                // This example uses port 11000 on the local computer.

                IPHostEntry ipHostInfo = Dns.Resolve(Dns.GetHostName());
                //IPAddress ipAddress = ipHostInfo.AddressList[0];
                IPAddress ipAddress = System.Net.IPAddress.Parse("127.0.0.1");
                IPEndPoint remoteEP = new IPEndPoint(ipAddress, 10001);

                // Create a TCP/IP  socket.
                sender = new Socket(AddressFamily.InterNetwork,
                    SocketType.Stream, ProtocolType.Tcp);
                // Connect the socket to the remote endpoint. Catch any errors.
                try
                {
                    sender.Connect(remoteEP);
                    _isConnectedToPython = true;
                    labelConnectedToPython.Text = Convert.ToString(_isConnectedToPython);
                    socketConnectionSuccessful = _isConnectedToPython;
                }
                catch (ArgumentNullException ane)
                {
                    Console.WriteLine("ArgumentNullException : {0}", ane.ToString());
                }
                catch (SocketException se)
                {
                    Console.WriteLine("SocketException : {0}", se.ToString());
                }
                catch (Exception e)
                {
                    Console.WriteLine("Unexpected exception : {0}", e.ToString());
                }

                if (!socketConnectionSuccessful)
                {
                    labelConnectedToPython.Text = "Error connecting.";
                }
            }
            else
            {
                Console.WriteLine("Sender socket not created since it already exists.");
            }
        }

        private void sendMessageAndDisplayResponse(String message)
        {

            if (sender != null)
            {
                // Data buffer for incoming data.
                byte[] bytes = new byte[1024];
                Console.WriteLine("Sending message: " + message);
                try
                {
                    Console.WriteLine("Socket connected to {0}",
                        sender.RemoteEndPoint.ToString());

                    // Encode the data string into a byte array.
                    //byte[] msg = Encoding.ASCII.GetBytes("This is a test<EOF>");
                    //byte[] msg = Encoding.ASCII.GetBytes("This is a test");
                    byte[] msg = Encoding.ASCII.GetBytes(message);

                    // Send the data through the socket.
                    int bytesSent = sender.Send(msg);

                    // Receive the response from the remote device.
                    int bytesRec = sender.Receive(bytes);
                    Console.WriteLine("Echoed test = {0}",
                        Encoding.ASCII.GetString(bytes, 0, bytesRec));

                }
                catch (ArgumentNullException ane)
                {
                    Console.WriteLine("ArgumentNullException : {0}", ane.ToString());
                }
                catch (SocketException se)
                {
                    Console.WriteLine("SocketException : {0}", se.ToString());
                }
                catch (Exception e)
                {
                    Console.WriteLine("Unexpected exception : {0}", e.ToString());
                }
            }
            else
            {
                Console.WriteLine("Socket not connected. Sending nothing.");
            }
        }


        private void sendMessageViaSocketToPython(String message)
        {

            if (sender != null)
            {
                // Data buffer for incoming data.
                byte[] bytes = new byte[91024];
                //Console.WriteLine("Sending message: " + message);
                try
                {
                    Console.WriteLine("Socket connected to {0}",sender.RemoteEndPoint.ToString());

                    // Encode the data string into a byte array.
                    //byte[] msg = Encoding.ASCII.GetBytes("This is a test<EOF>");
                    //byte[] msg = Encoding.ASCII.GetBytes("This is a test");
                    byte[] msg = Encoding.ASCII.GetBytes(message);

                    // Send the data through the socket.
                    int bytesSent = sender.Send(msg);

                    // Receive the response from the remote device.
                    int bytesRec = sender.Receive(bytes);
                    //Console.WriteLine("Echoed test = {0}",Encoding.ASCII.GetString(bytes, 0, bytesRec));
                    Console.WriteLine("Got response of " + bytesRec + " bytes long.");
                    sentPacketsToPython++;
                    sentPacketsToPythonInLastInterval++;
                    updateLabel(Convert.ToString(sentPacketsToPython), labelNoSentMessages);
                    //labelNoSentMessages.Text = Convert.ToString(sentPacketsToPython);

                }
                catch (ArgumentNullException ane)
                {
                    Console.WriteLine("ArgumentNullException : {0}", ane.ToString());
                }
                catch (SocketException se)
                {
                    Console.WriteLine("SocketException : {0}", se.ToString());
                }
                catch (Exception e)
                {
                    Console.WriteLine("Unexpected exception : {0}", e.ToString());
                }
            }
            else
            {
                Console.WriteLine("Socket not connected. Sending nothing.");
            }
        }

        private void destroySocket()
        {
            if (sender != null)
            {
                Console.WriteLine("Destroying socket sender.");
                // Release the socket.
                try {
                    _isConnectedToPython = false;
                    labelConnectedToPython.Text = Convert.ToString(_isConnectedToPython);
                    sender.Shutdown(SocketShutdown.Both);
                    sender.Close();
                    sender = null;
                }
                catch (ArgumentNullException ane)
                {
                    Console.WriteLine("ArgumentNullException : {0}", ane.ToString());
                }
                catch (SocketException se)
                {
                    Console.WriteLine("SocketException : {0}", se.ToString());
                }
                catch (Exception e)
                {
                    Console.WriteLine("Unexpected exception : {0}", e.ToString());
                }
            }
            else
            {
                Console.WriteLine("Sender socket == null. Doing nothing.");
            }
        }




        public static void StartClient(String message)
        {
            // Data buffer for incoming data.
            byte[] bytes = new byte[1024];

            // Connect to a remote device.
            try
            {
                // Establish the remote endpoint for the socket.
                // This example uses port 11000 on the local computer.

                IPHostEntry ipHostInfo = Dns.Resolve(Dns.GetHostName());
                //IPAddress ipAddress = ipHostInfo.AddressList[0];
                IPAddress ipAddress = System.Net.IPAddress.Parse("127.0.0.1");
                IPEndPoint remoteEP = new IPEndPoint(ipAddress, 10001);

                // Create a TCP/IP  socket.
                Socket sender = new Socket(AddressFamily.InterNetwork,
                    SocketType.Stream, ProtocolType.Tcp);

                // Connect the socket to the remote endpoint. Catch any errors.
                try
                {
                    sender.Connect(remoteEP);

                    Console.WriteLine("Socket connected to {0}",
                        sender.RemoteEndPoint.ToString());

                    // Encode the data string into a byte array.
                    //byte[] msg = Encoding.ASCII.GetBytes("This is a test<EOF>");
                    //byte[] msg = Encoding.ASCII.GetBytes("This is a test");
                    byte[] msg = Encoding.ASCII.GetBytes(message);

                    // Send the data through the socket.
                    int bytesSent = sender.Send(msg);

                    // Receive the response from the remote device.
                    int bytesRec = sender.Receive(bytes);
                    Console.WriteLine("Echoed test = {0}",
                        Encoding.ASCII.GetString(bytes, 0, bytesRec));

                    // Release the socket.
                    sender.Shutdown(SocketShutdown.Both);
                    sender.Close();

                }
                catch (ArgumentNullException ane)
                {
                    Console.WriteLine("ArgumentNullException : {0}", ane.ToString());
                }
                catch (SocketException se)
                {
                    Console.WriteLine("SocketException : {0}", se.ToString());
                }
                catch (Exception e)
                {
                    Console.WriteLine("Unexpected exception : {0}", e.ToString());
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        private void connectToFaceReader()
        {
            // if the FaceReaderController already exists, Dispose it. Note that Dispose also Disconnects
            if (mFaceReaderController != null)
            {
                mFaceReaderController.Dispose();
                mFaceReaderController = null;
                updateLabel("False", labelConnectedToNoldus);

            }

            // create a new instance of FaceReaderDataReceiver, with the ipaddress and the port
            //mFaceReaderController = new FaceReaderController(txtIPAdress.Text, (int)numPort.Value);
            Console.WriteLine("Connecting to FaceReader API using 127.0.0.1:9090");
            mFaceReaderController = new FaceReaderController("127.0.0.1", 9090);

            try
            {
                // register the events
                mFaceReaderController.ClassificationReceived +=
                    new EventHandler<ClassificationEventArgs>(a_faceReaderController_ClassificationReceived);

                mFaceReaderController.Disconnected +=
                    new EventHandler(a_faceReaderController_Disconnected);

                mFaceReaderController.Connected +=
                    new EventHandler(a_faceReaderController_Connected);

                mFaceReaderController.ActionSucceeded +=
                    new EventHandler<MessageEventArgs>(a_faceReaderController_ActionSucceeded);

                mFaceReaderController.ErrorOccured +=
                    new EventHandler<ErrorEventArgs>(a_faceReaderController_ErrorOccured);

                mFaceReaderController.AvailableStimuliReceived +=
                    new EventHandler<AvailableStimuliEventArgs>(a_faceReaderController_AvailableStimuliReceived);

                mFaceReaderController.AvailableEventMarkersReceived +=
                    new EventHandler<AvailableEventMarkersEventArgs>(a_faceReaderController_AvailableEventMarkersReceived);

                // connect to FaceReader. If the connection was succesful, Connected will fire, otherwise Disconnected will fire
                mFaceReaderController.ConnectToFaceReader();
                updateLabel("True", labelConnectedToNoldus);
            }
            catch (Exception ex)
            {
                //WriteInfo(rtbMessages, ex.Message);
                Console.WriteLine(ex.Message);
            }
            Console.WriteLine("Enabling detailed log");
            if (mFaceReaderController != null)
                mFaceReaderController.StartLogSending(LogType.DetailedLog);

        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            connectToFaceReader();
        }


        #region EventHandlers

        void a_faceReaderController_AvailableStimuliReceived(object sender, AvailableStimuliEventArgs e)
        {
            //WriteInfo(rtbMessages, "Stimuli received:\n" + ToMultilineString(e.Stimuli));
            MarkoWriteInfo("Stimuli received:\n" + ToMultilineString(e.Stimuli));

            // put in combobox
            //AddToCombobox(cmbStimuli, e.Stimuli);
        }

        void a_faceReaderController_AvailableEventMarkersReceived(object sender, AvailableEventMarkersEventArgs e)
        {
            //WriteInfo(rtbMessages, "Event Markers received:\n" + ToMultilineString(e.EventMarkers));
            MarkoWriteInfo("Event Markers received:\n" + ToMultilineString(e.EventMarkers));

            // put in combobox
            //AddToCombobox(cmbEventMarkers, e.EventMarkers);
        }

        void a_faceReaderController_ErrorOccured(object sender, ErrorEventArgs e)
        {
            //WriteInfo(rtbMessages, "Error occured\t-> " + e.Exception.Message);
            MarkoWriteInfo("Error occured\t-> " + e.Exception.Message);
        }

        void a_faceReaderController_ActionSucceeded(object sender, MessageEventArgs e)
        {
            //WriteInfo(rtbMessages, "Action Succeeded\t-> " + e.Message);
            MarkoWriteInfo("Action Succeeded\t-> " + e.Message);
        }

        void a_faceReaderController_Connected(object sender, EventArgs e)
        {
            //Console.WriteLine("Marko: Connected.");
            //SendSocketMessage("Marko: Connected.");
            //WriteInfo(rtbMessages, "Connection to FaceReader was succesfull");
            MarkoWriteInfo("Connection to FaceReader was succesfull");
        }

        void a_faceReaderController_Disconnected(object sender, EventArgs e)
        {
            //Console.WriteLine("Marko: Disconnected");
            //WriteInfo(rtbMessages, "Disconnected");
            MarkoWriteInfo("Disconnected");
        }









        #region label updating in thread
        // see https://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k%28EHInvalidOperation.WinForms.IllegalCrossThreadCall%29;k%28TargetFrameworkMoniker-.NETFramework,Version%3Dv4.5.2%29;k%28DevLang-csharp%29&rd=true
        private void labelUpdaterThreadProcSafe()
        {
            //Console.WriteLine("updating label.");
            //this.SetText("This text was set safely.");
            this.SetText(text2Update, this.label2update);
        }

  

        private void SetText(string text, Label label2update )
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (label2update.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetText);
                this.Invoke(d, new object[] { text , label2update});
            }
            else
            {
                label2update.Text = text;
            }
        }

        /**
        Call this method to update label in thread.
        */
        private void updateLabel(String text, Label label)
        {
            //Console.WriteLine("Trying to update label in thread");
            this.label2update = label;
            this.text2Update = text;
            this.labelReceivedInSessionUpdaterThread = new Thread(new ThreadStart(this.labelUpdaterThreadProcSafe));
            this.labelReceivedInSessionUpdaterThread.Start();
        }
        #endregion

        void a_faceReaderController_ClassificationReceived(object sender, ClassificationEventArgs e)
        {
            //Console.WriteLine("Marko: Classification received");
            //Console.Write("C");
            // get the classification from the event arguments
            FaceReaderAPI.Data.Classification classification = e.Classification;

            // if a classification was received
            if (classification != null)
            {

                receivedInSession++;
                // tu je sesulo https://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k%28EHInvalidOperation.WinForms.IllegalCrossThreadCall%29;k%28TargetFrameworkMoniker-.NETFramework,Version%3Dv4.5.2%29;k%28DevLang-csharp%29&rd=true

                //labelReceivedInSession.Text = receivedInSession.ToString();
                //this.backgroundWorker1.RunWorkerAsync();


                Console.WriteLine(receivedInSession.ToString());
                /**
                this.labelReceivedInSessionUpdaterThread = new Thread(new ThreadStart(this.ThreadProcSafe));
                this.labelReceivedInSessionUpdaterThread.Start();
                **/
                //updateLabel("Hell yeah!", labelReceivedInSession);
                updateLabel(receivedInSession.ToString(), labelReceivedInSession);



                receivedInSessionInLastInterval++;

                String timeStamp = (DateTime.Now).ToString("dd.MM.yyyy HH:mm:ss.ffff");
                updateLabel(timeStamp, labelTimeStamp);
                //labelTimeStamp.Text = timeStamp;



                TimeSpan ts = DateTime.Now - interval;
                if (ts.Seconds > observingIntervalSec)
                {
                    double messagesPerSecond = sentPacketsToPythonInLastInterval / observingIntervalSec;
                    interval = DateTime.Now;
                    sentPacketsToPythonInLastInterval = 0;
                    updateLabel(messagesPerSecond.ToString(), labelMessagesPerSecond);
                    /*try {
                        //labelMessagesPerSecond.Text = messagesPerSecond.ToString();
                        Console.WriteLine(messagesPerSecond.ToString());

                    }catch(Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }*/
                    double samplesPerSecond = receivedInSessionInLastInterval / observingIntervalSec;
                    receivedInSessionInLastInterval = 0;
                    updateLabel(samplesPerSecond.ToString(), labelSampelsPerSecond);
                    /*
                    try {
                        //labelSampelsPerSecond.Text = samplesPerSecond.ToString();
                        Console.WriteLine(samplesPerSecond.ToString());
                    } catch (Exception ex)
                    {
                        Console.WriteLine(ex.StackTrace);
                    }*/
                    
                }


                // if the classification is in the form of a StateLogs
                if (classification.LogType == FaceReaderAPI.Data.LogType.StateLog)
                {
                    // show the information
                    //WriteInfo(rtbStateClassification, classification.ToString());
                    Console.WriteLine("1:Sending data to python.");
                    sendMessageViaSocketToPython(classification.ToString());
                    //MarkoWriteInfo(classification.ToString());
                    //Console.WriteLine(classification.ToString());
                    //SendSocketMessage(classification.ToString());
                }
                // if the classification is in the form of a DetailedLog
                else
                {
                    // show the information
                    //WriteInfo(rtbDetailedClassification, classification.ToString());
                    //MarkoWriteInfo(classification.ToString());
                    Console.WriteLine("1:Sending data to python.");
                    sendMessageViaSocketToPython(classification.ToString());
                    //Console.WriteLine(classification.ToString());
                }
            }
        }

        #endregion


        #region HelperFunctions

        /// <summary>
        /// Helper function to write information to a RichTextBox, in backwards order, with maximum of
        /// 100 lines
        /// </summary>
        /// <param name="rtb"></param>
        /// <param name="info"></param>
        //private void WriteInfo(RichTextBox rtb, string info)
        private void MarkoWriteInfo(string info)
        {
            Console.WriteLine(info);
            sendMessageViaSocketToPython(info);
            /*
            if (rtb.InvokeRequired)
            {
                GuiCallback<RichTextBox, string> callback = new GuiCallback<RichTextBox, string>(WriteInfo);
                rtbDetailedClassification.Invoke(callback, rtb, info);
            }
            else
            {
                rtb.Text = info + "\n" + rtb.Text;

                while (rtb.Lines.Length > 100)
                {
                    string[] lines = new string[rtb.Lines.Length - 1];
                    Array.Copy(rtb.Lines, 0, lines, 0, lines.Length);

                    rtb.Lines = lines;
                }
            }*/
        }

        private string ToMultilineString(string[] strArray)
        {
            string txt = "";
            foreach (string s in strArray)
                txt += s + "\n";

            return txt;
        }


        #endregion

        private void btnEnableStateLogs_Click(object sender, EventArgs e)
        {
            if (mFaceReaderController != null)
                mFaceReaderController.StartLogSending(LogType.StateLog);
        }

        private void btnDisconnect_Click(object sender, EventArgs e)
        {
            Console.WriteLine("Disconnect FaceReader");
            if (mFaceReaderController != null)
            {
                // if there is a connection, disconnect
                if (mFaceReaderController.IsConnected)
                    mFaceReaderController.DisconnectFromFaceReader();
                else
                    //WriteInfo(rtbMessages, "There is no connection to disconnect");
                    MarkoWriteInfo("There is no connection to disconnect");
            }
            updateLabel("False", labelConnectedToNoldus);


        }

        private void btnEnableDetailedLogs_Click(object sender, EventArgs e)
        {
            if (mFaceReaderController != null)
                mFaceReaderController.StartLogSending(LogType.DetailedLog);
        }

        private void btnStartAnalysis_Click(object sender, EventArgs e)
        {
            if (mFaceReaderController != null)
                mFaceReaderController.StartAnalyzing();
        }

        private void btnStopAnalysis_Click(object sender, EventArgs e)
        {
            if (mFaceReaderController != null)
                mFaceReaderController.StopAnalyzing();
        }

        private void labelMessagesPerSecond_Click(object sender, EventArgs e)
        {

        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            labelAbout.Text = "LUCAMI Noldus FaceReader 2 Python via socket application. Version v0.3 2016-02-05. Author: Marko Meža";
        }



        private void autostartAll()
        {
            Console.WriteLine("Autostarting all.");
            Console.WriteLine("Creating socket.");
            createSocket();

            Console.WriteLine("Connecting to FaceReader");
            connectToFaceReader();

            Console.WriteLine("Starting analysis.");

            if (mFaceReaderController != null)
                mFaceReaderController.StartAnalyzing();

        }

        private void buttonAutoStart_Click(object sender, EventArgs e)
        {
            autostartAll();
        }
    }


}
