using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;

namespace Rosteras
{
    public class PlayerSearch : Search
    {
        private List<Player> playersSearched;
        private String name;
        private bool flag;

        public PlayerSearch(String nam): base(nam)
        {
            playersSearched = TeamsConnection.getPlayersSearched(nam);
            name = nam;
            flag = true;
        }
        public PlayerSearch(String nam,int r)
            : base(nam)
        {
            playersSearched = TeamsConnection.getPlayersSearched(nam,432423);
            flag = false;
        }

        public String getSearchHeading()
        {
            String head = @"
            <div>
            <h2 id = 'plSearch'> Αναζήτηση για το όνομα '" + name + @"' </h2>
            </div>";
            return head;
        }

        public override String displaySearchedElements()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(@"
            <table id = 'playersSearched'>
            ");

            if (playersSearched.Count == 0)
            {
                sb.Append(@"
                <tr>
                <th colspan = 3> Δεν βρέθηκε παίκτης με αυτό το όνομα </th>
                </tr>
                <tr>
                <td colspan = 3> (Αναζητήστε Έλληνες παίκτες στα Ελληνικά *με τόνους*) </td>
                </tr>
                <tr>
                <td colspan = 3> (Αναζητήστε Ξένους παίκτες στα Αγγλικά) </td>
                </tr>
                ");
            } //if

            else
            {
                if (!flag)
                {
                    foreach (Player player in playersSearched)
                    {
                        sb.Append(String.Format(@"
                <tr>
                <td colspan = 3> {0} ({1}) </a> </td>
                </tr>
                ", player.name, player.presentTeam));

                    }//foreach
                } //if
                else
                {
                    foreach (Player player in playersSearched)
                    {
                        sb.Append(String.Format(@"
            <tr>
            <th colspan = 2> <a href = 'PlayerInfo.aspx?playerID={0}&team={2}&league={3}' onclick = 'HideAndShow(&#39;{0}&#39;,1)'> {1} </a> </th>
            <td> (<a href = 'Team.aspx?team={2}&league={3}' > {2} </a>) </td>
            </tr> 
            ", player.playerID, player.name, player.presentTeam, player.presentTeamLeague));

                    }//foreach
                }
            } //else

            sb.Append("</table>");
            return sb.ToString();
        }
    }
}