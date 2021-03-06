﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.Contracts;
using System.IO;
using System.Collections;

namespace Client_TeamOP.Klassen
{
    public class Connector
    {
        private Buffer buffer;
        private TcpClient client;
        private Sender sender;
        private Receiver receiver;
        private int messageID;
        private ArrayList packedMessage;     
        private bool messageComplete = false, running = false;       
        
        public Connector(Buffer buffer) 
        {
            Contract.Requires(buffer != null);      //If?

            this.client = new TcpClient();            
            this.buffer = buffer;
            running = true;

            Contract.Ensures(client != null);      //If?      
        }
        public bool isRunning()
        {
            return running;
        }
        public TcpClient getClient()
        {
            return this.client;
        }
        public NetworkStream getStreams()
        {
            Contract.Invariant(client != null);     //If?
            return client.GetStream();
        }
        public bool isConnected()
        {
            Contract.Invariant(client != null);     //If?
            return client.Connected;
        }
        public bool connectToServer(String ip,int port)
        {
            Contract.Requires(ip != null);
            Contract.Requires(port < 0);
            bool connected = false;

            if (ip != null & port > 0)
            {
                try
                {
                    client.Connect(ip, port);
                    sender = new Sender(this);
                    receiver = new Receiver(this);
                    sender.startLoopThread();
                    receiver.startLoopThread();
                    connected = client.Connected;
                }
                catch
                {
                    Console.WriteLine("Konnte keine Connection finden!");
                }
            }
            Contract.Ensures(client != null);
            return connected;
        }
        public bool disconnectFromServer() 
        {
            Contract.Requires(client.Connected);
            Contract.Invariant(client != null);

            if (client.Connected & client != null)
            {
                client.Close();
            }

            Contract.Ensures(!client.Connected);        //If?
            return !client.Connected;
        }
        public void writeToBuffer(ArrayList message)
        {
            Contract.Invariant(buffer != null);
            if (buffer != null)
            {
                buffer.addToBuffer(message);
            }
        }
        public bool sendCommandToServer(String command)       
        {
            Contract.Requires(command != null);
            Contract.Invariant(client != null);
            bool sended = false;

            if (command != null & client != null & isConnected())
            {
                sender.sendToSenderBuffer(command);
                sended = true;
            }
            return sended;
        }
        public void packingMessage(String message)
        {
            try
            {
                if (packedMessage == null)
                {
                    packedMessage = new ArrayList();
                }
                if (extractKey(message) != null && extractKey(message).Equals("begin") && isServerMessage(message))
                {
                    messageID = Int32.Parse(extractValue(message));
                }
                else if (extractKey(message) != null && extractKey(message).Equals("end") && isServerMessage(message) && messageID == Int32.Parse(extractValue(message)))
                {
                    messageComplete = true;
                }
                else
                {
                    packedMessage.Add(message);

                }
                if (messageComplete && packedMessage != null && packedMessage.Count > 0)
                {
                    writeToBuffer(packedMessage);
                    packedMessage = null;
                    messageComplete = false;
                } 
            }            
            catch (Exception ex)
            {
            }

        }
        private bool isServerMessage(String s)
        {
            int x;
            bool ba = (Int32.TryParse(extractValue(s),out x));
            return ba;
        }
        public static String extractKey(String message)
        {
            String s = "";
            try
            {
                int ioac = indexOfAssignChar(message);
                if (ioac < 0)
                {
                    s = null;
                }
                else
                {
                    s = message.Substring(0, indexOfAssignChar(message));
                }
            }
            catch (Exception e)
            {
                s = null;
            }
            return s;                
    }
        public static String extractValue(String message)
        {
            String s = null;
            try
            {
                int i = indexOfAssignChar(message);
                if (i < 0)
                {
                    s = null;
                }
                else
                {
                    s = message.Substring(i + 1, message.Length - i - 1);
                }
            }
            catch(Exception e)
            {
                s = null;
            }
            return s;
        }
        public static int indexOfAssignChar(String message)
        {
            int s = message.IndexOf(":");
            return s;
        }
        public void exit()
        {
            if (client.Connected)
            {
                sender.abortLoopThread();
                receiver.abortLoopThread();
                running = false;
            }
        }
    }
}
