namespace BasicEyetrackingSample
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this._box1 = new System.Windows.Forms.GroupBox();
            this._connectButton = new System.Windows.Forms.Button();
            this._trackerInfoLabel = new System.Windows.Forms.Label();
            this._trackerList = new System.Windows.Forms.ListView();
            this._statusStrip = new System.Windows.Forms.StatusStrip();
            this._connectionStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this._box2 = new System.Windows.Forms.GroupBox();
            this._calibrateButton = new System.Windows.Forms.Button();
            this._trackButton = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._saveCalibrationMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._viewCalibrationMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._loadCalibrationMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.propertiesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._framerateMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this._saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.labelRightPupilDiameter = new System.Windows.Forms.Label();
            this.labelLeftPupilDiameter = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.labelMessagesPerSecond = new System.Windows.Forms.Label();
            this.labelRightGaze = new System.Windows.Forms.Label();
            this.labelLeftGaze = new System.Windows.Forms.Label();
            this.labelRightPos = new System.Windows.Forms.Label();
            this.labelLeftPos = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.labelTimeStamp = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.labelNoSentMessages = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.labelConnectedToPython = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonDisconnectSocket = new System.Windows.Forms.Button();
            this.buttonConnectSocket = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.labelSampelsPerSecond = new System.Windows.Forms.Label();
            this.labelReceivedInSession = new System.Windows.Forms.Label();
            this.labelOservingStart = new System.Windows.Forms.Label();
            this.labelObservigDataFilename = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.labelAbout = new System.Windows.Forms.Label();
            this._trackStatus = new BasicEyetrackingSample.TrackStatusControl();
            this._box1.SuspendLayout();
            this._statusStrip.SuspendLayout();
            this._box2.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // _box1
            // 
            this._box1.Controls.Add(this._connectButton);
            this._box1.Controls.Add(this._trackerInfoLabel);
            this._box1.Controls.Add(this._trackerList);
            this._box1.Location = new System.Drawing.Point(13, 38);
            this._box1.Name = "_box1";
            this._box1.Size = new System.Drawing.Size(237, 373);
            this._box1.TabIndex = 1;
            this._box1.TabStop = false;
            this._box1.Text = "Eyetrackers Found on the  Network";
            // 
            // _connectButton
            // 
            this._connectButton.Enabled = false;
            this._connectButton.Location = new System.Drawing.Point(45, 334);
            this._connectButton.Name = "_connectButton";
            this._connectButton.Size = new System.Drawing.Size(142, 27);
            this._connectButton.TabIndex = 2;
            this._connectButton.Text = "Connect to Eyetracker";
            this._connectButton.UseVisualStyleBackColor = true;
            this._connectButton.Click += new System.EventHandler(this._connectButton_Click);
            // 
            // _trackerInfoLabel
            // 
            this._trackerInfoLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this._trackerInfoLabel.Location = new System.Drawing.Point(19, 225);
            this._trackerInfoLabel.Name = "_trackerInfoLabel";
            this._trackerInfoLabel.Size = new System.Drawing.Size(196, 95);
            this._trackerInfoLabel.TabIndex = 1;
            // 
            // _trackerList
            // 
            this._trackerList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this._trackerList.Location = new System.Drawing.Point(19, 33);
            this._trackerList.MultiSelect = false;
            this._trackerList.Name = "_trackerList";
            this._trackerList.ShowItemToolTips = true;
            this._trackerList.Size = new System.Drawing.Size(196, 179);
            this._trackerList.TabIndex = 0;
            this._trackerList.UseCompatibleStateImageBehavior = false;
            this._trackerList.View = System.Windows.Forms.View.SmallIcon;
            this._trackerList.SelectedIndexChanged += new System.EventHandler(this._trackerList_SelectedIndexChanged);
            // 
            // _statusStrip
            // 
            this._statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._connectionStatusLabel});
            this._statusStrip.Location = new System.Drawing.Point(0, 466);
            this._statusStrip.Name = "_statusStrip";
            this._statusStrip.Size = new System.Drawing.Size(1248, 22);
            this._statusStrip.TabIndex = 2;
            this._statusStrip.Text = "statusStrip1";
            // 
            // _connectionStatusLabel
            // 
            this._connectionStatusLabel.Name = "_connectionStatusLabel";
            this._connectionStatusLabel.Size = new System.Drawing.Size(79, 17);
            this._connectionStatusLabel.Text = "Disconnected";
            // 
            // _box2
            // 
            this._box2.Controls.Add(this._calibrateButton);
            this._box2.Controls.Add(this._trackStatus);
            this._box2.Controls.Add(this._trackButton);
            this._box2.Location = new System.Drawing.Point(256, 38);
            this._box2.Name = "_box2";
            this._box2.Size = new System.Drawing.Size(395, 373);
            this._box2.TabIndex = 3;
            this._box2.TabStop = false;
            this._box2.Text = "Eyetracker Status";
            // 
            // _calibrateButton
            // 
            this._calibrateButton.Location = new System.Drawing.Point(197, 334);
            this._calibrateButton.Name = "_calibrateButton";
            this._calibrateButton.Size = new System.Drawing.Size(111, 27);
            this._calibrateButton.TabIndex = 2;
            this._calibrateButton.Text = "Run Calibration";
            this._calibrateButton.UseVisualStyleBackColor = true;
            this._calibrateButton.Click += new System.EventHandler(this._calibrateButton_Click);
            // 
            // _trackButton
            // 
            this._trackButton.Location = new System.Drawing.Point(74, 334);
            this._trackButton.Name = "_trackButton";
            this._trackButton.Size = new System.Drawing.Size(111, 27);
            this._trackButton.TabIndex = 0;
            this._trackButton.Text = "Start Tracking";
            this._trackButton.UseVisualStyleBackColor = true;
            this._trackButton.Click += new System.EventHandler(this._trackButton_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.propertiesToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1248, 24);
            this.menuStrip1.TabIndex = 4;
            this.menuStrip1.Text = "_menuStrip";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._saveCalibrationMenuItem,
            this._viewCalibrationMenuItem,
            this._loadCalibrationMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // _saveCalibrationMenuItem
            // 
            this._saveCalibrationMenuItem.Name = "_saveCalibrationMenuItem";
            this._saveCalibrationMenuItem.Size = new System.Drawing.Size(161, 22);
            this._saveCalibrationMenuItem.Text = "Save Calibration";
            this._saveCalibrationMenuItem.Click += new System.EventHandler(this._saveCalibrationMenuItem_Click);
            // 
            // _viewCalibrationMenuItem
            // 
            this._viewCalibrationMenuItem.Name = "_viewCalibrationMenuItem";
            this._viewCalibrationMenuItem.Size = new System.Drawing.Size(161, 22);
            this._viewCalibrationMenuItem.Text = "View Calibration";
            this._viewCalibrationMenuItem.Click += new System.EventHandler(this._viewCalibrationMenuItem_Click);
            // 
            // _loadCalibrationMenuItem
            // 
            this._loadCalibrationMenuItem.Name = "_loadCalibrationMenuItem";
            this._loadCalibrationMenuItem.Size = new System.Drawing.Size(161, 22);
            this._loadCalibrationMenuItem.Text = "Load Calibration";
            this._loadCalibrationMenuItem.Click += new System.EventHandler(this._loadCalibrationMenuItem_Click);
            // 
            // propertiesToolStripMenuItem
            // 
            this.propertiesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._framerateMenuItem});
            this.propertiesToolStripMenuItem.Name = "propertiesToolStripMenuItem";
            this.propertiesToolStripMenuItem.Size = new System.Drawing.Size(72, 20);
            this.propertiesToolStripMenuItem.Text = "Properties";
            // 
            // _framerateMenuItem
            // 
            this._framerateMenuItem.Name = "_framerateMenuItem";
            this._framerateMenuItem.Size = new System.Drawing.Size(139, 22);
            this._framerateMenuItem.Text = "FrameRate...";
            this._framerateMenuItem.Click += new System.EventHandler(this._framerateMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // _openFileDialog
            // 
            this._openFileDialog.DefaultExt = "calib";
            this._openFileDialog.FileName = "file";
            this._openFileDialog.Filter = "Calibration Files |*.calib";
            this._openFileDialog.Title = "Load Calibration File";
            // 
            // _saveFileDialog
            // 
            this._saveFileDialog.DefaultExt = "calib";
            this._saveFileDialog.FileName = "file";
            this._saveFileDialog.Filter = "Calibration Files|*.calib";
            this._saveFileDialog.Title = "Save Calibration File";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.labelRightPupilDiameter);
            this.groupBox1.Controls.Add(this.labelLeftPupilDiameter);
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.labelMessagesPerSecond);
            this.groupBox1.Controls.Add(this.labelRightGaze);
            this.groupBox1.Controls.Add(this.labelLeftGaze);
            this.groupBox1.Controls.Add(this.labelRightPos);
            this.groupBox1.Controls.Add(this.labelLeftPos);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.labelTimeStamp);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.labelNoSentMessages);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.labelConnectedToPython);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.buttonDisconnectSocket);
            this.groupBox1.Controls.Add(this.buttonConnectSocket);
            this.groupBox1.Location = new System.Drawing.Point(658, 38);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(572, 260);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Python app Data forwarding controls";
            // 
            // labelRightPupilDiameter
            // 
            this.labelRightPupilDiameter.AutoSize = true;
            this.labelRightPupilDiameter.Location = new System.Drawing.Point(301, 215);
            this.labelRightPupilDiameter.Name = "labelRightPupilDiameter";
            this.labelRightPupilDiameter.Size = new System.Drawing.Size(53, 13);
            this.labelRightPupilDiameter.TabIndex = 19;
            this.labelRightPupilDiameter.Text = "Unknown";
            // 
            // labelLeftPupilDiameter
            // 
            this.labelLeftPupilDiameter.AutoSize = true;
            this.labelLeftPupilDiameter.Location = new System.Drawing.Point(65, 215);
            this.labelLeftPupilDiameter.Name = "labelLeftPupilDiameter";
            this.labelLeftPupilDiameter.Size = new System.Drawing.Size(53, 13);
            this.labelLeftPupilDiameter.TabIndex = 18;
            this.labelLeftPupilDiameter.Text = "Unkonwn";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(10, 215);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(58, 13);
            this.label13.TabIndex = 8;
            this.label13.Text = "Pupil diam:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(8, 110);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(98, 13);
            this.label8.TabIndex = 17;
            this.label8.Text = "Messages/second:";
            // 
            // labelMessagesPerSecond
            // 
            this.labelMessagesPerSecond.AutoSize = true;
            this.labelMessagesPerSecond.Location = new System.Drawing.Point(148, 112);
            this.labelMessagesPerSecond.Name = "labelMessagesPerSecond";
            this.labelMessagesPerSecond.Size = new System.Drawing.Size(53, 13);
            this.labelMessagesPerSecond.TabIndex = 16;
            this.labelMessagesPerSecond.Text = "Unknown";
            // 
            // labelRightGaze
            // 
            this.labelRightGaze.AutoSize = true;
            this.labelRightGaze.Location = new System.Drawing.Point(301, 197);
            this.labelRightGaze.Name = "labelRightGaze";
            this.labelRightGaze.Size = new System.Drawing.Size(53, 13);
            this.labelRightGaze.TabIndex = 15;
            this.labelRightGaze.Text = "Unknown";
            // 
            // labelLeftGaze
            // 
            this.labelLeftGaze.AutoSize = true;
            this.labelLeftGaze.Location = new System.Drawing.Point(65, 197);
            this.labelLeftGaze.Name = "labelLeftGaze";
            this.labelLeftGaze.Size = new System.Drawing.Size(53, 13);
            this.labelLeftGaze.TabIndex = 14;
            this.labelLeftGaze.Text = "Unknown";
            // 
            // labelRightPos
            // 
            this.labelRightPos.AutoSize = true;
            this.labelRightPos.Location = new System.Drawing.Point(301, 180);
            this.labelRightPos.Name = "labelRightPos";
            this.labelRightPos.Size = new System.Drawing.Size(53, 13);
            this.labelRightPos.TabIndex = 13;
            this.labelRightPos.Text = "Unknown";
            // 
            // labelLeftPos
            // 
            this.labelLeftPos.AutoSize = true;
            this.labelLeftPos.Location = new System.Drawing.Point(65, 177);
            this.labelLeftPos.Name = "labelLeftPos";
            this.labelLeftPos.Size = new System.Drawing.Size(53, 13);
            this.labelLeftPos.TabIndex = 12;
            this.labelLeftPos.Text = "Unknown";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(300, 164);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(32, 13);
            this.label7.TabIndex = 11;
            this.label7.Text = "Right";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(65, 160);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(25, 13);
            this.label6.TabIndex = 10;
            this.label6.Text = "Left";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(10, 197);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(35, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Gaze:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(10, 180);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(47, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Position:";
            // 
            // labelTimeStamp
            // 
            this.labelTimeStamp.AutoSize = true;
            this.labelTimeStamp.Location = new System.Drawing.Point(148, 131);
            this.labelTimeStamp.Name = "labelTimeStamp";
            this.labelTimeStamp.Size = new System.Drawing.Size(53, 13);
            this.labelTimeStamp.TabIndex = 7;
            this.labelTimeStamp.Text = "Unknown";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 130);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(61, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Timestamp:";
            // 
            // labelNoSentMessages
            // 
            this.labelNoSentMessages.AutoSize = true;
            this.labelNoSentMessages.Location = new System.Drawing.Point(148, 93);
            this.labelNoSentMessages.Name = "labelNoSentMessages";
            this.labelNoSentMessages.Size = new System.Drawing.Size(53, 13);
            this.labelNoSentMessages.TabIndex = 5;
            this.labelNoSentMessages.Text = "Unknown";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 93);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(124, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Nuber of sent messages:";
            // 
            // labelConnectedToPython
            // 
            this.labelConnectedToPython.AutoSize = true;
            this.labelConnectedToPython.Location = new System.Drawing.Point(148, 76);
            this.labelConnectedToPython.Name = "labelConnectedToPython";
            this.labelConnectedToPython.Size = new System.Drawing.Size(53, 13);
            this.labelConnectedToPython.TabIndex = 3;
            this.labelConnectedToPython.Text = "Unknown";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 76);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(110, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Connected to Python:";
            // 
            // buttonDisconnectSocket
            // 
            this.buttonDisconnectSocket.Location = new System.Drawing.Point(7, 46);
            this.buttonDisconnectSocket.Name = "buttonDisconnectSocket";
            this.buttonDisconnectSocket.Size = new System.Drawing.Size(178, 23);
            this.buttonDisconnectSocket.TabIndex = 1;
            this.buttonDisconnectSocket.Text = "Disconnect socket";
            this.buttonDisconnectSocket.UseVisualStyleBackColor = true;
            this.buttonDisconnectSocket.Click += new System.EventHandler(this.buttonDisconnectSocket_Click);
            // 
            // buttonConnectSocket
            // 
            this.buttonConnectSocket.Location = new System.Drawing.Point(7, 16);
            this.buttonConnectSocket.Name = "buttonConnectSocket";
            this.buttonConnectSocket.Size = new System.Drawing.Size(178, 23);
            this.buttonConnectSocket.TabIndex = 0;
            this.buttonConnectSocket.Text = "Connect socket";
            this.buttonConnectSocket.UseVisualStyleBackColor = true;
            this.buttonConnectSocket.Click += new System.EventHandler(this.buttonConnectSocket_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.labelSampelsPerSecond);
            this.groupBox2.Controls.Add(this.labelReceivedInSession);
            this.groupBox2.Controls.Add(this.labelOservingStart);
            this.groupBox2.Controls.Add(this.labelObservigDataFilename);
            this.groupBox2.Controls.Add(this.label12);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Location = new System.Drawing.Point(658, 304);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(572, 107);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Data logging status";
            // 
            // labelSampelsPerSecond
            // 
            this.labelSampelsPerSecond.AutoSize = true;
            this.labelSampelsPerSecond.Location = new System.Drawing.Point(158, 54);
            this.labelSampelsPerSecond.Name = "labelSampelsPerSecond";
            this.labelSampelsPerSecond.Size = new System.Drawing.Size(53, 13);
            this.labelSampelsPerSecond.TabIndex = 7;
            this.labelSampelsPerSecond.Text = "Unknown";
            // 
            // labelReceivedInSession
            // 
            this.labelReceivedInSession.AutoSize = true;
            this.labelReceivedInSession.Location = new System.Drawing.Point(158, 37);
            this.labelReceivedInSession.Name = "labelReceivedInSession";
            this.labelReceivedInSession.Size = new System.Drawing.Size(53, 13);
            this.labelReceivedInSession.TabIndex = 6;
            this.labelReceivedInSession.Text = "Unknown";
            // 
            // labelOservingStart
            // 
            this.labelOservingStart.AutoSize = true;
            this.labelOservingStart.Location = new System.Drawing.Point(158, 20);
            this.labelOservingStart.Name = "labelOservingStart";
            this.labelOservingStart.Size = new System.Drawing.Size(56, 13);
            this.labelOservingStart.TabIndex = 5;
            this.labelOservingStart.Text = "Unknown.";
            // 
            // labelObservigDataFilename
            // 
            this.labelObservigDataFilename.AutoSize = true;
            this.labelObservigDataFilename.Location = new System.Drawing.Point(157, 82);
            this.labelObservigDataFilename.Name = "labelObservigDataFilename";
            this.labelObservigDataFilename.Size = new System.Drawing.Size(56, 13);
            this.labelObservigDataFilename.TabIndex = 4;
            this.labelObservigDataFilename.Text = "Unknown.";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(8, 82);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(142, 13);
            this.label12.TabIndex = 3;
            this.label12.Text = "Saved observing data to file:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(8, 54);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(90, 13);
            this.label11.TabIndex = 2;
            this.label11.Text = "Samples/second:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(7, 37);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(98, 13);
            this.label10.TabIndex = 1;
            this.label10.Text = "Recorded samples:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(7, 20);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(81, 13);
            this.label9.TabIndex = 0;
            this.label9.Text = "Observing start:";
            // 
            // labelAbout
            // 
            this.labelAbout.AutoSize = true;
            this.labelAbout.Location = new System.Drawing.Point(13, 429);
            this.labelAbout.Name = "labelAbout";
            this.labelAbout.Size = new System.Drawing.Size(10, 13);
            this.labelAbout.TabIndex = 7;
            this.labelAbout.Text = ".";
            // 
            // _trackStatus
            // 
            this._trackStatus.BackColor = System.Drawing.Color.Black;
            this._trackStatus.Location = new System.Drawing.Point(74, 33);
            this._trackStatus.Name = "_trackStatus";
            this._trackStatus.Size = new System.Drawing.Size(234, 179);
            this._trackStatus.TabIndex = 1;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1248, 488);
            this.Controls.Add(this.labelAbout);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this._box2);
            this.Controls.Add(this._statusStrip);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this._box1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "LUCAMI Tobii Eye tracking logger and forwarder v0.3 /autostart";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this._box1.ResumeLayout(false);
            this._statusStrip.ResumeLayout(false);
            this._statusStrip.PerformLayout();
            this._box2.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox _box1;
        private System.Windows.Forms.ListView _trackerList;
        private System.Windows.Forms.Label _trackerInfoLabel;
        private System.Windows.Forms.Button _connectButton;
        private System.Windows.Forms.StatusStrip _statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel _connectionStatusLabel;
        private System.Windows.Forms.GroupBox _box2;
        private System.Windows.Forms.Button _trackButton;
        private TrackStatusControl _trackStatus;
        private System.Windows.Forms.Button _calibrateButton;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem _saveCalibrationMenuItem;
        private System.Windows.Forms.ToolStripMenuItem _viewCalibrationMenuItem;
        private System.Windows.Forms.ToolStripMenuItem _loadCalibrationMenuItem;
        private System.Windows.Forms.OpenFileDialog _openFileDialog;
        private System.Windows.Forms.SaveFileDialog _saveFileDialog;
        private System.Windows.Forms.ToolStripMenuItem propertiesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem _framerateMenuItem;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button buttonDisconnectSocket;
        private System.Windows.Forms.Button buttonConnectSocket;
        private System.Windows.Forms.Label labelConnectedToPython;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labelNoSentMessages;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label labelTimeStamp;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label labelLeftPos;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label labelRightPos;
        private System.Windows.Forms.Label labelLeftGaze;
        private System.Windows.Forms.Label labelRightGaze;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label labelMessagesPerSecond;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label labelObservigDataFilename;
        private System.Windows.Forms.Label labelOservingStart;
        private System.Windows.Forms.Label labelReceivedInSession;
        private System.Windows.Forms.Label labelSampelsPerSecond;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.Label labelAbout;
        private System.Windows.Forms.Label labelRightPupilDiameter;
        private System.Windows.Forms.Label labelLeftPupilDiameter;
        private System.Windows.Forms.Label label13;
    }
}

