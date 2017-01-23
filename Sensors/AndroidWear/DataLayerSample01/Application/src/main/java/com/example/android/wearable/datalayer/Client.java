/**
 * Created by markome on 1/27/2016.
 * copy from http://androidsrc.net/android-client-server-using-sockets-client-implementation/
 */

package com.example.android.wearable.datalayer;

import java.io.BufferedReader;
import java.io.ByteArrayOutputStream;
import java.io.IOException;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.io.PrintWriter;
import java.net.Socket;
import java.net.UnknownHostException;
import android.os.AsyncTask;
import android.widget.TextView;

public class Client extends AsyncTask<Void, Void, Void> {

    String dstAddress;
    int dstPort;
    String message = "";
    //String
    //TextView textResponse;

    Client(String addr, int port, String message) {
        dstAddress = addr;
        dstPort = port;
        this.message = message;
        //this.textResponse = textResponse;
    }

    @Override
    protected Void doInBackground(Void... arg0) {

        try{
            //192.168.81.88 port 10005
            System.out.println("Connecting...");
            Socket toPythonSocket = new Socket("192.168.81.88", 10005);
            System.out.println("Still Connecting...");
            PrintWriter toPythonOut = new PrintWriter(toPythonSocket.getOutputStream(),
                    true);
            BufferedReader toPythonIn = new BufferedReader(new InputStreamReader(
                    toPythonSocket.getInputStream()));
            System.out.println("Yeah!");
            toPythonOut.println(message);
            System.out.println("Yeah! x2");
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
        super.onPostExecute(result);
    }

}