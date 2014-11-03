using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client_TeamOP.Klassen
{
    class Ringpuffer
    {
        ArrayList[] buffer;
        int head;
        int tail;
        int length;
        int message;
        int bufferSize;
        

        public Ringpuffer(int bufferSize)
        {
              buffer = new ArrayList[bufferSize];
              this.bufferSize = bufferSize;
              head = bufferSize - 1;
              message = 0;
        }

        public bool IsEmpty
        {
            get { return length == 0; }
        }

        public bool IsFull
        {
            get { return length == bufferSize; }
        }

        public ArrayList Dequeue()
        {                
                ArrayList dequeued = buffer[tail];
                tail = NextPosition(tail);
                length--;
                return dequeued;            
        }

        private int NextPosition(int position)
        {
            return (position + 1) % bufferSize;
        }

        public void Enqueue(ArrayList toAdd)        
        {            
                head = NextPosition(head);
                buffer[head] = toAdd;
                if (IsFull)
                    tail = NextPosition(tail);
                else
                    length++;            
        }
    }
}
