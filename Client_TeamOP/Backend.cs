using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.Contracts;

namespace Client_TeamOP
{
    public class Backend
    {
        private Connector connector;
        private GUI gui;
        private List<Object> positionable;
        public bool sendToConnector(String message)
        {
            Contract.Invariant(connector != null);
            return false;
        }

        public bool storeEnemy(Object positionable)
        {
            Contract.Requires(this.positionable != null);
            Contract.Requires(positionable != null);
            return false;
        }

        public void deleteEnemey(Object positionable)
        {
            Contract.Requires(this.positionable != null);
            Contract.Requires(positionable != null);
        }


    }
}
