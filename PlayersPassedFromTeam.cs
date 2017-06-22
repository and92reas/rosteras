using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;

namespace Rosteras
{
    public class PlayersPassedFromTeam: PlayerChoice
    {
        String team;
        List<Player> players;

        public PlayersPassedFromTeam(String team)
        {
            this.team = team;
            players = TeamsConnection.getAllTheFormerOrPresentPlayers(team);
        }

        public override String getThePlayers()
        {
            StringBuilder sb = new StringBuilder();
            bool flag = false;
            sb.Append(@"
            <table id = 'passedFromTeam' class = 'searchResults'>
            ");

            foreach (Player player in players)
            {
                flag = true;
                sb.Append(String.Format(@"
            <tr>
            <th colspan = 2> <a href = 'PlayerInfo.aspx?playerID={0}&team={2}&league={3}' onclick = 'HideAndShow(&#39;{0}&#39;,1)'> {1} </a> </th>
            <td> (<a href = 'Team.aspx?team={2}&league={3}' > {2} </a>) </td>
            </tr> 
            ", player.playerID, player.name, player.presentTeam, player.presentTeamLeague));
            }

            if (!flag)
            {
                sb.Append(@"
            <tr>
            <th> Κανείς </th>
            </tr>
            ");
            }

            sb.Append("</table>");
            return sb.ToString();
        }

            
    }
}