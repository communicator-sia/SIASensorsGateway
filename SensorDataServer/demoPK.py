"""
@author: primozkocevar

  USB connection for supplying power with MultiLux is problematic and can disconnect!

  Requirements:
      - python -m pip install --upgrade  --trusted-host wxpython.org --pre -f http://wxpython.org/Phoenix/snapshot-builds/ wxPython_Phoenix 
      - python -m pip install pybluez

  THE RIGHT WAY TO CLOSE IT:
      - STOP button to stop sending data.
      - DISCCONECT to stop the bluetooth connection and release the sockets.
      - EXIT the taskbar app, to also close socket with Typing Tutor and exit the code.
     
  Max Performance measured, not crashed yet:
      -20Hz
      -4g
      -500dps
      -approx. 135 000 measurements
      -approx. 30 min
"""

from communication import CommunicationService;
from tray import SysTrayIcon;
import threading;
import wx, wx.lib.newevent, wx.adv;
import struct;
import time;
import csv
import sys
import json
import socket
from datetime import datetime;


global accelHost
accelHost = 'localhost'
global accelPort
accelPort = 10006

global gsrHost
gsrHost = 'localhost'
global gsrPort
gsrPort = 10007

global isSocketConnected
isSocketConnected=False

global eventAccelCounter
eventAccelCounter=0

global eventGsrCounter
eventGsrCounter=0

EventSettings, EVENT_SETTINGS = wx.lib.newevent.NewEvent()
EventExit, EVENT_EXIT = wx.lib.newevent.NewEvent()


COMMAND_START = 0x01
COMMAND_STOP = 0x02
COMMAND_SET_DIVIDER = 0x03
COMMAND_SET_FILTER = 0x04
COMMAND_SET_GYRO_SCALE = 0x05
COMMAND_SET_ACC_SCALE = 0x06
COMMAND_START = 0x01

PACKET_TYPE_COMMAND  = 0x01
PACKET_TYPE_RESPOND  = 0x02
PACKET_TYPE_DATA  = 0x03
PACKET_TYPE_ANALOG  = 0x04

TRAY_TOOLTIP = 'Sensor aquisition'
TRAY_ICON = 'mux.ico'

def create_menu_item(menu, label, func):
    item = wx.MenuItem(menu, -1, label)
    menu.Bind(wx.EVT_MENU, func, id=item.GetId())
    menu.AppendItem(item)
    return item

class TaskBarIcon(wx.adv.TaskBarIcon):
    def __init__(self, frame):
        super(TaskBarIcon, self).__init__()
        self.set_icon(TRAY_ICON)
        self.Bind(wx.adv.EVT_TASKBAR_LEFT_DOWN, self.on_left_down)
        self.frame = frame;

    def CreatePopupMenu(self):
        menu = wx.Menu()
        create_menu_item(menu, 'Settings', self.on_settings)
        menu.AppendSeparator()
        create_menu_item(menu, 'Exit', self.on_exit)
        return menu

    def set_icon(self, path):
        icon = wx.Icon(wx.Bitmap(path))
        self.SetIcon(icon, TRAY_TOOLTIP)

    def on_left_down(self, event):
        wx.PostEvent(self.frame, EventSettings())

    def on_settings(self, event):
        wx.PostEvent(self.frame, EventSettings())

    def on_exit(self, event):
        wx.PostEvent(self.frame, EventExit())
        wx.CallAfter(self.Destroy)

class TrayHandler(threading.Thread):
    def __init__(self, callback, wx):
        threading.Thread.__init__(self);
        self.daemon = True;
        self._running = False;
        self._callback = callback;
        self._wx = wx;

        self.start();

    def run(self):
        self._running = True;
        menu_options = (('settings', None, self.settings),)
        SysTrayIcon('mux.ico', "Multilux acquistion", menu_options, on_quit=self.exit, default_menu_index=1)

    def exit(self, event):
        wx.PostEvent(self._wx, EventExit())

    def settings(self, event):
        self._callback();

class GuiFrame(wx.Frame):

    STATE_IDLE = 1
    STATE_SAVING = 2

    def __init__(self, script = True):     

        
        wx.Frame.__init__ ( self, None, id = wx.ID_ANY, title = wx.EmptyString, pos = wx.DefaultPosition, size = wx.Size( 800,260 ), style = wx.DEFAULT_FRAME_STYLE)
        
        self._comm = CommunicationService();
        self._comm.add_type_callback(PACKET_TYPE_RESPOND, self.on_respond_received);
        self._comm.add_type_callback(PACKET_TYPE_DATA, self.on_data_received);
        self._comm.add_type_callback(PACKET_TYPE_ANALOG, self.on_analog_received);
        self._exit = False;
        self._script = True;

        self.state = GuiFrame.STATE_IDLE;

        sizer_main = wx.BoxSizer( wx.HORIZONTAL )
        panel_controls = wx.Panel( self, wx.ID_ANY, wx.DefaultPosition, wx.DefaultSize, wx.TAB_TRAVERSAL )

        self.addr = wx.TextCtrl(panel_controls, pos=(4, 10), size = (200, 24), style = wx.TE_READONLY)
        self.addr.SetValue("00:13:43:16:A0:D0 - LUCAMI band");

        self.butt_connect = wx.Button(panel_controls, wx.ID_ANY, "Bluetooth Connect", pos=(220, 10), size = (160, 24), style = wx.TE_READONLY)
        wx.StaticLine(panel_controls, id=wx.ID_ANY, pos=(390, 4), size=(2, 200), style=wx.LI_VERTICAL)
        wx.StaticLine(panel_controls, id=wx.ID_ANY, pos=(410, 125), size=(250,2), style=wx.LI_HORIZONTAL)
        self.quote = wx.StaticText(panel_controls,label="Sockets to connect:\n - %s:%d  \n - %s:%d \n" % (gsrHost,gsrPort,accelHost,accelPort),pos=(420,40))
        #self.quote = wx.StaticText(panel_controls,label="Sent Accel events: %s, Sent GSR events: %s" % (eventAccelCounter,eventGsrCounter),pos=(420,130))
        self.butt_conSocket =wx.Button(panel_controls,wx.ID_ANY,"Socket connect",pos=(400,10),size=(160,24),style=wx.TE_READONLY)
        # DELIMITER
        wx.StaticLine(panel_controls, id=wx.ID_ANY, pos=(4, 40), size=(374, 2), style=wx.LI_HORIZONTAL)

        # ODR
        iso_static = wx.StaticText(panel_controls, wx.ID_ANY, u"Settings:", (4, 53), (55, 24), 0 )
        self.combo_odr = wx.ComboBox(panel_controls, size=(70, 24),pos=(59, 50),style=wx.CB_DROPDOWN, choices=[])
        self.combo_odr.Append("41 Hz", 1);
        self.combo_odr.Append("20 Hz", 2);
        self.combo_odr.Append("10 Hz", 3);
        self.combo_odr.Append("5 Hz", 4);
        self.combo_odr.SetSelection(1)

        # ACC scale
        self.combo_acc_scale = wx.ComboBox(panel_controls,size=(70, 24),pos=(140, 50),style=wx.CB_DROPDOWN,choices=[])
        self.combo_acc_scale.Append(u"\u00B12g", 1);
        self.combo_acc_scale.Append(u"\u00B14g", 2);
        self.combo_acc_scale.Append(u"\u00B18g", 3);
        self.combo_acc_scale.Append(u"\u00B116g", 4);
        self.combo_acc_scale.SetSelection(1)

        # ACC scale
        self.combo_gyro_scale = wx.ComboBox(panel_controls,size=(80, 24),pos=(225, 50),style=wx.CB_DROPDOWN,choices=[])
        self.combo_gyro_scale.Append(u"\u00B1250dps", 1);
        self.combo_gyro_scale.Append(u"\u00B1500dps", 2);
        self.combo_gyro_scale.Append(u"\u00B1100dps", 3);
        self.combo_gyro_scale.Append(u"\u00B12000dps", 4);
        self.combo_gyro_scale.SetSelection(1)

        self.filePath = wx.TextCtrl(panel_controls, pos=(4, 80), size = (370, 24))
        self.filePath.SetValue("C:\ ")

        self.filePath_res = wx.TextCtrl(panel_controls, pos=(4, 110), size = (370, 24))
        self.filePath_res.SetValue("C:\ ")

        self.butt_browse = wx.Button(panel_controls, wx.ID_ANY, "Browse...", pos=(4, 140), size = (80, 24))

        wx.StaticLine(panel_controls, id=wx.ID_ANY, pos=(4, 170), size=(374, 2), style=wx.LI_HORIZONTAL)

        self.butt_command = wx.Button(panel_controls, wx.ID_ANY, "Start", pos=(4, 180), size = (80, 24))

        sizer_main.Add(panel_controls, 1, wx.EXPAND|wx.ALL, 1);
        self.SetSizer( sizer_main )
        self.Layout()
        
        self.Centre( wx.BOTH )
        
        self.Bind(wx.EVT_BUTTON, self.on_click_button_browse, self.butt_browse)
        self.Bind(wx.EVT_BUTTON, self.on_click_button_command, self.butt_command)
        self.Bind(wx.EVT_BUTTON, self.on_click_button_connect, self.butt_connect)

        self.Bind(wx.EVT_BUTTON, self.on_click_button_conSocket, self.butt_conSocket)
        self.Bind(EVENT_EXIT, self.on_exit)
        self.Bind(wx.EVT_CLOSE, self.close_window)  #Bind the EVT_CLOSE event to closeWindow()

        self.gyro_scale = 1.0;
        self.acc_scale = 1.0;

        self.csv_file_d = False;
        self.csv_writer = False;

        self.csv_file_d_analog = False;
        self.csv_writer_analog = False;

    def on_exit(self, event):
        self._exit = True;
        self.Close();

        # safety if socket still open whequestin we want to exit.
        # http://stackoverflow.com/ons/409783/socket-shutdown-vs-socket-close
        global isSocketConnected
        if isSocketConnected==True:
            self.sockA.shutdown(socket.SHUT_RDWR)
            self.sockA.close()
            self.sockG.shutdown(socket.SHUT_RDWR)
            self.sockG.close()
            print("Gracefully closing sockets with Typing Tutor")
            isSocketConnected=False
        else:
            print("Socket not even open!")

    def on_respond_received(self, packet):
        print("respond recevied")
        
    # USTVARJANJE CSV ZA GYRO IN ACCEL
    def on_data_received(self, packet):
        global eventAccelCounter
        global eventGsrCounter
        eventAccelCounter += 1
        # za izpis poslanih eventov, vendar ne dela, ker se ne obnavlja
        # self.quote.SetLabel("Sent Accel events: %s, Sent GSR events: %s" % (eventAccelCounter,eventGsrCounter))
        if self.state == GuiFrame.STATE_SAVING:
            if not self._start_timestamp:
                self._start_timestamp = time.time();

            data = struct.unpack("hhhhhhI", packet.data);
            # množenje vsakega podatka posebej z scalom, da ga skaliramo
            out = [data[0] * self.acc_scale, data[1] * self.acc_scale, data[2] * self.acc_scale, 
                                      data[3] * self.gyro_scale, data[4] * self.gyro_scale, data[5] * self.gyro_scale, 
                                      int(round((self._start_timestamp + (data[6] * 0.02)) * 1000))];

            # tukaj sestavi json in pošlji s socektom!
            # https://docs.python.org/2/library/json.html
            # https://docs.python.org/2/library/socket.html
            # tukaj jih bomo tako kreirali, na drugi strani jih bomo pa appendali! ( verjetno bo treba še oglate odstranit, da bo to lepo serija ki se bo pol sm appendala drugje)
            jsonData=json.dumps([{'multiLuxAccel':{'timestamp':out[6],'accel':{'ax':out[0],'ay':out[1],'az':out[2]},'gyro':{'gx':out[3],'gy':out[4],'gz':out[5]}}}])

            self.sockA.sendall(jsonData)

            self.csv_writer.writerow(['{:2.5f}'.format(x) for x in out[:6]] + [out[6]]);

            self.csv_file_d.flush();


# USTARJANJE CSV ZA MERJNJE UPORNOSTI
    def on_analog_received(self, packet):
        global eventGsrCounter
        eventGsrCounter += 1
        data = struct.unpack("<III", packet.data);
        out = [data[0], data[1], int(round((self._start_timestamp + (data[2] * 0.05)) * 1000))];
               
        jsonData=json.dumps([{'multiLuxGSR':{'timestamp':out[2],'resistanceOhm':{'Ra':out[1],'Rb':out[0]}}}])

        self.sockG.sendall(jsonData)       
        
        self.csv_writer_res.writerow(['{0}'.format(x) for x in out[:2]] + [out[2]]);
        self.csv_file_res.flush();



    def on_click_button_connect(self, event):
        if self._comm.is_connected():
            self._comm.disconnect();
            self.butt_connect.SetLabel("connect");

        else:
            succ, err = self._comm.connect("00:13:43:16:A0:D0", 1);

            if (succ):
                self.butt_connect.SetLabel("disconnect");
            else:
                dlg = wx.MessageDialog(self, "Error connecting to the module: {0}".format(err), "Error", wx.OK | wx.ICON_WARNING)
                dlg.ShowModal()
                dlg.Destroy()


    def on_click_button_command(self, event):
        if self.state == GuiFrame.STATE_IDLE:

            odr = self.combo_odr.GetClientData(self.combo_odr.GetSelection())
            acc_scale = self.combo_acc_scale.GetClientData(self.combo_acc_scale.GetSelection())
            gyro_scale = self.combo_gyro_scale.GetClientData(self.combo_gyro_scale.GetSelection())

            if self._comm.is_connected():

                self.csv_file_d = open(self.filePath.GetValue(), 'wb');
                self.csv_writer =  csv.writer(self.csv_file_d, delimiter=',')

                self.csv_file_res = open(self.filePath_res.GetValue(), 'wb');
                self.csv_writer_res =  csv.writer(self.csv_file_res, delimiter=',')

                acc_conv = [0, 2, 4, 8, 16];
                gyro_conv = [0, 250, 500, 1000, 2000];
                self.gyro_scale = gyro_conv[int(gyro_scale)] / 32767.0
                self.acc_scale = (acc_conv[int(acc_scale)] / 32767.0) * 9.81;   

                # we fix the datarate to 50Hz
                self._comm.send_command(PACKET_TYPE_COMMAND, chr(COMMAND_SET_DIVIDER) + chr(19));         
                self._comm.send_command(PACKET_TYPE_COMMAND, chr(COMMAND_SET_FILTER) + chr(odr));
                self._comm.send_command(PACKET_TYPE_COMMAND, chr(COMMAND_SET_ACC_SCALE) + chr(acc_scale));
                self._comm.send_command(PACKET_TYPE_COMMAND, chr(COMMAND_SET_GYRO_SCALE) + chr(gyro_scale));

                self._comm.send_command(PACKET_TYPE_COMMAND, chr(COMMAND_START));

                self._start_timestamp = False;
                self.state = GuiFrame.STATE_SAVING;

                self.butt_command.SetLabel("Stop");

        else:
            self.csv_file_d.close();
            self._packet_time = False;
            self.state = GuiFrame.STATE_IDLE;
            self.butt_command.SetLabel("Start");

            if self._comm.is_connected():
                self._comm.send_command(PACKET_TYPE_COMMAND, chr(COMMAND_STOP));

    def on_click_button_browse(self, event):
        openFileDialog = wx.FileDialog(self, "Open csv file", "", "",
                                       "CSV files (*.csv)|*.csv", wx.FD_SAVE)

        if openFileDialog.ShowModal() == wx.ID_CANCEL:
            return     # the user changed idea...

        self.filePath.SetValue(openFileDialog.GetPath());
    def on_click_button_conSocket(self,event):
        # vse v enem gumbu narejeno, connect in disconnect!
        global isSocketConnected
        global accelHost
        global accelPort
        global gsrHost
        global gsrPort
        
        if isSocketConnected==False:
            self.sockA = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
            self.sockG = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
            # http://stackoverflow.com/questions/177389/testing-socket-connection-in-python
            try:
                self.sockA.connect((accelHost,accelPort))
                self.sockG.connect((gsrHost,gsrPort))
                isSocketConnected=True
                self.butt_conSocket.SetLabel("Disconnect")
            except Exception as e:
                   print("something's wrong with %s:%d. Exception is %s" % (gsrHost, gsrPort, e))
                   isSocketConnected=False
                   self.butt_conSocket.SetLabel("Can't connect, Retry!")
        else: 
            if isSocketConnected==True:
                self.sockA.shutdown(socket.SHUT_RDWR)
                self.sockA.close()
                self.sockG.shutdown(socket.SHUT_RDWR)
                self.sockG.close()
                print("Gracefully closing sockets with Typing Tutor")
                isSocketConnected=False
                self.butt_conSocket.SetLabel("Socket Connect")
            else:
                print("Socket not even open!")
                

    def close_window(self, event):
        self.Hide();
        if self._exit:
            self.Destroy();

class GuiHandler():

    def __init__(self, script = False):
        self.app = wx.App()

        
        self.frame = GuiFrame(script = True);
        self.frame.Bind(EVENT_SETTINGS, self.toggle_frame)

        self.taskbar = TaskBarIcon(self.frame);
        #self.tray = TrayHandler(self.on_click_settings, self.frame);
        

        if script == True:
            self.frame.on_click_button_connect(None);
            if self.frame._comm._conn:
                self.taskbar.ShowBalloon("Bluetooth connected", "bluetooth was succesfully connected", 300)

                filename = datetime.now().strftime("acc-gyro-%m%d%Y-%H%M%S.csv");
                filename_res = datetime.now().strftime("resistance-%m%d%Y-%H%M%S.csv");
                self.frame.filePath.SetValue(filename);
                self.frame.filePath_res.SetValue(filename_res);
                
                self.frame.on_click_button_command(None);
        self.app.MainLoop()

    def toggle_frame(self, event):
        print("showing frame")
        self.frame.Show();

        

if __name__ == '__main__':
    if len(sys.argv) > 1 and sys.argv[1] == "script":
        GuiHandler(script = True);
    else:
        GuiHandler(script = False);