using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client_TeamOP
{
    interface IConnector
    {
        public void connectTo(String ipAddress, int port);
        public void transferToBuffer(String message);
        public void sendCommandsToServer(String message);
    }
}
