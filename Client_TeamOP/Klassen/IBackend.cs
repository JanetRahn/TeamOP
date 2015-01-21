using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client_TeamOP.Klassen {
    public interface IBackend
    {

        String getMinigameType();
        void sendDecision(String decision);
        List<IPositionable> getPositionableDragon();
        List<IPositionable> getPositionableHumans();
        IMap[,] getMap();
        bool sendCommand(string command);
        Boolean moveUp();
        Boolean moveDown();
        Boolean moveLeft();
        Boolean moveRight();
        List<String> getLog();
        void autowalkGotoField(int x, int y);
        void exit();
    }
}
