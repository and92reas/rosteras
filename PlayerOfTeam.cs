using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Rosteras
{
    public class PlayerOfTeam
    {
        String playerID;
        String team;
        String league;
        Player player;
        int overallLeagueAppearancesTeamOrder;
        int overallLeagueAppearancesLeagueOrder;
        int presentLeagueAppearancesTeamOrder;
        int presentLeagueAppearancesLeagueOrder;
        int overallLeagueGoalsTeamOrder;
        int overallLeagueGoalsLeagueOrder;
        int presentLeagueGoalsTeamOrder;
        int presentLeagueGoalsLeagueOrder;
        int overallLeagueGoalsRateTeamOrder;
        int overallLeagueGoalsRateLeagueOrder;
        int presentLeagueGoalsRateTeamOrder;
        int presentLeagueGoalsRateLeagueOrder;
        Info info;
        List<PastTeams> pastTeams;
        StringBuilder sb;



        public PlayerOfTeam(String pID,String tm, String lg)
        {
            playerID = pID;
            team = tm;
            league = lg;
            player = TeamsConnection.getPlayerByID(pID, lg);
            overallLeagueAppearancesTeamOrder = TeamsConnection.getLeagueAppearancesTeamOrder(pID, tm, lg, false);
            overallLeagueAppearancesLeagueOrder = TeamsConnection.getLeagueAppearancesLeagueOrder(pID, lg, false);
            presentLeagueAppearancesTeamOrder = TeamsConnection.getLeagueAppearancesTeamOrder(pID, tm, lg, true);
            presentLeagueAppearancesLeagueOrder = TeamsConnection.getLeagueAppearancesLeagueOrder(pID, lg, true);
            overallLeagueGoalsTeamOrder = TeamsConnection.getLeagueGoalsTeamOrder(pID,tm,lg,false);
            overallLeagueGoalsLeagueOrder = TeamsConnection.getLeagueGoalsLeagueOrder(pID, lg, false);
            presentLeagueGoalsTeamOrder = TeamsConnection.getLeagueGoalsTeamOrder(pID, tm, lg, true);
            presentLeagueGoalsLeagueOrder = TeamsConnection.getLeagueGoalsLeagueOrder(pID, lg, true);
            overallLeagueGoalsRateTeamOrder = TeamsConnection.getLeagueGoalsRateTeamOrder(pID, tm, lg, false);
            overallLeagueGoalsRateLeagueOrder = TeamsConnection.getLeagueGoalsRateLeagueOrder(pID, lg, false);
            presentLeagueGoalsRateTeamOrder = TeamsConnection.getLeagueGoalsRateTeamOrder(pID, tm, lg, true);
            presentLeagueGoalsRateLeagueOrder = TeamsConnection.getLeagueGoalsRateLeagueOrder(pID, lg, true);
            info = TeamsConnection.teamInfo(tm);
            pastTeams = TeamsConnection.getPastTeamsOfPlayer(pID);
        }

        public String playerTeamDataLoading()
        {
            sb = new StringBuilder();
            sb.Append(string.Format(@"
              <div id = '{0}'>
             <a href='Team.aspx?team={4}&league={5}'> <img id = 'teamImage' src= '{1}' alt = '{2}' > </a>
                    <div id = 'teamName'> {3} </div> 
              </div>", info.cssid, info.image, info.capitals, player.name, team, league));

            return sb.ToString();
        }

        public String playerInfoLoading()
        {
            sb = new StringBuilder();

            bool ptfound = false;
            
                if (player.height == 0)
                {
                    String unknown = "(Άγνωστο)"; 
                    sb.Append(String.Format(@"
                <table class = 'individuals'>
                <tr>
                <td colspan = 2> Ύψος: </td>
                <th> {0} </th>
                </tr>
                    ", unknown));
                }

                else
                {
                    sb.Append(String.Format(@"
                <table class = 'individuals'>
                <tr>
                <td colspan = 2> Ύψος: </td>
                <th> {0} </th>
                </tr>
                    ", player.height));
                } //else

                sb.Append(String.Format(@"
                <tr>
                <td colspan = 2> Ηλικία: </td>
                <th> {0} </th>
                <tr>
                <td colspan = 2> Χώρα: </td>
                <th> {1} </th>
                </tr>
                <tr>
                <td colspan = 2> Θέση: </td>
                <th> {2} </th>
                </tr>
                    ", player.age, player.country, player.position));

                if (!player.lentBy.Equals('-'))
                {
                    sb.Append(String.Format(@"
                <tr>
                <td colspan = 2> Δανεικός Από: </td>
                <th> {0} </th>
                </tr>", player.lentBy));
                }

                if (league.Equals("Superleague"))
                {

                    sb.Append(String.Format(@"
                <tr>
                <td colspan = 2> Συνολικές Συμμετοχές (Superleague): </td>
                <th class = 'overall'> {0} </th>
                </tr>
                
                
                <tr>
                <td colspan = 2> Συνολικά Γκολ (Superleague): </td>
                <th class = 'overall'> {1} </th>
                </tr>
                
                
                <tr>
                <td colspan = 2> Συνολικά Γκολ ανα Συμμετοχή (Superleague): </td>
                <th class = 'overall'> {8} </th>
                </tr>
               
                
                <tr>
                <td colspan = 2> Συνολικές Συμμετοχές (Football League): </td>
                <th> {2} </th>
                </tr>
               
                <tr>
                <td colspan = 2> Συνολικά Γκολ (Football League): </td>
                <th> {3} </th>
                </tr>
                
                <tr>
                <td colspan = 2> Συνολικά Γκολ ανα Συμμετοχή (Football League): </td>
                <th> {9} </th>
                </tr>
               
                <tr>
                <td colspan = 2> Φετινές Συμμετοχές (Superleague): </td>
                <th class = 'present'> {4} </th>
                </tr>
                
               
                <tr>
                <td colspan = 2> Φετινά Γκολ (Superleague): </td>
                <th class = 'present'> {5} </th>
                </tr>
                
                
                <tr>
                <td colspan = 2> Φετινά Γκολ ανα Συμμετοχή (Superleague): </td>
                <th class = 'present'> {10} </th>
                </tr>
                
                <tr>
                <td colspan = 2> Φετινές Συμμετοχές (Football League): </td>
                <th> {6} </th>
                </tr>
                
                <tr>
                <td colspan = 2> Φετινά Γκολ (Football League): </td>
                <th> {7} </th>
                </tr>
                
                <tr>
                <td colspan = 2> Φετινά Γκολ ανα Συμμετοχή (Football League): </td>
                <th> {11} </th>
                </tr>
                
                <tr>
                <td colspan = 3> Προηγούμενες Ομάδες </td>
                </tr>
                ", player.overallAppsSL, player.overallGoalsSL, player.overallAppsFL, player.overallGoalsFL, player.presentAppsSL,
                     player.presentGoalsSL, player.presentAppsFL, player.presentGoalsFL,player.overallGoalRateSL.ToString("0.00"), 
                     player.overallGoalRateFL.ToString("0.00"), player.presentGoalRateSL.ToString("0.00")
                     , player.presentGoalRateFL.ToString("0.00")));
                } //if

                else
                {
                    sb.Append(String.Format(@"
                <tr>
                <td colspan = 2> Συνολικές Συμμετοχές (Superleague): </td>
                <th> {0} </th>
                </tr>
               
                <tr>
                <td colspan = 2> Συνολικά Γκολ (Superleague): </td>
                <th> {1} </th>
                </tr>
                
                <tr>
                <td colspan = 2> Συνολικά Γκολ ανα Συμμετοχή (Superleague): </td>
                <th> {8} </th>
                </tr>
               
                <tr>
                <td colspan = 2> Συνολικές Συμμετοχές (Football League): </td>
                <th class = 'overall'> {2} </th>
                </tr>
               
                <tr>
                <td colspan = 2> Συνολικά Γκολ (Football League): </td>
                <th class = 'overall'> {3} </th>
                </tr>
                
                <tr>
                <td colspan = 2> Συνολικά Γκολ ανα Συμμετοχή (Football League): </td>
                <th class = 'overall'> {9} </th>
                </tr>
                
                <tr>
                <td colspan = 2> Φετινές Συμμετοχές (Superleague): </td>
                <th> {4} </th>
                </tr>
                
                <tr>
                <td colspan = 2> Φετινά Γκολ (Superleague): </td>
                <th> {5} </th>
                </tr>
                
                <tr>
                <td colspan = 2> Φετινά Γκολ ανα Συμμετοχή (Superleague): </td>
                <th> {10} </th>
                </tr>
               
                <tr>
                <td colspan = 2> Φετινές Συμμετοχές (Football League): </td>
                <th class = 'present'> {6} </th>
                </tr>
                
                <tr>
                <td colspan = 2> Φετινά Γκολ (Football League): </td>
                <th class = 'present'> {7} </th>
                </tr>
                
                <tr>
                <td colspan = 2> Φετινά Γκολ ανα Συμμετοχή (Football League): </td>
                <th class = 'present'> {11} </th>
                </tr>
                
                <tr>
                <td colspan = 3> Προηγούμενες Ομάδες </td>
                </tr>
                ", player.overallAppsSL, player.overallGoalsSL, player.overallAppsFL, player.overallGoalsFL, player.presentAppsSL,
                         player.presentGoalsSL, player.presentAppsFL, player.presentGoalsFL, player.overallGoalRateSL.ToString("0.00"),
                     player.overallGoalRateFL.ToString("0.00"), player.presentGoalRateSL.ToString("0.00")
                     , player.presentGoalRateFL.ToString("0.00")));
                } //else

                ptfound = false;
                foreach (PastTeams pastTeam in pastTeams) //simpler
                {
                        if (!pastTeam.team.Equals(player.presentTeam))
                        {
                            ptfound = true;
                            sb.Append(String.Format(@"
                        <tr>
                        <th colspan = 2> {0} </th>
                        <td> ({1}) </td>
                        </tr>
                        ", pastTeam.team, pastTeam.country));
                        } //if
                    
                } //foreach
                if (!ptfound)
                {
                    sb.Append(String.Format(@"
                  <tr>
                  <td> </td>
                  <td colspan = 2> Καμία </td>
                  </tr>"));
                }

            sb.Append(String.Format("</table>"));

            return sb.ToString();
        }

        public String playerOrderInfoLoading() { 

        sb = new StringBuilder();

            if (league.Equals("Superleague"))
            {

                sb.Append(String.Format(@"
                <table class = 'individuals'>
                <tr>
                <td colspan = 2> Κατάταξη Συνολικών Συμμετοχών (ομάδα): </td>
                <th class = 'overall'> {0}ος </th>
                </tr>
                <tr>
                <td colspan = 2> Κατάταξη Συνολικών Συμμετοχών (πρωτάθλημα): </td>
                <th class = 'overall'> {1}ος </th>
                </tr>
                <tr>
                <th colspan = 3 class = 'infoSeparator' >------------------------------------------------------</th>
                </tr>
                <tr>
                <td colspan = 2> Κατάταξη Συνολικών Γκολ (ομάδα): </td>
                <th class = 'overall'> {2}ος </th>
                </tr>
                <tr>
                <td colspan = 2> Κατάταξη Συνολικών Γκολ (πρωτάθλημα): </td>
                <th class = 'overall'> {3}ος </th>
                </tr>
                <tr>
                <th colspan = 3 class = 'infoSeparator' >------------------------------------------------------</th>
                </tr>
                 <tr>
                <td colspan = 2> Κατάταξη Συνολικών Γκολ ανα Συμμετοχή (ομάδα): </td>
                <th class = 'overall'> {4}ος </th>
                </tr>
                <tr>
                <td colspan = 2> Κατάταξη Συνολικών Γκολ ανα Συμμετοχή (πρωτάθλημα): </td>
                <th class = 'overall'> {5}ος </th>
                </tr>
                <tr>
                <th colspan = 3 class = 'infoSeparator' >------------------------------------------------------</th>
                </tr>
                <tr>
                <td colspan = 2> Κατάταξη Φετινών Συμμετοχών (ομάδα): </td>
                <th class = 'present'> {6}ος </th>
                </tr>
                <tr>
                <td colspan = 2> Κατάταξη Φετινών Συμμετοχών (πρωτάθλημα): </td>
                <th class = 'present'> {7}ος </th>
                </tr>
                <tr>
                <th colspan = 3 class = 'infoSeparator' >------------------------------------------------------</th>
                </tr>
                <tr>
                <td colspan = 2> Κατάταξη Φετινών Γκολ (ομάδα): </td>
                <th class = 'present'> {8}ος </th>
                </tr>
                <tr>
                <td colspan = 2> Κατάταξη Φετινών Γκολ (πρωτάθλημα): </td>
                <th class = 'present'> {9}ος </th>
                </tr>
                <tr>
                <th colspan = 3 class = 'infoSeparator' >------------------------------------------------------</th>
                </tr>
                <tr>
                <td colspan = 2> Κατάταξη Φετινών Γκολ ανα Συμμετοχή (ομάδα): </td>
                <th class = 'present'> {10}ος </th>
                </tr>
                <tr>
                <td colspan = 2> Κατάταξη Φετινών Γκολ ανα Συμμετοχή (πρωτάθλημα): </td>
                <th class = 'present'> {11}ος </th>
                </tr>
                </table>", overallLeagueAppearancesTeamOrder,
                     overallLeagueAppearancesLeagueOrder, overallLeagueGoalsTeamOrder,
                     overallLeagueGoalsLeagueOrder, overallLeagueGoalsRateTeamOrder, overallLeagueGoalsRateLeagueOrder,
                     presentLeagueAppearancesTeamOrder, presentLeagueAppearancesLeagueOrder,
                     presentLeagueGoalsTeamOrder, presentLeagueGoalsLeagueOrder, presentLeagueGoalsRateTeamOrder,
                     presentLeagueGoalsRateLeagueOrder));
            }
            else
            {
                sb.Append(String.Format(@"
                <table class = 'individuals'>
                 <tr>
                <td colspan = 2> Κατάταξη Συνολικών Συμμετοχών (ομάδα): </td>
                <th class = 'overall'> {0}ος </th>
                </tr>
                <tr>
                <td colspan = 2> Κατάταξη Συνολικών Συμμετοχών (πρωτάθλημα): </td>
                <th class = 'overall'> {1}ος </th>
                </tr>
                <tr>
                <th colspan = 3 class = 'infoSeparator' >------------------------------------------------------</th>
                </tr>
                <tr>
                <td colspan = 2> Κατάταξη Συνολικών Γκολ (ομάδα): </td>
                <th class = 'overall'> {2}ος </th>
                </tr>
                <tr>
                <td colspan = 2> Κατάταξη Συνολικών Γκολ (πρωτάθλημα): </td>
                <th class = 'overall'> {3}ος </th>
                </tr>
                <tr>
                <th colspan = 3 class = 'infoSeparator' >------------------------------------------------------</th>
                </tr>
                <tr>
                <td colspan = 2> Κατάταξη Συνολικών Γκολ ανα Συμμετοχή (ομάδα): </td>
                <th class = 'overall'> {4}ος </th>
                </tr>
                <tr>
                <td colspan = 2> Κατάταξη Συνολικών Γκολ ανα Συμμετοχή (πρωτάθλημα): </td>
                <th class = 'overall'> {5}ος </th>
                </tr>
                <tr>
                <th colspan = 3 class = 'infoSeparator' >------------------------------------------------------</th>
                </tr>
                <tr>
                <td colspan = 2> Κατάταξη Φετινών Συμμετοχών (ομάδα): </td>
                <th class = 'present'> {6}ος </th>
                </tr>
                <tr>
                <td colspan = 2> Κατάταξη Φετινών Συμμετοχών (πρωτάθλημα): </td>
                <th class = 'present'> {7}ος </th>
                </tr>
                <tr>
                <th colspan = 3 class = 'infoSeparator' >------------------------------------------------------</th>
                </tr>
                <tr>
                <td colspan = 2> Κατάταξη Φετινών Γκολ (ομάδα): </td>
                <th class = 'present'> {8}ος </th>
                </tr>
                <tr>
                <td colspan = 2> Κατάταξη Φετινών Γκολ (πρωτάθλημα): </td>
                <th class = 'present'> {9}ος </th>
                </tr>
                <tr>
                <th colspan = 3 class = 'infoSeparator' >------------------------------------------------------</th>
                </tr>
                <tr>
                <td colspan = 2> Κατάταξη Φετινών Γκολ ανα Συμμετοχή (ομάδα): </td>
                <th class = 'present'> {10}ος </th>
                </tr>
                <tr>
                <td colspan = 2> Κατάταξη Φετινών Γκολ ανα Συμμετοχή (πρωτάθλημα): </td>
                <th class = 'present'> {11}ος </th>
                </tr></table>", overallLeagueAppearancesTeamOrder,
                     overallLeagueAppearancesLeagueOrder, overallLeagueGoalsTeamOrder,
                     overallLeagueGoalsLeagueOrder, overallLeagueGoalsRateTeamOrder, overallLeagueGoalsRateLeagueOrder,
                     presentLeagueAppearancesTeamOrder, presentLeagueAppearancesLeagueOrder,
                     presentLeagueGoalsTeamOrder, presentLeagueGoalsLeagueOrder, presentLeagueGoalsRateTeamOrder,
                     presentLeagueGoalsRateLeagueOrder));
            }
            return sb.ToString();
}

    }
}