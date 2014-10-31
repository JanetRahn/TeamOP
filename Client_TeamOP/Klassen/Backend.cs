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
        private Parser parser;
        private GUI gui;
        private List<Positionable> positionable_Human;
        private List<Positionable> positionable_Dragon;
        private Map map;
        private MiniGame currentGame;
        private Boolean isMiniGame;
        private List<String> log;

        public bool sendCommand(String message)
        {
            Contract.Requires(message != null);
            Contract.Invariant(connector != null);
            return false;
        }

        public bool storeHuman(Positionable positionable)
        {
            Contract.Requires(this.positionable_Human != null);
            Contract.Requires(positionable != null);
            return false;
        }

        public void deleteHuman(Positionable positionable)
        {
            Contract.Requires(this.positionable_Human != null);
            Contract.Requires(positionable != null);
        }

        public List<Positionable> getPositionableHumans()
        {

        }

        public bool storeDragon(Positionable positionable)
        {
            Contract.Requires(this.positionable_Dragon != null);
            Contract.Requires(positionable != null);
            return false;
        }

        public void deleteDragon(Positionable positionable)
        {
            Contract.Requires(this.positionable_Dragon != null);
            Contract.Requires(positionable != null);
        }

        public List<Positionable> getPositionableDragon()
        {

        }

        public Boolean moveUp()
        {
            return true;
        }

        public Boolean moveDown()
        {
            return true;
        }

        public Boolean moveRight()
        {
            return true;
        }

        public Boolean moveLeft()
        {
            return true;
        }

        public void challengePlayer()
        {

        }

        public void rename()
        {

        }

        public void connectServer()
        {

        }

        public Boolean isConnected()
        {
            return false;
        }

        public void disconnectServer()
        {

        }

        public String getCurrentMiniGame()
        {
            return "";
        }

        public Map getMap()
        {
            return map;
        }

        public void interactMiniGame(String action)
        {
        }

        public Boolean isMiniGame()
        {
            return false;
        }

        public List<String> getLog()
        {
            return log;
        }
    }
}
