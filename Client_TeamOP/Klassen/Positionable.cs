using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client_TeamOP.Klassen
{
    public class Positionable : IPositionable
    {
        private int id;
        private int points;
        private int x;
        private int y;
        private String description;
        private String type;
        private bool busy;
        private bool autopilot;


       public Positionable(int x, int y)
        {
            this.x = x; 
            this.y = y; 
        }

        public void autoWalk(bool status)
        {

        }

        private void setY(int y)
        {
            this.y = y; 
        }

        private void setX(int x)
        {
            this.x = x;
        }

        public int getX()
        {
            return x;
        }

        public int getY()
        {
            return y;
        }

        public String getDescription()
        {
            return description;
        }

        private void setDescription(String description)
        {
            this.description = description;
        }

        public int getID()
        {
            return id;
        }

        public int getPoints()
        {
            return points;
        }

        public String getType()
        {
            return type;
        }

        public bool getBusy()
        {
            return busy;
        }
       

    }
}
