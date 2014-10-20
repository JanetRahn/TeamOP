using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client_TeamOP
{
    interface IParser
    {
        public String readFromBuffer();
        public void parseRule(String rule);
        public void dontKnowWhat(); //Aufgabe nicht ganz verstanden noch mal drüberschauen
    }
}
