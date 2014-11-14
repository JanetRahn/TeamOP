using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client_TeamOP.Klassen {
    public interface IBackend {
    List<IPositionable> getDragons();
    List<IPositionable> getPlayers();
    IMap[][] getMap();
    void sendCommand(string command);
    void sendChat(string message);
    }
}
