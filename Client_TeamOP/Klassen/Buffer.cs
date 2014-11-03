using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.Contracts;
using System.Collections;

namespace Client_TeamOP.Klassen
{
    public class Buffer {
        private Ringpuffer buffer = new Ringpuffer(10);

        public Buffer(){
            Contract.Ensures(buffer != null);            
        }
        public void addToBuffer(ArrayList message){
            Contract.Requires(message != null);
            Contract.Invariant(buffer != null);
            buffer.Enqueue(message);
            Console.WriteLine("Addet ArrayList");
        }

        public ArrayList getMessageFromBuffer(){
            Contract.Invariant(buffer != null);            
            return buffer.Dequeue();
        }

        public bool isBufferFull() {
            Contract.Invariant(buffer != null);
            return buffer.IsFull;
        }

        public bool isBufferEmpty(){
            Contract.Invariant(buffer != null);
            return buffer.IsEmpty;
        }

    }
}
