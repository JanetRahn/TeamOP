﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text;
using System.Runtime.InteropServices;
using System.IO;

namespace Client_TeamOP.Klassen
{
    public class Positionable : IPositionable, IObservable
    {

        Autowalk observer;
        private int id;
        private int points;
        private int x;
        private int y;
        private String description;
        private String type;
        private bool busy;
        private bool autopilot;



        public Positionable(int x, int y, int id, int points, String description, String type)
        {
            this.x = x;
            this.y = y;
            this.autopilot = false;
            this.autopilot = false;
            this.description = description;
            this.type = type;
            this.x = x;
            this.y = y;
            this.points = points;
            this.id = id;
        }


        public void setY(int y)
        {
            this.y = y;
        }

        public void setX(int x)
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

        public void setBusy(bool busy)
        {
            this.busy = busy;
        }

        public Boolean Equals(Positionable p)
        {
            return p.getID() == this.id;
        }

        public void callTheObserverForChanges()
        {
            if (observer != null)
            {
                observer.playerHasBeenMoved(this);
            }
        }

        public void setObserver(Autowalk observer)
        {
            this.observer = observer;
        }
       
    }
}
