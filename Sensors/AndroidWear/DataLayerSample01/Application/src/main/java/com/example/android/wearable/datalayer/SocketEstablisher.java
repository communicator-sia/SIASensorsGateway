package com.example.android.wearable.datalayer;

import android.os.AsyncTask;

import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;
import java.io.PrintWriter;
import java.net.Socket;
import java.net.UnknownHostException;

/**
 * Created by markome on 1/29/2016.
 */
public class SocketEstablisher extends AsyncTask<Void, Void, Void> {
    private String dstAddress;
    private int dstPort;
    private Socket toPythonSocket;
    private PrintWriter toPythonOut;
    private BufferedReader toPythonIn;
    Communicator communicator;

    public SocketEstablisher(String dstAddress, int dstPort, Socket toPythonSocket, PrintWriter toPythonOut, BufferedReader toPythonIn, Communicator communicator){
        this.dstAddress = dstAddress;
        this.dstPort = dstPort;
        this.toPythonSocket = toPythonSocket;
        this.toPythonOut = toPythonOut;
        this.toPythonIn = toPythonIn;
        this.communicator = communicator;

    }


    @Override
    protected Void doInBackground(Void... params) {
        System.out.println("Connecting...");
        //Socket toPythonSocket = null;
        if(toPythonSocket!=null){
            System.out.println("!!!!!!!!!!!!!!!!!!!!to python Socket is not null!!!! possible fuckup.");
        }
        try {
            System.out.println("Connecting socket...");
            toPythonSocket = new Socket(dstAddress, dstPort);
            System.out.println("Getting i/o streams from socket ");
            toPythonOut = new PrintWriter(toPythonSocket.getOutputStream(), true);
            toPythonIn = new BufferedReader(new InputStreamReader(toPythonSocket.getInputStream()));
            System.out.println("Looks like socket is connected.");
        } catch (UnknownHostException e) {

            System.out.println("Unknown host: 192.168.81.88");
            e.printStackTrace();
            //System.exit(1);
        } catch  (IOException e) {

            System.out.println("No I/O");
            e.printStackTrace();
            //System.exit(1);
        }
        return null;
    }


    @Override
    protected void onPostExecute(Void result) {
        //textResponse.setText(response);
        System.out.println("onPostExecute Marko");
        communicator.toPythonIn = toPythonIn;
        communicator.toPythonOut = toPythonOut;
        communicator.toPythonSocket = toPythonSocket;
        super.onPostExecute(result);
    }
}
