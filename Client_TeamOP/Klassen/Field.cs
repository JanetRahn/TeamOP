using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client_TeamOP.Klassen
{
    class Field
    {
        private Boolean forest;
        private Boolean unwalkable;
        private Boolean water;
        private Boolean huntable;
        private Positionable positionable;

        public Field()
        {

        }
        public Boolean getForest()
        {
            return forest;
        }
        public Boolean getUnwalkable()
        {
            return unwalkable;
        }
        public Boolean getWater()
        {
            return water;
        }
        public Boolean getHuntable()
        {
            return huntable;
        }
        public void setPositionable(Positionable pl)
        {
            this.positionable = pl;
        }
        public void removePositionable(Positionable pl)
        {

        }
    }
}
