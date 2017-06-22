using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;

namespace Rosteras
{
    public class League
    {
        List<Team> teams;
        List<Winner> topChampions;
        List<Winner> topCupWinners;
        List<Goalscorer> topCurrentLeagueScorers;
        List<Appearance> topCurrentLeagueAppearances;
        List<Goalscorer> topOverallLeagueScorers;
        List<Appearance> topOverallLeagueAppearances;
        String leagueForeignGoals;
        String league;
        String lastChamp;
        StringBuilder sb;


        public League(String lg)
        {
            teams = TeamsConnection.displayTeams(lg);
            league = lg;
            lastChamp = TeamsConnection.lastChampion(lg);
            topChampions = TeamsConnection.top5Champions();
            topCupWinners = TeamsConnection.top5CupWinners();
            topCurrentLeagueAppearances = TeamsConnection.top10LeagueAppearances(lg, true);
            topCurrentLeagueScorers = TeamsConnection.top10LeagueScorers(lg, true);
            topOverallLeagueAppearances = TeamsConnection.top10LeagueAppearances(lg, false);
            topOverallLeagueScorers = TeamsConnection.top10LeagueScorers(lg, false);
            leagueForeignGoals = TeamsConnection.LeagueGoalsFromForeigners(lg);
        }

        public String LoadTeams()
        {
            sb = new StringBuilder();
            sb.Append(string.Format(@"<table id = 'teams' class = 'teamsPlayersDisplayed'>
                <tr> 
                <th> Θέση </th>
                <th> Ονομασία </th>
                <th> Αγώνες </th>
                <th> Διαφορά </th>
                <th> Βαθμοί </th>
                </tr>"));

            foreach (Team team in teams)
            {
                
                sb.Append(string.Format(
                @"<tr> 
                <td> {1}. </td>
                <th> <a href = 'Team.aspx?team={0}&league={5}'>{0}</a> </th>
                <td> {3} </td>
                <td> {4} </td>
                <th> {2} </th> 
                </tr>",team.name, team.position, team.points, team.games,team.goalsFor-team.goalsBy,league));

            }//foreach
            sb.Append("</table>");
            return sb.ToString();
        }

        public String leagueInfo()
        {
            sb = new StringBuilder();
            sb.Append(String.Format(@"
            <table class = 'leagueInfo'>
            <tr>
            <td colspan = 3> Τελευταίος Πρωταθλητής </td>
            </tr>
            <tr>
            <th colspan = 3 id = 'lastChamp'> {0} </th>
            </tr>
            </table>
            ",lastChamp));

            sb.Append(@"
            <table class = 'leagueInfo'>
            <tr>
            <td> <a href = '#topChampions' onclick = 'HideAndShow(&#39;topChampions&#39;,0)'> Πρώτες 5 ομάδες σε κατακτήσεις πρωταθλημάτων </a> </td>
            </tr>
            <tr id = 'topChampions' class = 'hidden'>
            <td>
            <table class = 'moreLeagueInfo'>
            ");

            foreach (Winner winner in topChampions)
            {
                sb.Append(String.Format(@"
            <tr>
            <th colspan = 2> {0} </th>
            <td> {1} </td>
            </tr> 
            ", winner.team, winner.timesWon));
            }

            sb.Append(@"
            </table>
            </td>
            </tr>
            <tr>
            <td> <a href = '#topCupWinners' onclick = 'HideAndShow(&#39;topCupWinners&#39;,0)'> Πρώτες 5 ομάδες σε κατακτήσεις κυπέλλων </a> </td>
            </tr>
            <tr id = 'topCupWinners' class = 'hidden'>
            <td>
            <table class = 'moreLeagueInfo'>
            ");

            foreach (Winner winner in topCupWinners)
            {
                sb.Append(String.Format(@"
            <tr>
            <th colspan = 2> {0} </th>
            <td> {1} </td>
            </tr> 
            ", winner.team, winner.timesWon));
            }

            sb.Append(@"
            </table>
            </td>
            </tr>
            <tr>
            <td> <a href = '#topCurrentApps' onclick = 'HideAndShow(&#39;topCurrentApps&#39;,0)'> Πρώτοι 10 του πρωταθλήματος σε συμμετοχές φέτος </a> </td>
            </tr>
            <tr id = 'topCurrentApps' class = 'hidden'>
            <td>
            <table class = 'moreLeagueInfo'>
            ");

            foreach (Appearance app in topCurrentLeagueAppearances)
            {
                sb.Append(String.Format(@"
            <tr>
            <th> <a href = 'Team.aspx?team={3}&league={4}#{2}' onclick = 'HideAndShow(&#39;{2}&#39;,1)'> {0} </a> </th>
            <td> (<a href = 'Team.aspx?team={3}&league={4}' > {3} </a>) </td>
            <td> {1} </td>
            </tr> 
            ", app.name, app.appearances, app.playerID, app.team, league));
            }

            sb.Append(@"
            </table>
            </td>
            </tr>
            <tr>
            <td> <a href = '#topCurrentScorers' onclick = 'HideAndShow(&#39;topCurrentScorers&#39;,0)'> Πρώτοι 10 σκόρερ του πρωταθλήματος φέτος </a> </td>
            </tr>
            <tr id = 'topCurrentScorers' class = 'hidden'>
            <td>
            <table class = 'moreLeagueInfo'>
            ");

            foreach (Goalscorer scorer in topCurrentLeagueScorers)
            {
                sb.Append(String.Format(@"
            <tr>
            <th> <a href = 'Team.aspx?team={3}&league={4}#{2}' onclick = 'HideAndShow(&#39;{2}&#39;,1)'> {0} </a> </th>
            <td> (<a href = 'Team.aspx?team={3}&league={4}' > {3} </a>) </td>
            <td> {1} </td>
            </tr> 
            ", scorer.name, scorer.goals, scorer.playerID, scorer.team, league));
            }

            sb.Append(@"
            </table>
            </td>
            </tr>
            <tr>
            <td> <a href = '#topOverallApps' onclick = 'HideAndShow(&#39;topOverallApps&#39;,0)'> Πρώτοι 10 του πρωταθλήματος σε συμμετοχές διαχρονικά </a> </td>
            </tr>
            <tr id = 'topOverallApps' class = 'hidden'>
            <td>
            <table class = 'moreLeagueInfo'>
            ");

            foreach (Appearance app in topOverallLeagueAppearances)
            {
                sb.Append(String.Format(@"
            <tr>
            <th> <a href = 'Team.aspx?team={3}&league={4}#{2}' onclick = 'HideAndShow(&#39;{2}&#39;,1)'> {0} </a> </th>
            <td> (<a href = 'Team.aspx?team={3}&league={4}' > {3} </a>) </td>
            <td> {1} </td>
            </tr> 
            ", app.name, app.appearances, app.playerID, app.team, league));
            }

            sb.Append(@"
            </table>
            </td>
            </tr>
            <tr>
            <td> <a href = '#topOverallScorers' onclick = 'HideAndShow(&#39;topOverallScorers&#39;,0)'> Πρώτοι 10 σκόρερ του πρωταθλήματος διαχρονικά </a> </td>
            </tr>
            <tr id = 'topOverallScorers' class = 'hidden'>
            <td>
            <table class = 'moreLeagueInfo'>
            ");

            foreach (Goalscorer scorer in topOverallLeagueScorers)
            {
                sb.Append(String.Format(@"
            <tr>
            <th> <a href = 'Team.aspx?team={3}&league={4}#{2}' onclick = 'HideAndShow(&#39;{2}&#39;,1)'> {0} </a> </th>
            <td> (<a href = 'Team.aspx?team={3}&league={4}' > {3} </a>) </td>
            <td> {1} </td>
            </tr> 
            ", scorer.name, scorer.goals, scorer.playerID, scorer.team,league));
            }


            sb.Append(@"</table>
            </td>
            </tr>
            </table>
            ");
            return sb.ToString();
        }

        /*public String moreLeagueInfo()
        {

            sb = new StringBuilder();
            sb.Append(@"
            <table class = 'moreLeagueInfo'>
            <tr>
            <th colspan = 3> TOP 5 ΚΑΤΑΚΤΗΚΤΕΣ SUPERLEAGUE </th>
            </tr>");
            sb.Append(@"
            <p>
            <a name = 'topChampions' id = 'topChampions'> </a>
            </p>");

            foreach (Winner winner in topChampions)
            {
                sb.Append(String.Format(@"
            <tr>
            <th colspan = 2> {0} </th>
            <td> {1} </td>
            </tr> 
            ", winner.team, winner.timesWon));
            }

            sb.Append(@"
            </table>
            <table class = 'moreLeagueInfo'>
            <tr>
            <th colspan = 3> TOP 5 ΚΑΤΑΚΤΗΤΕΣ ΚΥΠΕΛΛΩΝ </th>
            </tr>");

            sb.Append(@"
            <p>
            <a name = 'topCupWinners' id = 'topCupWinners'> </a>
            </p>");

            foreach (Winner winner in topCupWinners)
            {
                sb.Append(String.Format(@"
            <tr>
            <th colspan = 2> {0} </th>
            <td> {1} </td>
            </tr> 
            ", winner.team, winner.timesWon));
            }

            sb.Append(@"
            </table>
            <table class = 'moreLeagueInfo'>
            <tr>
            <th colspan = 3> TOP 10 ΣΕ ΣΥΜΜΕΤΟΧΕΣ ΦΕΤΟΣ </th>
            </tr>");

            sb.Append(@"
            <p>
            <a name = 'topCurrentApps' id = 'topCurrentApps'> </a>
            </p>");

            foreach (Appearance app in topCurrentLeagueAppearances)
            {
                sb.Append(String.Format(@"
            <tr>
            <th> <a href = 'Team.aspx?team={3}&league={4}#{2}'> {0} </a> </th>
            <td> (<a href = 'Team.aspx?team={3}&league={4}' > {3} </a>) </td>
            <td> {1} </td>
            </tr> 
            ", app.name, app.appearances, app.playerID, app.team,league));
            }

            sb.Append(@"
            </table>
            <table class = 'moreLeagueInfo'>
            <tr>
            <th colspan = 3> TOP 10 ΣΚΟΡΕΡ ΦΕΤΟΣ </th>
            </tr>");

            sb.Append(@"
            <p>
            <a name = 'topCurrentScorers' id = 'topCurrentScorers'> </a>
            </p>");

            foreach (Goalscorer scorer in topCurrentLeagueScorers)
            {
                sb.Append(String.Format(@"
            <tr>
            <th> <a href = 'Team.aspx?team={3}&league={4}#{2}'> {0} </a> </th>
            <td> (<a href = 'Team.aspx?team={3}&league={4}' > {3} </a>) </td>
            <td> {1} </td>
            </tr> 
            ", scorer.name, scorer.goals, scorer.playerID, scorer.team,league));
            }

            sb.Append(@"
            </table>
            <table class = 'moreLeagueInfo'>
            <tr>
            <th colspan = 3> TOP 10 ΣΕ ΣΥΜΜΕΤΟΧΕΣ ΔΙΑΧΡΟΝΙΚΑ </th>
            </tr>");

            sb.Append(@"
            <p>
            <a name = 'topOverallApps' id = 'topOverallApps'> </a>
            </p>");

            foreach (Appearance app in topOverallLeagueAppearances)
            {
                sb.Append(String.Format(@"
            <tr>
            <th> <a href = 'Team.aspx?team={3}&league={4}#{2}'> {0} </a> </th>
            <td> (<a href = 'Team.aspx?team={3}&league={4}' > {3} </a>) </td>
            <td> {1} </td>
            </tr> 
            ", app.name, app.appearances, app.playerID, app.team,league));
            }

            sb.Append(@"
            </table>
            <table class = 'moreLeagueInfo'>
            <tr>
            <th colspan = 3> TOP 10 ΣΚΟΡΕΡ ΔΙΑΧΡΟΝΙΚΑ </th>
            </tr>");

            sb.Append(@"
            <p>
            <a name = 'topOverallScorers' id = 'topOverallScorers'> </a>
            </p>");

            foreach (Goalscorer scorer in topOverallLeagueScorers)
            {
                sb.Append(String.Format(@"
            <tr>
            <th> <a href = 'Team.aspx?team={3}&league={4}#{2}'> {0} </a> </th>
            <td> (<a href = 'Team.aspx?team={3}&league={4}' > {3} </a>) </td>
            <td> {1} </td>
            </tr> 
            ", scorer.name, scorer.goals, scorer.playerID, scorer.team,league));
            }


            sb.Append("</table>");

            return sb.ToString();
        }*/
    }
}
