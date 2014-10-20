using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client_TeamOP
{
    interface IBackend
    {
        public void sendToConnector(String message);
        
        //Klassen und Methoden von Backend erstellen
        public void storePlayer(Player p);
        public void deletePlayer(int id);
        public void storeDragon(Dragon d);
        public void deleteDragon(Dragon d);
        public void storeMapField(Field f);
        public void deleteMapField(Field f);

    }
}
