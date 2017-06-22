using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rosteras
{
    class PlayerOfSuperleagueTeam: PlayerOfTeam
    {
        public PlayerOfSuperleagueTeam(String playerID, String team) :
            base(playerID, team, "Superleague")
        {
        }
    }
}
