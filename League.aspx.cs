using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Rosteras
{
    public partial class League1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            loading();
        }

        private void loading()
        {
            String league = Request.QueryString["league"];
            if(league.Equals("Superleague")) {
                Superleague l = new Superleague();
                teamsHTML.Text = l.LoadTeams();
                leagueInfoHTML.Text = l.leagueInfo();
                //moreLeagueInfoHTML.Text = l.moreLeagueInfo();
            }
            else
            {
                Football_League l = new Football_League();
                teamsHTML.Text = l.LoadTeams();
                leagueInfoHTML.Text = l.leagueInfo();
                //moreLeagueInfoHTML.Text = l.moreLeagueInfo();
            }
            
            
        }

       
    }
}