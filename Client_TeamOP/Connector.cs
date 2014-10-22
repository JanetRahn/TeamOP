using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Client_TeamOP
{
    public class Connector
    {
        Parser buffer;
        TcpClient client;

        public Connector() 
        { 
        }

        public NetworkStream getStreams()
        {
            return client.GetStream();
        }
        public bool isConnected()
        {
            return client.Connected;
        }
        public void connectToServer(String ip,int port)
        {

        }

        public bool disconnectFromServer() 
        {
            return false;
        }

        public void writeToBuffer(String message)
        {

        }

        public bool readFromStream()
        {

            return false;
        }

        public bool sendCommandToServer(String command)
        {
            return false;
        }
    }
}
