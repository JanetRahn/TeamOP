using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Client_TeamOP.Klassen
{
    class Sender : ClientThread
    {
       private StreamWriter sw;
       private Queue<String> senderStack = new Queue<String>();

        public Sender(Connector c) : base(c)
        {
           
            this.parent = c;
            sw = new StreamWriter(getStream());
            
            
        }

        protected override void loop()
        {
            
            while (this.active && parent.isRunning())
            {
                                
                if (parent.isConnected())
                {
                    if (getStream() != null && getStream().CanWrite)
                    {
                        if (senderStack.Count > 0)
                        {
                            sw.WriteLine(senderStack.Dequeue());
                            sw.Flush();
                        }
                    }                
                }                
            }
        }
        public override void start()
        {            
            base.start();
        }

        public override void stop()
        {
            base.stop();
        }

        public void sendToSenderBuffer(String sendToServerMessage)
        {
            
            senderStack.Enqueue(sendToServerMessage);
        }
    }
}
