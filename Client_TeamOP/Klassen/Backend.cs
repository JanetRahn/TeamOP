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
        private int myID;


       public Backend(GUI gui)
        {
            Buffer b = new Buffer();
           connector = new Connector(b);
           parser = new Parser(b,this);
           connector.connectToServer("127.0.0.1", 666);           
           log = new List<String>();
           if (gui != null)
            {
                this.gui = gui;
                positionableHuman=new List<Positionable>();
                positionableDragon=new List<Positionable>();
            }
        }
        public bool sendCommand(String message)
        {         
            Contract.Requires(message != null);
            Contract.Invariant(connector != null);
            bool sended;
            log.Add(message);
            if (!message.StartsWith("/"))
            {
                message = "ask:say:" + message;
            }
            else
            {
                message = message.TrimStart('/');
            }
            sended = connector.sendCommandToServer(message);
            gui.refreshGui();
            message = null;
            return sended;

        }

        public bool storeHuman(Positionable positionable)
        {
            Boolean status = false;
            Contract.Requires(this.positionableHuman != null);
            Contract.Requires(positionable != null);
            if (positionableHuman != null && positionable != null)
            {
                for (int i = 0; i < positionableHuman.Count; i++)
                {
                    if (positionableHuman[i].Equals(positionable))
                    {
                        positionableHuman.RemoveAt(i);
                        positionableHuman.Insert(i, positionable);
                        status = true;
                        break;
                    }
                    if(i==(positionableHuman.Count-1))
                    {
                        positionableHuman.Add(positionable);
                        log.Add("New Player "+positionable.getDescription()+" arrived at"+positionable.getX()+"/"+positionable.getY()+"!");
                        status=true;
                        break;
                    }
                }
                gui.Refresh();
            }
            else
            {
                status = false;
            }
            return status;
        }

        public void deleteHuman(Positionable positionable)
        {
            Contract.Requires(this.positionableHuman != null);
            Contract.Requires(positionable != null);
            if (positionableHuman != null && positionable != null)
            {
                for (int i = 0; i < positionableHuman.Count; i++)
                {
                    if (positionableHuman[i].Equals(positionable))
                    {
                        positionableHuman.RemoveAt(i);
                        log.Add("Player "+positionable.getDescription()+" leave!");
                    }
                }
            }
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
            Boolean status = false;
            Contract.Requires(this.positionableDragon != null);
            Contract.Requires(positionable != null);
            if (positionableDragon != null && positionable != null)
            {
                for (int i = 0; i < positionableDragon.Count; i++)
                {
                    if (positionableDragon[i].Equals(positionable))
                    {
                        positionableDragon.RemoveAt(i);
                        positionableDragon.Insert(i, positionable);
                        status = true;
                        break;
                    }
                    else if(i==(positionableDragon.Count-1))
                    {
                        positionableDragon.Add(positionable);
                        log.Add("New Dragon spawn at "+positionable.getX()+"/"+positionable.getY()+"!");
                        status=true;
                        break;
                    }
                }
                gui.Refresh();
            }
            else
            {
                status = false;
            }
                return status;
        }

        public void deleteDragon(Positionable positionable)
        {
            Contract.Requires(this.positionableDragon != null);
            Contract.Requires(positionable != null);
            if (positionableDragon != null && positionable != null)
            {
                for (int i = 0; i < positionableDragon.Count; i++)
                {
                    if (positionableDragon[i].Equals(positionable))
                    {
                        positionableDragon.RemoveAt(i);
                        log.Add("Dragon on "+positionable.getX()+"/"+positionable.getY()+" died!");
                    }
                }
            }
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
            Boolean status = false;
            if ((positionableHuman.First().getY() - 1) >= 0)
            { 
                connector.sendCommandToServer("ask:mv:up");
                positionableHuman.First().setX(positionableHuman.First().getX());
                positionableHuman.First().setY(positionableHuman.First().getY()-1);
                this.gui.refreshGui();
                status=true;
            }
            return status;
        }

        public Boolean moveDown()
        {
            Boolean status = false;
            if ((positionableHuman.First().getY() + 1) <= map.getHigh()-1) { 
                connector.sendCommandToServer("ask:mv:dwn");
                this.gui.refreshGui();
                status=true;
            }
            return status;
        }

        public Boolean moveRight()
        {
            Boolean status = false;
            if ((positionableHuman.First().getX() + 1) <= map.getWidth()-1)
            { 
                connector.sendCommandToServer("ask:mv:rgt");
                this.gui.refreshGui();
                status=true;
            }
            return status;
        }

        public Boolean moveLeft()
        {
            Boolean status=false;
            if ((positionableHuman.First().getX() - 1) >= 0)
            { 
                connector.sendCommandToServer("ask:mv:lft");
                this.gui.refreshGui();
                status=true;
            }
            return status;
        }

        public void challengePlayer(String game, Positionable enemy)
        {
            if(game.Equals("dhunt")){
                connector.sendCommandToServer("ask:chal:dhunt:"+enemy.getID());
            }else if(game.Equals("skirm")){
                connector.sendCommandToServer("ask:chal:skirm:"+enemy.getID());
            }else if(game.Equals("shunt")){
                connector.sendCommandToServer("ask:chal:shunt:"+enemy.getID());
            }
        }

        public void rename(String newName)
        {
            if (this.connector.isConnected())
            {
                connector.sendCommandToServer("ask:rn:" + newName);
            }
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
            String games = "";
            int x = 0;
            int y = 0;
            foreach (Positionable p in positionableHuman)
            {
                if (p.getID() == myID)
                {
                    x = p.getX();
                    y = p.getY();
                    break;
                }
            }
            Field f = map.getFieldAt(x, y);
            if (this.playerOnMyPos().Count >= 2)
            {
                if (f.isHuntable())
                {
                    games += "Staghunt/";
                }
                if (this.dragonOnMyPos().Count>0)
                {
                    games+="Dragonhunt/";
                }
                games += "Skirmish";
            }
            return games;
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
            currentGame.interaction(action);
        }

        public List<String> getLog()
        {
            return log;
        }

        public void storeMapcell(Field cell)
        {
            map.setField(cell.getX(), cell.getY(), cell);
        }

        internal void storeMap(Map map)
        {
            this.map = map;
            gui.Refresh();
        }

        internal void setMyId(int id)
        {
            this.myID=id;
        }

        public List<Positionable> playerOnMyPos()
        {
            List<Positionable> listP = new List<Positionable>();
            int x = 0;
            int y = 0;
            foreach (Positionable p in positionableHuman)
            {
                if (p.getID() == myID)
                {
                    x = p.getX();
                    y = p.getY();
                    break;
                }
            }
            foreach (Positionable p in positionableHuman)
            {
                if (p.getX() == x && p.getY() == y)
                {
                    listP.Add(p);
                }
            }
            return listP;
        }

        public List<Positionable> dragonOnMyPos()
        {
            List<Positionable> listD = new List<Positionable>();
            int x = 0;
            int y = 0;
            foreach (Positionable p in positionableDragon)
            {
                if (p.getID() == myID)
                {
                    x = p.getX();
                    y = p.getY();
                    break;
                }
            }
            foreach (Positionable p in positionableDragon)
            {
                if (p.getX() == x && p.getY() == y)
                {
                    listD.Add(p);
                }
            }
            return listD;
        }
    }
}
