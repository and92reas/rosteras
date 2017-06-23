using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

namespace Rosteras
{
    public partial class FootballLeague : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            loading();
        }

        private void loading()
        {
            Football_League fl = new Football_League();
            teamsHTML.Text = fl.LoadTeams();
            leagueInfoHTML.Text = fl.leagueInfo();
            //moreLeagueInfoHTML.Text = fl.moreLeagueInfo();
            //
            }
    }
}
