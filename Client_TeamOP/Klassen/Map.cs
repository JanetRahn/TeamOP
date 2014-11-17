using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client_TeamOP.Klassen
{
    class Map
    {
        private Field[,] field;
        private int width, length;

        public Map(int width, int length)
        {
            this.width = width;
            this.length = length;
            field = new Field[width, length];
        }
        public Field[,] getField()
        {
            return field;
        }
        public int getWidth()
        {
            return width;
        }

        public int getHigh()
        {
            return length;
        }

        internal void creatTestMap()
        {
            int size = 10;
            // init
            field = new Field[size, size];
            Random r = new Random();
            for (int x = 0; x < size; x++)
            {
                for (int y = 0; y < size; y++)
                {
                    List<MapEnum> attr = new List<MapEnum>();
                    switch (r.Next(0, 4))
                    {
                        case 0:
                            attr.Add(MapEnum.WATER);
                            field[x, y] = new Field(x, y, attr);
                            break;
                        case 1:
                            attr.Add(MapEnum.HUNTABLE);
                            attr.Add(MapEnum.FOREST);
                            field[x, y] = new Field(x, y, attr);
                            break;
                        case 2:
                            attr.Add(MapEnum.FOREST);
                            field[x, y] = new Field(x, y, attr);
                            break;
                        case 3:
                            attr.Add(MapEnum.UNWALKABLE);
                            field[x, y] = new Field(x, y, attr);
                            break;
                    }
                }
            }
        }
    }
}
