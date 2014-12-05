using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client_TeamOP.Klassen {
   public  interface IPositionable {

         int getX();

         int getY();
         void autoWalk(int fromX, int fromY, int toX, int toY, Map map);
    }
}
