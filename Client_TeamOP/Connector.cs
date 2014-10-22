using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.Contracts;

namespace Client_TeamOP
{
    public class Connector
    {
        Buffer buffer;
        TcpClient client;

        public Connector(Buffer buffer) 
        {
            Contract.Requires(buffer != null);
            Contract.Ensures(client != null);
            this.buffer = buffer;
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
        public void connectToServer(String ip,int port)
        {
            Contract.Requires(ip != null);
            Contract.Requires(port!=null);
            Contract.Ensures(client != null);
        }

        public bool disconnectFromServer() 
        {
            Contract.Requires(client.Connected);
            Contract.Invariant(client != null);
            Contract.Ensures(!client.Connected);
            return false;
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
