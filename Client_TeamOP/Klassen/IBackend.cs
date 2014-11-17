using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client_TeamOP.Klassen {
    public interface IBackend {
        List<IPositionable> getPositionableDragon();
         List<IPositionable> getPositionableHumans();
         IMap[,] getMap();
         bool sendCommand(string command);
         bool sendChat(string message);
         Boolean moveUp();
         Boolean moveDown();
         Boolean moveLeft();
         Boolean moveRight();
    }
}
