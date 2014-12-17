using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client_TeamOP.Klassen
{
    public class Map
    {
        private Field[,] field;
        private int width, length;

        public Map(int width, int length)
        {
            this.width = width;
            this.length = length;
            field = new Field[width, length];
            List<MapEnum> attributes = new List<MapEnum>();
            attributes.Add(MapEnum.FOREST);
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < length; y++)
                {
                    field[x, y] = new Field(x,y,attributes);
                }
            }
        }
        public Field[,] getField()
        {
            return field;
        }

        public Field getFieldAt(int x, int y)
        {
            return field[x, y];
        }
        public int getWidth()
        {
            return width;
        }

        public int getHigh()
        {
            return length;
        }

        public void setField(int x, int y, Field f)
        {
            field[f.getX(), f.getY()] = f;
        }
    }
}
