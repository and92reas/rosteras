using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;

namespace Rosteras
{
    public class TeamOfLeague
    {
        String t;
        String l;
        StringBuilder sb;
        List<Player> players;
        List<PastTeams> pastTeams;
        Team team;
        List<Player> loaned;
        List<Appearance> currentAppearances;
        List<Goalscorer> currentGoals;
        List<Appearance> overallAppearances;
        List<Goalscorer> overallGoals;
        String avHeight;
        String avAge;
        List<Traveller> travellers;
        int formerPlayers;
        int foreigners;
        Info info;
        //String foreignGoals;

        public TeamOfLeague(String tm, String league)
        {
            t = tm;
            l = league;
            players = TeamsConnection.displayPlayers(t);
            pastTeams = TeamsConnection.loadPastTeams();
            team = TeamsConnection.displayCurrentTeam(t,l);
            loaned = TeamsConnection.LoanedPlayers(t);
            currentAppearances = TeamsConnection.top5Appearances(t, l, true);
            currentGoals = TeamsConnection.Top5goalscorers(t, l, true);
            overallAppearances = TeamsConnection.top5Appearances(t, l, false);
            overallGoals = TeamsConnection.Top5goalscorers(t, l, false);
            avHeight = TeamsConnection.averageHeight(t);
            avAge = TeamsConnection.averageAge(t);
            travellers = TeamsConnection.top3travellers(t);
            formerPlayers = TeamsConnection.FormerPlayers(t);
            foreigners = TeamsConnection.foreigners(t);
            info = TeamsConnection.teamInfo(t);
            
            //foreignGoals = TeamsConnection.TeamGoalsFromForeigners(t, l);
        }

        public String teamDataLoading()
        {
            sb = new StringBuilder();
            sb.Append(string.Format(@"
              <div id = '{0}'>
                  <img id = 'teamImage' src= '{1}' alt = '{2}' >
                    <div id = 'teamName'> {2} </div> 
              </div>", info.cssid,info.image,info.capitals));
             
            return sb.ToString();
        }


        public String playersLoading()
        {
            ////////////////////////////////////////////////////players

            sb = new StringBuilder();
            int apps;
            int goals;
            
            sb.Append(string.Format(@"
                <table class = 'players'>
                <tr> 
                <th> Όνομα </th>
                <th> Εμφανίσεις </th>
                <th> Γκολ </th>
                <th> Θέση </th>
                <th> Ηλικία </th>
                </tr>"));

            foreach (Player player in players)
            {
                if (team.league.Equals("Superleague"))
                {
                    apps = player.presentAppsSL;
                    goals = player.presentGoalsSL;
                }
                else
                {
                    apps = player.presentAppsFL;
                    goals = player.presentGoalsFL;
                }

                if (player.lentBy.Equals("-"))
                {
                    sb.Append(String.Format(@"<tr> 
                <th> <a href = 'PlayerInfo.aspx?playerID={5}&team={6}&league={7}'> {0} </a>  </th>
                <th> {1} </th>
                <th> {2} </th>
                <td> {3} </td>
                <td> {4} </td>
                </tr>
                <tr id = '{5}' class = 'hidden'><td colspan = 5>
                ", player.name, apps, goals, player.position, player.age, player.playerID, player.presentTeam,l));
                individualLoading(player,sb);
                sb.Append("</td></tr>");
                }

                else
                {
                    sb.Append(String.Format(@"
                <tr>                              
                <th> <a href = 'PlayerInfo.aspx?playerID={5}&team={6}&league={7}' class = 'lentPlayer' > {0} </a>  </th>
                <th> {1} </th>
                <th> {2} </th>
                <td> {3} </td>
                <td> {4} </td> 
                </tr> 
                <tr id = '{5}' class = 'hidden'><td colspan = 5>
                ", player.name, apps, goals, player.position, player.age, player.playerID,player.presentTeam,l));
                 individualLoading(player,sb);
                 sb.Append("</td></tr>");
                }
                
            }//foreach
            sb.Append(string.Format(@"</table>"));

            return sb.ToString();
        } //players





        public void individualLoading(Player player,StringBuilder sb)
        {

            //sb = new StringBuilder();
            bool ptfound = false;
            //foreach (Player player in players)
            {
                if (player.height == 0)
                {
                String unknown = "(Άγνωστο)";
                sb.Append(String.Format(@"
                <table class = 'individuals'>
                <tr>
                <td colspan = 2> Ύψος: </td>
                <th> {0} </th>
                </tr>
                <tr>
                <td colspan = 2> Χώρα: </td>
                <th> {1} </th>
                </tr>
                    ",unknown,player.country));
                }
                
                else
                {
                    sb.Append(String.Format(@"
                <table class = 'individuals'>
                <tr>
                <td colspan = 2> Ύψος: </td>
                <th> {0} </th>
                </tr>
                <tr>
                <td colspan = 2> Χώρα: </td>
                <th> {1} </th>
                </tr>
                    ",player.height, player.country));
                } //else

                if (!player.lentBy.Equals('-'))
                {
                    sb.Append(String.Format(@"
                <tr>
                <td colspan = 2> Δανεικός Από: </td>
                <th> {0} </th>
                </tr>", player.lentBy));
                }

                if (l.Equals("Superleague"))
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
                <td colspan = 2> Συνολικές Συμμετοχές (Football League): </td>
                <th> {2} </th>
                </tr>
                <tr>
                <td colspan = 2> Συνολικά Γκολ (Football League): </td>
                <th> {3} </th>
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
                <td colspan = 2> Φετινές Συμμετοχές (Football League): </td>
                <th> {6} </th>
                </tr>
                <tr>
                <td colspan = 2> Φετινά Γκολ (Football League): </td>
                <th> {7} </th>
                </tr>
                <tr>
                <td colspan = 3> Προηγούμενες Ομάδες </td>
                </tr>
                ", player.overallAppsSL, player.overallGoalsSL, player.overallAppsFL, player.overallGoalsFL, player.presentAppsSL,
                     player.presentGoalsSL, player.presentAppsFL, player.presentGoalsFL));
                } //if
                
                else {
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
                <td colspan = 2> Συνολικές Συμμετοχές (Football League): </td>
                <th class = 'overall'> {2} </th>
                </tr>
                <tr>
                <td colspan = 2> Συνολικά Γκολ (Football League): </td>
                <th class = 'overall'> {3} </th>
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
                <td colspan = 2> Φετινές Συμμετοχές (Football League): </td>
                <th class = 'present'> {6} </th>
                </tr>
                <tr>
                <td colspan = 2> Φετινά Γκολ (Football League): </td>
                <th class = 'present'> {7} </th>
                </tr>
                <tr>
                <td colspan = 3> Προηγούμενες Ομάδες </td>
                </tr>
                ", player.overallAppsSL, player.overallGoalsSL, player.overallAppsFL, player.overallGoalsFL, player.presentAppsSL,
                     player.presentGoalsSL, player.presentAppsFL, player.presentGoalsFL));
                } //else

                ptfound = false;
                foreach (PastTeams pastTeam in pastTeams)
                {
                    if (player.playerID.Equals(pastTeam.playerID))
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
                    }
                } //foreach
                if (!ptfound)
                {
                    sb.Append(String.Format(@"
                  <tr>
                  <td> </td>
                  <td colspan = 2> Καμία </td>
                  </tr>"));
                }
               
            } //foreach
            sb.Append(String.Format("@</table>"));

            //return sb.ToString();
        }








        public string teamInfoLoading()
        {
            sb = new StringBuilder();

            sb.Append(String.Format(@"
        <table id = 'teamInfo' > 
        <tr>
        <th colspan = 3> {0} </th>
        </tr>
        <tr>
        <th colspan = 3> {1} </th>
        </tr>
        <tr>
        <td colspan = 2> Έτος Ίδρυσης </td>
        <th> {2} </th>
        </tr>
        <tr>
        <td colspan = 2> Πρωταθλήματα </td>
        <th> {3} </th>
        </tr>
        <tr>
        <td colspan = 2> Κύπελλα </td>
        <th> {4} </th>
        </tr>
        <tr>
        <td colspan = 2> Τελευταίο Τρόπαιο </td>
        <th> {5} </th>
        </tr>
        <tr>
        <td colspan = 2> Έδρα </td>
        <th> {6} </th>
        </tr>
        <tr>
        <td colspan = 2> Προπονητής </td>
        <th> {7} </th>
        </tr>
        <tr>
        <td colspan = 2> Πρόεδρος </td>
        <th> {8} </th>
        </tr>
        </table>
        ", team.league, team.area, team.foundationYear, team.leaguesWon, team.cupsWon, team.lastTrophy, team.field, team.manager, team.chairman));

            sb.Append(String.Format(@"
        <table id = 'teamInfo2'> 
        <tr>
        <th> Περισσότερες Πληροφορίες </th>
        </tr>
        <tr>
        <td> <a href='#loaned' onclick = 'HideAndShow(&#39;loaned&#39;,0)'> Δανεικοί παίκτες σε άλλες ομάδες των πρώτων δύο κατηγοριών </a> </td>
        </tr>
        <tr id = 'loaned' class = 'hidden'>
        <td>
        <table class = 'moreInfo2'>
        "));
       
            foreach (Player player in loaned)
            {
                if (!player.presentTeam.Equals("Εκτός Βάσης"))
                {
                    sb.Append(String.Format(@"
            <tr>
            <th colspan = 2> <a href='PlayerInfo.aspx?playerID={2}&team={1}&league={3}' onclick = 'HideAndShow(&#39;{2}&#39;,1)'> {0} </a> </th>
            <td> (<a href ='Team.aspx?team={1}&league={3}'> {1} </a>) </td>
            </tr> 
            ", player.name, player.presentTeam, player.playerID,l));
                }//if
            }

        sb.Append(String.Format(@"
        </table>
        </td>
        </tr>
        <tr>
        <td> <a href='#currentApps' onclick = 'HideAndShow(&#39;currentApps&#39;,0)'> Πρώτοι 5 της ομάδας σε συμμετοχές στη {0} φέτος </a> </td>
        </tr>
        <tr id = 'currentApps'  class = 'hidden'>
        <td>
        <table class = 'moreInfo2'>
        ", l));

            foreach (Appearance app in currentAppearances)
            {
                sb.Append(String.Format(@"
            <tr>
            <th colspan = 2> <a href = '#{2}'  onclick = 'HideAndShow(&#39;{2}&#39;,1)'> {0} </a> </th>
            <td> {1} </td>
            </tr>
            ", app.name, app.appearances, app.playerID));
            }
        
        sb.Append(String.Format(@"
        </table>
        </td>
        </tr>
        <tr>
        <td> <a href='#currentGoals' onclick = 'HideAndShow(&#39;currentGoals&#39;,0)'> Πρώτοι 5 σκόρερ της ομάδας στη {0} φέτος</a> </td>
        </tr> 
        <tr id = 'currentGoals' class = 'hidden'>
        <td>
        <table class = 'moreInfo2'>
        ", l));

        foreach (Goalscorer scorer in currentGoals)
        {
            sb.Append(String.Format(@"
            <tr>
            <th colspan = 2> <a href = '#{2}' onclick = 'HideAndShow(&#39;{2}&#39;,1)'> {0} </a> </th>
            <td> {1} </td>
            </tr>
            ", scorer.name, scorer.goals, scorer.playerID));
        }



            sb.Append(String.Format(@"
        </table>
        </td>
        </tr>
        <tr>
        <td> <a href='#overallApps' onclick = 'HideAndShow(&#39;overallApps&#39;,0)'> Πρώτοι 5 της ομάδας σε συμμετοχές στη {0} διαχρονικά </a> </td>
        </tr>
        <tr id = 'overallApps' class = 'hidden'>
        <td>
        <table class = 'moreInfo2'>
        ", l));

            foreach (Appearance app in overallAppearances)
            {
                sb.Append(String.Format(@"
            <tr>
            <th colspan = 2> <a href = '#{2}'  onclick = 'HideAndShow(&#39;{2}&#39;,1)'> {0} </a> </th>
            <td> {1} </td>
            </tr>
            ", app.name, app.appearances, app.playerID));
            }


            sb.Append(String.Format(@"
            </table>
        </td>
        </tr>
        <tr>
        <td> <a href ='#overallGoals' onclick = 'HideAndShow(&#39;overallGoals&#39;,0)'> Πρώτοι 5 σκόρερ της ομάδας στη {0} διαχρονικά </a> </td>
        </tr>
        <tr id ='overallGoals' class = 'hidden'>
        <td>
        <table class = 'moreInfo2'>
        ", l));

            foreach (Goalscorer scorer in overallGoals)
            {
                sb.Append(String.Format(@"
            <tr>
            <th colspan = 2> <a href = '#{2}' onclick = 'HideAndShow(&#39;{2}&#39;,1)'> {0} </a> </th>
            <td> {1} </td>  
            </tr>
            ", scorer.name, scorer.goals, scorer.playerID));
            }

            sb.Append(String.Format(@"
        </table>
        </td>
        </tr>        
        <tr>
        <td> <a href = '#avAge' onclick = 'HideAndShow(&#39;avAge&#39;,0)'> Μέσος όρος ηλικίας της ομάδας </a> </td>
        </tr>
        <tr id = 'avAge' class = 'hidden'>
        <td>"));
        sb.Append(String.Format(@"
        <table class = 'moreInfo2'>
        <tr>
        <th colspan = 2> {0} </th>
        <td> χρόνια </td>
        <tr>
        </table>",avAge));
        sb.Append(String.Format(@"
        </td>
        </tr>
        <tr> 
        <td> <a href = '#avHeight' onclick = 'HideAndShow(&#39;avHeight&#39;,0)'> Ο μέσος όρος ύψους της ομαδας είναι <td>
        </tr>       
        <tr id = 'avHeight' class = 'hidden'>
        <td>
        <table class = 'moreInfo2'>
        <tr>
        <th colspan = 2> {0} </th>
        <td> μέτρα </td>
        </table>
        ", avHeight));
        sb.Append(String.Format(@"
        </td>
        </tr>
        <tr>
        <td> <a href = '#travellers' onclick = 'HideAndShow(&#39;travellers&#39;,0)'> Οι 3 παίκτες της ομάδας που έχουν παίξει στις περισσότερες ομάδες </a> </td>
        </tr>
        <tr id = 'travellers' class = 'hidden'>
        <td>
        <table class = 'moreInfo2'>
        "));
            foreach (Traveller traveller in travellers)
            {
                sb.Append(String.Format(@"
            <tr>
            <th colspan = 2> <a href = '#{0}' onclick = 'HideAndShow(&#39;{0}&#39;,1)'> {1} </a> </th>
            <td> {2} ομάδες</td>
            </tr>
            ", traveller.playerID,traveller.name, traveller.numOfTeams));
            }

            sb.Append(String.Format(@"
        </table>
        </td>
        </tr>
        <tr>
        <td> <a href = '#foreigners' onclick = 'HideAndShow(&#39;foreigners&#39;,0)'> Πόσοι από τους παίκτες της ομάδας έχουν αγωνιστεί στο εξωτερικό </a> </td>
        </tr>
        <tr id = 'foreigners' class = 'hidden'>
        <td>
        <table class = 'moreInfo2'>
        <tr>
        <td colspan = 3> Οι παικτες που αυτή τη στιγμή βρίσκονται στο ρόστερ της ομάδας </td>
        </tr>
        <tr>
        <td colspan = 3> και που στο παρελθόν έχουν περάσει από ομάδα του εξωτερικού είναι </td>
        </tr>
        <tr>
        <th colspan = 3> {0} </th>
        </table> 
        ", foreigners));
            sb.Append(String.Format(@"
        </td>
        </tr>
        <tr>
        <td> <a href = '#formerPlayers' onclick = 'HideAndShow(&#39;formerPlayers&#39;,0)'> Πόσοι από τους ενεργούς παίκτες των άλλων ομάδων των δύο πρώτων κατηγοριών έχουν περάσει από την ομάδα </a> </td>
        </tr>
        <tr id = 'formerPlayers' class = 'hidden'>
        <td>
        <table class = 'moreInfo2'>
        <tr>
        <td colspan = 3> Οι ενεργοί παίκτες των δύο πρώτων κατηγοριών που </td>
        </tr>
        <tr>
        <td colspan = 3> στο παρελθόν έχουν περάσει από την ομάδα είναι </td>
        </tr>
        <tr>
        <th colspan = 3> {0} </th>
        </table>", formerPlayers));
        sb.Append(@"
        </td>
        </tr>
        </table>");

            return sb.ToString();
        } //////////////////////////////////////////////////////////////////////////////////////////////more info

        /*public String moreTeamInfoLoading()
        {

            sb = new StringBuilder();
            sb.Append(@"
        <table id = 'firstMoreInfo' class = 'moreInfo2'>
        <tr>
        <th colspan = 3> ΔANEIKOI ΣΕ ΕΛΛΗΝΙΚΕΣ ΟΜΑΔΕΣ </th>
        </tr>");
            sb.Append(@"
        <p>
        <a name = 'loaned' id = 'loaned'> </a>
        </p>");


            foreach (Player player in loaned)
            {
                if (!player.presentTeam.Equals("Εκτός Βάσης"))
                {
                    sb.Append(String.Format(@"
            <tr>
            <th colspan = 2> <a href='Team.aspx?team={1}&league={3}#{2}'> {0} </a> </th>
            <td> (<a href ='Team.aspx?team={1}&league={3}'> {1} </a>) </td>
            </tr> 
            ", player.name, player.presentTeam, player.playerID,l));
                }//if
            }

            sb.Append(@"
        </table>
        <table class = 'moreInfo2'>
        <tr>
        <th colspan = 3> TOP 5 ΣΕ ΣΥΜΜΕΤΟΧΕΣ ΦΕΤΟΣ </th>
        </tr>");

            sb.Append(@"
        <p>
        <a name = 'currentApps' id = 'currentApps'> </a>
        </p>");

            foreach (Appearance app in currentAppearances)
            {
                sb.Append(String.Format(@"
            <tr>
            <th colspan = 2> <a href = '#{2}'> {0} </a> </th>
            <td> {1} </td>
            </tr>
            ", app.name, app.appearances, app.playerID));
            }



            sb.Append(@"
        </table>
        <table class = 'moreInfo2'>
        <tr>
        <th colspan = 3> TOP 5 ΣΚΟΡΕΡ ΦΕΤΟΣ </th>
        </tr>");

            sb.Append(@"
        <p>
        <a name = 'currentGoals' id = 'currentGoals'> </a>
        </p>");

            foreach (Goalscorer scorer in currentGoals)
            {
                sb.Append(String.Format(@"
            <tr>
            <th colspan = 2> <a href = '#{2}'> {0} </a> </th>
            <td> {1} </td>
            </tr>
            ", scorer.name, scorer.goals, scorer.playerID));
            }

            sb.Append(@"
        </table>
        <table class = 'moreInfo2'>
        <tr>
        <th colspan = 3> TOP 5 ΣΕ ΣΥΜΜΕΤΟΧΕΣ ΔΙΑΧΡΟΝΙΚΑ </th>
        </tr>");

            sb.Append(@"
        <p>
        <a name = 'overallApps' id = 'overallApps'> </a>
        </p>");

            foreach (Appearance app in overallAppearances)
            {
                sb.Append(String.Format(@"
            <tr>
            <th colspan = 2> <a href = '#{2}'> {0} </a> </th>
            <td> {1} </td>
            </tr>
            ", app.name, app.appearances, app.playerID));
            }


            sb.Append(@"
            </table>
            <table class = 'moreInfo2'>
            ");
            sb.Append(@"
        <p>
        <a name = 'overallGoals' id = 'overallGoals'> </a>
        </p>");

            foreach (Goalscorer scorer in overallGoals)
            {
                sb.Append(String.Format(@"
            <tr>
            <th colspan = 2> <a href = '#{2}'> {0} </a> </th>
            <td> {1} </td>
            </tr>
            ", scorer.name, scorer.goals, scorer.playerID));
            }

            sb.Append(String.Format(@"
        </table>
        <table class = 'moreInfo2'>
        <tr>
        <th colspan = 3> ΜΕΣΟΣ ΟΡΟΣ ΗΛΙΚΙΑΣ </th>
        </tr>
        <p>
        <a name = 'avAge' id = 'AvAge'> </a>
        </p>
        <tr> 
        <td colspan = 3> Ο μέσος όρος ηλικίας της ομαδας είναι <td>
        </tr>
        <tr>
        <th colspan = 2> {0} </th>
        <td> χρόνια </td>
        <tr>
        </table>
        <table class = 'moreInfo2'>
        <th colspan = 3> ΜΕΣΟΣ ΟΡΟΣ ΥΨΟΥΣ </th>
        </tr>
        <p>
        <a name = 'avHeight' id = 'avHeight'> </a>
        </p>
        <tr> 
        <td colspan = 3> Ο μέσος όρος ύψους της ομαδας είναι <td>
        </tr>
        <tr>
        <th colspan = 2> {1} </th>
        <td> μέτρα </td>
        </table>
        ", avAge, avHeight));



            sb.Append(@"
        <table class = 'moreInfo2'>
        <tr>
        <th colspan = 3> TOP 3 ΓΥΡΟΛΟΓΟΙ </th>
        </tr>");
            sb.Append(@"
        <p>
        <a name = 'travellers' id = 'travellers'> </a>
        </p>");

            foreach (Traveller traveller in travellers)
            {
                sb.Append(String.Format(@"
            <tr>
            <th colspan = 2> <a href = '#{0}'> {1} </a> </th>
            <td> {2} ομάδες</td>
            </tr>
            ", traveller.playerID,traveller.name, traveller.numOfTeams));
            }

            sb.Append(String.Format(@"
        </table>
        <table class = 'moreInfo2'>
        <tr>
        <th colspan = 3> ΠΑΙΚΤΕΣ ΠΟΥ ΕΧΟΥΝ ΠΑΙΞΕΙ ΕΞΩΤΕΡΙΚΟ </th>
        </tr>
        <p>
        <a name = 'foreigners' id = 'foreigners'> </a>
        </p>
        <tr>
        <td colspan = 3> Οι παικτες που αυτή τη στιγμή βρίσκονται στο ρόστερ της ομάδας </td>
        </tr>
        <tr>
        <td colspan = 3> και που στο παρελθόν έχουν περάσει από ομάδα του εξωτερικού είναι </td>
        </tr>
        <tr>
        <th colspan = 3> {0} </th>
        </table> 
        ", foreigners));


            sb.Append(String.Format(@"
        </table>
        <table class = 'moreInfo2'>
        <tr>
        <th colspan = 3> ΕΝΕΡΓΟΙ ΠΡΩΗΝ ΠΑΙΚΤΕΣ </th>
        </tr>
        <p>
        <a name = 'formerPlayers' id = 'formerPlayers'> </a>
        </p>
        <tr>
        <td colspan = 3> Οι ενεργοί παίκτες των δύο πρώτων κατηγοριών που </td>
        </tr>
        <tr>
        <td colspan = 3> στο παρελθόν έχουν περάσει από την ομάδα είναι </td>
        </tr>
        <tr>
        <th colspan = 3> {0} </th>
        </table>", formerPlayers));

            return sb.ToString();

        }*/ //////////////////////////////////////////////////////////////////////////////////////////////more info2
    }
}