using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client_TeamOP.Klassen
{
    class Minigame
    {
        private int roundCounter;
        private int points = 0;
        private String gameName;
        private String[] interactions;

        public void interaction(String gameName)
        {
            this.gameName = gameName;
            roundCounter = 0;
            points = 0;
            interactions = new String[3];
        }

        public int getRound()
        {
            return roundCounter;
        }

        public int getPoints()
        {
            return points;
        }

        public String getGameName()
        {
            return gameName;
        }

        public String getInteraction(int x)
        {
            return interactions[x];
        }
    }
}
