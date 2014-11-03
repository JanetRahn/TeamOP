using System;
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
        Buffer buffer;
        TcpClient client;
        Sender sender;
        Receiver receiver;
        int messageID;
        ArrayList packedMessage;
        bool messageComplete = false,running = false;       
        
        public Connector(Buffer buffer) 
        {
            Contract.Requires(buffer != null);
            this.client = new TcpClient();            
            this.buffer = buffer;
            running = true;
            Contract.Ensures(client != null);            
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
            Contract.Invariant(client != null);
            return client.GetStream();
        }
        public bool isConnected()
        {
            Contract.Invariant(client != null);
            return client.Connected;
        }
        public bool connectToServer(String ip,int port)
        {
            Contract.Requires(ip != null);
            Contract.Requires(port < 0);

            client.Connect(ip, port);
            sender = new Sender(this);
            receiver = new Receiver(this);
            sender.startLoopThread();
            receiver.startLoopThread();
            
            Contract.Ensures(client != null);
            return client.Connected;
        }
        public bool disconnectFromServer() 
        {
            Contract.Requires(client.Connected);
            Contract.Invariant(client != null);

            client.Close();

            Contract.Ensures(!client.Connected);
            return !client.Connected;
        }
        public void writeToBuffer(ArrayList message)
        {
            Contract.Invariant(buffer != null);
            buffer.addToBuffer(message);
        }
        public bool readFromStream()
        {
            Contract.Invariant(client != null);
            return false;
        }
        public bool sendCommandToServer(String command)
        {
            Contract.Requires(command != null);
            Contract.Invariant(client != null);
            return false;
        }
        public void packingMessage(String message)
        {
            try
            {
                if (packedMessage == null)
                {
                    packedMessage = new ArrayList();
                }
                if (extractKey(message).Equals("begin") && isServerMessage(message))
                {
                    messageID = Int32.Parse(extractValue(message));
                    Console.WriteLine("A new Message arrived");
                }
                else if (extractKey(message).Equals("end") && isServerMessage(message) && messageID == Int32.Parse(extractValue(message)))
                {
                    messageComplete = true;
                    Console.WriteLine("Message completed");
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
                Console.WriteLine("Fehler");
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
            try
            {
                return message.Substring(0, indexOfAssignChar(message));
            }
            catch
            {
                return "";
            }
        }
        public static String extractValue(String message)
        {
            try
            {
                int i = indexOfAssignChar(message);
                return message.Substring(i + 1, message.Length - i - 1);
            }
            catch
            {
                return "";
            }
        }
        public static int indexOfAssignChar(String message)
        {
            return message.IndexOf(":");
        }


    }
}
