using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.Contracts;
using System.IO;

namespace Client_TeamOP
{
    public class Connector
    {
        Buffer buffer;
        TcpClient client;
        bool messageComplete;
        StreamWriter writeStream;
        StreamReader readStream;
        
        public Connector(Buffer buffer) 
        {
            Contract.Requires(buffer != null);
            this.client = new TcpClient();            
            this.buffer = buffer;
            Contract.Ensures(client != null);            
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
            Contract.Requires(port!=null);
            client.Connect(ip, port);
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

        public void writeToBuffer(String message)
        {
            Contract.Invariant(buffer != null);
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
    }
}
