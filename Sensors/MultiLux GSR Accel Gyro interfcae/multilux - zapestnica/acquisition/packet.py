
class PacketDecode:
    def __init__(self):

        self._state_data = b'';
        self._state_data_length = 0;
        self._state_position = 0;
        self._state_type = 0;

    def decode_stream(self, stream):

        packets = [];

        for character in stream:
            bin_character = ord(character)
            if self._state_position == 0:
                if bin_character != 0xAA:
                    pass;
                else:
                    self._state_position = 1;
            elif self._state_position == 1:
                self._state_type = bin_character;
                self._state_position = 2;
                self._state_data = b'';
            elif self._state_position == 2:
                self._state_data_length = bin_character;
                self._state_position = 3;
            elif self._state_position == 3:
                if len(self._state_data) < self._state_data_length:
                    self._state_data += chr(bin_character);

                if len(self._state_data) >= self._state_data_length:
                    self._state_position = 4;
            elif self._state_position == 4:
                if bin_character != 0x55: 
                    print("Hell no!")
                    print(hex(bin_character))
                else:
                    packets.append(Packet(self._state_type, self._state_data))

                self._state_position = 0;

        return packets; 

class Packet:
    def __init__(self, type, data):
        self.data = data;
        self.type = type;