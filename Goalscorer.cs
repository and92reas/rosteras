using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Rosteras
{
    public class Goalscorer
    {
        public String playerID { get; set; }
        public String name { get; set; }
        public int goals { get; set; }
        public String team { get; set; }
        public String teamPage { get; set; }

        public Goalscorer(String playerID, String name, int goals)
        {
            this.playerID = playerID;
            this.name = name;
            this.goals = goals;        
        }

        public Goalscorer(String playerID, String name, int goals, String team, String teamPage)
        {
            this.playerID = playerID;
            this.name = name;
            this.goals = goals;
            this.team = team;
            this.teamPage = teamPage;
        }
    }
}