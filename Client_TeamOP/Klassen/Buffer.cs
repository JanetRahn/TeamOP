﻿using System;
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
        private readonly object queueSync = new object(); 

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
                lock (queueSync)
                {
                    buffer.Enqueue(message);
                }
                Monitor.Pulse(queueSync);
                Console.WriteLine("Addet ArrayList");
            }
        }

        public ArrayList getMessageFromBuffer(){
            Contract.Invariant(buffer != null); 
            ArrayList message = null;
            
            if (buffer != null)
            {
                lock (queueSync)
                {
                    while (buffer.Count == 0)
                    {
                        Monitor.Wait(queueSync);
                    }
                    message = buffer.Dequeue();
                }
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
