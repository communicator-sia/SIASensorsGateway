package com.example.android.wearable.datalayer;

import android.os.AsyncTask;

import java.io.BufferedReader;
import java.io.PrintWriter;
import java.net.Socket;

/**
 * Created by markome on 1/29/2016.
 */
public class SocketDataSender extends AsyncTask<Void, Void, Void> {
    private Socket toPythonSocket;
    private PrintWriter toPythonOut;
    private BufferedReader toPythonIn;
    String message;

    public SocketDataSender (Socket toPythonSocket, PrintWriter toPythonOut, BufferedReader toPythonIn, String message){
        this.toPythonSocket = toPythonSocket;
        this.toPythonOut = toPythonOut;
        this.toPythonIn = toPythonIn;
        this.message = message;
    }

    @Override
    protected Void doInBackground(Void... params) {
        try{
            System.out.println("Sending message "+message);
            toPythonOut.println(message);
            System.out.println("Message sent.");
        } catch (Exception ex){
            ex.printStackTrace();
        }

        return null;
    }
}
