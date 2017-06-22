using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Rosteras
{
    public class Winner
    {
        public String team { get; set; }
        public int timesWon { get; set; }

        public Winner(String team, int timesWon)
        {
            this.team = team;
            this.timesWon = timesWon;
        }
    }
}