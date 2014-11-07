using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Client_TeamOP.Klassen
{
    class Receiver : ClientThread
    {
        private StreamReader sr;

        public Receiver(Connector c) : base(c)
        {
            this.parent = c; 
            sr = new StreamReader(getStream());
        }

        protected override void loop()
        {
            while (this.active && parent.isRunning())
            {
                if (parent.isConnected())
                {
                    if (getStream() != null && getStream().CanRead)
                    {                            
                            try {
                            String messageLine = sr.ReadLine();
                            parent.packingMessage(messageLine);
                            }
                            catch (Exception ex) { Console.WriteLine("Fehler: " + ex.Message); }
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



    }
}
