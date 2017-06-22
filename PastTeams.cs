using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Rosteras
{
    public class PastTeams
    {
        public String playerName { get; set; }
        public String playerID { get; set; }
        public String team { get; set; }
        public String country { get; set; }

        public PastTeams(String playerID, String team, String country) {
            this.playerID = playerID;
            this.team = team;
            this.country = country;
        }

        public PastTeams(String playerName, String team, String country,int r)
        {
            this.playerName = playerName;
            this.team = team;
            this.country = country;
        }

        
    }
}