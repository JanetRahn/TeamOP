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


       public Backend(GUI gui)
        {
           connector = new Connector(new Buffer());
            connector.connectToServer("127.0.0.1", 666);
            if (gui != null)
            {
                this.gui = gui;
                positionableHuman=new List<Positionable>();
                positionableDragon=new List<Positionable>();
            }
            //Test
            map = new Map(10,10);
            map.creatTestMap();
            Field[,] field = map.getField();
            bool first = true;
            bool zweiter = false;
            for (int x = 0; x < map.getWidth(); x++)
            {
                if (zweiter)
                {
                    break;
                }
                for (int y = 0; y < map.getHigh(); y++)
                {
                    if (field[x, y].isWalkable())
                    {
                        if (first)
                        {
                            positionableHuman.Add(new Positionable(x, y, 0, 0, "Im a Human", "Human"));
                            first = false;
                        }
                        else
                        {
                            positionableDragon.Add(new Positionable(x, y, 1, 0, "Im a Dragon", "Dragon"));
                            break;
                            zweiter = true;
                        }
                    }
                }
            }
            this.gui.refreshGui();
            //End Test
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
            foreach (Positionable p in positionableHuman)
            {
                humans.Add(p);
            }           
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
            List<IPositionable> dragon = new List<IPositionable>();
            foreach (Positionable p in positionableDragon)
            {
                dragon.Add(p);
            }
            return dragon; 
        }

        public Boolean moveUp()
        {
            if ((positionableHuman.First().getY() - 1) >= 0)
            { 
                //connector.sendCommandToServer("ask:mv:up");
                positionableHuman.First().setX(positionableHuman.First().getX());
                positionableHuman.First().setY(positionableHuman.First().getY()-1);
                this.gui.refreshGui();
                return true;
            }
            else
            {
                return false;
            }
        }

        public Boolean moveDown()
        {
            if ((positionableHuman.First().getY() + 1) <= map.getHigh()) { 
                //connector.sendCommandToServer("ask:mv:dwn");
                positionableHuman.First().setX(positionableHuman.First().getX());
                positionableHuman.First().setY(positionableHuman.First().getY()+1);
                this.gui.refreshGui();
                return true;
            }
            else
            {
                return false;
            }
        }

        public Boolean moveRight()
        {
            if ((positionableHuman.First().getX() + 1) <= map.getWidth())
            { 
                //connector.sendCommandToServer("ask:mv:rgt");
                positionableHuman.First().setX(positionableHuman.First().getX()+1);
                positionableHuman.First().setY(positionableHuman.First().getY());
                this.gui.refreshGui();
                return true;
            }
            else
            {
                return false;
            }
        }

        public Boolean moveLeft()
        {
            if ((positionableHuman.First().getX() - 1) >= 0)
            { 
                //connector.sendCommandToServer("ask:mv:lft");
                positionableHuman.First().setX(positionableHuman.First().getX()-1);
                positionableHuman.First().setY(positionableHuman.First().getY());
                this.gui.refreshGui();
                return true;
            }
            else
            {
                return false;
            }
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

        public IMap[,] getMap() //getMap
        {
            Field[,] tmpmap = this.map.getField();
            int size = this.map.getWidth();
            IMap[,] map = new IMap[size,size];
            Random r = new Random();
            for(int x = 0; x < size; x++) {
                for(int y = 0; y < size; y++) {
                    List<MapEnum> attr = new List<MapEnum>();
                    if (tmpmap[x, y].isForest())
                    {
                        attr.Add(MapEnum.FOREST);  
                    }
                    if (tmpmap[x, y].isHuntable())
                    {
                        attr.Add(MapEnum.HUNTABLE);  
                    }
                    if (!tmpmap[x, y].isWalkable())
                    {
                        attr.Add(MapEnum.UNWALKABLE);  
                    }
                    if (tmpmap[x, y].isWater())
                    {
                        attr.Add(MapEnum.WATER);  
                    }               
                    map[x,y] = new Field(x, y, attr);
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
