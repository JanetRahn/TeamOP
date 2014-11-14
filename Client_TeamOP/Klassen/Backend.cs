using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.Contracts;

namespace Client_TeamOP.Klassen
{
    public class Backend : IBackend
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

        public bool sendChat(String message) {
        return false;
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

        public List<IPositionable> getPositionableHumans() //getPlayer
        {
            List<IPositionable> humans = new List<IPositionable>();
            Positionable p = new Positionable(0, 1);
            humans.Add(p);
            return humans; 
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

        public List<IPositionable> getPositionableDragon()  //getDragons
        {
            List<IPositionable> dragons = new List<IPositionable>();
            Positionable p = new Positionable(0,1);
            dragons.Add(p);
            return dragons; 
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

        public IMap[][] getMap() //getMap
        {
            int size = 10;
            // init
            IMap[][] map = new IMap[size][];
            for(int i = 0; i < size; i++) {
                map[i] = new IMap[size];
            }
            Random r = new Random();
            for(int x = 0; x < size; x++) {
                for(int y = 0; y < size; y++) {
                    List<MapEnum> attr = new List<MapEnum>();
                    switch(r.Next(0, 5)) {
                        case 0:
                            attr.Add(MapEnum.WATER);
                            break;
                        case 1:
                            attr.Add(MapEnum.HUNTABLE);
                            attr.Add(MapEnum.FOREST);
                            break;
                        case 2:
                            attr.Add(MapEnum.FOREST);
                            break;
                        case 3:
                            attr.Add(MapEnum.UNWALKABLE);
                            break;
                        case 4:
                            break;

                    }
                    map[x][y] = new Field(x, y, attr);
                }
            }
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
    }
}
