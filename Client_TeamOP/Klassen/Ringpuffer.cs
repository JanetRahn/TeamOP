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
        private ArrayList[] buffer;
        private int head;
        private int tail;
        private int length;
        private int message;
        private int bufferSize;
        

        public Ringpuffer(int bufferSize)
        {
              buffer = new ArrayList[bufferSize];
              this.bufferSize = bufferSize;
              head = 0;
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
            ArrayList dequeued = null;
            if (tail != head)
            {
                dequeued = buffer[tail];
                buffer[tail] = null;
            } 
            else { tail = NextPosition(tail); }

            if (dequeued != null)
            {
                length--;
            }
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
            {
                tail = NextPosition(tail);
            }
            else
            {
                length++;
            }
        }
    }
}
