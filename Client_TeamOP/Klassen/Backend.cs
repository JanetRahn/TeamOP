﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.Contracts;
using System.Runtime.InteropServices;
using System.Threading;

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
        private int myID,playersOnline;
        private long serverOnlineTime;
        private int serverVersion;
        private bool autoPilot;
        private Autowalk autowalk;

       public Backend(GUI gui)
        {
           map = new Map(1, 1);
           log = new List<String>();
           Buffer b = new Buffer();
           connector = new Connector(b);
           parser = new Parser(b,this);
           connector.connectToServer("127.0.0.1", 666);
           autowalk = new Autowalk(map, positionableHuman, this);
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
            if (message.StartsWith("/challenge"))
            {
                Positionable enemy = playerOnMyPos()[0];

                if (message.EndsWith("skrim"))
                {
                    challengePlayer("skrim", enemy);
                }
                else if (message.EndsWith("dragon"))
                {
                    challengePlayer("dragon", enemy);
                }
                else if (message.EndsWith("shunt"))
                {
                    challengePlayer("shunt", enemy);
                }
                
                
                message = null;
            }
            else if (!message.StartsWith("/"))
            {
                message = "ask:say:" + message;                
            }
            else
            {
                log.Add(message);
                message = message.TrimStart('/');
            }
            sended = connector.sendCommandToServer(message);
            refreshGui();
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
                if (positionableHuman.Count != 0)
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
                        if (i == (positionableHuman.Count - 1))
                        {
                            positionableHuman.Add(positionable);
                            log.Add("New Player " + positionable.getDescription() + " arrived at" + positionable.getX() + "/" + positionable.getY() + "!");
                            status = true;
                            break;
                        }
                    }
                }
                else
                {
                    positionableHuman.Add(positionable);
                    log.Add("New Player " + positionable.getDescription() + " arrived at" + positionable.getX() + "/" + positionable.getY() + "!");
                    status = true;
                }
                positionable.setObserver(autowalk);
                autowalk.setObsPlayer(positionable);
                refreshGui();                
            }
            else
            {
                status = false;
            }                        
            positionable.callTheObserverForChanges();
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

        public void setPlayersOnline(int playersCount)
        {
            if (playersCount >= 0)
            {
                this.playersOnline = playersCount;
            }
        }

        public void setServerVersion(int version)
        {
            if (version >= 0)
            {
                this.serverVersion = version;
            }
        }

        public void setServerOnlineTime(int onlineTime)
        {
            if (onlineTime >= 0)
            {
                this.serverOnlineTime = onlineTime;
            }
        }

        public void addToLog(String logMessage)
        {
            if (logMessage != null & log != null)
            {
                log.Add(logMessage);
            }
            refreshGui();
        }

        public bool storeDragon(Positionable positionable)
        {
            Boolean status = false;
            Contract.Requires(this.positionableDragon != null);
            Contract.Requires(positionable != null);
            if (positionableDragon != null && positionable != null)
            {
                if (positionableDragon.Count != 0)
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
                        else if (i == (positionableDragon.Count - 1))
                        {
                            positionableDragon.Add(positionable);
                            log.Add("New Dragon spawn at " + positionable.getX() + "/" + positionable.getY() + "!");
                            status = true;
                            break;
                        }
                    }
                }
                else
                {
                    positionableDragon.Add(positionable);
                    log.Add("New Dragon spawn at " + positionable.getX() + "/" + positionable.getY() + "!");
                    status = true;
                }
                refreshGui();
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

            connector.sendCommandToServer("ask:mv:up");
                
                status=true;            
            return status;
        }

        public Boolean moveDown()
        {
            Boolean status = false;

            connector.sendCommandToServer("ask:mv:dwn");
                
                status=true;
            
            return status;
        }

        public Boolean moveRight()
        {
            Boolean status = false;

            connector.sendCommandToServer("ask:mv:rgt");
                
                status=true;
            
            return status;
        }

        public Boolean moveLeft()
        {
            Boolean status=false;
            
                connector.sendCommandToServer("ask:mv:lft");
                
                status=true;
            
            return status;
        }

        public void challengePlayer(String game, Positionable enemy)
        {
            if(game.Equals("dragon")){
                connector.sendCommandToServer("ask:chal:dragon:"+enemy.getID());
            }else if(game.Equals("skrim")){
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
            while(this.map == null)
            {

            }
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
            autowalk.setMap(this.map);
            refreshGui();
        }

        internal void setMyID(int id)
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
                if (p.getX() == x && p.getY() == y & p.getID() != myID)
                {                    
                    listP.Add(p);
                }
            }
            return listP;
        }

        public void refreshGui()
        {
            if (gui.Created)
            {
                gui.Invoke(gui.myDelegate);
            }
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

        public void exit()
        {
            connector.exit();
            parser.exit();
        }

        public void autowalkGotoField(int x, int y)
        {
            autowalk.init(x, y, positionableHuman[0]);
            autowalk.playerHasBeenMoved(positionableHuman[0]);
        }

        public Positionable getMe()
        {
            Positionable me = null;

            foreach (Positionable p in positionableHuman)
            {
                if (p.getID() == myID)
                {
                    me = p;
                }
            }
            return me;
        }

        public void startMinigame(String type)
        {
            currentGame = new Minigame(type);
            gui.Invoke(gui.myDelegatePopUp);
        }

        public void sendDecision(String decision)
        {
            connector.sendCommandToServer("ask:set:" + currentGame.getGameType() + ":" + decision);
        }

        public String getMinigameType()
        {
            return currentGame.getGameName();
        }

        
    }
}
