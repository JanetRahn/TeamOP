using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client_TeamOP.Klassen {
    public interface IMap : IPositionable {

         bool isWalkable();
         bool isForest();
         bool isHuntable();
         bool isWater();
    }
}
