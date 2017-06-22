using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Transactions;

namespace Rosteras
{
    public partial class GameUpdate : System.Web.UI.Page
    {
        private static Table table;
        private static TextBox[] hgoals;
        private static TextBox hscore;
        private static String[] hnames;
        private static TextBox[] happs;
        private static TextBox[] agoals;
        private static TextBox ascore;
        private static String[] anames;
        private static TextBox[] aapps;
        private static String homeTeam;
        private static String awayTeam;

        protected void Page_Load(object sender, EventArgs e)
        {
            homeTeam = ddlTeams.SelectedValue;
            awayTeam = ddlTeams2.SelectedValue;
            //createControls(homeTeam, Panel1, true);
            //createControls(awayTeam, Panel2, false);
        }

        protected void Team_Changing(object sender, EventArgs e)
        {
            homeTeam = ddlTeams.SelectedValue;
            createControls(homeTeam, Panel1, true);
            awayTeam = ddlTeams2.SelectedValue;
            createControls(awayTeam, Panel2, false);
        }

        private void createControls(String team,Panel panel,bool home)
        {
                String prefix;
                if(home)
                    prefix = "h"; 
                else
                    prefix = "a";
                List<String> teamPlayers = TeamsConnection.displayPlayers(team,23);
                TextBox[] goals = new TextBox[teamPlayers.Count];
                TextBox score;
                TextBox[] apps = new TextBox[teamPlayers.Count];
                String[] names = new String[teamPlayers.Count];
                table = new Table();
                table.CssClass = "players";
                TableCell cell;
                TableCell cell2;
                TableCell cell3;
                TableRow row;
                TextBox c;
                Label l;
                TextBox t;
                cell = new TableCell();
                cell2 = new TableCell();
                row = new TableRow();
                cell.Text = "SCORE";
                cell.ColumnSpan = 2;
                score = new TextBox();
                score.ID = prefix + "_score";
                score.Width = 15;
                score.Text = "0";
                cell2.Controls.Add(score);
                row.Cells.Add(cell);
                row.Cells.Add(cell2);
                table.Rows.Add(row);
                cell = new TableCell();
                cell2 = new TableCell();
                cell3 = new TableCell();
                row = new TableRow();
                cell.Text = "PLAYED";
                cell2.Text = "NAME";
                cell3.Text = "GOALS";
                row.Cells.Add(cell);
                row.Cells.Add(cell2);
                row.Cells.Add(cell3);
                table.Rows.Add(row);
                int counter = 0; 

                foreach (String pl in teamPlayers)
                {
                    row = new TableRow();
                    cell = new TableCell();
                    cell2 = new TableCell();
                    cell3 = new TableCell();
                    c = new TextBox();
                    c.ID = prefix + "_app_" + counter.ToString();
                    if (prefix.Equals("h"))
                        c.TextChanged += new EventHandler(onHomePlayerNumber_Change);
                    else
                        c.TextChanged += new EventHandler(onAwayPlayerNumber_Change);
                    c.Text = "0";
                    c.Width = 10;
                    //c.AutoPostBack = true;
                    l = new Label();
                    l.Text = pl;
                    l.CssClass = "playerName";
                    t = new TextBox();
                    t.ID = prefix + "_goals_" + counter.ToString();
                    t.Width = 10;
                    t.Text = "0";
                    t.CssClass = "playerGoals";
                    cell.Controls.Add(c);
                    cell2.Controls.Add(l);
                    cell3.Controls.Add(t);
                    goals[counter] = t;
                    apps[counter] = c;
                    names[counter] = pl;
                    row.Cells.Add(cell);
                    row.Cells.Add(cell2);
                    row.Cells.Add(cell3);
                    table.Rows.Add(row);
                    counter++;
                }
                panel.Controls.Add(table);
                if (home)
                {
                    happs = apps;
                    hgoals = goals;
                    hnames = names;
                    hscore = score;
                }
                else
                {
                    aapps = apps;
                    agoals = goals;
                    anames = names;
                    ascore = score;
                }
            }

        protected void onHomePlayerNumber_Change(object sender, EventArgs e)
        {
            int counter = 0;
            for (int i = 0; i < hnames.Length; i++)
            {
                if (!GetValue(happs[i].ID).Equals("0"))
                {
                    counter++;
                }

            }
            PlayerNumber1.Text = counter.ToString();
        }

        protected void onAwayPlayerNumber_Change(object sender, EventArgs e)
        {
            int counter = 0;
            for (int i = 0; i < anames.Length; i++)
            {
                if (!GetValue(aapps[i].ID).Equals("0"))
                {
                    counter++;
                }
            }
            PlayerNumber2.Text = counter.ToString();
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

        protected void Submit_Click(object sender, EventArgs e)
        {
            String league = dllLeague.SelectedValue;
            int homeScore = Convert.ToInt16(GetValue("h_score"));
            int awayScore = Convert.ToInt16(GetValue("a_score"));
            List<Scorer> pls;
        
            using (TransactionScope tran = new TransactionScope())
            {
                pls = new List<Scorer>();
                String playerID;
                int goals;
                
                
                for (int i = 0; i < hnames.Length; i++)
                {
                    if (!GetValue(happs[i].ID).Equals("0"))
                    {
                        goals = Convert.ToInt16(GetValue(hgoals[i].ID));
                        playerID = TeamsConnection.transferedPlayerId(hnames[i], homeTeam);
                        pls.Add(new Scorer(playerID, goals));
                    }
                    
                }

                for (int i = 0; i < anames.Length; i++)
                {
                    if (!GetValue(aapps[i].ID).Equals("0"))
                    {
                        goals = Convert.ToInt16(GetValue(agoals[i].ID));
                        playerID = TeamsConnection.transferedPlayerId(anames[i], awayTeam);
                        pls.Add(new Scorer(playerID, goals));
                    }

                }

                TeamsConnection.updateGoalsForAwayPointsAndGames(homeTeam, homeScore, awayScore);
                TeamsConnection.updateGoalsForAwayPointsAndGames(awayTeam, awayScore, homeScore);
                TeamsConnection.updatePlayerAppearancesGoals(pls, league);

                    tran.Complete();
            }
        }
        
    }

    
}