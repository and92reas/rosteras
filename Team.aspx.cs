using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Rosteras
{
    public partial class Team1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            loading();
        }

        private void loading()
        {
            String name = Request.QueryString["team"];
            String league = Request.QueryString["league"];
            if(Request.QueryString["id"]!= null) {
                String id = Request.QueryString["id"];
                Response.Redirect("#" + id);
            }

            if (league.Equals("Superleague"))
            {
                TeamOfSuperleague t = new TeamOfSuperleague(name);
                teamDataHTML.Text = t.teamDataLoading();
                playersHTML.Text = t.playersLoading();
                //individualsHTML.Text = tm.individualLoading();
                teamInfoHTML.Text = t.teamInfoLoading();
                //morePlayerInfoHTML.Text = tm.moreTeamInfoLoading();
            }
            else
            {
                TeamOfFootballLeague t = new TeamOfFootballLeague(name);
                teamDataHTML.Text = t.teamDataLoading();
                playersHTML.Text = t.playersLoading();
                //individualsHTML.Text = tm.individualLoading();
                teamInfoHTML.Text = t.teamInfoLoading();
                //morePlayerInfoHTML.Text = tm.moreTeamInfoLoading();
            }

            

        } //loading

        }
}