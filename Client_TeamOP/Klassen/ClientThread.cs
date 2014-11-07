using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Client_TeamOP.Klassen
{
     public abstract class ClientThread
     {
            private TcpClient client;
            protected Connector parent;
            protected bool active = false;

            private Thread loopThread;

            public void startLoopThread()
            {
                this.loopThread = new Thread(this.start);
                loopThread.Start();
            }

            public void abortLoopThread()
            {
                this.stop();
                this.loopThread.Abort();
            }

            protected TcpClient getClient()
            {
                return parent.getClient();
            }

            public ClientThread(Connector parent)
            {
                this.parent = parent;               
            }

            // TCP-Datenstrom 
            protected NetworkStream getStream()
            {
                return parent.getClient().GetStream();
            }

            protected abstract void loop();

            public virtual void start()
            {
                this.active = true;
                this.loop();
            }

            public virtual void stop()
            {
                active = false;
            }

        }
    }

