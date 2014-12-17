using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Client_TeamOP.Klassen
{
    public class Autowalk
    {

        private Map map;
        private List<Positionable> positionableHuman;
        private List<IObservable> playerToObserv;
        int[] path;
        int counter;
        Backend backend;

        public Autowalk(Map map, List<Positionable> positionableHuman, Backend backend)
        {
            this.map = map;
            this.positionableHuman = positionableHuman;
            playerToObserv = new List<IObservable>();
            this.backend = backend;
            counter = 1;
        }


        //AutoWalk Method

        [DllImport("MapDll.dll", CallingConvention = CallingConvention.Cdecl)]
        static extern IntPtr findPath(int from, int to, int[] map, int mapw, int maph, int plength);

        [DllImport("MapDll.dll", CallingConvention = CallingConvention.Cdecl)]
        static extern void freeArray(IntPtr pointer);


        

        public void init(int x, int y, Positionable p)
        {
            playerToObserv[0] = p;
            path = autoWalkCalc(map.getFieldAt(x, y), map.getFieldAt(backend.getPositionableHumans()[0].getX(), backend.getPositionableHumans()[0].getY()));
        }

        public int[] autoWalkCalc(Field start, Field end)
        {            
            int[,] mapConvertToMinimal = new int[map.getWidth(), map.getHigh()];

            for (int i = 0; i < map.getWidth(); i++)
            {
                for (int j = 0; j < map.getHigh(); j++)
                {
                    if (map.getFieldAt(i, j).isWater() | !map.getFieldAt(i, j).isWalkable())
                    {
                        mapConvertToMinimal[i, j] = 1;
                    }
                    else
                    {
                        mapConvertToMinimal[i, j] = 0;
                    }
                }
            }

            int[] path = new int[map.getWidth() * map.getHigh()];

            int[] convertMap = convertMapToPointer(mapConvertToMinimal);

            bool convertable;

            if (convertMap[findPoint(start)] == 0 & !(start == null & end == null & convertMap == null & map == null))
            {
                convertable = true;
                IntPtr pointer = findPath(findPoint(start), findPoint(end), convertMap, map.getWidth(), map.getHigh(), (map.getWidth() * map.getHigh()));
                Marshal.Copy(pointer, path, 0, path.Length);
            }
            else
            {
                convertable = false;
            } 
            return path;
        }

        private void walkAutomatic(int x, int y)
        {
            if (counter > 0)
            {
                if (x > findPoint(path[counter - 1])[0])
                {
                    backend.moveRight();
                }
                else if (x < findPoint(path[counter - 1])[0])
                {
                    backend.moveLeft();
                }
                else if (y > findPoint(path[counter - 1])[1])
                {
                    backend.moveDown();
                }
                else if (y < findPoint(path[counter - 1])[1])
                {
                    backend.moveUp();
                }
            }
        }

        private int[] findPoint(int index)
        {
            int[] coords = new int[2];
            int x = 0, y = 0;
            while (index > map.getWidth())
            {
                index = index - map.getWidth();
                y++;
            }
            x = index;
            coords[0] = x;
            coords[1] = y;
            return coords;
        }

        private int findPoint(Field point)
        {
            int convertedPoint = -1;
            convertedPoint = point.getX() + point.getY() * map.getWidth();
            return convertedPoint;
        }

        private int[] convertMapToPointer(int[,] convMap)
        {
            int[] tmp = new int[map.getWidth() * map.getHigh()];
            for (int i = 0; i < map.getWidth(); i++)
            {
                for (int j = 0; j < map.getHigh(); j++)
                {
                    tmp[i * map.getWidth() + j] = convMap[j, i];
                }
            }
            return tmp;
        }

        public void playerHasBeenMoved(Positionable player)
        {
            if (playerToObserv.Contains(player) & path != null)
            {
                nextField();
            }
        }

        public void setObsPlayer(Positionable p)
        {
            playerToObserv.Insert(0,p);
        }

        private void nextField()
        {
            if (path[counter] >= 0)
            {
                int[] coord = findPoint(path[counter]);
                walkAutomatic(coord[0], coord[1]);
            }
            else
            {
                clearAutowalk();
            }
            counter++;
            

        }

        public void setMap(Map map)
        {
            this.map = map;
        }

        public void clearAutowalk()
        {
            path = null;
            counter = 1;
        }
    }
}
