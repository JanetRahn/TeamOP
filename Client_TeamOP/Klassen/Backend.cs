using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.Contracts;

namespace Client_TeamOP.Klassen
{
    public class Backend
    {
        private Connector connector;
        private Parser parser;
        private GUI gui;
        private List<Positionable> positionableHuman;
        private List<Positionable> positionableDragon;
        private Map map;
        private Minigame currentGame;
        private bool isMiniGame;
        private List<String> log;


        public Backend()
        {
            connector = new Connector(new Buffer());
            connector.connectToServer("127.0.0.1", 666);
        }
        public bool sendCommand(String message)
        {
            bool sended;
            Contract.Requires(message != null);
            Contract.Invariant(connector != null);
            sended = connector.sendCommandToServer(message);
            return sended;
        }

        public bool storeHuman(Positionable positionable)
        {
            Contract.Requires(this.positionableHuman != null);
            Contract.Requires(positionable != null);
            return false;
        }

        public void deleteHuman(Positionable positionable)
        {
            Contract.Requires(this.positionableHuman != null);
            Contract.Requires(positionable != null);
        }

        public List<Positionable> getPositionableHumans()
        {
            return null;
        }

        public bool storeDragon(Positionable positionable)
        {
            Contract.Requires(this.positionableDragon != null);
            Contract.Requires(positionable != null);
            return false;
        }

        public void deleteDragon(Positionable positionable)
        {
            Contract.Requires(this.positionableDragon != null);
            Contract.Requires(positionable != null);
        }

        public List<Positionable> getPositionableDragon()
        {
            return null;
        }

        public Boolean moveUp()
        {
            connector.sendCommandToServer("ask:mv:up");
            return true;
        }

        public Boolean moveDown()
        {
            connector.sendCommandToServer("ask:mv:dwn");
            return true;
        }

        public Boolean moveRight()
        {
            connector.sendCommandToServer("ask:mv:rgt");
            return true;
        }

        public Boolean moveLeft()
        {
            connector.sendCommandToServer("ask:mv:lft");
            return true;
        }

        public void challengePlayer(Positionable enemy)
        {

        }

        public void rename(String newName)
        {

        }

        public Positionable[] getOnPos()
        {
            return null;
        }

        public Boolean isConnected()
        {
            return connector.isConnected();
        }

        public void disconnectServer()
        {
            connector.disconnectFromServer();
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

        public bool isMiniGameRunning()
        {
            return isMiniGame;
        }

        public List<String> getLog()
        {
            return log;
        }

        public void putLog(String newLogElement)
        {
            log.Add(newLogElement);
        }
    }
}
