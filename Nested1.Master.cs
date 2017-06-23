using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Rosteras
{
    public partial class Nested1 : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void search_Click(object sender, EventArgs e)
        {
            if (playerSearched.Text.Equals("*********"))
                Response.Redirect("Query.aspx");
            Response.Redirect("PlayersSearched.aspx?name=" + playerSearched.Text);   
        }
    }
}
