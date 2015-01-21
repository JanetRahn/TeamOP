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
        private String gameType;
        private String[] interactions;


        public Minigame(String gameType)
        {
            this.gameType = gameType;
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
            return gameType;
        }

        public String getInteraction(int x)
        {
            return interactions[x];
        }

        public String getGameType()
        {
            String toReturn = null;

            if (gameType.Equals("DRAGON"))
            {
                toReturn = "dragon";
            }
            else if (gameType.Equals("SKIRMISH"))
            {
                toReturn = "skirm";
            }
            else if (gameType.Equals("STAGHUNT"))
            {
                toReturn = "shunt";
            }

            return toReturn;
        }
    }
}
