using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Rosteras
{
    public partial class MasterDatabase : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void pastTeamSearched_Click(object sender, EventArgs e)
        {
            TeamSearch s = new TeamSearch(teamSearched.Text);
            searchRes.Text = s.displaySearchedElements();
            
        }

        protected void search_Click(object sender, EventArgs e)
        {
            PlayerSearch s = new PlayerSearch(playerSearched.Text,14234);
            //searchRes.Text = s.displaySearchedElements();
            searchRes.Text = s.displaySearchedElements();
        }

        
    }
}