using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Rosteras
{
    public class Traveller
    {
        public String name { get; set; }
        public int numOfTeams { get; set; }
        public string playerID { get; set; }

        public Traveller(String name, int numOfTeams, string playerID)
        {
            this.name = name;
            this.numOfTeams = numOfTeams;
            this.playerID = playerID;
        }

    }
}