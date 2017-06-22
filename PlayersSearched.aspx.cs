using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Rosteras
{
    public partial class PlayersSearched : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            loading();
        }

        private void loading()
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
            PlayerSearch s = new PlayerSearch(Request.QueryString["name"]);
            searchHeading.Text = s.getSearchHeading();
            playersSearched.Text = s.displaySearchedElements();
        }

        protected void SearchButton_Click(object sender, EventArgs e)
        {
            PlayersPassedFromTeam ppft = new PlayersPassedFromTeam(ddlTeams3.SelectedValue);
            playersPassedFromTeamHTML.Text = ppft.getThePlayers();
            //resultsBelow2.Text = "Αποτελέσματα παρακάτω";
        }

        protected void SearchButton2_Click(object sender, EventArgs e)
        {
            PlayerComingFromCountry pcfc = new PlayerComingFromCountry(ddlTeams4.SelectedValue);
            playersComingFromCountryHTML.Text = pcfc.getThePlayers();
            //resultsBelow2.Text = "Αποτελέσματα παρακάτω";
        }

        protected void SearchButton3_Click(object sender, EventArgs e)
        {
            PlayerPlayedInCountry ppic = new PlayerPlayedInCountry(ddlTeams5.SelectedValue);
            playersPlayedInCountryHTML.Text = ppic.getThePlayers();
            //resultsBelow2.Text = "Αποτελέσματα παρακάτω";
        }
    }
}