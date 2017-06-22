using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Rosteras
{
    public partial class ComparePlayers : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            teamSearchHTML.Text = @"
            Αναζητήστε ποιοι από τους ενεργούς παίκτες της βάσης έχουν περάσει από την (ελληνική ή ξένη) ομάδα που θα επιλέξετε:
            ";
            teamSearchHTML2.Text = @"
            Αναζητήστε ποιοι από τους ενεργούς παίκτες της βάσης κατάγονται από την χώρα που θα επιλέξετε:
            ";
            teamSearchHTML3.Text = @"
            Αναζητήστε ποιοι από τους ενεργούς παίκτες της βάσης έχουν αγωνιστεί στην χώρα που θα επιλέξετε:
            ";

        }

        protected void compareButton_Click(object sender, EventArgs e)
        {
            ComparePlayer players = new ComparePlayer(ddlPlayers.SelectedValue, ddlTeams.SelectedValue,
            ddlPlayers2.SelectedValue, ddlTeams2.SelectedValue);
            comparisonHTML.Text = players.getComparisonResults();
            //ResultsBelow.Text = "Αποτελέσματα παρακάτω";
        }


        protected void Roster_Selecting(object sender, SqlDataSourceSelectingEventArgs e)
        {
        }

        protected void Roster3_Selecting(object sender, SqlDataSourceSelectingEventArgs e)
        {

        }

        protected void SearchButton_Click(object sender, EventArgs e)
        {
            if (SearchButton1.Text.Equals("Search"))
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
            }
            
            //resultsBelow2.Text = "Αποτελέσματα παρακάτω";
        }
    }
}