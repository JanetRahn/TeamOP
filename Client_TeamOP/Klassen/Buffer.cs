using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.Contracts;
using System.Collections;
using System.Threading;
using System.Collections.Concurrent;

namespace Client_TeamOP.Klassen
{
    public class Buffer {
        
        private static readonly int bufferMaxSize = 20;
        private Queue<ArrayList> buffer = new Queue<ArrayList>(bufferMaxSize);         

        public Buffer(){
            Contract.Ensures(buffer != null);
            if (buffer == null)
            {
                buffer = new Queue<ArrayList>(bufferMaxSize);
            }
        }
        public void addToBuffer(ArrayList message){
           
            Contract.Requires(message != null);
            Contract.Invariant(buffer != null);

            if (message != null & buffer != null)
            {
                Monitor.Enter(buffer);

                while (buffer.Count == bufferMaxSize)
                {
                    Monitor.Wait(buffer);
                }

                buffer.Enqueue(message);

                Monitor.PulseAll(buffer);
                Monitor.Exit(buffer);
                

                Console.WriteLine("Addet ArrayList");
            }
        }

        public ArrayList getMessageFromBuffer(){
            Contract.Invariant(buffer != null); 
            ArrayList message = null;

            if (buffer != null)
            {
                Monitor.Enter(buffer);
                    while (buffer.Count == 0)
                    {
                        Monitor.Wait(buffer);
                    }
                    message = buffer.Dequeue();

                Monitor.PulseAll(buffer);
                Monitor.Exit(buffer);                
            }
            return message;
        }

        public bool isBufferFull() {
            Contract.Invariant(buffer != null);
            bool full = false;

            if (buffer != null)
            {

                if (buffer.Count == bufferMaxSize)
                {
                    full = true;
                }
            }
                return full;            
        }

        public bool isBufferEmpty(){
            Contract.Invariant(buffer != null);
            bool empty = false;

            if (buffer != null)
            {

                if (buffer.Count == 0)
                {
                    empty = true;
                }
            }
            return empty; 
        }

    }
}
