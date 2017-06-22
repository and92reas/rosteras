using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;

namespace Rosteras
{
    public class CompareTeam : Compare
    {
        String name1;
        String name2;
        String league1;
        String league2;
        int pos1;
        int pos2;
        Team team1;
        Team team2;
        List<Player> presentCommonPlayers;
        List<Player> allTimeCommonPlayers;

        public CompareTeam(String name1, String league1, String name2, String league2)
        {
            this.name1 = name1;
            this.name2 = name2;
            this.league1 = league1;
            this.league2 = league2;
            team1 = TeamsConnection.ComparingTeams(name1);
            team2 = TeamsConnection.ComparingTeams(name2);
            pos1 = TeamsConnection.getTeamPosition(name1, league1);
            pos2 = TeamsConnection.getTeamPosition(name2, league2);
            presentCommonPlayers = TeamsConnection.ActivePlayersPlayedInBothTeams(name1, name2);
            allTimeCommonPlayers = TeamsConnection.PlayersPlayedInBothTeams(name1, name2);
        }

        public override String getComparisonResults()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(String.Format(@"
            <table id = 'comparisonTable'>
            <tr>
            <th colspan = 2> {0} </th>
            <td class = 'dash'> Αποτέλεσμα </td>
            <th colspan = 2> {1} </th>
            </tr>
            <p>
            <a name = 'comparison' id = 'comparison'> </a>
            </p>
            <tr>
            <td> Πρωτάθλημα </td>
            <th> {2} </th>
            <td class = 'dash'> - </td>
            <th> {3} </th>
            <td> Πρωτάθλημα </td>
            </tr>
            <tr>
            <td> Περιοχή </td>
            <th> {4} </th>
            <td class = 'dash'> - </td>
            <th> {5} </th>
            <td> Περιοχή </td>
            </tr>
            <tr>
            <td> Έδρα </td>
            <th> {6} </th>
            <td class = 'dash'> - </td>
            <th> {7} </th>
            <td> Έδρα </td>
            </tr>
            <tr>
            <td> Προπονητής </td>
            <th> {8} </th>
            <td class = 'dash'> - </td>
            <th> {9} </th>
            <td> Προπονητής </td>
            </tr>
            <tr>
            <td> Πρόεδρος </td>
            <th> {10} </th>
            <td class = 'dash'> - </td>
            <th> {11} </th>
            <td> Πρόεδρος </td>
            </tr>
            <tr>
            <td> Τελευταίο τρόπαιο </td>
            <th> {12} </th>
            <td class = 'dash'> - </td>
            <th> {13} </th>
            <td> Τελευταίο τρόπαιο </td>
            </tr>
            ", team1.name, team2.name, team1.league, team2.league, team1.area, team2.area, team1.field, team2.field, team1.manager, team2.manager,
             team1.chairman, team2.chairman, team1.lastTrophy, team2.lastTrophy));

            if (team1.leaguesWon > team2.leaguesWon)
            {
                sb.Append(String.Format(@"
                <tr>
                <td> Κερδισμένα Πρωταθλήματα (Superleague) </td>
                <th> {0} </th>
                <th class = 'greater'> +{1} </td>
                <th> {2} </th>
                <td> Κερδισμένα Πρωταθλήματα (Superleague) </td>
                </tr>
                ", team1.leaguesWon, team1.leaguesWon - team2.leaguesWon, team2.leaguesWon));
            }

            else if (team1.leaguesWon < team2.leaguesWon)
            {
                sb.Append(String.Format(@"
                <tr>
                <td> Κερδισμένα Πρωταθλήματα (Superleague) </td>
                <th> {0} </th>
                <th class = 'smaller'> {1} </td>
                <th> {2} </th>
                <td> Κερδισμένα Πρωταθλήματα (Superleague) </td>
                </tr>
                ", team1.leaguesWon, team1.leaguesWon - team2.leaguesWon, team2.leaguesWon));
            }

            else
            {
                sb.Append(String.Format(@"
                <tr>
                <td> Κερδισμένα Πρωταθλήματα (Superleague) </td>
                <th> {0} </th>
                <th class = 'equals'> {1} </td>
                <th> {2} </th>
                <td> Κερδισμένα Πρωταθλήματα (Superleague) </td>
                </tr>
                ", team1.leaguesWon, team1.leaguesWon - team2.leaguesWon, team2.leaguesWon));
            }

            if (team1.cupsWon > team2.cupsWon)
            {
                sb.Append(String.Format(@"
                <tr>
                <td> Κερδισμένα Κύπελλα </td>
                <th> {0} </th>
                <th class = 'greater'> +{1} </td>
                <th> {2} </th>
                <td> Κερδισμένα Κύπελλα </td>
                </tr>
                ", team1.cupsWon, team1.cupsWon - team2.cupsWon, team2.cupsWon));
            }

            else if (team1.cupsWon < team2.cupsWon)
            {
                sb.Append(String.Format(@"
                <tr>
                <td> Κερδισμένα Κύπελλα </td>
                <th> {0} </th>
                <th class = 'smaller'> {1} </td>
                <th> {2} </th>
                <td> Κερδισμένα Κύπελλα </td>
                </tr>
                ", team1.cupsWon, team1.cupsWon - team2.cupsWon, team2.cupsWon));
            }

            else
            {
                sb.Append(String.Format(@"
                <tr>
                <td> Κερδισμένα Κύπελλα </td>
                <th> {0} </th>
                <th class = 'equals'> {1} </td>
                <th> {2} </th>
                <td> Κερδισμένα Κύπελλα </td>
                </tr>
                ", team1.cupsWon, team1.cupsWon - team2.cupsWon, team2.cupsWon));
            }

            if (!team1.league.Equals(team2.league))
            {
                sb.Append(String.Format(@"
                <tr>
                <td> Θέση </td>
                <th> {0}η </th>
                <th class = 'dash'> - </td>
                <th> {1}η </th>
                <td> Θέση </td>
                </tr>
                <tr>
                <td> Βαθμοί </td>
                <th> {2} </th>
                <th class = 'dash'> - </td>
                <th> {3} </th>
                <td> Βαθμοί </td>
                </tr>
                ", pos1, pos2, team1.points, team2.points));
            }

            else
            {

                if (pos1 > pos2)
                {
                    sb.Append(String.Format(@"
                <tr>
                <td> Θέση </td>
                <th> {0}η </th>
                <th class = 'smaller'> +{1} </td>
                <th> {2}η </th>
                <td> Θέση </td>
                </tr>
                ", pos1, pos1 - pos2, pos2));
                }

                else if (pos1 < pos2)
                {
                    sb.Append(String.Format(@"
                <tr>
                <td> Θέση </td>
                <th> {0}η </th>
                <th class = 'greater'> {1} </td>
                <th> {2}η </th>
                <td> Θέση </td>
                </tr>
                ", pos1, pos1 - pos2, pos2));
                }

                else
                {
                    sb.Append(String.Format(@"
                <tr>
                <td> Θέση </td>
                <th> {0}η </th>
                <th class = 'equals'> {1} </td>
                <th> {2}η </th>
                <td> Θέση </td>
                </tr>
                ", pos1, pos1 - pos2, pos2));
                }

                if (team1.points > team2.points)
                {
                    sb.Append(String.Format(@"
                <tr>
                <td> Βαθμοί </td>
                <th> {0} </th>
                <th class = 'greater'> +{1} </td>
                <th> {2} </th>
                <td> Βαθμοί </td>
                </tr>
                ", team1.points, team1.points - team2.points, team2.points));
                }

                else if (team1.points < team2.points)
                {
                    sb.Append(String.Format(@"
                <tr>
                <td> Βαθμοί </td>
                <th> {0} </th>
                <th class = 'smaller'> {1} </td>
                <th> {2} </th>
                <td> Βαθμοί </td>
                </tr>
                ", team1.points, team1.points - team2.points, team2.points));
                }

                else
                {
                    sb.Append(String.Format(@"
                <tr>
                <td> Βαθμοί </td>
                <th> {0} </th>
                <th class = 'equals'> {1} </td>
                <th> {2} </th>
                <td> Βαθμοί </td>
                </tr>
                ", team1.points, team1.points - team2.points, team2.points));
                }
            } //else
            if (presentCommonPlayers.Count == 0)
            {
                sb.Append(@"
                <table id = 'playerComparison'>
                <tr> 
                <td colspan = 3> Κανένας ενεργός παίκτης της μιας ή της άλλης ομάδας δεν έχει παίξει και στις δύο ομάδες </td>
                </tr>
                </table>
                ");
            }

            else
            {
                sb.Append(@"
                <table id = 'playerComparison'>
                <tr> 
                <td colspan = 3> Οι ενεργοί παίκτες των δύο ομάδων που έχουν παίξει και στις δύο ομάδες είναι οι εξής: </td>
                </tr>
                ");

                foreach (Player player in presentCommonPlayers)
                {
                    sb.Append(String.Format(@"
                <tr> 
                <th colspan = 2> <a href = 'PlayerInfo.aspx?playerID={2}&team={1}&league={3}' onload = 'HideAndShow(&#39;{2}&#39;,1)'> {0} </a> </th>
                <td> (<a href = 'Team.aspx?team={1}&league={3}'> {1} </a>) </td>
                </tr>
                ", player.name, player.presentTeam, player.playerID, player.presentTeamLeague));
                } //foreach
                                
                sb.Append("</table>");

            }

                
                    if (allTimeCommonPlayers.Count == 0)
            {
                sb.Append(@"</table>
                <table id = 'alltimeCommon'>
                <tr> 
                <td colspan = 3> Δεν υπάρχουν ενεργοί παίκτες των δύο πρώτων κατηγοριών που να έχουν στο παρελθόν και στις δύο ομάδες </td>
                </tr>
                </table>
                ");
            }

            else
            {
                sb.Append(String.Format(@"</table>
                <table id = 'alltimeCommon'>
                <tr> 
                <td colspan = 3> Οι ενεργοί παίκτες των δύο πρώτων κατηγοριών που έχουν παίξει και στις δύο ομάδες είναι: </td>
                </tr>
                "));

                    foreach (Player player in allTimeCommonPlayers)
                    {
                        sb.Append(String.Format(@"
                <tr> 
                <th colspan = 2> <a href = 'PlayerInfo.aspx?playerID={2}&team={1}&league={3}' onload = 'HideAndShow(&#39;{2}&#39;,1)'> {0} </a> </th>
                <td> (<a href = 'Team.aspx?team={1}&league={3}'> {1} </a>) </td>
                </tr>
                ", player.name, player.presentTeam, player.playerID, player.presentTeamLeague));
                    } //foreach
                    sb.Append("</table>");
                } //else

                return sb.ToString();

            
        }
    }
}