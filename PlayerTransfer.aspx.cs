using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Transactions;

namespace Rosteras
{
    public partial class PlayerTransfer : System.Web.UI.Page
    {
        private static Table table;
        private static List<TextBox> ts;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack && !ddlTeams.SelectedValue.Equals("Εκτός Βάσης") && !PastTeamsFlag2.Checked)
            {
                PastTeamsNumber.Visible = false;
                ptlabel.Visible = false;
            }
        }

        protected void Team_Selecting(object sender, EventArgs e)
        {
            if((ddlTeams.SelectedValue).Equals("Εκτός Βάσης")) 
            {
                PastTeamsNumber.Visible = true;
                ptlabel.Visible = true;
            }
        }

        protected void AdjustTextBoxes(object sender, EventArgs e)
        {
            createControls();
        }

        private void createControls()
        {
            int num = Convert.ToInt16(PastTeamsNumber.Text);
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
                Panel4.Controls.Add(table);
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

        protected void ActivatePastTeams(object sender, EventArgs e)
        {
            if (PastTeamsFlag2.Checked)
            {
                PastTeamsNumber.Visible = true;
                ptlabel.Visible = true;
            }
        }

        protected void Submit_Click(object sender, EventArgs e)
        {
            List<PastTeams> pastteams = new List<PastTeams>();
            String playerID = TeamsConnection.transferedPlayerId(ddlPlayers.SelectedValue,ddlTeams.SelectedValue);
            String newTeam = ddlTeams2.SelectedValue;
            String formerTeam = ddlTeams.SelectedValue;
            String lb = loanedBy.Text;
            
            using (TransactionScope tran = new TransactionScope())
            {

                if (ptlabel.Visible || PastTeamsFlag2.Checked)
            {
                int psts = Convert.ToInt16(PastTeamsNumber.Text);
                if(psts>0) {
                    
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
                            pastteams.Add(new PastTeams(playerID, team, country));
                            fl = false;
                        }
                    }
                    TeamsConnection.addPastTeamsOfNewPlayer(pastteams);
                }
            }

                if (!TeamsConnection.isPastTeam(playerID, formerTeam) && !PastTeamsFlag.Checked && !formerTeam.Equals("Εκτός Βάσης"))
                {
                pastteams.Clear();
                pastteams.Add(new PastTeams(playerID,formerTeam,"Ελλάδα"));
                TeamsConnection.addPastTeamsOfNewPlayer(pastteams);
            }
            TeamsConnection.transferPlayerToTeam(playerID, newTeam,lb);
            tran.Complete();
            }
        }
    }
}