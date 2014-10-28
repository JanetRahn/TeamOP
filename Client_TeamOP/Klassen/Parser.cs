using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.Contracts;
using System.Threading;

namespace Client_TeamOP
{
    public class Parser
    {
        Buffer buffer;
        Backend backend;
        Thread listening;
        List<Thread> StillParsing;
        

        
        public Parser(Buffer buffer)
        {
            Contract.Requires(buffer != null);
            this.buffer = buffer;
            listening = new Thread(new ThreadStart(listeningThread));
            StillParsing = new List<Thread>();            
        }
       
        public String readFromBuffer(){
            Contract.Requires(buffer != null);
            return null;
        }
        public void sendToMethod(String message){
            Contract.Requires(message != null);
        }
        public void parseS(String message){
            Contract.Requires(message != null);
        }
        public void parseServermes(String message){
            Contract.Requires(message != null);
        }
        public void parseServer(String message){
            Contract.Requires(message != null);
        }
        public void parseResult(String message){
            Contract.Requires(message != null);
        }
        public void parseOpponent(String message)
        {
            Contract.Requires(message != null);
        }
        public void parseChallenge(String message)
        {
            Contract.Requires(message != null);
        }
        public bool parseDragon(String message)
        {
            Contract.Requires(message != null);
            return false; 
        }
        public void parsePlayer(String message)
        {
            Contract.Requires(message != null);
        }
        public void parsePlayers(String message)
        {
            Contract.Requires(message != null);
        }

        public void parseEntities(String message)
        {
            Contract.Requires(message != null);
        }
        public void parseMapcell(String message)
        {
            Contract.Requires(message != null);
        }
        public void parseMap(String message)
        {
            Contract.Requires(message != null);
        }
        public void parseMessage(String message)
        {
            Contract.Requires(message != null);
        }

        public void parseUpdate(String message)
        {
            Contract.Requires(message != null);
        }
        public void parseDelete(String message)
        {
            Contract.Requires(message != null);
        }
        public void parseAnswer(String message)
        {
            Contract.Requires(message != null);
        }
        public void parseOkay(String message)
        {
            Contract.Requires(message != null);
        }
        public void parseDeny(String message)
        {
            Contract.Requires(message != null);
        }
        public void parseUnknow(String message)
        {
            Contract.Requires(message != null);
        }
        public void parseInvalid(String message)
        {
            Contract.Requires(message != null);
        }
        public void parseYourID(String message)
        {
            Contract.Requires(message != null);
        }
        public void parseTime(String message)
        {
            Contract.Requires(message != null);
        }
        public void parseOnline(String message)
        {
            Contract.Requires(message != null);
        }
        public void parseDecision(String message)
        {
            Contract.Requires(message != null);
        }
        public void parseDragonfight(String message)
        {
            Contract.Requires(message != null);
        }
        public void parseStaghunt(String message)
        {
            Contract.Requires(message != null);
        }
        public void parseSkirmish(String message)
        {
            Contract.Requires(message != null);
        }
        public void parseProperty(String message)
        {
            Contract.Requires(message != null);
        }
    }
}
