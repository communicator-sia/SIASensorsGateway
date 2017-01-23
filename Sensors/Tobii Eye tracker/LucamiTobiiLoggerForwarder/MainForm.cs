using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using Tobii.EyeTracking.IO;

namespace BasicEyetrackingSample
{
    public partial class MainForm : Form
    {
        private readonly EyeTrackerBrowser _trackerBrowser;
        private readonly Clock _clock;

        private IEyeTracker _connectedTracker;
        private ISyncManager _syncManager;
        private string _connectionName;
        private bool _isTracking;

        private bool _isConnectedToPython = false;
        private int sentPacketsToPython = 0;

        private int receivedInSession = 0;
        private int receivedInSessionInLastInterval = 0;

        private int sentPacketsToPythonInLastInterval = 0;
        DateTime interval = DateTime.Now;
        DateTime observingStart;

        int observingIntervalSec = 3;


        private Point3D _leftPos;
        private Point3D _rightPos;
        private Point3D _leftGaze;
        private Point3D _rightGaze;
        private float _leftPupilDiameter;
        private float _rightPupilDiameter;

        Socket sender = null;

        private String csvData;

        private Boolean autostart = false;

        public MainForm(Boolean autostart)
        {
            this.autostart = autostart;
            InitializeComponent();

            _clock = new Clock();

            _trackerBrowser = new EyeTrackerBrowser();
            _trackerBrowser.EyeTrackerFound += EyetrackerFound;
            _trackerBrowser.EyeTrackerUpdated += EyetrackerUpdated;
            _trackerBrowser.EyeTrackerRemoved += EyetrackerRemoved;
        }


        private void EyetrackerFound(object sender, EyeTrackerInfoEventArgs e)
        {
            // When an eyetracker is found on the network we add it to the listview
            var trackerItem = CreateTrackerListViewItem(e.EyeTrackerInfo);
            _trackerList.Items.Add(trackerItem);
            UpdateUIElements();
            //autostart = true;
            if (autostart)
            {
                autostartAll();
            }
        }

        private void EyetrackerRemoved(object sender, EyeTrackerInfoEventArgs e)
        {
            // When an eyetracker disappears from the network we remove it from the listview
            _trackerList.Items.RemoveByKey(e.EyeTrackerInfo.ProductId);
            UpdateUIElements();
        }

        private void EyetrackerUpdated(object sender, EyeTrackerInfoEventArgs e)
        {
            // When an eyetracker is updated we simply create a new 
            // listviewitem and replace the old one
            int index = _trackerList.Items.IndexOfKey(e.EyeTrackerInfo.ProductId);
            if(index >= 0)
            {
                _trackerList.Items[index] = CreateTrackerListViewItem(e.EyeTrackerInfo);
            }
        }


        private static ListViewItem CreateTrackerListViewItem(EyeTrackerInfo info)
        {
            var trackerItem = new ListViewItem(info.ProductId);
            trackerItem.Name = info.ProductId;

            var sb = new StringBuilder();
            sb.AppendLine("Model: " + info.Model);
            sb.AppendLine("Status: " + info.Status);
            sb.AppendLine("Generation: " + info.Generation);
            sb.AppendLine("Product Id: " + info.ProductId);
            sb.AppendLine("Given Name: " + info.GivenName);
            sb.AppendLine("Firmware Version: " + info.Version);

            trackerItem.ToolTipText = sb.ToString();
            trackerItem.Tag = info;

            return trackerItem;
        }

        private ListViewItem GetSelectedItem()
        {
            if (_trackerList.SelectedItems.Count == 1)
            {
                return _trackerList.SelectedItems[0];
            }
            return null;
        }

        private ListViewItem selectFirstItem()
        {
            if (_trackerList.Items.Count > 0)
            {
                _trackerList.Items[0].Selected = true;
                return GetSelectedItem();
            }
            return null;
        }

        private void UpdateUIElements()
        {
            var selectedItemInfo = GetSelectedItem();

            if(selectedItemInfo != null)
            {
                _trackerInfoLabel.Text = selectedItemInfo.ToolTipText;
                _connectButton.Enabled = true;
            }
            else
            {
                _trackerInfoLabel.ResetText();
                _connectButton.Enabled = false;
            }

            if(_connectedTracker !=  null)
            {
                _connectionStatusLabel.Text = "Connected to " + _connectionName;
                _trackButton.Enabled = true;
                _calibrateButton.Enabled = true;
                _loadCalibrationMenuItem.Enabled = true;
                _saveCalibrationMenuItem.Enabled = true;
                _viewCalibrationMenuItem.Enabled = true;
                _framerateMenuItem.Enabled = true;
            }
            else
            {
                _connectionStatusLabel.Text = "Disconnected";
                _trackButton.Enabled = false;
                _calibrateButton.Enabled = false;
                _loadCalibrationMenuItem.Enabled = false;
                _saveCalibrationMenuItem.Enabled = false;
                _viewCalibrationMenuItem.Enabled = false;
                _framerateMenuItem.Enabled = false;
            }

            if(_isTracking)
            {
                _trackButton.Text = "Stop Tracking";
                _trackStatus.Enabled = true;
            }
            else
            {
                _trackButton.Text = "Start Tracking";
                _trackStatus.Enabled = false;
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            // Start browsing for eyetrackers on the network
            _trackerBrowser.StartBrowsing();
            UpdateUIElements();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Shutdown browser service
            _trackerBrowser.StopBrowsing();

            // Cleanup connections
            DisconnectTracker();
        }

        private void _trackerList_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateUIElements();
        }

        private void _connectButton_Click(object sender, EventArgs e)
        {
            // Disconnect existing connection
            DisconnectTracker();

            // Create new connection
            var selectedItem = GetSelectedItem();
            if(selectedItem != null)
            {
                var info = (EyeTrackerInfo) selectedItem.Tag;
                ConnectToTracker(info);    
            }
            UpdateUIElements();
        }

        private void ConnectToTracker(EyeTrackerInfo info)
        {
            try
            {
                _connectedTracker = info.Factory.CreateEyeTracker();
                _connectedTracker.ConnectionError += HandleConnectionError;
                _connectionName = info.ProductId;

                _syncManager = info.Factory.CreateSyncManager(_clock);

                _connectedTracker.GazeDataReceived += _connectedTracker_GazeDataReceived;
                _connectedTracker.FrameRateChanged += _connectedTracker_FrameRateChanged;
            }
            catch (EyeTrackerException ee)
            {
                if(ee.ErrorCode == 0x20000402)
                {
                    MessageBox.Show("Failed to upgrade protocol. " + 
                        "This probably means that the firmware needs" +
                        " to be upgraded to a version that supports the new sdk.","Upgrade Failed",MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show("Eyetracker responded with error " + ee, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);    
                }

                DisconnectTracker();
            }
            catch(Exception)
            {
                MessageBox.Show("Could not connect to eyetracker.","Connection Failed",MessageBoxButtons.OK,MessageBoxIcon.Error);
                DisconnectTracker();
            }

            UpdateUIElements();
            
        }

        private void _connectedTracker_GazeDataReceived(object sender, GazeDataEventArgs e)
        {
            //added by marko
            //Console.WriteLine("Marko Data.");

            receivedInSession++;
            labelReceivedInSession.Text = receivedInSession.ToString();
            receivedInSessionInLastInterval++;
            
            // Convert to centimeters

            const double D = 10.0;

            _leftPos.X = e.GazeDataItem.LeftEyePosition3D.X / D;
            _leftPos.Y = e.GazeDataItem.LeftEyePosition3D.Y / D;
            _leftPos.Z = e.GazeDataItem.LeftEyePosition3D.Z / D;

            _rightPos.X = e.GazeDataItem.RightEyePosition3D.X / D;
            _rightPos.Y = e.GazeDataItem.RightEyePosition3D.Y / D;
            _rightPos.Z = e.GazeDataItem.RightEyePosition3D.Z / D;

            _leftGaze.X = e.GazeDataItem.LeftGazePoint3D.X / D;
            _leftGaze.Y = e.GazeDataItem.LeftGazePoint3D.Y / D;
            _leftGaze.Z = e.GazeDataItem.LeftGazePoint3D.Z / D;

            _rightGaze.X = e.GazeDataItem.RightGazePoint3D.X / D;
            _rightGaze.Y = e.GazeDataItem.RightGazePoint3D.Y / D;
            _rightGaze.Z = e.GazeDataItem.RightGazePoint3D.Z / D;

            _leftPupilDiameter = e.GazeDataItem.LeftPupilDiameter;
            _rightPupilDiameter = e.GazeDataItem.RightPupilDiameter;



            String coordinateFormat = "{0:00.00}";

            labelLeftPos.Text = "X=" + String.Format(coordinateFormat, _leftPos.X) + " Y=" + String.Format(coordinateFormat, _leftPos.Y) + " Z=" + String.Format(coordinateFormat, _leftPos.Z);
            labelRightPos.Text = "X=" + String.Format(coordinateFormat, _rightPos.X) + " Y=" + String.Format(coordinateFormat, _rightPos.Y) + " Z=" + String.Format(coordinateFormat, _rightPos.Z);

            labelLeftGaze.Text = "X=" + String.Format(coordinateFormat, _leftGaze.X) + " Y=" + String.Format(coordinateFormat, _leftGaze.Y) + " Z=" + String.Format(coordinateFormat, _leftGaze.Z);
            labelRightGaze.Text = "X=" + String.Format(coordinateFormat, _rightGaze.X) + " Y=" + String.Format(coordinateFormat, _rightGaze.Y) + " Z=" + String.Format(coordinateFormat, _rightGaze.Z);

            labelLeftPupilDiameter.Text = String.Format(coordinateFormat, _leftPupilDiameter);
            labelRightPupilDiameter.Text = String.Format(coordinateFormat, _rightPupilDiameter);
            String timeStamp = (DateTime.Now).ToString("dd.MM.yyyy HH:mm:ss.ffff");
            labelTimeStamp.Text = timeStamp;

            TimeSpan ts = DateTime.Now - interval;
            if (ts.Seconds > observingIntervalSec)
            {
                double messagesPerSecond = sentPacketsToPythonInLastInterval / observingIntervalSec;
                interval = DateTime.Now;
                sentPacketsToPythonInLastInterval = 0;
                labelMessagesPerSecond.Text = messagesPerSecond.ToString();

                double samplesPerSecond = receivedInSessionInLastInterval / observingIntervalSec;
                receivedInSessionInLastInterval = 0;
                labelSampelsPerSecond.Text = samplesPerSecond.ToString();
            }

            //String formattedData = " \"a\" ";
            
            String formattedData = "{" +
                "\"tobiiEyeTracker\":{" +
                    "\"timeStamp\":\"" + timeStamp + "\"" +
                    "," +
                    "\"leftPos\":{" +
                        "\"x\":\"" + _leftPos.X + "\"," +
                        "\"y\":\"" + _leftPos.Y + "\"," +
                        "\"z\":\"" + _leftPos.Z + "\"" +
                    "}," +
                    "\"rightPos\":{" +
                        "\"x\":\"" + _rightPos.X + "\"," +
                        "\"y\":\"" + _rightPos.Y + "\"," +
                        "\"z\":\"" + _rightPos.Z + "\"" +
                    "}," +
                    "\"leftGaze\":{" +
                        "\"x\":\"" + _leftGaze.X + "\"," +
                        "\"y\":\"" + _leftGaze.Y + "\"," +
                        "\"z\":\"" + _leftGaze.Z + "\"" +
                    "}," +
                    "\"rightGaze\":{" +
                        "\"x\":\"" + _rightGaze.X + "\"," +
                        "\"y\":\"" + _rightGaze.Y + "\"," +
                        "\"z\":\"" + _rightGaze.Z + "\"" +
                    "}" +
                    "," +
                    "\"leftPupilDiameter\":\"" + _leftPupilDiameter + "\"" +
                    "," +
                    "\"rightPupilDiameter\":\"" + _rightPupilDiameter + "\"" +
                "}" +
                "}";
            Console.Write(formattedData);
            sendMessageViaSocketToPython(formattedData);

            //"timeStamp, leftPos.X, leftPos.Y, leftPos.Z, rightPos.X, rightPos.Y, rightPos.Z, leftGaze.X, leftGaze.Y, leftGaze.Z, rightGaze.X, rightGaze.Y, rightGaze.Z";

            csvData = csvData + "\n" +
                timeStamp + ";" + _leftPos.X + ";" + _leftPos.Y + ";" + _leftPos.Z + ";" + _rightPos.X + ";" + _rightPos.Y + ";" + _rightPos.Z + ";" + _leftGaze.X + ";" + _leftGaze.Y + ";" + _leftGaze.Z + ";" + _rightGaze.X + ";" + _rightGaze.Y + ";" + _rightGaze.Z + ";" + _leftPupilDiameter + ";" + _rightPupilDiameter;



            // end added by marko





            int trigSignal;
            if (e.GazeDataItem.TryGetExtensionValue(IntegerExtensionValue.TrigSignal, out trigSignal))
            {
                Console.WriteLine(string.Format("Trig signal: {0}", trigSignal));
            }

            // Send the gaze data to the track status control.
            var gd = e.GazeDataItem;

            _trackStatus.OnGazeData(gd);

            if (_syncManager.CurrentSyncState.Status == SyncStatus.Synchronized)
            {
                Int64 convertedTime = _syncManager.RemoteToLocal(gd.Timestamp);
                Int64 localTime = _clock.Time;
            }
            else
            {
                Console.WriteLine("Warning. Sync state is " + _syncManager.CurrentSyncState.Status);
            }
        }


        private static void _connectedTracker_FrameRateChanged(object sender, FrameRateChangedEventArgs e)
        {
            Console.WriteLine("FrameRate changed " + e.FrameRate);
        }

        private void HandleConnectionError(object sender, ConnectionErrorEventArgs e)
        {
            // If the connection goes down we dispose 
            // the IAsyncEyetracker instance. This will release 
            // all resources held by the connection
            DisconnectTracker();
            UpdateUIElements();
        }

        private void DisconnectTracker()
        {
            if(_connectedTracker != null)
            {
                _connectedTracker.GazeDataReceived -= _connectedTracker_GazeDataReceived;
                _connectedTracker.Dispose();
                _connectedTracker = null;
                _connectionName = string.Empty;
                _isTracking = false;

                _syncManager.Dispose();
            }
        }

        private void _trackButton_Click(object sender, EventArgs e)
        {
            if(_isTracking)
            {
                // Unsubscribe from gaze data stream
                _connectedTracker.StopTracking();
                _isTracking = false;
                saveCsvData();
            }
            else
            {
                // Start subscribing to gaze data stream
                _connectedTracker.StartTracking();
                _isTracking = true;
                initCsvStorage();
            }
            UpdateUIElements();
        }

        private void _calibrateButton_Click(object sender, EventArgs e)
        {
            var runner = new CalibrationRunner();
            
            try
            {
                // Start a new calibration procedure
                var result = runner.RunCalibration(_connectedTracker);

                // Show a calibration plot if everything went OK
                if (result != null)
                {
                    var resultForm = new CalibrationResultForm();
                    resultForm.SetPlotData(result);
                    resultForm.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Not enough data to create a calibration (or calibration aborted).");
                }                
            }
            catch(EyeTrackerException ee)
            {
                MessageBox.Show("Failed to calibrate. Got exception " + ee, 
                    "Calibration Failed", 
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }            
        }

        private void _saveCalibrationMenuItem_Click(object sender, EventArgs e)
        {
            if(_saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    var calibration = _connectedTracker.GetCalibration();
                    
                    using(var stream = _saveFileDialog.OpenFile())
                    using(var writer = new BinaryWriter(stream))
                    {
                        writer.Write(calibration.RawData);
                    }
                }
                catch (EyeTrackerException ee)
                {
                    MessageBox.Show("Failed to get calibration data. Got exception " + ee,
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }    
            }
        }

        private void _viewCalibrationMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                var calibration =_connectedTracker.GetCalibration();
                var resultForm = new CalibrationResultForm();
                resultForm.SetPlotData(calibration);
                resultForm.ShowDialog();
            }
            catch(EyeTrackerException ee)
            {
                MessageBox.Show("Failed to get calibration data. Got exception " + ee,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void _loadCalibrationMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (_openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    using (var stream = _openFileDialog.OpenFile())
                    using (var reader = new BinaryReader(stream))
                    {
                        byte[] data = reader.ReadBytes((int)stream.Length);
                        Calibration calibration = new Calibration(data);
                        _connectedTracker.SetCalibration(calibration);
                    }
                }
            }
            catch (EyeTrackerException ee)
            {
                MessageBox.Show("Failed to load calibration data. Got exception " + ee,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            
        }

        private void _framerateMenuItem_Click(object sender, EventArgs e)
        {
            var framerate = _connectedTracker.GetFrameRate();
            var availableFrameRates = _connectedTracker.EnumerateFrameRates();

            int fpsIndex = availableFrameRates.IndexOf(framerate);

            FrameRateDialog fpsDialog = new FrameRateDialog(availableFrameRates,fpsIndex);

            if(fpsDialog.ShowDialog() == DialogResult.OK)
            {
                _connectedTracker.SetFrameRate(fpsDialog.CurrentFrameRate);
            }
        }



        #region csvSaving
        private void initCsvStorage()
        {
            csvData = "timeStamp;leftPos.X;leftPos.Y;leftPos.Z;rightPos.X;rightPos.Y;rightPos.Z;leftGaze.X;leftGaze.Y;leftGaze.Z;rightGaze.X;rightGaze.Y;rightGaze.Z;leftPupilDiameter;rightPupilDiameter";
            observingStart = DateTime.Now;
            labelOservingStart.Text=observingStart.ToString("yyyyMMddHHmmss");
            receivedInSession = 0;
            receivedInSessionInLastInterval = 0;
            labelObservigDataFilename.Text = "Stop tracking to save observing data to file.";
    }
        #endregion

        #region Python app communication

        private void createSocket()
        {
            if (sender == null)
            {
                Console.WriteLine("Creating new sender socket.");

                // Establish the remote endpoint for the socket.
                // This example uses port 11000 on the local computer.

                IPHostEntry ipHostInfo = Dns.Resolve(Dns.GetHostName());
                //IPAddress ipAddress = ipHostInfo.AddressList[0];
                IPAddress ipAddress = System.Net.IPAddress.Parse("127.0.0.1");
                IPEndPoint remoteEP = new IPEndPoint(ipAddress, 10003);

                // Create a TCP/IP  socket.
                sender = new Socket(AddressFamily.InterNetwork,
                    SocketType.Stream, ProtocolType.Tcp);
                // Connect the socket to the remote endpoint. Catch any errors.
                try
                {
                    sender.Connect(remoteEP);
                    _isConnectedToPython = true;
                    labelConnectedToPython.Text = Convert.ToString(_isConnectedToPython);
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
                Console.WriteLine("Sender socket not created since it already exists.");
            }
        }

        private void destroySocket()
        {
            if (sender != null)
            {
                Console.WriteLine("Destroying socket sender.");
                // Release the socket.
                try
                {
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

        private void sendMessageViaSocketToPython(String message)
        {

            if (sender != null)
            {
                // Data buffer for incoming data.s
                byte[] bytes = new byte[91024];
                //Console.WriteLine("Sending message: " + message);
                try
                {
                    Console.WriteLine("Socket connected to {0}", sender.RemoteEndPoint.ToString());

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
                    labelNoSentMessages.Text = Convert.ToString(sentPacketsToPython);

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



        #endregion

        private void buttonConnectSocket_Click(object sender, EventArgs e)
        {
            Console.WriteLine("Connect socket.");
            createSocket();
        }

        private void buttonDisconnectSocket_Click(object sender, EventArgs e)
        {
            Console.WriteLine("Disconnect socket.");
            destroySocket();
        }

        private void saveCsvData()
        {            
            DateTime observingStop = DateTime.Now;            
            String fileName = "tobiiTrackFrom_"+observingStart.ToString("yyyyMMddHHmmss")+"_to_"+ observingStop.ToString("yyyyMMddHHmmss")+".csv";
            Console.WriteLine("Saving data to "+fileName);        
            StreamWriter outputFile = new StreamWriter(fileName) ;
            outputFile.Write(csvData);
            outputFile.Close();
            csvData = "";
            Console.WriteLine("Saving data finished.");
            labelObservigDataFilename.Text = fileName;
            labelOservingStart.Text = "Not running";

        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            labelAbout.Text = "Lucami tobii eye tracker event logger and forwarder. Version 0.3 /autostart. 5. 2. 2016. Marko Meza.";
        }

        private void autostartAll()
        {
            Console.WriteLine("Autostarting all.");
            Console.WriteLine("Creating socket.");
            createSocket();

            Console.WriteLine("Connecting to tracker.");
            // Disconnect existing connection
            DisconnectTracker();

            // Create new connection
            ListViewItem selectedItem = null;
            while(selectedItem == null) { 
                selectedItem = selectFirstItem();
            }
            if (selectedItem != null)
            {
                var info = (EyeTrackerInfo)selectedItem.Tag;
                ConnectToTracker(info);
            }
            UpdateUIElements();

            Console.WriteLine("Starting tracking.");
            if (_isTracking)
            {
                // Unsubscribe from gaze data stream
                _connectedTracker.StopTracking();
                _isTracking = false;
                saveCsvData();
            }
            else
            {
                // Start subscribing to gaze data stream
                _connectedTracker.StartTracking();
                _isTracking = true;
                initCsvStorage();
            }
            UpdateUIElements();



        }


    }
}
