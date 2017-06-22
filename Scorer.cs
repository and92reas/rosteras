using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Rosteras
{
    public class Scorer
    {
        public String playerID { get; set; }
        public int goals { get; set; }

        public Scorer(String playerID, int goals)
        {
            this.playerID = playerID;
            this.goals = goals;
        }
    }
}