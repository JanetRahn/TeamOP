using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.Contracts;
using System.Threading;
using System.Collections;

namespace Client_TeamOP.Klassen
{
    public class Parser
    {
        private Buffer buffer;
        private Backend backend;
        private Thread convertParser;

        private int counter;
        private ArrayList message;
        
        
        public Parser(Buffer buffer, Backend backend)
        {
            Contract.Requires(buffer != null);
            Contract.Requires(backend != null);

            this.backend = backend;
            this.buffer = buffer;
            counter = 0;
            convertParser = new Thread(new ThreadStart(sendToMethod));
            convertParser.Start();  
                  
        }
       
        public ArrayList readFromBuffer(){
            Contract.Requires(buffer != null);
            return buffer.getMessageFromBuffer();
        }
        public void sendToMethod()
        {
            //Thread.Sleep(500);
            while (this.convertParser.IsAlive)
            {
                if (message == null)
                {
                    message = readFromBuffer();
                }
                else if(message != null)
                {
                    parseS();
                    message = null;
                    counter = 0;
                }
            }
        }
        public void parseS()
        {
            Contract.Requires(message != null);
            parseServermes();
        }
        
        public void parseServermes(){
            Contract.Requires(message != null);
            if (message != null)
            {
                String tmpMsg = (String)message[counter++];

                if (tmpMsg.StartsWith("ans:"))
                {
                    counter--;
                    parseAnswer();
                }
                else
                {
                    tmpMsg = extractValue(tmpMsg);
                    switch (tmpMsg)
                    {
                        case "upd":
                            parseUpdate();
                            break;
                        case "del":
                            parseDelete();
                            break;
                        case "map":
                            parseMap();
                            break;
                        case "mes":
                            parseMessage();
                            break;
                        case "result":
                            parseResult();
                            break;
                        case "challenge":
                            parseChallenge();
                            break;
                        case "player":
                            counter--;
                            parsePlayer(false);
                            break;
                        case "yourid":
                            parseYourID();
                            break;
                        case "online":
                            parseOnline();
                            break;
                        case "ents":
                            parseEntities();
                            break;
                        case "players":
                            parsePlayers();
                            break;
                        case "server":
                            parseServer();
                            break;
                        case "time":
                            parseTime();
                            break;
                    }
                }
                if (counter != message.Count - 1)
                {
                    Console.WriteLine("Message parsing failed");
                }
            }
        }
        public void parseServer(){
            Contract.Requires(message != null);
            if (message != null)
            { 
                int serverVersion = Int32.Parse((String)message[counter++]);
                backend.setServerVersion(serverVersion);
            }
        }       //Backend Implementation
        public void parseResult(){
            if (message != null)
            {
                Contract.Requires(message != null);

                int round = -1, delay = -1;
                bool running = false;

                String tmpMsg = "";

                for (int i = 1; i <= 5; i++)
                {
                    tmpMsg = (String)message[counter];
                    if (i == 1)
                    {
                        round = Int32.Parse(extractValue(tmpMsg));
                    }
                    else if (i == 2)
                    {
                        running = bool.Parse(extractValue(tmpMsg));
                    }
                    else if (i == 3)
                    {
                        delay = Int32.Parse(extractValue(tmpMsg));
                    }
                    else if (i == 4)
                    {
                        counter++;
                        parseOpponent();
                    }
                    else if (i == 5)
                    {
                        parseOpponent();
                        counter = counter + 2;
                    }

                    if (!tmpMsg.Equals("end:result"))
                    {
                        counter++;
                    }
                }
            }

        }           //Backend Implementation saving?
        public void parseOpponent()
        {
            Contract.Requires(message != null);
            if (message != null)
            {
                String tmpMsg = "";
                int id = -1, points = -1, total = -1;
                String decision = "";
                counter++;

                for (int i = 1; i <= 4; i++)
                {
                    tmpMsg = (String)message[counter];
                    if (i == 1)
                    {
                        id = Int32.Parse(extractValue(tmpMsg));
                    }
                    else if (i == 2)
                    {
                        decision = extractValue(tmpMsg);
                    }
                    else if (i == 3)
                    {
                        points = Int32.Parse(extractValue(tmpMsg));
                    }
                    else if (i == 4)
                    {
                        total = Int32.Parse(extractValue(tmpMsg));
                    }
                    if (!tmpMsg.Equals("end:opponent"))
                    {
                        counter++;
                    }
                }
            }
        }
        public void parseChallenge()
        {
            Contract.Requires(message != null);
            if (message != null)
            {
                int id = -1;
                String type = "";
                bool accepted = false;

                String tmpMsg = "";
                for (int i = 1; i <= 3; i++)
                {
                    tmpMsg = (String)message[counter];
                    if (i == 1)
                    {
                        id = Int32.Parse(extractValue(tmpMsg));
                    }
                    else if (i == 2)
                    {
                        type = extractValue(tmpMsg);
                    }
                    else if (i == 3)
                    {
                        accepted = bool.Parse(extractValue(tmpMsg));
                    }

                    if (!tmpMsg.Equals("end:challenge"))
                    {
                        counter++;
                    }
                }
            }

        }       //Backend Implementation (Test?)
        public void parseDragon(bool delete)
        {
            Contract.Requires(message != null);
            if (message != null)
            {
                bool busy = false;
                int id = -1, x = -1, y = -1, count = 0;
                String type = null, desc = null;

                String tmpMsg = "";
                counter++;

                for (int i = 1; i <= 6; i++)
                {
                    tmpMsg = (String)message[counter];
                    if (i == 1)
                    {
                        id = Int32.Parse(extractValue(tmpMsg));
                    }
                    else if (i == 2)
                    {
                        type = extractValue(tmpMsg);
                    }
                    else if (i == 3)
                    {
                        busy = Boolean.Parse(extractValue(tmpMsg));
                    }
                    else if (i == 4)
                    {
                        desc = extractValue(tmpMsg);
                    }
                    else if (i == 5)
                    {
                        x = Int32.Parse(extractValue(tmpMsg));
                    }
                    else if (i == 6)
                    {
                        y = Int32.Parse(extractValue(tmpMsg));
                    }
                    if (!tmpMsg.Equals("end:dragon"))
                    {
                        counter++;
                    }
                }

                Positionable dragon = new Positionable(x, y, id, 0, desc, type);
                dragon.setBusy(busy);
                if (!delete)
                {
                    backend.storeDragon(dragon);
                }
                else
                {
                    backend.deleteDragon(dragon);
                }
            }
       }   //complete
        public void parsePlayer(bool delete)
        {
            Contract.Requires(message != null);
            if (message != null)
            {
                bool busy = false;
                int id = -1, x = -1, y = -1, points = -1;
                String type = null, desc = null;
                String tmpMsg = "";
                counter++;

                for (int i = 1; i <= 7; i++)
                {
                    tmpMsg = (String)message[counter];
                    if (i == 1)
                    {
                        id = Int32.Parse(extractValue(tmpMsg));
                    }
                    else if (i == 2)
                    {
                        type = extractValue(tmpMsg);
                    }
                    else if (i == 3)
                    {
                        busy = Boolean.Parse(extractValue(tmpMsg));
                    }
                    else if (i == 4)
                    {
                        desc = extractValue(tmpMsg);
                    }
                    else if (i == 5)
                    {
                        x = Int32.Parse(extractValue(tmpMsg));
                    }
                    else if (i == 6)
                    {
                        y = Int32.Parse(extractValue(tmpMsg));
                    }
                    else if (i == 7)
                    {
                        points = Int32.Parse(extractValue(tmpMsg));
                    }

                    if (!tmpMsg.Equals("end:player"))
                    {
                        counter++;
                    }
                }

                Positionable player = new Positionable(x, y, id, points, desc, type);
                player.setBusy(busy);

                if (!delete)
                {
                    backend.storeHuman(player);
                }
                else
                {
                    backend.deleteHuman(player);
                }
            }
        }   //complete
        public void parsePlayers()
        {
            Contract.Requires(message != null);
            if (message != null)
            {
                String tmpMsg = "";

                while (!tmpMsg.Equals("end:players"))
                {
                    tmpMsg = (String)message[counter];

                    if (extractValue(tmpMsg).Equals("player"))
                    {
                        parsePlayer(false);
                    }
                    if (!tmpMsg.Equals("end:players"))
                    {
                        counter++;
                    }
                }
            }
        }   // loop stay; unknown amount of players
        public void parseEntities()
        {
            Contract.Requires(message != null);
            if (message != null)
            {
                String tmpMsg = "";

                while (!tmpMsg.Equals("end:ents"))
                {
                    tmpMsg = (String)message[counter];

                    if (extractValue(tmpMsg).Equals("player"))
                    {
                        parsePlayer(false);
                    }
                    else if (extractValue(tmpMsg).Equals("dragon"))
                    {
                        parseDragon(false);
                    }
                    if (!tmpMsg.Equals("end:ents"))
                    {
                        counter++;
                    }
                }
            }
        }           //loop stays; unknown amount of entities
        public Field parseMapcell(bool mapExist)
        {    
            int row = 0, col = 0;
            List<MapEnum> props = new List<MapEnum>();
            Field cell = null;
            String tmpMsg = "";
            counter++;

            for (int i = 1; i <= 3; i++ )
            {
                tmpMsg = (String)message[counter];

                if (i==1)
                {
                    row = Int32.Parse(extractValue(tmpMsg));
                }
                else if (i==2)
                {
                    col = Int32.Parse(extractValue(tmpMsg));
                }
                else if (i==3)
                {
                    props = parseProperty();
                }

                cell = new Field(row, col, props);

                if (mapExist)
                {
                    backend.storeMapcell(cell);
                }
                if (!tmpMsg.Equals("end:cell"))
                {
                    counter++;
                }
            } 
      
            return cell;
        }   //loop stays; unknown amount of mapcells
        public void parseMap()
        {
            Contract.Requires(message != null);
            if (message != null)
            {
                String tmpMsg = "";
                int width = -1, height = -1;
                Map map = null;


                width = Int32.Parse(extractValue((String)message[counter++]));
                height = Int32.Parse(extractValue((String)message[counter++]));
                counter++;

                while (!tmpMsg.Equals("end:map"))
                {
                    tmpMsg = (String)message[counter];

                    if (!tmpMsg.Equals("end:cells") & !tmpMsg.Equals("end:map"))
                    {
                        if ((width > -1 & height > -1) & map == null)
                        {
                            map = new Map(width, height);
                        }
                        tmpMsg = (String)message[counter];
                        Field f = parseMapcell(false);
                        map.setField(f.getX(), f.getY(), f);
                        tmpMsg = (String)message[counter];

                    }
                    if (!tmpMsg.Equals("end:map"))
                    {
                        counter++;
                    }
                }
                backend.storeMap(map);
            }
        }       //loop stays; unknown amount of mapcells incoming
        public void parseMessage()
        {
            Contract.Requires(message != null);
            if (message != null)
            {
                String tmpMsg = "", playerMessage = "";

                for (int i = 1; i <= 3; i++)
                {
                    tmpMsg = (String)message[counter];

                    if (i == 1)
                    {
                        playerMessage += extractValue((String)message[counter]);
                    }
                    else if (i == 2)
                    {
                        playerMessage = playerMessage + " - " + extractValue((String)message[counter]);
                    }
                    else if (i == 3)
                    {
                        playerMessage = playerMessage + " : " + extractValue((String)message[counter]);
                    }

                    if (!tmpMsg.Equals("end:mes"))
                    {
                        counter++;
                    }
                }
                backend.addToLog(playerMessage);
            }
        }           //Complete
        public void parseUpdate()
        {
            Contract.Requires(message != null);
            if (message != null)
            {
                String tmpMsg = extractValue((String)message[counter]);

                switch (tmpMsg)
                {
                    case "dragon":
                        parseDragon(false);
                        counter++;
                        break;
                    case "player":
                        parsePlayer(false);
                        counter++;
                        break;
                    case "cell":
                        // counter++;
                        parseMapcell(true);
                        break;
                }
            }
        }           //Complete
        public void parseDelete()
        {
            Contract.Requires(message != null);
            if (message != null)
            {
                String tmpMsg = (String)message[counter++];
                if (extractValue(tmpMsg).Equals("player"))
                {
                    parsePlayer(true);
                }
                else if (extractValue(tmpMsg).Equals("dragon"))
                {
                    parseDragon(true);
                }
            }
        }           //Complete
        public void parseAnswer()
        {
            Contract.Requires(message != null);
            if (message != null)
            {
                String tmpMsg = extractValue((String)message[counter]);

                switch (tmpMsg)
                {
                    case "ans:ok":
                        parseOkay();
                        break;
                    case "ans:no":
                        parseDeny();
                        break;
                    case "ans:unknown":
                        parseUnknow();
                        break;
                    case "ans:invalid":
                        parseInvalid();
                        break;
                }
            }
        }               //Complete
        public void parseOkay()
        {
            Contract.Requires(message != null);
            if (message != null)
            {
                backend.addToLog(extractValue((String)message[counter]));
            }
        }       //Complete
        public void parseDeny()
        {
            Contract.Requires(message != null);
            if (message != null)
            {
                backend.addToLog(extractValue((String)message[counter]));
            }
        }       //Complete
        public void parseUnknow()
        {
            Contract.Requires(message != null);
            if (message != null)
            {
                backend.addToLog(extractValue((String)message[counter]));
            }
        }       //Complete
        public void parseInvalid()
        {
            Contract.Requires(message != null);
            if (message != null)
            {
                backend.addToLog(extractValue((String)message[counter]));
            }
        }       //Complete
        public void parseYourID()
        {
            Contract.Requires(message != null);
            if (message != null)
            {
                int myID = Int32.Parse((String)message[counter++]);
                backend.setMyID(myID);
            }
        }       
        public void parseTime()
        {
            Contract.Requires(message != null);
            if (message != null)
            {
                long time = long.Parse((String)message[counter++]);
            }
            
        }       //Complete 
        public void parseOnline()
        {
            Contract.Requires(message != null);
            if (message != null)
            {
                int time = Int32.Parse((String)message[counter++]);
            }
        }       //Complete
        public void parseDecision()
        {
            Contract.Requires(message != null);
        }
        public void parseDragonfight()
        {
            Contract.Requires(message != null);
        }
        public void parseStaghunt()
        {
            Contract.Requires(message != null);
        }
        public void parseSkirmish()
        {
            Contract.Requires(message != null);
        }
        public List<MapEnum> parseProperty()
        {
            
            List<MapEnum> props = new List<MapEnum>();
            String tmpMsg = "";
            int i = 0;

            while (!tmpMsg.Equals("end:props"))
            {
                tmpMsg = (String)message[counter];
                switch (tmpMsg)
                {
                    case "WALKABLE":
                        break;
                    case "WALL":
                        props.Add(MapEnum.UNWALKABLE);
                        break;
                    case "FOREST":
                        props.Add(MapEnum.FOREST);
                        break;
                    case "WATER":
                        props.Add(MapEnum.WATER);
                        break;
                    case "HUNTABLE":
                        props.Add(MapEnum.HUNTABLE);
                        break;
                }
                if (!tmpMsg.Equals("end:props"))
                {
                    counter++;
                }
            }                
            return props;
        }       //loop stays; unknown amount of properties
        public static String extractKey(String message)
        {
            String s = "";
            try
            {
                int ioac = indexOfAssignChar(message);
                if (ioac < 0)
                {
                    s = null;
                }
                else
                {
                    s = message.Substring(0, indexOfAssignChar(message));
                }
            }
            catch (Exception e)
            {
                s = null;
            }
            return s;
        }
        public static String extractValue(String message)
        {
            String s = null;
            try
            {
                int i = indexOfAssignChar(message);
                if (i < 0)
                {
                    s = null;
                }
                else
                {
                    s = message.Substring(i + 1, message.Length - i - 1);
                }
            }
            catch (Exception e)
            {
                s = null;
            }
            return s;
        }
        public static int indexOfAssignChar(String message)
        {
            int s = message.IndexOf(":");
            return s;
        }
    }
}
