using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client_TeamOP.Klassen
{
    static class Sort
    {

        public static List<Positionable> quicksortList(List<Positionable> toSort, String lambda, int left, int right)
        {
            List<Positionable> listToReturn = null;

            if (lambda.Equals("points"))
            {

            }
            else if (lambda.Equals("points"))
            {

            }
            else if (lambda.Equals("points"))
            {

            }
            return listToReturn;
        }

        private static List<Positionable> quicksortListPoints(List<Positionable> toSort, String lambda, int left, int right)
        {
            
        }

        private static List<Positionable> quicksortListName(List<Positionable> toSort, String lambda, int left, int right)
        {
          
        }

        private static List<Positionable> quicksortListDistance(List<Positionable> toSort, String lambda, int left, int right)
        {
          
        }

        public static int binarySearchOfSortedList(List<Positionable> toSearch, Positionable searchObject)
        {
            return toSearch.BinarySearch(searchObject);            
        }

        public static int binarySearchOfUnsortedList(List<Positionable> toSearch, String lambda, Backend b)
        {
            int indexToReturn = -1;

            if (lambda.Equals("busy"))
            {
                foreach (Positionable p in toSearch)
                {
                    if (!p.getBusy())
                    {
                        indexToReturn = toSearch.IndexOf(p);
                    }
                }
            }
            else if (lambda.Equals("points"))
            {
                Positionable tmpObject = toSearch[0];
                foreach (Positionable p in toSearch)
                {
                    if (tmpObject.getPoints() < p.getPoints())
                    {
                        tmpObject = p;
                    }
                }
                indexToReturn = toSearch.IndexOf(tmpObject);
            }
            else if (lambda.Equals("distance"))
            {
                Positionable me = b.getMe();

                foreach (Positionable p in toSearch)
                {
                    
                    
                }
                indexToReturn = toSearch.IndexOf(tmpObject);
            }
            return indexToReturn;
        }


        private static int[] getDistance(Positionable enemy, Positionable me)
        {
            int[] size = new int[2];

            int minX = me.getX();
            int minY = me.getY();

            if ((enemy.getX() >= me.getX()) & (enemy.getX() - me.getX() <= minX))
                    {
                        int tmp = minX;
                        minX = enemy.getX() - me.getX();

                        if ((enemy.getY() >= me.getY()) & (enemy.getY() - me.getY() <= minY))
                        {
                            minY = enemy.getY() - me.getY();
                           
                        }
                        else if ((enemy.getY() <= me.getY()) & (me.getY() - enemy.getY() <= minY))
                        {
                            minY = me.getY() - enemy.getY();
                            
                        }
                        else
                        {
                            minX = tmp;
                        }
                    }
            else if ((enemy.getX() <= me.getX()) & (me.getX() - enemy.getX() <= minX))
                    {
                        int tmp = minX;
                        minX = me.getX() - enemy.getX();

                        if ((enemy.getY() >= me.getY()) & (enemy.getY() - me.getY() <= minY))
                        {
                            minY = enemy.getY() - me.getY();
                            
                        }
                        else if ((enemy.getY() <= me.getY()) & (me.getY() - enemy.getY() <= minY))
                        {
                            minY = me.getY() - enemy.getY();
                            
                        }
                        else
                        {
                            minY = tmp;
                        }
                    }

            size[0] = minX;
            size[1] = minY;
            return size;
        }

    }
}
