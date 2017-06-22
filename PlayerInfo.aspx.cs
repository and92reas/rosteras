using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Rosteras
{
    public partial class PlayerInfo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            loading();
        }

        protected void SearchButton_Click(object sender, EventArgs e)
        {
            /*if (SearchButton1.Text.Equals("Search"))
            {
                SearchButton1.Text = "Hide";
                PlayersPassedFromTeam ppft = new PlayersPassedFromTeam(ddlTeams3.SelectedValue);
                playersPassedFromTeamHTML.Text = ppft.getThePlayers();
            }
            else
            {
                SearchButton1.Text = "Search";
                playersPassedFromTeamHTML.Text = "";
            }


            //SearchButton1.Attributes.Remove("onclick");
            //resultsBelow2.Text = "Αποτελέσματα παρακάτω";
        }

        protected void SearchButton2_Click(object sender, EventArgs e)
        {
            if (SearchButton2.Text.Equals("Search"))
            {
                SearchButton2.Text = "Hide";
                PlayerComingFromCountry pcfc = new PlayerComingFromCountry(ddlTeams4.SelectedValue);
                playersComingFromCountryHTML.Text = pcfc.getThePlayers();
            }
            else
            {
                SearchButton2.Text = "Search";
                playersComingFromCountryHTML.Text = "";
            }

            //resultsBelow2.Text = "Αποτελέσματα παρακάτω";
        }

        protected void SearchButton3_Click(object sender, EventArgs e)
        {
            if (SearchButton3.Text.Equals("Search"))
            {
                SearchButton3.Text = "Hide";
                PlayerPlayedInCountry ppic = new PlayerPlayedInCountry(ddlTeams5.SelectedValue);
                playersPlayedInCountryHTML.Text = ppic.getThePlayers();
            }
            else
            {
                SearchButton3.Text = "Search";
                playersPlayedInCountryHTML.Text = "";
            }*/

            //resultsBelow2.Text = "Αποτελέσματα παρακάτω";
        }

        private void loading()
        {
            String playerID = Request.QueryString["playerID"];
            String team = Request.QueryString["team"];
            String league = Request.QueryString["league"];

            if (league.Equals("Superleague"))
            {
                PlayerOfSuperleagueTeam ps = new PlayerOfSuperleagueTeam(playerID,team);
                playerTeamDataHTML.Text = ps.playerTeamDataLoading();
                playerInfoHTML.Text = ps.playerInfoLoading();
                PlayerOrderHTML.Text = ps.playerOrderInfoLoading();
                //individualsHTML.Text = tm.individualLoading();
                //teamInfoHTML.Text = t.teamInfoLoading();
                //morePlayerInfoHTML.Text = tm.moreTeamInfoLoading();
            }
            else
            {
                PlayerOfFootballLeagueTeam pf = new PlayerOfFootballLeagueTeam(playerID,team);
                playerTeamDataHTML.Text = pf.playerTeamDataLoading();
                playerInfoHTML.Text = pf.playerInfoLoading();
                PlayerOrderHTML.Text = pf.playerOrderInfoLoading();
                //individualsHTML.Text = tm.individualLoading();
                //teamInfoHTML.Text = t.teamInfoLoading();
                //morePlayerInfoHTML.Text = tm.moreTeamInfoLoading();
            }

           


        }

        protected void ddlTeams3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}