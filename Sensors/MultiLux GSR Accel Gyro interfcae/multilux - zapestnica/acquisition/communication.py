from packet import Packet, PacketDecode
import threading;
from bluetooth import *

class CommunicationService(threading.Thread):
    TYPE_COMMAND = 1;
    TYPE_SETTING = 2;

    def __init__(self):

        threading.Thread.__init__(self)
        self.daemon = True;

        self._running = False;
        self._conn = False;

        self._callbacks = {};

        self._packetDecoder = PacketDecode();

    def run(self):
        self._running = True;
        while self._running:
            if self._conn:
                try:
                    d = self._conn.recv(258)

                    packets = self._packetDecoder.decode_stream(d)

                    if packets:
                        for packet in packets:
                            if packet.type in self._callbacks:
                                for callback in self._callbacks[packet.type]:
                                    callback(packet);
                except Exception as e:
                    raise(e);
                    self._conn = False;
            else:
                time.sleep(1);
                #self.connect(self._port, self._baud);

    def send_command(self, type, value):
        if self._conn:
            bytes_out = '\xaa';
            bytes_out += chr(int(type));
            bytes_out += chr(len(value));
            bytes_out += value;
            bytes_out += b"\x55";

            self._conn.send(bytes_out);

    def is_connected(self):
        return self._conn;


    def disconnect(self):
        if self._conn:
            try:
                self._conn.close();
            except Exception as e:
                print("Error closing serial connection");

            self._conn = False;

    def connect(self, addr, port):
        if self._conn:
            try:
                self._conn.close();
            except Exception as e:
                print("Error closing serial connection");

            self._conn = (False);

        try:
            self._port = port;
            self._addr = addr;
            self._conn = sock=BluetoothSocket( RFCOMM )
            self._conn.connect((self._addr, self._port))
            
        except Exception as e:
            self._conn = False;
            return (False, str(e));

        if self._running == False:
            self.start();

        return (True, "");

    def add_type_callback(self, type, callback):
        if type in self._callbacks:
            self._callbacks[type].append(callback);
        else:
            self._callbacks[type] = [callback];