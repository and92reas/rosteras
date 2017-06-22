using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;


namespace Rosteras
{
    public class ComparePlayer : Compare
    {
        String player1;
        String player2;
        String team1;
        String team2;
        Player pl1;
        Player pl2;
        List<PastTeams> pst1;
        List<PastTeams> pst2;
        List<PastTeams> simps;

        public ComparePlayer(String player1, String team1, String player2, String team2)
        {
            this.player1 = player1;
            this.team1 = team1;
            this.player2 = player2;
            this.team2 = team2;
            pl1 = TeamsConnection.comparingPlayers(player1, team1);
            pl2 = TeamsConnection.comparingPlayers(player2, team2);
            pst1 = TeamsConnection.comparingFormerTeams(player1, team1);
            pst2 = TeamsConnection.comparingFormerTeams(player2, team2);
        }

        public override String getComparisonResults()
        {
            StringBuilder sb = new StringBuilder();
            int numOfTeams1 = pst1.Count();
            int numOfTeams2 = pst2.Count(); //the number of past teams of the players

            PastTeams p = new PastTeams(null, team1, "Ελλάδα");
            pst1.Add(p);
            p = new PastTeams(null, team2, "Ελλάδα");
            pst2.Add(p); //adding the present team to the past teams' list

            simps = new List<PastTeams>();

            foreach (PastTeams ps1 in pst1)
            {
                foreach (PastTeams ps2 in pst2)
                {
                    if (ps1.team.Equals(ps2.team))
                    {
                        simps.Add(ps2);
                        break;
                    } //if
                } //foreach
            } //foreach
            sb.Append(String.Format(@"<div id = 'cplayers'>
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
            <td> Ομάδα </td>
            <th> {2} </th>
            <td class = 'dash'> - </td>
            <th> {3} </th>
            <td> Ομάδα </td>
            </tr>
            <tr>
            <td> Πρωτάθλημα </td>
            <th> {4} </th>
            <td class = 'dash'> - </td>
            <th> {5} </th>
            <td> Πρωτάθλημα </td>
            </tr>
            <tr>
            <td> Δανεικός Από </td>
            <th> {6} </th>
            <td class = 'dash'> - </td>
            <th> {7} </th>
            <td> Δανεικός Από </td>
            </tr>
            <tr>
            <td> Χώρα </td>
            <th> {8} </th>
            <td class = 'dash'> - </td>
            <th> {9} </th>
            <td> Χώρα </td>
            </tr>
            <tr>
            <td> Θέση </td>
            <th> {10} </th>
            <td class = 'dash'> - </td>
            <th> {11} </th>
            <td> Θέση </td>
            </tr>            
            ", pl1.name, pl2.name, pl1.presentTeam, pl2.presentTeam, pl1.presentTeamLeague, pl2.presentTeamLeague, pl1.lentBy, pl2.lentBy,
            pl1.country, pl2.country, pl1.position, pl2.position));

            if (pl1.height > pl2.height)
            {
                if (pl2.height != 0)
                {
                    sb.Append(String.Format(@"
                <tr>
                <td> Ύψος </td>
                <th> {0} </th>
                <th class = 'greater'> +{1} </th>
                <th> {2} </th>
                <td> Ύψος </td>
                </tr>
                ", pl1.height, Math.Round(pl1.height - pl2.height, 2), pl2.height));
                }
                else
                {
                    sb.Append(String.Format(@"
                <tr>
                <td> Ύψος </td>
                <th> {0} </th>
                <th> {1} </th>
                <th> {2} </th>
                <td> Ύψος </td>
                </tr>
                ", pl1.height, "-", "(άγνωστο)"));
                }
            }

            else if (pl1.height < pl2.height)
            {
                if (pl1.height != 0)
                {
                    sb.Append(String.Format(@"
                <tr>
                <td> Ύψος </td>
                <th> {0} </th>
                <th class = 'smaller'> {1} </th>
                <th> {2} </th>
                <td> Ύψος </td>
                </tr>
                ", pl1.height, Math.Round(pl1.height - pl2.height, 2), pl2.height));
                }
                else
                {
                    sb.Append(String.Format(@"
                <tr>
                <td> Ύψος </td>
                <th> {0} </th>
                <th> {1} </th>
                <th> {2} </th>
                <td> Ύψος </td>
                </tr>
                ", "(άγνωστο)", "-", pl2.height));
                }
            }

            else
            {
                if (pl1.height != 0)
                {
                    sb.Append(String.Format(@"
                <tr>
                <td> Ύψος </td>
                <th> {0} </th>
                <th class = 'equals'> {1} </th>
                <th> {2} </th>
                <td> Ύψος </td>
                </tr>
                ", pl1.height, Math.Round(pl1.height - pl2.height, 2), pl2.height));
                }
                else
                {
                    sb.Append(String.Format(@"
                <tr>
                <td> Ύψος </td>
                <th> {0} </th>
                <th> {1} </th>
                <th> {2} </th>
                <td> Ύψος </td>
                </tr>
                ", "(άγνωστο)", "-", "(άγνωστο)"));
                }
            }

            if (pl1.age > pl2.age)
            {
                sb.Append(String.Format(@"
                <tr>
                <td> Ηλικία </td>
                <th> {0} </th>
                <th class = 'greater'> +{1} </td>
                <th> {2} </th>
                <td> Ηλικία </td>
                </tr>
                ", pl1.age, pl1.age - pl2.age, pl2.age));
            }

            else if (pl1.age < pl2.age)
            {
                sb.Append(String.Format(@"
                <tr>
                <td> Ηλικία </td>
                <th> {0} </th>
                <th class = 'smaller'> {1} </td>
                <th> {2} </th>
                <td> Ηλικία </td>
                </tr>
                ", pl1.age, pl1.age - pl2.age, pl2.age));
            }

            else
            {
                sb.Append(String.Format(@"
                <tr>
                <td> Ηλικία </td>
                <th> {0} </th>
                <th class = 'equals'> {1} </td>
                <th> {2} </th>
                <td> Ηλικία </td>
                </tr>
                ", pl1.age, pl1.age - pl2.age, pl2.age));
            }

            if (pl1.overallAppsSL - pl2.overallAppsSL > 0)
            {
                sb.Append(String.Format(@"
                <tr>
                <td> Συνολικές Εμφανίσεις (Superleague) </td>
                <th> {0} </th>
                <th class = 'greater'> +{1} </td>
                <th> {2} </th>
                <td> Συνολικές Εμφανίσεις (Superleague) </td>
                </tr>
                ", pl1.overallAppsSL, pl1.overallAppsSL - pl2.overallAppsSL, pl2.overallAppsSL));
            }

            else if (pl1.overallAppsSL - pl2.overallAppsSL < 0)
            {
                sb.Append(String.Format(@"
                <tr>
                <td> Συνολικές Εμφανίσεις (Superleague) </td>
                <th> {0} </th>
                <th class = 'smaller'> {1} </td>
                <th> {2} </th>
                <td> Συνολικές Εμφανίσεις (Superleague) </td>
                </tr>
                ", pl1.overallAppsSL, pl1.overallAppsSL - pl2.overallAppsSL, pl2.overallAppsSL));
            }

            else
            {
                sb.Append(String.Format(@"
                <tr>
                <td> Συνολικές Εμφανίσεις (Superleague) </td>
                <th> {0} </th>
                <th class = 'equals'> {1} </td>
                <th> {2} </th>
                <td> Συνολικές Εμφανίσεις (Superleague) </td>
                </tr>
                ", pl1.overallAppsSL, pl1.overallAppsSL - pl2.overallAppsSL, pl2.overallAppsSL));
            }

            if (pl1.overallGoalsSL - pl2.overallGoalsSL > 0)
            {
                sb.Append(String.Format(@"
                <tr>
                <td> Συνολικά γκολ (Superleague) </td>
                <th> {0} </th>
                <th class = 'greater'> +{1} </td>
                <th> {2} </th>
                <td> Συνολικά γκολ (Superleague) </td>
                </tr>
                ", pl1.overallGoalsSL, pl1.overallGoalsSL - pl2.overallGoalsSL, pl2.overallGoalsSL));
            }

            else if (pl1.overallGoalsSL - pl2.overallGoalsSL < 0)
            {
                sb.Append(String.Format(@"
                <tr>
                <td> Συνολικά γκολ (Superleague) </td>
                <th> {0} </th>
                <th class = 'smaller'> {1} </td>
                <th> {2} </th>
                <td> Συνολικά γκολ (Superleague) </td>
                </tr>
                ", pl1.overallGoalsSL, pl1.overallGoalsSL - pl2.overallGoalsSL, pl2.overallGoalsSL));
            }

            else
            {
                sb.Append(String.Format(@"
                <tr>
                <td> Συνολικά γκολ (Superleague) </td>
                <th> {0} </th>
                <th class = 'equals'> {1} </td>
                <th> {2} </th>
                <td> Συνολικά γκολ (Superleague) </td>
                </tr>
                ", pl1.overallGoalsSL, pl1.overallGoalsSL - pl2.overallGoalsSL, pl2.overallGoalsSL));
            }

            if (pl1.overallAppsFL - pl2.overallAppsFL > 0)
            {
                sb.Append(String.Format(@"
                <tr>
                <td> Συνολικές Εμφανίσεις (Football League) </td>
                <th> {0} </th>
                <th class = 'greater'> +{1} </td>
                <th> {2} </th>
                <td> Συνολικές Εμφανίσεις (Football League) </td>
                </tr>
                ", pl1.overallAppsFL, pl1.overallAppsFL - pl2.overallAppsFL, pl2.overallAppsFL));
            }

            else if (pl1.overallAppsFL - pl2.overallAppsFL < 0)
            {
                sb.Append(String.Format(@"
                <tr>
                <td> Συνολικές Εμφανίσεις (Football League) </td>
                <th> {0} </th>
                <th class = 'smaller'> {1} </td>
                <th> {2} </th>
                <td> Συνολικές Εμφανίσεις (Football League) </td>
                </tr>
                ", pl1.overallAppsFL, pl1.overallAppsFL - pl2.overallAppsFL, pl2.overallAppsFL));
            }

            else
            {
                sb.Append(String.Format(@"
                <tr>
                <td> Συνολικές Εμφανίσεις (Football League) </td>
                <th> {0} </th>
                <th class = 'equals'> {1} </td>
                <th> {2} </th>
                <td> Συνολικές Εμφανίσεις (Football League) </td>
                </tr>
                ", pl1.overallAppsFL, pl1.overallAppsFL - pl2.overallAppsFL, pl2.overallAppsFL));
            }

            if (pl1.overallGoalsFL - pl2.overallGoalsFL > 0)
            {
                sb.Append(String.Format(@"
                <tr>
                <td> Συνολικά γκολ (Football League) </td>
                <th> {0} </th>
                <th class = 'greater'> +{1} </td>
                <th> {2} </th>
                <td> Συνολικά γκολ (Football League) </td>
                </tr>
                ", pl1.overallGoalsFL, pl1.overallGoalsFL - pl2.overallGoalsFL, pl2.overallGoalsFL));
            }

            else if (pl1.overallGoalsFL - pl2.overallGoalsFL < 0)
            {
                sb.Append(String.Format(@"
                <tr>
                <td> Συνολικά γκολ (Football League) </td>
                <th> {0} </th>
                <th class = 'smaller'> {1} </td>
                <th> {2} </th>
                <td> Συνολικά γκολ (Football League) </td>
                </tr>
                ", pl1.overallGoalsFL, pl1.overallGoalsFL - pl2.overallGoalsFL, pl2.overallGoalsFL));
            }

            else
            {
                sb.Append(String.Format(@"
                <tr>
                <td> Συνολικά γκολ (Football League) </td>
                <th> {0} </th>
                <th class = 'equals'> {1} </td>
                <th> {2} </th>
                <td> Συνολικά γκολ (Football League) </td>
                </tr>
                ", pl1.overallGoalsFL, pl1.overallGoalsFL - pl2.overallGoalsFL, pl2.overallGoalsFL));
            }

            if (pl1.presentAppsSL - pl2.presentAppsSL > 0)
            {
                sb.Append(String.Format(@"
                <tr>
                <td> Φετινές Εμφανίσεις (Superleague) </td>
                <th> {0} </th>
                <th class = 'greater'> +{1} </td>
                <th> {2} </th>
                <td> Φετινές Εμφανίσεις (Superleague) </td>
                </tr>
                ", pl1.presentAppsSL, pl1.presentAppsSL - pl2.presentAppsSL, pl2.presentAppsSL));
            }

            else if (pl1.presentAppsSL - pl2.presentAppsSL < 0)
            {
                sb.Append(String.Format(@"
                <tr>
                <td> Φετινές Εμφανίσεις (Superleague) </td>
                <th> {0} </th>
                <th class = 'smaller'> {1} </td>
                <th> {2} </th>
                <td> Φετινές Εμφανίσεις (Superleague) </td>
                </tr>
                ", pl1.presentAppsSL, pl1.presentAppsSL - pl2.presentAppsSL, pl2.presentAppsSL));
            }

            else
            {
                sb.Append(String.Format(@"
                <tr>
                <td> Φετινές Εμφανίσεις (Superleague) </td>
                <th> {0} </th>
                <th class = 'equals'> {1} </td>
                <th> {2} </th>
                <td> Φετινές Εμφανίσεις (Superleague) </td>
                </tr>
                ", pl1.presentAppsSL, pl1.presentAppsSL - pl2.presentAppsSL, pl2.presentAppsSL));
            }

            if (pl1.presentGoalsSL - pl2.presentGoalsSL > 0)
            {
                sb.Append(String.Format(@"
                <tr>
                <td> Φετινά γκολ (Superleague) </td>
                <th> {0} </th>
                <th class = 'greater'> +{1} </td>
                <th> {2} </th>
                <td> Φετινά γκολ (Superleague) </td>
                </tr>
                ", pl1.presentGoalsSL, pl1.presentGoalsSL - pl2.presentGoalsSL, pl2.presentGoalsSL));
            }

            else if (pl1.presentGoalsSL - pl2.presentGoalsSL < 0)
            {
                sb.Append(String.Format(@"
                <tr>
                <td> Φετινά γκολ (Superleague) </td>
                <th> {0} </th>
                <th class = 'smaller'> {1} </td>
                <th> {2} </th>
                <td> Φετινά γκολ (Superleague) </td>
                </tr>
                ", pl1.presentGoalsSL, pl1.presentGoalsSL - pl2.presentGoalsSL, pl2.presentGoalsSL));
            }

            else
            {
                sb.Append(String.Format(@"
                <tr>
                <td> Φετινά γκολ (Superleague) </td>
                <th> {0} </th>
                <th class = 'equals'> {1} </td>
                <th> {2} </th>
                <td> Φετινά γκολ (Superleague) </td>
                </tr>
                ", pl1.presentGoalsSL, pl1.presentGoalsSL - pl2.presentGoalsSL, pl2.presentGoalsSL));
            }

            if (pl1.presentAppsFL - pl2.presentAppsFL > 0)
            {
                sb.Append(String.Format(@"
                <tr>
                <td> Φετινές Εμφανίσεις (Football League) </td>
                <th> {0} </th>
                <th class = 'greater'> +{1} </td>
                <th> {2} </th>
                <td> Φετινές Εμφανίσεις (Football League) </td>
                </tr>
                ", pl1.presentAppsFL, pl1.presentAppsFL - pl2.presentAppsFL, pl2.presentAppsFL));
            }

            else if (pl1.presentAppsFL - pl2.presentAppsFL < 0)
            {
                sb.Append(String.Format(@"
                <tr>
                <td> Φετινές Εμφανίσεις (Football League) </td>
                <th> {0} </th>
                <th class = 'smaller'> {1} </td>
                <th> {2} </th>
                <td> Φετινές Εμφανίσεις (Football League) </td>
                </tr>
                ", pl1.presentAppsFL, pl1.presentAppsFL - pl2.presentAppsFL, pl2.presentAppsFL));
            }

            else
            {
                sb.Append(String.Format(@"
                <tr>
                <td> Φετινές Εμφανίσεις (Football League) </td>
                <th> {0} </th>
                <th class = 'equals'> {1} </td>
                <th> {2} </th>
                <td> Φετινές Εμφανίσεις (Football League) </td>
                </tr>
                ", pl1.presentAppsFL, pl1.presentAppsFL - pl2.presentAppsFL, pl2.presentAppsFL));
            }

            if (pl1.presentGoalsFL - pl2.presentGoalsFL > 0)
            {
                sb.Append(String.Format(@"
                <tr>
                <td> Φετινά γκολ (Football League) </td>
                <th> {0} </th>
                <th class = 'greater'> +{1} </td>
                <th> {2} </th>
                <td> Φετινά γκολ (Football League) </td>
                </tr>
                ", pl1.presentGoalsFL, pl1.presentGoalsFL - pl2.presentGoalsFL, pl2.presentGoalsFL));
            }

            else if (pl1.presentGoalsFL - pl2.presentGoalsFL < 0)
            {
                sb.Append(String.Format(@"
                <tr>
                <td> Φετινά γκολ (Football League) </td>
                <th> {0} </th>
                <th class = 'smaller'> {1} </td>
                <th> {2} </th>
                <td> Φετινά γκολ (Football League) </td>
                </tr>
                ", pl1.presentGoalsFL, pl1.presentGoalsFL - pl2.presentGoalsFL, pl2.presentGoalsFL));
            }

            else
            {
                sb.Append(String.Format(@"
                <tr>
                <td> Φετινά γκολ (Football League) </td>
                <th> {0} </th>
                <th class = 'equals'> {1} </td>
                <th> {2} </th>
                <td> Φετινά γκολ (Football League) </td>
                </tr>
                ", pl1.presentGoalsFL, pl1.presentGoalsFL - pl2.presentGoalsFL, pl2.presentGoalsFL));
            }

            if (numOfTeams1 > numOfTeams2)
            {
                sb.Append(String.Format(@"
                <tr>
                <td> Σύνολο προηγούμενων ομάδων </td>
                <th> {0} </th>
                <th class = 'greater'> +{1} </td>
                <th> {2} </th>
                <td> Σύνολο προηγούμενων ομάδων </td>
                </tr>
                ", numOfTeams1, numOfTeams1 - numOfTeams2, numOfTeams2));
            }

            else if (numOfTeams1 < numOfTeams2)
            {
                sb.Append(String.Format(@"
                <tr>
                <td> Σύνολο προηγούμενων ομάδων </td>
                <th> {0} </th>
                <th class = 'smaller'> {1} </td>
                <th> {2} </th>
                <td> Σύνολο προηγούμενων ομάδων </td>
                </tr>
                ", numOfTeams1, numOfTeams1 - numOfTeams2, numOfTeams2));
            }

            else
            {
                sb.Append(String.Format(@"
                <tr>
                <td> Σύνολο προηγούμενων ομάδων </td>
                <th> {0} </th>
                <th class = 'equals'> {1} </td>
                <th> {2} </th>
                <td> Σύνολο προηγούμενων ομάδων </td>
                </tr>
                ", numOfTeams1, numOfTeams1 - numOfTeams2, numOfTeams2));
            }




            if (simps.Count() == 0)
            {
                sb.Append(@"
                
                <tr> 
                <td colspan = 5> Δεν υπάρχουν ομάδες στις οποίες να έχουν αγωνιστεί και οι δύο παίκτες </td>
                </tr>
                </table></div>
                ");
            }

            else
            {//<table id = 'formerTeamsComparison'>
                sb.Append(@"
                
                <tr> 
                <td colspan = 5> Οι ομάδες στις οποίες έχουν αγωνιστεί και οι δύο παίκτες είναι οι εξής: </td>
                </tr>
                ");

                foreach (PastTeams pastTeam in simps)
                {
                    sb.Append(String.Format(@"
                <tr> 
                <th colspan = 3> {0} </th>
                <td colspan = 2> ({1}) </td>
                </tr>
                ", pastTeam.team, pastTeam.country));
                } //foreach

                sb.Append("</table></div>");
            } //else

            return sb.ToString();

        }

    }
}