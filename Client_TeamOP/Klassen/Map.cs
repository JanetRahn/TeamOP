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
            field=new Field[width, length];
        }
        public int[] getField(int x, int y)
        {
            int[] array=null;
            return array;
        }
        public int getWidth(){
            
            return width;
        }
        public int getLength()
        {
            return length;
        }

    }
}
