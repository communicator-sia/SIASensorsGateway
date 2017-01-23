package com.example.android.wearable.datalayer;

import java.io.BufferedReader;
import java.io.PrintWriter;
import java.net.Socket;

/**
 * Created by markome on 1/29/2016.
 */
public class Communicator {
    private String dstAddress;
    private int dstPort;
    Socket toPythonSocket;
    PrintWriter toPythonOut;
    BufferedReader toPythonIn;

    public void establishLinkWithPython(){
        System.out.println("IP AND PORT, This is hardcoded. CHANGE!!!!");
        dstAddress = "192.168.81.88";
        dstPort = 10005;
        SocketEstablisher se = new SocketEstablisher(dstAddress, dstPort, toPythonSocket, toPythonOut, toPythonIn, this);
        //se.doInBackground();
        se.execute();
    }

    public void sendDataViaSocketToPython(String message){
        SocketDataSender sds = new SocketDataSender(toPythonSocket, toPythonOut, toPythonIn, message);
        sds.execute();
    }

}
