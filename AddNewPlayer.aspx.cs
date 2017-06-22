using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Transactions;
using System.Globalization;

namespace Rosteras
{
    public partial class AddNewPlayer : System.Web.UI.Page
    {
        private static Table table;
        private static List<TextBox> ts;

       
        

        protected void Page_Load(object sender, EventArgs e)
        {
            loading();
            
        }

        private void loading() {
            lpid.Text = TeamsConnection.getLastPlayerID();
        }

        protected void AdjustTextBoxes(object sender, EventArgs e)
        {
            createControls();
             
        }

        private void createControls() {
            int num = Convert.ToInt16(npptnumber.Text);
            ts = new List<TextBox>();
            if (num > 0)
            {

                table = new Table();
                table.ID = "pastTeams";
                TableCell cell;
                TableCell cell2;
                TableRow row;
                TextBox t1;
                TextBox t2;

                cell = new TableCell();
                cell2 = new TableCell();
                row = new TableRow();
                cell.Text = "TEAM";
                cell2.Text = "COUNTRY";
                row.Cells.Add(cell);
                row.Cells.Add(cell2);
                table.Rows.Add(row);
                for (int i = 0; i < num; i++)
                {
                    row = new TableRow();
                    cell = new TableCell();
                    cell2 = new TableCell();
                    t1 = new TextBox();
                    t2 = new TextBox();
                    t1.ID = "pt" + i;
                    t1.CssClass = "pastTeamName";
                    t2.ID = "ptc" + i;
                    t2.CssClass = "pastTeamCountry";
                    cell.Controls.Add(t1);
                    cell2.Controls.Add(t2);
                    ts.Add(t1);
                    ts.Add(t2);
                    row.Cells.Add(cell);
                    row.Cells.Add(cell2);
                    table.Rows.Add(row);
                }
                PastTeamsPanel.Controls.Add(table);
            }
        }

        private string GetValue(string ControlID)
        {
            string[] keys = Request.Form.AllKeys;
            string value = string.Empty;
            foreach (string key in keys)
            {
                if (key.IndexOf(ControlID) >= 0)
                {
                    value = Request.Form[key].ToString();
                    break;
                }
            }

            return value;
        }

        protected void newPlayerSubmit_Click(object sender, EventArgs e)
        {
            bool flag = false;
            if (npid.Text.Equals(""))
            {
                npid.Text = "FILL ME";
                flag = true;
            }
            if (npname.Text.Equals(""))
            {
                npname.Text = "FILL ME";
                flag = true;
            }
            if (npteam.Text.Equals(""))
            {
                npteam.Text = "FILL ME";
                flag = true;
            }
            if (npheight.Text.Equals(""))
            {
                npheight.Text = "FILL ME";
                flag = true;
            }
            if (npsgoals.Text.Equals(""))
            {
                npsgoals.Text = "FILL ME";
                flag = true;
            }
            if (npfgoals.Text.Equals(""))
            {
                npfgoals.Text = "FILL ME";
                flag = true;
            }
            if (npage.Text.Equals(""))
            {
                npage.Text = "FILL ME";
                flag = true;
            }
            if (npposition.Text.Equals(""))
            {
                npposition.Text = "FILL ME";
                flag = true;
            }
            if (npcountry.Text.Equals(""))
            {
                npcountry.Text = "FILL ME";
                flag = true;
            }
            if (npsapps.Text.Equals(""))
            {
                npsapps.Text = "FILL ME";
                flag = true;
            }
            if (npfapps.Text.Equals(""))
            {
                npfapps.Text = "FILL ME";
                flag = true;
            }
            if (nploanedby.Text.Equals(""))
            {
                nploanedby.Text = "FILL ME";
                flag = true;
            }
            if (npspapps.Text.Equals(""))
            {
                npspapps.Text = "FILL ME";
                flag = true;
            }
            if (npfpapps.Text.Equals(""))
            {
                npfpapps.Text = "FILL ME";
                flag = true;
            }
            if (npspgoals.Text.Equals(""))
            {
                npspgoals.Text = "FILL ME";
                flag = true;
            }
            if (npfpgoals.Text.Equals(""))
            {
                npfpgoals.Text = "FILL ME";
                flag = true;
            }
            if (npptnumber.Text.Equals(""))
            {
                npptnumber.Text = "FILL ME";
                flag = true;
            }
            if (flag)
            {
                return;
            }
            using (TransactionScope tran = new TransactionScope())
            {
                float h = float.Parse(npheight.Text);
                /*float hei = (float)h;
                if (h.ToString().Length == 3)
                {
                    hei =(float) (h * 0.01f);
                }
                else if ((h.ToString().Length == 2))
                {
                    hei = (double) (h * 0.1f);
                }*/
                Player player = new Player(npid.Text, npname.Text, npteam.Text, h, Convert.ToInt16(npsgoals.Text)
                , Convert.ToInt16(npfgoals.Text), Convert.ToInt16(npage.Text), npposition.Text, npcountry.Text, Convert.ToInt16(npsapps.Text),
                Convert.ToInt16(npfapps.Text), nploanedby.Text, Convert.ToInt16(npspapps.Text), Convert.ToInt16(npfpapps.Text), Convert.ToInt16(npspgoals.Text),
                Convert.ToInt16(npfpgoals.Text));
                int res = TeamsConnection.addNewPlayer(player);
                

                if (res == -1)
                {
                    newPlayerWarnings.Text = "An error occured while inserting the players' personal data";
                    return;
                }

                int ptnum = Convert.ToInt16(npptnumber.Text);

                if (ptnum == 0)
                {
                    tran.Complete();
                    Response.Redirect(Request.RawUrl);
                    return;
                }
                List<PastTeams> psts = new List<PastTeams>();
                bool fl = false;
                String team = null;
                String country = null;

                foreach (TextBox t in ts)
                {
                    if (!fl)
                    {
                        team = GetValue(t.ID);
                        fl = true;
                    }
                    else
                    {
                        country = GetValue(t.ID);
                        psts.Add(new PastTeams(npid.Text, team, country));
                        fl = false;
                    }
                }
                    /*int counter = 0;
                    foreach (TableRow row in table.Rows)
                    {
                        bool fl = false;
                        string team = null;
                        string country = null;
                        TextBox t = null;
                        foreach (TableCell cell in row.Cells)
                        {
                            if (!cell.Text.Equals("TEAM") && !cell.Text.Equals("COUNTRY"))
                            {
                                if (!fl) {
                                    t = cell.FindControl("pt" + counter) as TextBox;
                                    team = t.Text;
                                }
                                else
                                {
                                    t = cell.FindControl("ptc" + counter) as TextBox;
                                    country = t.Text;
                                    psts.Add(new PastTeams(npid.Text, team, country));
                                    counter++;
                                }
                                fl = true;
                            }
                        }
                    }*/

                    res = TeamsConnection.addPastTeamsOfNewPlayer(psts);
                if (res == -1)
                {
                    newPlayerWarnings.Text = "An error occured while inserting the players' past teams' data";
                    return;
                }
                tran.Complete();
            }
            Response.Redirect(Request.RawUrl); 
        }

        
    }
}