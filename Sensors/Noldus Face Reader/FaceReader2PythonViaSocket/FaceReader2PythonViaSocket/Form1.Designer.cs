namespace WindowsFormsTestSocket_01
{
    partial class Form1
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
            this.buttonConnectSocket = new System.Windows.Forms.Button();
            this.buttonTestSend = new System.Windows.Forms.Button();
            this.buttonDisconnectSocket = new System.Windows.Forms.Button();
            this.buttonDoAllTest = new System.Windows.Forms.Button();
            this.buttonConnectFaceReader = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.buttonStartAnalyzing = new System.Windows.Forms.Button();
            this.buttonStopAnalyzing = new System.Windows.Forms.Button();
            this.buttonDisconnectFaceReader = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.labelConnectedToNoldus = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.labelReceivedInSession = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.labelSampelsPerSecond = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.labelMessagesPerSecond = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.labelNoSentMessages = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.labelConnectedToPython = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.labelTimeStamp = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.labelAbout = new System.Windows.Forms.Label();
            this.buttonAutoStart = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonConnectSocket
            // 
            this.buttonConnectSocket.Location = new System.Drawing.Point(6, 19);
            this.buttonConnectSocket.Name = "buttonConnectSocket";
            this.buttonConnectSocket.Size = new System.Drawing.Size(160, 23);
            this.buttonConnectSocket.TabIndex = 0;
            this.buttonConnectSocket.Text = "Connect socket";
            this.buttonConnectSocket.UseVisualStyleBackColor = true;
            this.buttonConnectSocket.Click += new System.EventHandler(this.buttonConnectSocket_Click);
            // 
            // buttonTestSend
            // 
            this.buttonTestSend.Location = new System.Drawing.Point(6, 142);
            this.buttonTestSend.Name = "buttonTestSend";
            this.buttonTestSend.Size = new System.Drawing.Size(160, 23);
            this.buttonTestSend.TabIndex = 1;
            this.buttonTestSend.Text = "Send message";
            this.buttonTestSend.UseVisualStyleBackColor = true;
            this.buttonTestSend.Click += new System.EventHandler(this.buttonSendMessage_Click);
            // 
            // buttonDisconnectSocket
            // 
            this.buttonDisconnectSocket.Location = new System.Drawing.Point(6, 48);
            this.buttonDisconnectSocket.Name = "buttonDisconnectSocket";
            this.buttonDisconnectSocket.Size = new System.Drawing.Size(160, 23);
            this.buttonDisconnectSocket.TabIndex = 2;
            this.buttonDisconnectSocket.Text = "Disconnect socket";
            this.buttonDisconnectSocket.UseVisualStyleBackColor = true;
            this.buttonDisconnectSocket.Click += new System.EventHandler(this.buttonDisconnectSocket_Click);
            // 
            // buttonDoAllTest
            // 
            this.buttonDoAllTest.Location = new System.Drawing.Point(6, 48);
            this.buttonDoAllTest.Name = "buttonDoAllTest";
            this.buttonDoAllTest.Size = new System.Drawing.Size(168, 23);
            this.buttonDoAllTest.TabIndex = 3;
            this.buttonDoAllTest.Text = "DoAll OLD TEST";
            this.buttonDoAllTest.UseVisualStyleBackColor = true;
            this.buttonDoAllTest.Click += new System.EventHandler(this.button4_Click);
            // 
            // buttonConnectFaceReader
            // 
            this.buttonConnectFaceReader.Location = new System.Drawing.Point(6, 19);
            this.buttonConnectFaceReader.Name = "buttonConnectFaceReader";
            this.buttonConnectFaceReader.Size = new System.Drawing.Size(168, 23);
            this.buttonConnectFaceReader.TabIndex = 4;
            this.buttonConnectFaceReader.Text = "Connect to FaceReader";
            this.buttonConnectFaceReader.UseVisualStyleBackColor = true;
            this.buttonConnectFaceReader.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(6, 19);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(168, 23);
            this.button6.TabIndex = 5;
            this.button6.Text = "Enable state log receiving";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.btnEnableStateLogs_Click);
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(6, 77);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(168, 23);
            this.button7.TabIndex = 6;
            this.button7.Text = "Enable detailed log receiving";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.btnEnableDetailedLogs_Click);
            // 
            // buttonStartAnalyzing
            // 
            this.buttonStartAnalyzing.Location = new System.Drawing.Point(6, 48);
            this.buttonStartAnalyzing.Name = "buttonStartAnalyzing";
            this.buttonStartAnalyzing.Size = new System.Drawing.Size(168, 23);
            this.buttonStartAnalyzing.TabIndex = 7;
            this.buttonStartAnalyzing.Text = "Start analyzing";
            this.buttonStartAnalyzing.UseVisualStyleBackColor = true;
            this.buttonStartAnalyzing.Click += new System.EventHandler(this.btnStartAnalysis_Click);
            // 
            // buttonStopAnalyzing
            // 
            this.buttonStopAnalyzing.Location = new System.Drawing.Point(6, 77);
            this.buttonStopAnalyzing.Name = "buttonStopAnalyzing";
            this.buttonStopAnalyzing.Size = new System.Drawing.Size(168, 23);
            this.buttonStopAnalyzing.TabIndex = 8;
            this.buttonStopAnalyzing.Text = "Stop analzing";
            this.buttonStopAnalyzing.UseVisualStyleBackColor = true;
            this.buttonStopAnalyzing.Click += new System.EventHandler(this.btnStopAnalysis_Click);
            // 
            // buttonDisconnectFaceReader
            // 
            this.buttonDisconnectFaceReader.Location = new System.Drawing.Point(6, 106);
            this.buttonDisconnectFaceReader.Name = "buttonDisconnectFaceReader";
            this.buttonDisconnectFaceReader.Size = new System.Drawing.Size(168, 23);
            this.buttonDisconnectFaceReader.TabIndex = 9;
            this.buttonDisconnectFaceReader.Text = "Disconnect from FaceReader";
            this.buttonDisconnectFaceReader.UseVisualStyleBackColor = true;
            this.buttonDisconnectFaceReader.Click += new System.EventHandler(this.btnDisconnect_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.labelConnectedToNoldus);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.labelReceivedInSession);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.labelSampelsPerSecond);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.buttonConnectFaceReader);
            this.groupBox1.Controls.Add(this.buttonDisconnectFaceReader);
            this.groupBox1.Controls.Add(this.buttonStopAnalyzing);
            this.groupBox1.Controls.Add(this.buttonStartAnalyzing);
            this.groupBox1.Location = new System.Drawing.Point(354, 30);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(221, 270);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Noldus Face reader";
            // 
            // labelConnectedToNoldus
            // 
            this.labelConnectedToNoldus.AutoSize = true;
            this.labelConnectedToNoldus.Location = new System.Drawing.Point(141, 148);
            this.labelConnectedToNoldus.Name = "labelConnectedToNoldus";
            this.labelConnectedToNoldus.Size = new System.Drawing.Size(53, 13);
            this.labelConnectedToNoldus.TabIndex = 15;
            this.labelConnectedToNoldus.Text = "Unknown";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(7, 148);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(108, 13);
            this.label6.TabIndex = 14;
            this.label6.Text = "Connected to noldus:";
            // 
            // labelReceivedInSession
            // 
            this.labelReceivedInSession.AutoSize = true;
            this.labelReceivedInSession.Location = new System.Drawing.Point(141, 161);
            this.labelReceivedInSession.Name = "labelReceivedInSession";
            this.labelReceivedInSession.Size = new System.Drawing.Size(53, 13);
            this.labelReceivedInSession.TabIndex = 13;
            this.labelReceivedInSession.Text = "Unknown";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(7, 161);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(97, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "Received samples:";
            // 
            // labelSampelsPerSecond
            // 
            this.labelSampelsPerSecond.AutoSize = true;
            this.labelSampelsPerSecond.Location = new System.Drawing.Point(141, 174);
            this.labelSampelsPerSecond.Name = "labelSampelsPerSecond";
            this.labelSampelsPerSecond.Size = new System.Drawing.Size(53, 13);
            this.labelSampelsPerSecond.TabIndex = 11;
            this.labelSampelsPerSecond.Text = "Unknown";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 174);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(106, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Samples per second:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.labelMessagesPerSecond);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.labelNoSentMessages);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.labelConnectedToPython);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.buttonConnectSocket);
            this.groupBox2.Controls.Add(this.buttonDisconnectSocket);
            this.groupBox2.Location = new System.Drawing.Point(12, 30);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(336, 270);
            this.groupBox2.TabIndex = 12;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Python app communication";
            // 
            // labelMessagesPerSecond
            // 
            this.labelMessagesPerSecond.AutoSize = true;
            this.labelMessagesPerSecond.Location = new System.Drawing.Point(141, 119);
            this.labelMessagesPerSecond.Name = "labelMessagesPerSecond";
            this.labelMessagesPerSecond.Size = new System.Drawing.Size(53, 13);
            this.labelMessagesPerSecond.TabIndex = 8;
            this.labelMessagesPerSecond.Text = "Unknown";
            this.labelMessagesPerSecond.Click += new System.EventHandler(this.labelMessagesPerSecond_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 119);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(114, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Messages per second:";
            // 
            // labelNoSentMessages
            // 
            this.labelNoSentMessages.AutoSize = true;
            this.labelNoSentMessages.Location = new System.Drawing.Point(141, 106);
            this.labelNoSentMessages.Name = "labelNoSentMessages";
            this.labelNoSentMessages.Size = new System.Drawing.Size(53, 13);
            this.labelNoSentMessages.TabIndex = 6;
            this.labelNoSentMessages.Text = "Unknown";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 106);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(132, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Number of sent messages:";
            // 
            // labelConnectedToPython
            // 
            this.labelConnectedToPython.AutoSize = true;
            this.labelConnectedToPython.Location = new System.Drawing.Point(141, 93);
            this.labelConnectedToPython.Name = "labelConnectedToPython";
            this.labelConnectedToPython.Size = new System.Drawing.Size(53, 13);
            this.labelConnectedToPython.TabIndex = 4;
            this.labelConnectedToPython.Text = "Unknown";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 93);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(117, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Is connectet to Python:";
            // 
            // labelTimeStamp
            // 
            this.labelTimeStamp.AutoSize = true;
            this.labelTimeStamp.Location = new System.Drawing.Point(114, 307);
            this.labelTimeStamp.Name = "labelTimeStamp";
            this.labelTimeStamp.Size = new System.Drawing.Size(47, 13);
            this.labelTimeStamp.TabIndex = 13;
            this.labelTimeStamp.Text = "Unkown";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.buttonAutoStart);
            this.groupBox3.Controls.Add(this.button6);
            this.groupBox3.Controls.Add(this.buttonDoAllTest);
            this.groupBox3.Controls.Add(this.button7);
            this.groupBox3.Controls.Add(this.buttonTestSend);
            this.groupBox3.Location = new System.Drawing.Point(581, 36);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(200, 264);
            this.groupBox3.TabIndex = 14;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "DO NOT PRESS!!!";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(13, 307);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(95, 13);
            this.label7.TabIndex = 15;
            this.label7.Text = "Internal timestamp:";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(852, 24);
            this.menuStrip1.TabIndex = 16;
            this.menuStrip1.Text = "menuStrip1";
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
            // labelAbout
            // 
            this.labelAbout.AutoSize = true;
            this.labelAbout.Location = new System.Drawing.Point(16, 324);
            this.labelAbout.Name = "labelAbout";
            this.labelAbout.Size = new System.Drawing.Size(10, 13);
            this.labelAbout.TabIndex = 17;
            this.labelAbout.Text = ".";
            // 
            // buttonAutoStart
            // 
            this.buttonAutoStart.Location = new System.Drawing.Point(7, 226);
            this.buttonAutoStart.Name = "buttonAutoStart";
            this.buttonAutoStart.Size = new System.Drawing.Size(167, 23);
            this.buttonAutoStart.TabIndex = 7;
            this.buttonAutoStart.Text = "Connect Pyt, Nold, START";
            this.buttonAutoStart.UseVisualStyleBackColor = true;
            this.buttonAutoStart.Click += new System.EventHandler(this.buttonAutoStart_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(852, 462);
            this.Controls.Add(this.labelAbout);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.labelTimeStamp);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Lucami Noldus FaceReader controller and data forwarder v0.3 /autostart";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonConnectSocket;
        private System.Windows.Forms.Button buttonTestSend;
        private System.Windows.Forms.Button buttonDisconnectSocket;
        private System.Windows.Forms.Button buttonDoAllTest;
        private System.Windows.Forms.Button buttonConnectFaceReader;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Button buttonStartAnalyzing;
        private System.Windows.Forms.Button buttonStopAnalyzing;
        private System.Windows.Forms.Button buttonDisconnectFaceReader;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label labelConnectedToPython;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labelNoSentMessages;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label labelTimeStamp;
        private System.Windows.Forms.Label labelMessagesPerSecond;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label labelSampelsPerSecond;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label labelReceivedInSession;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label labelConnectedToNoldus;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.Label labelAbout;
        private System.Windows.Forms.Button buttonAutoStart;
    }
}

