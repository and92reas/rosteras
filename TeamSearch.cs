using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;

namespace Rosteras
{
    public class TeamSearch : Search
    {
        private List<PastTeams> teamsSearched;

        public TeamSearch(String nam):base(nam)
        {
            teamsSearched = TeamsConnection.getTeamsSearched(nam);
        }

        public override String displaySearchedElements()
        {
            if(teamsSearched.Count==0) {
                return "Δεν βρέθηκε καμία ομάδα";
            }
            StringBuilder sb = new StringBuilder();
            sb.Append(@"
            <table id = 'tSearched'>
            ");

            foreach (PastTeams pst in teamsSearched)
            {
                sb.Append(String.Format(@"
                <tr>
                <th> {0} </th>
                <td> {2} </td>
                <td> {1} </td>
                </tr>",pst.team,pst.playerName,pst.country));
            }

            sb.Append("</table>");

            return sb.ToString();

        }
    }
}