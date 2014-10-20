using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client_TeamOP
{
    interface IBuffer
    {
        public void addToBuffer(String message);
        public void readFromBuffer();
        public bool status();

    }
}
