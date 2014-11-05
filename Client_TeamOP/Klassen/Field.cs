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
        private List<Positionable> positionable;

        public Field(Boolean forest, Boolean unwalkable, Boolean water, Boolean huntable)
        {
            this.forest = forest;
            this.unwalkable = unwalkable;
            this.water = water;
            this.huntable = huntable;
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
            this.positionable.Add(pl);
        }
        public void removePositionable(Positionable pl)
        {
            this.positionable.Remove(pl);
        }
    }
}
