using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;

namespace Rosteras
{
    public class TeamOfFootballLeague : TeamOfLeague
    {
        List<Player> formerSups;
        StringBuilder sb;

        public TeamOfFootballLeague(String t) :
            base(t, "Football League")
        {
            formerSups = TeamsConnection.FormerSuperleaguePlayers(t);
        }

        public String teamInfoLoading()
        {

            sb = new StringBuilder();
            sb.Append(base.teamInfoLoading());
            sb.Append(@"
        <table id = 'fsups'>
        <tr>
        <td> <a href = '#forSups' onclick = 'HideAndShow(&#39;forSups&#39;,0)'> Ποιοι παίκτες της ομάδας έχουν αγωνιστεί στο παρελθόν στην Superleague </a> </td>
        </tr>
        <tr id = 'forSups' class = 'hidden'>
        <td>
        <table class = 'moreInfo2'>
        <tr>
        <td> Όνομα </td>
        <td> Συμμετοχές (Superleague) </td>
        <td> Γκολ (Superleague) </td>
        </tr>");

            foreach (Player player in formerSups)
            {
                sb.Append(String.Format(@"
        <tr>
        <td> <a href = '#{3}'> {0} </a> </td>
        <td> {1} </td>
        <td> {2} </td>
        <tr>
        ", player.name, player.overallAppsSL, player.overallGoalsSL, player.playerID));
            }

            sb.Append(@"</table>
        </td>
        </tr>
        </table>
        ");

            return sb.ToString();
        }

        /*public String moreTeamInfoLoading()
        {
            sb = new StringBuilder();
            sb.Append(base.moreTeamInfoLoading());
            sb.Append(@"
        <table class = 'moreInfo2'>
        <tr>
        <th colspan = 3> ΠΑΙΚΤΕΣ ΜΕ ΠΑΡΑΣΤΑΣΕΙΣ SUPERLEAGUE </th>
        </tr>
        <p>
        <a name = 'forSups' id = 'forSups'> </a>
        </p>
        <tr>
        <td> Όνομα </td>
        <td> Συμμετοχές (Superleague) </td>
        <td> Γκολ (Superleague) </td>
        </tr>");

            foreach (Player player in formerSups)
            {
                sb.Append(String.Format(@"
        <tr>
        <td> <a href = '#{3}'> {0} </a> </td>
        <td> {1} </td>
        <td> {2} </td>
        <tr>
        ", player.name, player.overallAppsSL, player.overallGoalsSL, player.playerID));
            }

            sb.Append("</table>");

            return sb.ToString();
        }*/
    }
}