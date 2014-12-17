using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client_TeamOP.Klassen
{
    interface IObservable
    {        
        void callTheObserverForChanges();

        void setObserver(Autowalk observer);

    }
}
