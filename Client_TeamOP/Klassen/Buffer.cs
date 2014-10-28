using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.Contracts;

namespace Client_TeamOP
{
    public class Buffer {
        private List<String> buffer;
        public Buffer(){
            Contract.Ensures(buffer != null);
        }
        public void addToBuffer(String message){
            Contract.Requires(message != null);
            Contract.Invariant(buffer != null);
        }

        public String getMessageFromBuffer(){
            Contract.Invariant(buffer != null);
            return null;
        }

        public bool isBufferFull() {
            Contract.Invariant(buffer != null);
            return false;
        }

        public bool isBufferEmpty(){
            Contract.Invariant(buffer != null);
            return false;
        }

    }
}
