using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client_TeamOP.Klassen
{
    public class Field : IMap
    {
      
        private List<MapEnum> attributes;
        private int x, y;

        public Field(int x, int y, List<MapEnum> attributes)
        {
            this.x = x;
            this.y = y;
            this.attributes = new List<MapEnum>();
            this.attributes.AddRange(attributes);
        }
        public int[] getField(int x, int y)
        {
            int[] array=null;
            return array;
        }
        public int getX(){
            
            return x;
        }
        public int getY()
        {
            return y;
        }

        public bool isWalkable() {
            return !this.attributes.Contains(MapEnum.UNWALKABLE);
        }

        public bool isForest() {
            return this.attributes.Contains(MapEnum.FOREST);
        }

        public bool isHuntable() {
            return this.attributes.Contains(MapEnum.HUNTABLE);
        }

        public bool isWater() {
            return this.attributes.Contains(MapEnum.WATER);
        }

    }
 
}


