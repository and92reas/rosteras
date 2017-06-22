using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rosteras
{
    class PlayerOfFootballLeagueTeam: PlayerOfTeam
    {
        public PlayerOfFootballLeagueTeam(String playerID, String team) :
            base(playerID, team, "Football League")
        {
        }
    }
}
