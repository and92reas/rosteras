using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Rosteras
{
    public class Appearance
    {
        public String playerID { get; set; }
        public String name { get; set; }
        public int appearances { get; set; }
        public String team { get; set; }
        public String teamPage { get; set; }

        public Appearance(String playerID, String name, int appearances)
        {
            this.playerID = playerID;
            this.name = name;
            this.appearances = appearances;        
        }

        public Appearance(String playerID, String name, int appearances, String team, String teamPage)
        {
            this.playerID = playerID;
            this.name = name;
            this.appearances = appearances;
            this.team = team;
            this.teamPage = teamPage;
        }
    }
}