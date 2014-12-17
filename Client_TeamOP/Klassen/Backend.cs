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
            if (message.StartsWith("/autowalk"))
            {
                //setAutoPilot();
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
                this.gui.refreshGui();
                status=true;            
            return status;
        }

        public Boolean moveDown()
        {
            Boolean status = false;

            connector.sendCommandToServer("ask:mv:dwn");
                refreshGui();
                status=true;
            
            return status;
        }

        public Boolean moveRight()
        {
            Boolean status = false;

            connector.sendCommandToServer("ask:mv:rgt");
                refreshGui();
                status=true;
            
            return status;
        }

        public Boolean moveLeft()
        {
            Boolean status=false;
            
                connector.sendCommandToServer("ask:mv:lft");
                refreshGui();
                status=true;
            
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
                if (p.getX() == x && p.getY() == y)
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


        //AutoWalk Method

        //[DllImport("MapDll.dll", CallingConvention = CallingConvention.Cdecl)]
        //static extern IntPtr findPath(int from, int to, int[] map, int mapw, int maph, int plength);

        //[DllImport("MapDll.dll", CallingConvention = CallingConvention.Cdecl)]
        //static extern void freeArray(IntPtr pointer);

        //public void prefAutoWalk(int x, int y)
        //{
        //    autoWalk(map.getFieldAt(x, y), map.getFieldAt(positionableHuman[0].getX(), positionableHuman[0].getY()));
        //}

        //public bool autoWalk(Field start, Field end)
        //{
        //    searchMiniGame();
        //    int[,] mapConvertToMinimal = new int[map.getWidth(), map.getHigh()];

        //    for (int i = 0; i < map.getWidth(); i++)
        //    {
        //        for (int j = 0; j < map.getHigh(); j++)
        //        {
        //            if (map.getFieldAt(i, j).isWater() | !map.getFieldAt(i, j).isWalkable())
        //            {
        //                mapConvertToMinimal[i, j] = 1;
        //            }
        //            else
        //            {
        //                mapConvertToMinimal[i, j] = 0;
        //            }
        //        }
        //    }

        //    int[] path = new int[map.getWidth() * map.getHigh()];

        //    int[] convertMap = convertMapToPointer(mapConvertToMinimal);

        //    bool convertable;

        //    if (convertMap[findPoint(start)] == 0 & !(start == null & end == null & convertMap == null & map == null))
        //    {
        //        convertable = true;
        //        IntPtr pointer = findPath(findPoint(start), findPoint(end), convertMap, map.getWidth(), map.getHigh(), (map.getWidth() * map.getHigh()));
        //        Marshal.Copy(pointer, path, 0, path.Length);
        //    }
        //    else
        //    {
        //        convertable = false;
        //    }




        //    if (convertable)
        //    {
        //        int count = 1;
        //        foreach (int p in path)
        //        {
        //            if (p >= 0)
        //            {
        //                if (!(positionableHuman[0].getX() == findPoint(p)[0] & positionableHuman[0].getY() == findPoint(p)[1]))
        //                {
        //                    int[] test = findPoint(p);
        //                    Console.WriteLine(test[0] + " - " + test[1]);
        //                    walkAutomatic(test[0], test[1], path, count);
        //                    count++;
        //                }
        //            }
        //        }
        //    }
        //    return true;
        //}

        //private void walkAutomatic(int x, int y, int[] path, int count)
        //{
        //    if (count > 0)
        //    {
        //        if (x > findPoint(path[count - 1])[0])
        //        {
        //            moveRight();
        //        }
        //        else if (x < findPoint(path[count - 1])[0])
        //        {
        //            moveLeft();
        //        }
        //        else if (y > findPoint(path[count - 1])[1])
        //        {
        //            moveDown();
        //        }
        //        else if (y < findPoint(path[count - 1])[1])
        //        {
        //            moveUp();
        //        }
        //    }
        //}

        //private int[] findPoint(int index)
        //{
        //    int[] coords = new int[2];
        //    int x = 0, y = 0;
        //    while (index > map.getWidth())
        //    {
        //        index = index - map.getWidth();
        //        y++;
        //    }
        //    x = index;
        //    coords[0] = x;
        //    coords[1] = y;
        //    return coords;
        //}

        //private int findPoint(Field point)
        //{
        //    int convertedPoint = -1;
        //    convertedPoint = point.getX() + point.getY() * map.getWidth();
        //    return convertedPoint;
        //}

        //private int[] convertMapToPointer(int[,] convMap)
        //{
        //    int[] tmp = new int[map.getWidth() * map.getHigh()];
        //    for (int i = 0; i < map.getWidth(); i++)
        //    {
        //        for (int j = 0; j < map.getHigh(); j++)
        //        {
        //            tmp[i * map.getWidth() + j] = convMap[j, i];
        //        }
        //    }
        //    return tmp;
        //}

        // AutoPilot Test

        //private void setAutoPilot()
        //{
        //    if (!autoPilot)
        //    {
        //        autoPilot = true;
        //    }
        //    else
        //    {
        //        autoPilot = false;
        //    }

        //    bool arrived = false;
            
        //        if (!isMiniGame)
        //        {                    
        //            Field nextMiniGame = searchMiniGame();
        //            arrived = autoWalk(nextMiniGame, getMyPositionField());                    
        //            Console.WriteLine("Geht zu Minigame!");
        //        }
            
            
        //}

        //private Field searchMiniGame()
        //{
        //    Field nextMiniGameHuntable = null;
        //    Positionable nextMiniGameDragon = null;
        //    Positionable nextMiniGamePlayer = null;
        //    Field nextMiniGame = null;

        //    int[] myPos = getMyPositionArray();
        //    int minX = myPos[0], minY = myPos[1];

        //    foreach (Field f in map.getField())
        //    {
        //        if (f.isHuntable())
        //        {
        //            if ((f.getX() >= myPos[0]) & (f.getX() - myPos[0] <= minX))
        //            {
        //                int tmp = minX;
        //                minX = f.getX() - myPos[0];

        //                if ((f.getY() >= myPos[1]) & (f.getY() - myPos[1] <= minY))
        //                {
        //                    minY = f.getY() - myPos[1];
        //                    nextMiniGameHuntable = f;
        //                }
        //                else if ((f.getY() <= myPos[1]) & (myPos[1] - f.getY() <= minY))
        //                {
        //                    minY = myPos[1] - f.getY();
        //                    nextMiniGameHuntable = f;
        //                }
        //                else
        //                {
        //                    minX = tmp;
        //                }
        //            }
        //            else if ((f.getX() <= myPos[0]) & (myPos[0] - f.getX() <= minX))
        //            {
        //                int tmp = minX;
        //                minX = myPos[0] - f.getX();

        //                if ((f.getY() >= myPos[1]) & (f.getY() - myPos[1] <= minY))
        //                {
        //                    minY = f.getY() - myPos[1];
        //                    nextMiniGameHuntable = f;
        //                }
        //                else if ((f.getY() <= myPos[1]) & (myPos[1] - f.getY() <= minY))
        //                {
        //                    minY = myPos[1] - f.getY();
        //                    nextMiniGameHuntable = f;
        //                }
        //                else
        //                {
        //                    minY = tmp;
        //                }
        //            }
        //        }
        //    }

        //    minX = myPos[0];
        //    minY = myPos[1];

        //    foreach (Positionable d in positionableDragon)
        //    {
        //        if ((d.getX() >= myPos[0]) & (d.getX() - myPos[0] <= minX))
        //        {
        //            int tmp = minX;
        //            minX = d.getX() - myPos[0];

        //            if ((d.getY() >= myPos[1]) & (d.getY() - myPos[1] <= minY))
        //            {
        //                minY = d.getY() - myPos[1];
        //                nextMiniGameDragon = d;
        //            }
        //            else if ((d.getY() <= myPos[1]) & (myPos[1] - d.getY() <= minY))
        //            {
        //                minY = myPos[1] - d.getY();
        //                nextMiniGameDragon = d;
        //            }
        //            else
        //            {
        //                minX = tmp;
        //            }
        //        }
        //        else if ((d.getX() <= myPos[0]) & (myPos[0] - d.getX() <= minX))
        //        {
        //            int tmp = minX;
        //            minX = myPos[0] - d.getX();

        //            if ((d.getY() >= myPos[1]) & (d.getY() - myPos[1] <= minY))
        //            {
        //                minY = d.getY() - myPos[1];
        //                nextMiniGameDragon = d;
        //            }
        //            else if ((d.getY() <= myPos[1]) & (myPos[1] - d.getY() <= minY))
        //            {
        //                minY = myPos[1] - d.getY();
        //                nextMiniGameDragon = d;
        //            }
        //            else
        //            {
        //                minY = tmp;
        //            }
        //        }
        //    }

        //    minX = myPos[0];
        //    minY = myPos[1];
        //    foreach (Positionable d in positionableHuman)
        //    {
        //        if ((d.getX() >= myPos[0]) & (d.getX() - myPos[0] <= minX))
        //        {
        //            int tmp = minX;
        //            minX = d.getX() - myPos[0];

        //            if ((d.getY() >= myPos[1]) & (d.getY() - myPos[1] <= minY))
        //            {
        //                minY = d.getY() - myPos[1];
        //                nextMiniGamePlayer = d;
        //            }
        //            else if ((d.getY() <= myPos[1]) & (myPos[1] - d.getY() <= minY))
        //            {
        //                minY = myPos[1] - d.getY();
        //                nextMiniGamePlayer = d;
        //            }
        //            else
        //            {
        //                minX = tmp;
        //            }
        //        }
        //        else if ((d.getX() <= myPos[0]) & (myPos[0] - d.getX() <= minX))
        //        {
        //            int tmp = minX;
        //            minX = myPos[0] - d.getX();

        //            if ((d.getY() >= myPos[1]) & (d.getY() - myPos[1] <= minY))
        //            {
        //                minY = d.getY() - myPos[1];
        //                nextMiniGamePlayer = d;
        //            }
        //            else if ((d.getY() <= myPos[1]) & (myPos[1] - d.getY() <= minY))
        //            {
        //                minY = myPos[1] - d.getY();
        //                nextMiniGamePlayer = d;
        //            }
        //            else
        //            {
        //                minY = tmp;
        //            }
        //        }
        //    }

        //    Random r = new Random();
        //    int selected = r.Next(3);

        //    if (selected == 0)
        //    {
        //        nextMiniGame = nextMiniGameHuntable;
        //    }
        //    else if (selected == 1)
        //    {
        //        nextMiniGame = map.getFieldAt(nextMiniGameDragon.getX(), nextMiniGameDragon.getY());
        //    }
        //    else if (selected == 2)
        //    {
        //        nextMiniGame = map.getFieldAt(nextMiniGamePlayer.getX(), nextMiniGamePlayer.getY());
        //    }

        //    return nextMiniGame;
        //}

        //private int[] getMyPositionArray()
        //{
        //    int[] me = new int[2];
        //    me[0] = positionableHuman[0].getX();
        //    me[1] = positionableHuman[0].getY();
        //    return me;
        //}

        //private Field getMyPositionField()
        //{
        //    return map.getFieldAt(positionableHuman[0].getX(), positionableHuman[0].getY());
        //}
    }
}
