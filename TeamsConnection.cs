using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Text;



namespace Rosteras
{
    public static class TeamsConnection
    {
        private static String message;
        private static SqlConnection conn;
        private static SqlCommand command;
        private static SqlDataAdapter ad;
        private static DataSet set;
        private static List<Team> teams;
        private static List<Player> players;
        private static List<PastTeams> pastTeams;
        private static List<Traveller> topTravellers;
        private static List<Goalscorer> goalscorers;
        private static List<Appearance> appearances;
        private static List<Player> loaned;
        private static List<Goalscorer> topLeagueScorers;
        private static List<Appearance> topLeagueAppearances;
        private static List<Winner> topChampions;
        private static List<Winner> topCupWinners;
        private static List<Player> formerSups;
        private static List<Player> playersSearched;
        private static List<PastTeams> teamsSearched;
        private static List<Player> playersCompared;
        private static List<Player> playersPassedFromBothTeams;
        private static List<Player> allFormerOrPresentPlayers;
        private static List<Player> playersComingFromCountry;
        private static List<Player> playersPlayingInCountry;
        private static List<Player> AllTimeplayersPassedFromBothTeams;



        static TeamsConnection()
        {
            conn = new SqlConnection("data source=***********\\SQLEXPRESS;initial catalog=Ρόστερ;integrated security=True;multipleactiveresultsets=True;App=EntityFramework");
            command = new SqlCommand("", conn);
            conn.InfoMessage +=
            new SqlInfoMessageEventHandler(OnInfoMessage);
        }

        static void OnInfoMessage(object sender, SqlInfoMessageEventArgs args)
{ 
         message = args.Message;
}

        public static List<Team> displayTeams(String lg)
        {
            teams = new List<Team>();
            int position = 0;
            String name;
            String league;
            String area;
            String foundationYear;
            int leaguesWon;
            int cupsWon;
            String lastTrophy;
            String field;
            String manager;
            String chairman;
            int points;
            String page;
            int games;
            int goalsFor;
            int goalsBy;

            try
            {
                conn.Open();
                ad = new SqlDataAdapter(@"SELECT Ονομασία, Πρωτάθλημα, Περιοχή, Έτος_Ίδρυσης, Αριθμός_Πρωταθλημάτων, Αριθμός_Κυπέλλων
                                , Τελευταίο_Τρόπαιο,Έδρα, Προπονητής, Πρόεδρος, Βαθμοί, Ιστοσελίδα, Αγώνες, Γκολ_Υπέρ,
                                 Γκολ_Κατά from Ομάδες WHERE Πρωτάθλημα = @league order by Βαθμοί desc,
                                 Γκολ_Υπέρ - Γκολ_Κατά desc, Γκολ_Υπέρ desc, Ονομασία", conn);
                ad.SelectCommand.Parameters.AddWithValue("@league",lg);
                set = new DataSet();
                ad.Fill(set, "Ομάδες");
                foreach (DataRow row in set.Tables["Ομάδες"].Rows)
                {
                    name = row["Ονομασία"].ToString();
                    league = row["Πρωτάθλημα"].ToString();
                    area = row["Περιοχή"].ToString();
                    foundationYear = row["Έτος_Ίδρυσης"].ToString();
                    leaguesWon = Convert.ToInt32(row["Αριθμός_Πρωταθλημάτων"]);
                    cupsWon = Convert.ToInt32(row["Αριθμός_Κυπέλλων"]);
                    lastTrophy = row["Τελευταίο_Τρόπαιο"].ToString();
                    field = row["Έδρα"].ToString();
                    manager = row["Προπονητής"].ToString();
                    chairman = row["Πρόεδρος"].ToString();
                    points = Convert.ToInt32(row["Βαθμοί"]);
                    page = row["Ιστοσελίδα"].ToString();
                    games = Convert.ToInt32(row["Αγώνες"]);
                    goalsFor = Convert.ToInt32(row["Γκολ_Υπέρ"]);
                    goalsBy = Convert.ToInt32(row["Γκολ_Κατά"]);
                    position++;
                    Team team = new Team(name, league, area, foundationYear, leaguesWon, cupsWon, lastTrophy, field, manager, chairman, points, position, page, games, goalsFor, goalsBy);
                    teams.Add(team);
                }

            } //try

            finally
            {
                conn.Close();
            }
            return teams;

        }


        
        public static List<Player> displayPlayers(String team)
        {
            players = new List<Player>();
            String playerID;
            String name;
            String presentTeam;
            double height;
            int overallGoalsSL;
            int overallGoalsFL;
            int age;
            String position;
            String country;
            int overallAppsSL;
            int overallAppsFL;
            String lentBy;
            int presentAppsSL;
            int presentAppsFL;
            int presentGoalsSL;
            int presentGoalsFL;

            try
            {
                conn.Open();
                ad = new SqlDataAdapter(@"select * from Παίκτες, Θέση where Τρέχουσα_Ομάδα = @team and Θέση.Ονομασία = Παίκτες.Θέση 
                                        order by Θέση.Σειρά,Φετινές_Συμμετοχές_στη_Superleague desc,
                                        Φετινές_Συμμετοχές_στη_Football_league desc", conn);
                ad.SelectCommand.Parameters.AddWithValue("@team",team);
                set = new DataSet();
                ad.Fill(set,"Παίκτες");


                foreach (DataRow row in set.Tables["Παίκτες"].Rows)
                {
                    playerID = row["ID_Παίκτη"].ToString();
                    name = correctName(row["Όνομα"].ToString());
                    presentTeam = row["Τρέχουσα_Ομάδα"].ToString();
                    height = Convert.ToDouble(row["Ύψος"]);
                    overallGoalsSL = Convert.ToInt32(row["Γκολ_στη_Superleague"]);
                    overallGoalsFL = Convert.ToInt32(row["Γκολ_στη_Football_league"]);
                    age = Convert.ToInt32(row["Ηλικία"]);
                    position = row["Θέση"].ToString();
                    country = row["Χώρα"].ToString();
                    overallAppsSL = Convert.ToInt32(row["Εμφανίσεις_στη_Superleague"]);
                    overallAppsFL = Convert.ToInt32(row["Εμφανίσεις_στη_Football_league"]);
                    lentBy = row["Δανεικός_Από"].ToString();
                    presentAppsSL = Convert.ToInt32(row["Φετινές_Συμμετοχές_στη_Superleague"]);
                    presentAppsFL = Convert.ToInt32(row["Φετινές_Συμμετοχές_στη_Football_league"]);
                    presentGoalsSL = Convert.ToInt32(row["Φετινά_Γκολ_στη_Superleague"]);
                    presentGoalsFL = Convert.ToInt32(row["Φετινά_Γκολ_στη_Football_league"]);

                    Player player = new Player(playerID, name, presentTeam, height, overallGoalsSL, overallGoalsFL, age, position, country, overallAppsSL,
                    overallAppsFL, lentBy, presentAppsSL, presentAppsFL, presentGoalsSL, presentGoalsFL);
                    players.Add(player);
                } //while
            } //try

            finally
            {
                conn.Close();
            }
            return players;
        }

        public static List<String> displayPlayers(String team,int r)
        {
            List<String> players = new List<String>();
            ad = new SqlDataAdapter(@"SELECT Όνομα from Παίκτες, Θέση WHERE Τρέχουσα_Ομάδα = @team and Θέση.Ονομασία = Παίκτες.Θέση 
                                       order by Θέση.Σειρά,Φετινές_Συμμετοχές_στη_Football_league desc,Φετινές_Συμμετοχές_στη_Superleague
                                        desc", conn);
            ad.SelectCommand.Parameters.AddWithValue("@team", team);
            set = new DataSet();
            ad.Fill(set, "Παίκτες");
            
            foreach (DataRow row in set.Tables["Παίκτες"].Rows)
            {
                players.Add(row["Όνομα"].ToString());
            }
            return players;

        }


                

        public static List<PastTeams> loadPastTeams() //the list of the players is already full only of the players we are interested in
        {
            pastTeams = new List<PastTeams>();
            String playerID;
            String pTeam;
            String country;
            int count = 0;
            String param;
            if (players.Count > 0)
            {
                try
                {
                    conn.Open();
                    SqlCommand command = new SqlCommand("select * from Προηγούμενες_Ομάδες where ID_Παίκτη = @playerID", conn);
                    command.Parameters.AddWithValue("@playerID", players.First().playerID);

                    foreach (Player player in players)
                    {
                        param = "@playerID" + count;
                        command.CommandText += " OR ID_Παίκτη = " + param;
                        command.Parameters.AddWithValue(param, player.playerID);
                        count++;
                    }
                    ad = new SqlDataAdapter(command);
                    set = new DataSet();

                    ad.Fill(set, "Ομάδες");

                    foreach (DataRow row in set.Tables["Ομάδες"].Rows)
                    {
                        playerID = row["ID_Παίκτη"].ToString();
                        pTeam = row["Ομάδα"].ToString();
                        country = row["Χώρα"].ToString();

                        pastTeams.Add(new PastTeams(playerID, pTeam, country));
                    } //foreach
                } //try

                finally
                {
                    conn.Close();
                }
                return pastTeams;
            }
            return null;
        }


        public static Team displayCurrentTeam(String nam, String league)
        {
            List<Team> teams = displayTeams(league);
            foreach (Team team in teams)
            {
                if (team.name.Equals(nam))
                    return team;
            }
            return null;
        }



        public static List<Traveller> top3travellers(String team)
        {
            topTravellers = new List<Traveller>();
            String name;
            int numOfTeams;
            String playerID;

            try
            {
                conn.Open();
                ad = new SqlDataAdapter(@"select top 3 Παίκτες.Όνομα as Όνομα_Παίκτη, count(Ομάδα) as Αριθμός_Ομάδων,Παίκτες.ID_Παίκτη as ID_Παίκτη
                                    from Προηγούμενες_Ομάδες,Παίκτες
                                    where Παίκτες.ID_Παίκτη=Προηγούμενες_Ομάδες.ID_Παίκτη and Τρέχουσα_Ομάδα = @team
                                    and Ομάδα <> Τρέχουσα_Ομάδα
                                    group by Παίκτες.Όνομα,Παίκτες.ID_Παίκτη
                                    order by count(Ομάδα)  desc, Όνομα asc", conn);
                ad.SelectCommand.Parameters.AddWithValue("@team",team);
                set = new DataSet();
                ad.Fill(set,"Παίκτες");

                foreach (DataRow row in set.Tables["Παίκτες"].Rows)
                {
                    name = correctName(row["Όνομα_Παίκτη"].ToString());
                    numOfTeams = Convert.ToInt32(row["Αριθμός_Ομάδων"]) + 1;
                    playerID = row["ID_Παίκτη"].ToString();
                    topTravellers.Add(new Traveller(name, numOfTeams, playerID));
                }
            } //try

            finally
            {
                conn.Close();
            }
            return topTravellers;
        }


               public static List<Goalscorer> Top5goalscorers(String team, String league, bool current)
        {
            goalscorers = new List<Goalscorer>();
            String playerID;
            String name;
            int goals;

            try
            {
                conn.Open();
                if (current)
                {
                    if (league.Equals("Superleague"))
                    {
                        ad = new SqlDataAdapter(@"select top 5 ID_Παίκτη,Όνομα,Φετινά_Γκολ_στη_Superleague as Γκολ from Παίκτες
            where Τρέχουσα_Ομάδα = @team 
            order by Φετινά_Γκολ_στη_Superleague DESC, Όνομα ASC", conn);

                    }
                    else
                    {
                        ad = new SqlDataAdapter(@"select top 5 ID_Παίκτη,Όνομα,Φετινά_Γκολ_στη_Football_league as Γκολ from Παίκτες
            where Τρέχουσα_Ομάδα = @team 
            order by Φετινά_Γκολ_στη_Football_league DESC, Όνομα ASC", conn);
                    }
                }

                else
                {
                    if (league.Equals("Superleague"))
                    {
                        ad = new SqlDataAdapter(@"select top 5 ID_Παίκτη,Όνομα,Γκολ_στη_Superleague as Γκολ from Παίκτες
            where Τρέχουσα_Ομάδα = @team 
            order by Γκολ_στη_Superleague DESC, Όνομα ASC", conn);
                    }
                    else
                    {
                        ad = new SqlDataAdapter(@"select top 5 ID_Παίκτη,Όνομα,Γκολ_στη_Football_league as Γκολ from Παίκτες
            where Τρέχουσα_Ομάδα = @team 
            order by Γκολ_στη_Football_league DESC, Όνομα ASC", conn);
                    }
                }

                ad.SelectCommand.Parameters.AddWithValue("@team", team);
                set = new DataSet();
                ad.Fill(set, "Παίκτες");

                foreach (DataRow row in set.Tables["Παίκτες"].Rows)
                {
                    playerID = row["ID_Παίκτη"].ToString();
                    name = correctName(row["Όνομα"].ToString());
                    goals = Convert.ToInt32(row["Γκολ"]);
                    goalscorers.Add(new Goalscorer(playerID, name, goals));
                }
            } //try

            finally
            {
                conn.Close();
            }
            return goalscorers;
        }


       public static List<Appearance> top5Appearances(String team, String league, bool current)
        {
            appearances = new List<Appearance>();
            String playerID;
            String name;
            int apps;

            try
            {
                conn.Open();
                if (current)
                {
                    if (league.Equals("Superleague"))
                    {
                        ad = new SqlDataAdapter(@"select top 5 ID_Παίκτη,Όνομα,Φετινές_Συμμετοχές_στη_Superleague as Εμφανίσεις from Παίκτες
            where Τρέχουσα_Ομάδα = @team 
            order by Φετινές_Συμμετοχές_στη_Superleague DESC, Όνομα ASC", conn);
                    }
                    else
                    {
                        ad = new SqlDataAdapter(@"select top 5 ID_Παίκτη,Όνομα,Φετινές_Συμμετοχές_στη_Football_league as Εμφανίσεις from Παίκτες
            where Τρέχουσα_Ομάδα = @team 
            order by Φετινές_Συμμετοχές_στη_Football_league DESC, Όνομα ASC", conn);
                    }
                }

                else
                {
                    if (league.Equals("Superleague"))
                    {
                        ad = new SqlDataAdapter(@"select top 5 ID_Παίκτη,Όνομα,Εμφανίσεις_στη_Superleague as Εμφανίσεις from Παίκτες
            where Τρέχουσα_Ομάδα = @team 
            order by Εμφανίσεις_στη_Superleague DESC, Όνομα ASC", conn);
                    }
                    else
                    {
                        ad = new SqlDataAdapter(@"select top 5 ID_Παίκτη,Όνομα,Εμφανίσεις_στη_Football_league as Εμφανίσεις from Παίκτες
            where Τρέχουσα_Ομάδα = @team 
            order by Εμφανίσεις_στη_Football_league DESC, Όνομα ASC",conn);
                    }
                }
                ad.SelectCommand.Parameters.AddWithValue("@team", team);
                set = new DataSet();
                ad.Fill(set, "Παίκτες");

                foreach (DataRow row in set.Tables["Παίκτες"].Rows)
                {
                    playerID = row["ID_Παίκτη"].ToString();
                    name = correctName(row["Όνομα"].ToString());
                    apps = Convert.ToInt32(row["Εμφανίσεις"]);
                    appearances.Add(new Appearance(playerID, name, apps));
                }
            } //try

            finally
            {
                conn.Close();
            }
            return appearances;
        }

         

        public static String averageHeight(String team)
        {
            double height = 0;
            
            try
            {
                conn.Open();
                ad = new SqlDataAdapter(@"select cast (SUM(Ύψος) as float)/COUNT(Ύψος) as Μέσο_Ύψος from Παίκτες
                            where Τρέχουσα_Ομάδα= @team and Ύψος <> 0",conn);
                ad.SelectCommand.Parameters.AddWithValue("@team",team);
                set = new DataSet();
                ad.Fill(set,"Ύψος");
                
                foreach (DataRow row in set.Tables["Ύψος"].Rows)
                {
                    height = Convert.ToDouble(row["Μέσο_Ύψος"]);
                }

            } //try

            finally
            {
                conn.Close();
            }
            return height.ToString("0.00");
        }

             

        public static String averageAge(String team)
        {
            double age = 0;
            
            try
            {
                conn.Open();
                ad = new SqlDataAdapter(@"select cast (SUM(Ηλικία) as float)/COUNT(Ηλικία) as Μέση_Ηλικία from Παίκτες
                            where Τρέχουσα_Ομάδα= @team",conn);
                ad.SelectCommand.Parameters.AddWithValue("@team",team);
                set = new DataSet();
                ad.Fill(set, "Ηλικία");

                foreach (DataRow row in set.Tables["Ηλικία"].Rows)
                {
                    age = Convert.ToDouble(row["Μέση_Ηλικία"]);
                }
            } //try

            finally
            {
                conn.Close();
            }
            return age.ToString("0.00");
        }


            

        public static List<Player> LoanedPlayers(String team)
        {
            loaned = new List<Player>();
            String playerID;
            String name;
            String presentTeam;
            String presentTeamPage;

            try
            {
                conn.Open();
                ad = new SqlDataAdapter(@"select ID_Παίκτη, Όνομα, Τρέχουσα_Ομάδα, Ιστοσελίδα from Παίκτες,
                Ομάδες where Δανεικός_από = @team and Τρέχουσα_Ομάδα = Ονομασία order by Εμφανίσεις_στη_Superleague
                ,Εμφανίσεις_στη_Football_league",conn);
                ad.SelectCommand.Parameters.AddWithValue("@team",team);
                set = new DataSet();
                ad.Fill(set, "Παίκτες");

                foreach (DataRow row in set.Tables["Παίκτες"].Rows)
                {
                    playerID = row["ID_Παίκτη"].ToString();
                    name = correctName(row["Όνομα"].ToString());
                    presentTeam = row["Τρέχουσα_Ομάδα"].ToString();
                    presentTeamPage = row["Ιστοσελίδα"].ToString();
                    loaned.Add(new Player(playerID, name, presentTeam, presentTeamPage));
                }
            } //try

            finally
            {
                conn.Close();
            }
            return loaned;
        }

                    

        public static int FormerPlayers(String team)
        {
            int fp = 0;
            
            try
            {
                conn.Open();
                ad = new SqlDataAdapter(@"select COUNT(Όνομα) as Αριθμός_Παικτών from Παίκτες,Προηγούμενες_Ομάδες 
                where Παίκτες.ID_Παίκτη=Προηγούμενες_Ομάδες.ID_Παίκτη and Προηγούμενες_Ομάδες.Ομάδα  = @team",conn);
                ad.SelectCommand.Parameters.AddWithValue("@team",team);
                set = new DataSet();
                ad.Fill(set, "Παίκτες");

                foreach (DataRow row in set.Tables["Παίκτες"].Rows)
                {
                    fp = Convert.ToInt32(row["Αριθμός_Παικτών"]);
                }
            } //try

            finally
            {
                conn.Close();
            }
            return fp;
        }



                public static int foreigners(String team)
        {
            int fr = 0;
           
            try
            {
                conn.Open();
                ad = new SqlDataAdapter(@"select count(distinct Παίκτες.ID_Παίκτη) as Ξένοι from Παίκτες,Προηγούμενες_Ομάδες
where Προηγούμενες_Ομάδες.Χώρα <> 'Ελλάδα' and Παίκτες.ID_Παίκτη = Προηγούμενες_Ομάδες.ID_Παίκτη and Τρέχουσα_Ομάδα = @team",conn);
                ad.SelectCommand.Parameters.AddWithValue("@team",team);
                set = new DataSet();
                ad.Fill(set, "Παίκτες");

                foreach (DataRow row in set.Tables["Παίκτες"].Rows)
                {
                    fr = Convert.ToInt32(row["Ξένοι"]);
                }
            } //try

            finally
            {
                conn.Close();
            }
            return fr;
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////teams


        

        public static String lastChampion(String league)
        {
            String champ = "";
           
            try
            {
                conn.Open();
                ad = new SqlDataAdapter(@"select Τελευταίος_Πρωταθλήτης from Πρωταθλήματα
                where Ονομασία = @league",conn);
                ad.SelectCommand.Parameters.AddWithValue("@league", league);
                set = new DataSet();
                ad.Fill(set, "Ομάδα");

                foreach (DataRow row in set.Tables["Ομάδα"].Rows)
                {
                    champ = row["Τελευταίος_Πρωταθλήτης"].ToString();
                }
            } //try

            finally
            {
                conn.Close();
            }
            return champ;
        }

        public static List<Appearance> top10LeagueAppearances(String league, bool current)
        {
            topLeagueAppearances = new List<Appearance>();
            String playerID;
            String name;
            int apps;
            String team;
            String teamPage;
            String query;
            if (current)
            {
                if (league.Equals("Superleague"))
                {
                    query = @"select top 10 ID_Παίκτη,Όνομα,Φετινές_Συμμετοχές_στη_Superleague,Τρέχουσα_Ομάδα, Ιστοσελίδα from Παίκτες, Ομάδες
                    where Τρέχουσα_Ομάδα <> 'Εκτός Βάσης' and Τρέχουσα_Ομάδα = Ονομασία
                    order by Φετινές_Συμμετοχές_στη_Superleague DESC, Όνομα ASC";
                }
                else
                {
                    query = @"select top 10 ID_Παίκτη,Όνομα,Φετινές_Συμμετοχές_στη_Football_league, Τρέχουσα_Ομάδα, Ιστοσελίδα from Παίκτες, Ομάδες
                    where Τρέχουσα_Ομάδα <> 'Εκτός Βάσης' and Τρέχουσα_Ομάδα = Ονομασία
                    order by Φετινές_Συμμετοχές_στη_Football_league DESC, Όνομα ASC";
                }
            }

            else
            {
                if (league.Equals("Superleague"))
                {
                    query = @"select top 10 ID_Παίκτη,Όνομα,Εμφανίσεις_στη_Superleague,Τρέχουσα_Ομάδα, Ιστοσελίδα from Παίκτες, Ομάδες
                    where Τρέχουσα_Ομάδα <> 'Εκτός Βάσης' and Τρέχουσα_Ομάδα = Ονομασία
                    order by Εμφανίσεις_στη_Superleague DESC, Όνομα ASC";
                }
                else
                {
                    query = @"select top 10 ID_Παίκτη,Όνομα,Εμφανίσεις_στη_Football_league,Τρέχουσα_Ομάδα, Ιστοσελίδα from Παίκτες, Ομάδες
                    where Τρέχουσα_Ομάδα <> 'Εκτός Βάσης' and Τρέχουσα_Ομάδα = Ονομασία
                    order by Εμφανίσεις_στη_Football_league DESC, Όνομα ASC";
                }
            }

            try
            {
                conn.Open();
                command.CommandText = query;
                SqlDataReader reader = command.ExecuteReader();


                while (reader.Read())
                {

                    playerID = reader.GetString(0);
                    name = correctName(reader.GetString(1));
                    apps = reader.GetInt32(2);
                    team = reader.GetString(3);
                    teamPage = reader.GetString(4);
                    topLeagueAppearances.Add(new Appearance(playerID, name, apps, team, teamPage));
                }
            } //try

            finally
            {
                conn.Close();
            }
            return topLeagueAppearances;
        }

        public static List<Goalscorer> top10LeagueScorers(String league, bool current)
        {
            topLeagueScorers = new List<Goalscorer>();
            String playerID;
            String name;
            int goals;
            String team;
            String teamPage;
            String query;
            if (current)
            {
                if (league.Equals("Superleague"))
                {
                    query = @"select top 10 ID_Παίκτη,Όνομα,Φετινά_Γκολ_στη_Superleague, Τρέχουσα_Ομάδα, Ιστοσελίδα from Παίκτες, Ομάδες
            where Τρέχουσα_Ομάδα <> 'Εκτός Βάσης' and Τρέχουσα_Ομάδα = Ονομασία
            order by Φετινά_Γκολ_στη_Superleague DESC, Όνομα ASC";
                }
                else
                {
                    query = @"select top 10 ID_Παίκτη,Όνομα,Φετινά_Γκολ_στη_Football_league, Τρέχουσα_Ομάδα, Ιστοσελίδα from Παίκτες, Ομάδες
            where Τρέχουσα_Ομάδα <> 'Εκτός Βάσης' and Τρέχουσα_Ομάδα = Ονομασία 
            order by Φετινά_Γκολ_στη_Football_league DESC, Όνομα ASC";
                }
            }

            else
            {
                if (league.Equals("Superleague"))
                {
                    query = @"select top 10 ID_Παίκτη,Όνομα,Γκολ_στη_Superleague,Τρέχουσα_Ομάδα, Ιστοσελίδα  from Παίκτες, Ομάδες
            where Τρέχουσα_Ομάδα <> 'Εκτός Βάσης' and Τρέχουσα_Ομάδα = Ονομασία 
            order by Γκολ_στη_Superleague DESC, Όνομα ASC";
                }
                else
                {
                    query = @"select top 10 ID_Παίκτη,Όνομα,Γκολ_στη_Football_league,Τρέχουσα_Ομάδα, Ιστοσελίδα from Παίκτες, Ομάδες
            where Τρέχουσα_Ομάδα <> 'Εκτός Βάσης' and Τρέχουσα_Ομάδα = Ονομασία
            order by Γκολ_στη_Football_league DESC, Όνομα ASC";
                }
            }

            try
            {
                conn.Open();
                command.CommandText = query;
                SqlDataReader reader = command.ExecuteReader();


                while (reader.Read())
                {
                    playerID = reader.GetString(0);
                    name = correctName(reader.GetString(1));
                    goals = reader.GetInt32(2);
                    team = reader.GetString(3);
                    teamPage = reader.GetString(4);
                    topLeagueScorers.Add(new Goalscorer(playerID, name, goals, team, teamPage));
                }
            } //try

            finally
            {
                conn.Close();
            }
            return topLeagueScorers;
        }

        public static List<Winner> top5Champions()
        {
            topChampions = new List<Winner>();
            String team;
            int champsWon;
            String query = @"select top 5 Ονομασία,Αριθμός_Πρωταθλημάτων from Ομάδες
            where Ονομασία <> 'Εκτός Βάσης'
            order by Αριθμός_Πρωταθλημάτων desc";
            try
            {
                conn.Open();
                command.CommandText = query;
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    team = reader.GetString(0);
                    champsWon = reader.GetInt32(1);
                    topChampions.Add(new Winner(team, champsWon));
                }
            } //try

            finally
            {
                conn.Close();
            }
            return topChampions;
        }

        public static List<Winner> top5CupWinners()
        {
            topCupWinners = new List<Winner>();
            String team;
            int cupsWon;
            String query = @"select top 5 Ονομασία,Αριθμός_Κυπέλλων from Ομάδες
            where Ονομασία <> 'Εκτός Βάσης'
            order by Αριθμός_Κυπέλλων desc";
            try
            {
                conn.Open();
                command.CommandText = query;
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    team = reader.GetString(0);
                    cupsWon = reader.GetInt32(1);
                    topCupWinners.Add(new Winner(team, cupsWon));
                }
            } //try

            finally
            {
                conn.Close();
            }
            return topCupWinners;
        }

        public static String LeagueGoalsFromForeigners(String league)
        {
            int goals = 0;
            int fgoals = 0;
            double perc = 0.0;
            String query1 = "";
            String query2 = "";

            if (league.Equals("Superleague"))
            {
                query1 = @"select SUM(Φετινά_Γκολ_στη_Superleague) from Παίκτες, Ομάδες
            where Τρέχουσα_Ομάδα = Ομάδες.Ονομασία and Πρωτάθλημα = 'Superleague'";
                query2 = @"select SUM(Φετινά_Γκολ_στη_Superleague) from Παίκτες, Ομάδες
            where Τρέχουσα_Ομάδα = Ομάδες.Ονομασία and Πρωτάθλημα = 'Superleague' and Χώρα <> 'Ελλάδα'";
            }

            else 
            {
                query1 = @"select SUM(Φετινά_Γκολ_στη_Football_league) from Παίκτες, Ομάδες
            where Τρέχουσα_Ομάδα = Ομάδες.Ονομασία and Πρωτάθλημα = 'Football League'";
                query2 = @"select SUM(Φετινά_Γκολ_στη_Football_league) from Παίκτες, Ομάδες
            where Τρέχουσα_Ομάδα = Ομάδες.Ονομασία and Πρωτάθλημα = 'Football League' and Χώρα <> 'Ελλάδα'";
            }

            try
            {
                conn.Open();
                command.CommandText = query1;
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    goals = reader.GetInt32(0);
                }
            } //try

            finally
            {
                conn.Close();
            }

            try
            {
                conn.Open();
                command.CommandText = query2;
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    fgoals = reader.GetInt32(0);
                }
            } //try

            finally
            {
                conn.Close();
            }

            if (goals == 0)
                return goals.ToString("0.00");
            perc = 100 * (double) fgoals / goals;
            return perc.ToString("0.00");
        }

        /*public static String TeamGoalsFromForeigners(String team, String league)
        {
            int goals = 0;
            int fgoals = 0;
            double perc = 0.0;
            String query1 = "";
            String query2 = "";

            if (league.Equals("Superleague"))
            {
                query1 = @"select SUM(Φετινά_Γκολ_στη_Superleague) from Παίκτες
            where Τρέχουσα_Ομάδα = '" + team + "' and Χώρα <> 'Ελλάδα'";
                query2 = @"select SUM(Φετινά_Γκολ_στη_Superleague) from Παίκτες
            where Τρέχουσα_Ομάδα = '" + team + "'";
            }

            else
            {
                query1 = @"select SUM(Φετινά_Γκολ_στη_Football_league) from Παίκτες
            where Τρέχουσα_Ομάδα = '" + team + "' and Χώρα <> 'Ελλάδα'";
                query2 = @"select SUM(Φετινά_Γκολ_στη_Football_league) from Παίκτες
            where Τρέχουσα_Ομάδα = '" + team + "'";
            }

            try
            {
                conn.Open();
                command.CommandText = query1;
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    fgoals = reader.GetInt32(0);
                }
            } //try

            finally
            {
                conn.Close();
            }

            try
            {
                conn.Open();
                command.CommandText = query2;
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    goals = reader.GetInt32(0);
                }
            } //try

            finally
            {
                conn.Close();
            }

            if (goals == 0)
                return goals.ToString("0.00");
            perc = 100 * (double) fgoals / goals;
            return perc.ToString("0.00");
        }*/

        public static List<Player> FormerSuperleaguePlayers(String team)
        {
            formerSups = new List<Player>();
            String playerID;
            String name;
            int apps;
            int goals;

            try
            {
                conn.Open();
                ad = new SqlDataAdapter(@"select ID_Παίκτη,Όνομα,Εμφανίσεις_στη_Superleague,Γκολ_στη_Superleague from Παίκτες
                where Εμφανίσεις_στη_Superleague > 0 and Τρέχουσα_Ομάδα = @team order by Εμφανίσεις_Στη_Superleague desc",conn);
                ad.SelectCommand.Parameters.AddWithValue("@team", team);
                set = new DataSet();
                ad.Fill(set, "Παίκτες");

                foreach (DataRow row in set.Tables["Παίκτες"].Rows)
                {
                    playerID = row["ID_Παίκτη"].ToString();
                    name = row["Όνομα"].ToString();
                    apps = Convert.ToInt32(row["Εμφανίσεις_στη_Superleague"]);
                    goals = Convert.ToInt32(row["Γκολ_στη_Superleague"]);
                    formerSups.Add(new Player(playerID, name, apps, goals));
                }
            } //try

            finally
            {
                conn.Close();
            }
            return formerSups;
        }



        private static String correctName(String name)
        {
            if (name.Equals("Alexandre D\" Acol"))
                name = "Alexandre D'Acol";
            else if (name.Equals("Tenema N\"Diaye"))
                name = "Tenema N'Diaye";
            else if (name.Equals("Pape M\" Bow"))
                name = "Pape M'Bow";
            else if (name.Equals("Delvin N\"Dinga"))
                name = "Delvin N'Dinga";
            else if (name.Equals("Guirane N\"Daw"))
                name = "Guirane N'Daw";
            else if (name.Equals("Franck Songo\"o"))
                name = "Franck Songo'o";
            else if (name.Equals("Gaetan D\"Acunto"))
                name = "Gaetan D'Acunto";
                return name;
        }


        /*public static List<Player> getPlayersSearched(String nam)
        {
            playersSearched = new List<Player>();
            String query = null;
            nam.Replace("\"","'");

            if (!nam.Equals("") && !nam.Equals("'"))
            {
                query = @"select ID_Παίκτη, Όνομα, Τρέχουσα_Ομάδα, Πρωτάθλημα  from Παίκτες, Ομάδες
where Όνομα like '%" + nam + "%' and Τρέχουσα_Ομάδα = Ονομασία and Τρέχουσα_Ομάδα <> 'Εκτός Βάσης' order by Τρέχουσα_Ομάδα";
            }

            else
            {
                query = "select * from Παίκτες where Όνομα = 'οηξασβφρωςηβωξηως'";
            }

             try
            {
                conn.Open();
                command.CommandText = query;
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    String playerID = reader.GetString(0);
                    String name = reader.GetString(1);
                    String team = reader.GetString(2);
                    String league = reader.GetString(3);
                    Player player = new Player(playerID, name, team, league);
                    playersSearched.Add(player);
                }

             }
                 
            finally
            {
                conn.Close();
            }
            return playersSearched;
        }*/

        public static List<Player> getPlayersSearched(String nam)
        {
            playersSearched = new List<Player>();
            
            String playerID;
            String name;
            String team;
            String league;
            nam.Replace("\"", "'");

            if (!nam.Equals("") && !nam.Equals("'"))
            {
            
                try
                {
                    conn.Open();
                    ad = new SqlDataAdapter(@"select ID_Παίκτη, Όνομα, Τρέχουσα_Ομάδα, Πρωτάθλημα  from Παίκτες, Ομάδες
where Όνομα like @name and Τρέχουσα_Ομάδα = Ονομασία and Τρέχουσα_Ομάδα <> 'Εκτός Βάσης' order by Τρέχουσα_Ομάδα",conn);
                    ad.SelectCommand.Parameters.AddWithValue("@name", "%"+nam+"%");
                    set = new DataSet();
                    ad.Fill(set, "Παίκτες");

                    foreach (DataRow row in set.Tables["Παίκτες"].Rows)
                    {
                        playerID = row["ID_Παίκτη"].ToString();
                        name = row["Όνομα"].ToString();
                        team = row["Τρέχουσα_Ομάδα"].ToString();
                        league = row["Πρωτάθλημα"].ToString();
                        playersSearched.Add(new Player(playerID, name, team, league));
                    }

                }

                finally
                {
                    conn.Close();
                }
                
            }
            return playersSearched;
        }



        public static List<Player> getPlayersSearched(String nam,int r)
        {
            playersSearched = new List<Player>();
            String playerID;
            String name;
            String team;
            nam.Replace("\"", "'");

            if (!nam.Equals("") && !nam.Equals("'"))
            {
                                
                try
                {
                    conn.Open();
                    ad = new SqlDataAdapter(@"select ID_Παίκτη, Όνομα, Τρέχουσα_Ομάδα  from Παίκτες, Ομάδες
where Όνομα like @name and Τρέχουσα_Ομάδα = Ονομασία order by Τρέχουσα_Ομάδα",conn);
                    ad.SelectCommand.Parameters.AddWithValue("@name", "%" + nam + "%");
                    set = new DataSet();
                    ad.Fill(set, "Παίκτες");

                    foreach (DataRow row in set.Tables["Παίκτες"].Rows)
                    {
                        playerID = row["ID_Παίκτη"].ToString();
                        name = row["Όνομα"].ToString();
                        team = row["Τρέχουσα_Ομάδα"].ToString();
                        playersSearched.Add(new Player(playerID, name, team));
                    }

                }

                finally
                {
                    conn.Close();
                }
            }
            return playersSearched;
        }



        public static Player comparingPlayers(String name, String team) 
        {
            playersCompared = new List<Player>();
            String playerID;
            String nam;
            String presentTeam;
            double height;
            int overallGoalsSL;
            int overallGoalsFL;
            int age;
            String position;
            String country;
            int overallAppsSL;
            int overallAppsFL;
            String lentBy;
            int presentAppsSL;
            int presentAppsFL;
            int presentGoalsSL;
            int presentGoalsFL;
            String presentTeamLeague;
            Player player = null;
           

            try
            {
                conn.Open();
                ad = new SqlDataAdapter(@"select ID_Παίκτη, Όνομα, Τρέχουσα_Ομάδα, Ύψος, Γκολ_στη_Superleague, Γκολ_στη_Football_league, Ηλικία, Θέση, Χώρα,
            Εμφανίσεις_στη_Superleague, Εμφανίσεις_στη_Football_league, Δανεικός_από, Φετινές_Συμμετοχές_στη_Superleague, 
            Φετινές_Συμμετοχές_στη_Football_league, Φετινά_Γκολ_στη_Superleague, Φετινά_Γκολ_στη_Football_league, Πρωτάθλημα from Παίκτες, Ομάδες
            where Τρέχουσα_Ομάδα = @team and Όνομα = @name and Τρέχουσα_Ομάδα = Ονομασία",conn);
                ad.SelectCommand.Parameters.AddWithValue("@team",team);
                ad.SelectCommand.Parameters.AddWithValue("@name",name);
                set = new DataSet();
                ad.Fill(set, "Παίκτες");

                foreach (DataRow row in set.Tables["Παίκτες"].Rows)
                {
                    playerID = row["ID_Παίκτη"].ToString();
                    nam = correctName(row["Όνομα"].ToString());
                    presentTeam = row["Τρέχουσα_Ομάδα"].ToString();
                    height = Convert.ToDouble(row["Ύψος"]);
                    overallGoalsSL = Convert.ToInt32(row["Γκολ_στη_Superleague"]);
                    overallGoalsFL = Convert.ToInt32(row["Γκολ_στη_Football_league"]);
                    age = Convert.ToInt32(row["Ηλικία"]);
                    position = row["Θέση"].ToString();
                    country = row["Χώρα"].ToString();
                    overallAppsSL = Convert.ToInt32(row["Εμφανίσεις_στη_Superleague"]);
                    overallAppsFL = Convert.ToInt32(row["Εμφανίσεις_στη_Football_league"]);
                    lentBy = row["Δανεικός_Από"].ToString();
                    presentAppsSL = Convert.ToInt32(row["Φετινές_Συμμετοχές_στη_Superleague"]);
                    presentAppsFL = Convert.ToInt32(row["Φετινές_Συμμετοχές_στη_Football_league"]);
                    presentGoalsSL = Convert.ToInt32(row["Φετινά_Γκολ_στη_Superleague"]);
                    presentGoalsFL = Convert.ToInt32(row["Φετινά_Γκολ_στη_Football_league"]);
                    presentTeamLeague = row["Πρωτάθλημα"].ToString();
                    player = new Player(playerID, nam, presentTeam, height, overallGoalsSL, overallGoalsFL, age, position, country, overallAppsSL,
                    overallAppsFL, lentBy, presentAppsSL, presentAppsFL, presentGoalsSL, presentGoalsFL, presentTeamLeague);
                }
                }
                 
            finally
            {
                conn.Close();
            }

            
            return player;   
        }

        

        public static List<PastTeams> comparingFormerTeams(String name, String team)
        {
            pastTeams = new List<PastTeams>();
            String tm;
            String country;

            try
            {
                conn.Open();
                ad = new SqlDataAdapter(@"select Ομάδα, Προηγούμενες_Ομάδες.Χώρα as Χώρα_Ομάδας from Παίκτες, Προηγούμενες_Ομάδες
            where Τρέχουσα_Ομάδα <> Ομάδα and Παίκτες.ID_Παίκτη = Προηγούμενες_Ομάδες.ID_Παίκτη AND Όνομα = @name 
            and Τρέχουσα_Ομάδα = @team",conn);
                ad.SelectCommand.Parameters.AddWithValue("@team", team);
                ad.SelectCommand.Parameters.AddWithValue("@name", name);
                set = new DataSet();
                ad.Fill(set,"Ομάδες");

                foreach (DataRow row in set.Tables["Ομάδες"].Rows)
                {
                    tm = row["Ομάδα"].ToString();
                    country = row["Χώρα_Ομάδας"].ToString();
                    pastTeams.Add(new PastTeams(null, tm, country));
                }
            }

            finally
            {
                conn.Close();
            }

            return pastTeams;
        }



        /*public static Team ComparingTeams(String name) {

            Team team = null;
            String query = "select * from Ομάδες where Ονομασία = '" + name + "'";

            try
            {
                conn.Open();
                command.CommandText = query;
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    String nm = reader.GetString(0);
                    String league = reader.GetString(1);
                    String area = reader.GetString(2);
                    String colors = reader.GetString(3);
                    String foundationYear = reader.GetString(4);
                    int leaguesWon = reader.GetInt32(5);
                    int cupsWon = reader.GetInt32(6);
                    String lastTrophy = reader.GetString(7);
                    String field = reader.GetString(8);
                    String manager = reader.GetString(9);
                    String chairman = reader.GetString(10);
                    String page = reader.GetString(11);
                    int points = reader.GetInt32(12);
                    int games = reader.GetInt32(13);
                    int goalsFor = reader.GetInt32(14);
                    int goalsBy = reader.GetInt32(15);

                    team = new Team(nm, league, area, colors, foundationYear, leaguesWon,
            cupsWon, lastTrophy, field, manager, chairman, page, points,games,goalsFor,goalsBy);
                }
            }
            finally
            {
                conn.Close();
            }
            return team;
        }*/


        public static Team ComparingTeams(String name)
        {

            Team team = null;
            String nm;
            String league;
            String area;
            String colors;
            String foundationYear;
            int leaguesWon;
            int cupsWon;
            String lastTrophy;
            String field;
            String manager;
            String chairman;
            String page;
            int points;
            int games;
            int goalsFor;
            int goalsBy;
            

            try
            {
                conn.Open();
                ad = new SqlDataAdapter("select * from Ομάδες where Ονομασία = @name",conn);
                ad.SelectCommand.Parameters.AddWithValue("@name",name);
                set = new DataSet();
                ad.Fill(set,"Ομάδες");

                foreach (DataRow row in set.Tables["Ομάδες"].Rows)
                {
                    nm = row["Ονομασία"].ToString();
                    league = row["Πρωτάθλημα"].ToString();
                    area = row["Περιοχή"].ToString();
                    colors = row["Χρώματα"].ToString();
                    foundationYear = row["Έτος_Ίδρυσης"].ToString();
                    leaguesWon = Convert.ToInt32(row["Αριθμός_Πρωταθλημάτων"]);
                    cupsWon = Convert.ToInt32(row["Αριθμός_Κυπέλλων"]);
                    lastTrophy = row["Τελευταίο_Τρόπαιο"].ToString();
                    field = row["Έδρα"].ToString();
                    manager = row["Προπονητής"].ToString();
                    chairman = row["Πρόεδρος"].ToString();
                    page = row["Ιστοσελίδα"].ToString();
                    points = Convert.ToInt32(row["Βαθμοί"]);
                    games = Convert.ToInt32(row["Αγώνες"]);
                    goalsFor = Convert.ToInt32(row["Γκολ_Υπέρ"]);
                    goalsBy = Convert.ToInt32(row["Γκολ_Κατά"]);

                    team = new Team(nm, league, area, colors, foundationYear, leaguesWon,
            cupsWon, lastTrophy, field, manager, chairman, page, points, games, goalsFor, goalsBy);
                }
            }
            finally
            {
                conn.Close();
            }
            return team;
        }




        public static int getTeamPosition(String name, String league) 
        {
            int position = 1;
            String nm;

            try
            {
                conn.Open();
                ad = new SqlDataAdapter(@"select Ονομασία from Ομάδες
            where Πρωτάθλημα = @league order by Βαθμοί desc, Γκολ_Υπέρ - Γκολ_Κατά desc, Γκολ_Υπέρ desc, Ονομασία",conn);
                ad.SelectCommand.Parameters.AddWithValue("@league", league);
                set = new DataSet();
                ad.Fill(set,"Ομάδες");

                foreach (DataRow row in set.Tables["Ομάδες"].Rows)
                {
                    nm = row["Ονομασία"].ToString();
                    if (nm.Equals(name))
                        return position;
                    position++;
                }
            }

            finally
            {
                conn.Close();
            }

            return 55;
        }


             

        public static List<Player> PlayersPlayedInBothTeams(String team1, String team2)
        {
            AllTimeplayersPassedFromBothTeams = new List<Player>();
            HashSet<String> temp = new HashSet<String>();
            String playerID;
            String name;
            String presentTeam;
            String presentTeamLeague;


            try
            {
                conn.Open();
                ad = new SqlDataAdapter(@"select Παίκτες.ID_Παίκτη as ID_Παίκτη, Όνομα, Τρέχουσα_Ομάδα, Πρωτάθλημα from Παίκτες, Προηγούμενες_Ομάδες, Ομάδες
                where Τρέχουσα_Ομάδα <> @team1 and Τρέχουσα_Ομάδα <> @team2 and Τρέχουσα_Ομάδα <> 'Εκτός Βάσης' 
                and Παίκτες.ID_Παίκτη = Προηγούμενες_Ομάδες.ID_Παίκτη and (Ομάδα = @team1 or Ομάδα = @team2) 
                and Τρέχουσα_Ομάδα=Ονομασία order by Τρέχουσα_Ομάδα",conn);
                ad.SelectCommand.Parameters.AddWithValue("@team1",team1);
                ad.SelectCommand.Parameters.AddWithValue("@team2", team2);
                set = new DataSet();
                ad.Fill(set,"Παίκτες");

                foreach (DataRow row in set.Tables["Παίκτες"].Rows)
                {
                    playerID = row["ID_Παίκτη"].ToString();
                    name = correctName(row["Όνομα"].ToString());
                    presentTeam = row["Τρέχουσα_Ομάδα"].ToString();
                    presentTeamLeague = row["Πρωτάθλημα"].ToString();
                    if (temp.Contains(playerID))
                        AllTimeplayersPassedFromBothTeams.Add(new Player(playerID, name, presentTeam, presentTeamLeague));
                    temp.Add(playerID);
                }
            }

            finally
            {
                conn.Close();
            }

            return AllTimeplayersPassedFromBothTeams;
        }

        /*public static List<Player> ActivePlayersPlayedInBothTeams(String team1, String team2)
        {
            playersPassedFromBothTeams = new List<Player>();
            String query = @"select Παίκτες.ID_Παίκτη, Όνομα, Τρέχουσα_Ομάδα, Πρωτάθλημα from Παίκτες, Προηγούμενες_Ομάδες, Ομάδες
            where ((Τρέχουσα_Ομάδα = @team1 and Ομάδα = @team2) or (Τρέχουσα_Ομάδα = @team2 and Ομάδα = @team1)) 
            and Παίκτες.ID_Παίκτη = Προηγούμενες_Ομάδες.ID_Παίκτη and Τρέχουσα_Ομάδα = Ονομασία order by Τρέχουσα_Ομάδα";

            try
            {
                conn.Open();
                command.CommandText = query;
                SqlDataReader reader = command.ExecuteReader();


                while (reader.Read())
                {
                    String playerID = reader.GetString(0);
                    String name = reader.GetString(1);
                    name = correctName(name);
                    String presentTeam = reader.GetString(2);
                    String presentTeamLeague = reader.GetString(3);
                    Player player = new Player(playerID, name, presentTeam, presentTeamLeague);
                    playersPassedFromBothTeams.Add(player);

                } //while
            } //try

            finally
            {
                conn.Close();
            }
            return playersPassedFromBothTeams;

        }*/

        public static List<Player> ActivePlayersPlayedInBothTeams(String team1, String team2)
        {
            playersPassedFromBothTeams = new List<Player>();

            String playerID;
            String name;
            String presentTeam;
            String presentTeamLeague;


            try
            {
                conn.Open();
                ad = new SqlDataAdapter(@"select Παίκτες.ID_Παίκτη, Όνομα, Τρέχουσα_Ομάδα, Πρωτάθλημα from Παίκτες, Προηγούμενες_Ομάδες, Ομάδες
            where ((Τρέχουσα_Ομάδα = @team1 and Ομάδα = @team2) or (Τρέχουσα_Ομάδα = @team2 and Ομάδα = @team1)) 
            and Παίκτες.ID_Παίκτη = Προηγούμενες_Ομάδες.ID_Παίκτη and Τρέχουσα_Ομάδα = Ονομασία order by Τρέχουσα_Ομάδα",conn);
                ad.SelectCommand.Parameters.AddWithValue("@team1", team1);
                ad.SelectCommand.Parameters.AddWithValue("@team2", team2);
                set = new DataSet();
                ad.Fill(set, "Παίκτες");

                foreach (DataRow row in set.Tables["Παίκτες"].Rows)
                {
                    playerID = row["ID_Παίκτη"].ToString();
                    name = correctName(row["Όνομα"].ToString());
                    presentTeam = row["Τρέχουσα_Ομάδα"].ToString();
                    presentTeamLeague = row["Πρωτάθλημα"].ToString();
                    playersPassedFromBothTeams.Add(new Player(playerID, name, presentTeam, presentTeamLeague));

                } //while
            } //try

            finally
            {
                conn.Close();
            }
            return playersPassedFromBothTeams;

        }

        /*public static HashSet<String> searchTeam(String name)
        {
            teamNames = new HashSet<String>();
            String query = @"select Ονομασία from Ομάδες
            where Ονομασία like'%" + name + "%'";

            try
            {
                conn.Open();
                command.CommandText = query;
                SqlDataReader reader = command.ExecuteReader();

                while(reader.Read()) 
                {
                    String nm = reader.GetString(0);
                    teamNames.Add(nm);
                }
            } //try

            finally
            {
                conn.Close();
            }

            query = @"select distinct Ομάδα from Προηγούμενες_Ομάδες
                    where Ομάδα like'%"+ name +"%'";

            try
            {
                conn.Open();
                command.CommandText = query;
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    String nm = reader.GetString(0);
                    teamNames.Add(nm);
                }
            } //try

            finally
            {
                conn.Close();
            }

            return teamNames;
        }*/


            public static List<Player> getAllTheFormerOrPresentPlayers(String team)
        {
            allFormerOrPresentPlayers = new List<Player>();
            String playerID;
            String name;
            String presentTeam;
            String presentTeamLeague;

            try
            {
                conn.Open();
                ad = new SqlDataAdapter(@"select distinct Παίκτες.ID_Παίκτη as ID_Παίκτη, Όνομα, Τρέχουσα_Ομάδα, Πρωτάθλημα  from Παίκτες, Ομάδες, Προηγούμενες_Ομάδες
            where Τρέχουσα_Ομάδα = Ονομασία and Παίκτες.ID_Παίκτη = Προηγούμενες_Ομάδες.ID_Παίκτη and 
            ((Τρέχουσα_Ομάδα = @team or Ομάδα = @team) and Τρέχουσα_Ομάδα <> 'Εκτός Βάσης') order by Τρέχουσα_Ομάδα",conn);
                ad.SelectCommand.Parameters.AddWithValue("@team",team);
                set = new DataSet();
                ad.Fill(set,"Παίκτες");
                
                foreach (DataRow row in set.Tables["Παίκτες"].Rows)
                {
                    playerID = row["ID_Παίκτη"].ToString();
                    name = correctName(row["Όνομα"].ToString());
                    presentTeam = row["Τρέχουσα_Ομάδα"].ToString();
                    presentTeamLeague = row["Πρωτάθλημα"].ToString();
                    allFormerOrPresentPlayers.Add(new Player(playerID, name, presentTeam, presentTeamLeague));

                } //while
            } //try

            finally
            {
                conn.Close();
            }
            return allFormerOrPresentPlayers;


        }



        /*public static List<Player> getAllPlayersComingFromCountry(String country)
        {
            playersComingFromCountry = new List<Player>();
            String query = @"select Παίκτες.ID_Παίκτη, Όνομα, Τρέχουσα_Ομάδα, Πρωτάθλημα  from Παίκτες, Ομάδες
            where Χώρα = '" + country + "' and Τρέχουσα_Ομάδα = Ονομασία and Τρέχουσα_Ομάδα <> 'Εκτός Βάσης' order by Τρέχουσα_Ομάδα";

            try
            {
                conn.Open();
                command.CommandText = query;
                SqlDataReader reader = command.ExecuteReader();


                while (reader.Read())
                {
                    String playerID = reader.GetString(0);
                    String name = reader.GetString(1);
                    name = correctName(name);
                    String presentTeam = reader.GetString(2);
                    String presentTeamLeague = reader.GetString(3);
                    Player player = new Player(playerID, name, presentTeam, presentTeamLeague);
                    playersComingFromCountry.Add(player);

                } //while
            } //try

            finally
            {
                conn.Close();
            }
            return playersComingFromCountry;
        }*/

        public static List<Player> getAllPlayersComingFromCountry(String country)
        {
            playersComingFromCountry = new List<Player>();
            String playerID;
            String name;
            String presentTeam;
            String presentTeamLeague;

            try
            {
                conn.Open();
                ad = new SqlDataAdapter(@"select Παίκτες.ID_Παίκτη as ID_Παίκτη, Όνομα, Τρέχουσα_Ομάδα, Πρωτάθλημα  from Παίκτες, Ομάδες
            where Χώρα = @country and Τρέχουσα_Ομάδα = Ονομασία and Τρέχουσα_Ομάδα <> 'Εκτός Βάσης' order by Τρέχουσα_Ομάδα",conn);
                ad.SelectCommand.Parameters.AddWithValue("@country", country);
                set = new DataSet();
                ad.Fill(set,"Παίκτες");

                foreach (DataRow row in set.Tables["Παίκτες"].Rows)
                {
                    playerID = row["ID_Παίκτη"].ToString();
                    name = correctName(row["Όνομα"].ToString());
                    presentTeam = row["Τρέχουσα_Ομάδα"].ToString();
                    presentTeamLeague = row["Πρωτάθλημα"].ToString();
                    playersComingFromCountry.Add(new Player(playerID, name, presentTeam, presentTeamLeague));

                } //while
            } //try

            finally
            {
                conn.Close();
            }
            return playersComingFromCountry;
        }



        

        public static List<Player> getAllPlayersPlayingInCountry(String country)
        {
            playersPlayingInCountry = new List<Player>();
            String playerID;
            String name;
            String presentTeam;
            String presentTeamLeague;

            try
            {
                conn.Open();
                ad = new SqlDataAdapter(@"select distinct Παίκτες.ID_Παίκτη as ID_Παίκτη, Όνομα, Τρέχουσα_Ομάδα, Πρωτάθλημα  from Παίκτες, Ομάδες, Προηγούμενες_Ομάδες
            where Παίκτες.ID_Παίκτη = Προηγούμενες_Ομάδες.ID_Παίκτη and Τρέχουσα_Ομάδα = Ονομασία and Τρέχουσα_Ομάδα <> 'Εκτός Βάσης' 
            and Προηγούμενες_Ομάδες.Χώρα = @country order by Τρέχουσα_Ομάδα",conn);
                ad.SelectCommand.Parameters.AddWithValue("@country",country);
                set = new DataSet();
                ad.Fill(set, "Παίκτες");


                foreach (DataRow row in set.Tables["Παίκτες"].Rows)
                {
                    playerID = row["ID_Παίκτη"].ToString();
                    name = correctName(row["Όνομα"].ToString());
                    presentTeam = row["Τρέχουσα_Ομάδα"].ToString();
                    presentTeamLeague = row["Πρωτάθλημα"].ToString();
                    playersPlayingInCountry.Add(new Player(playerID, name, presentTeam, presentTeamLeague));

                } //while
            } //try

            finally
            {
                conn.Close();
            }
            return playersPlayingInCountry;
        }

        public static Info teamInfo(String name)
        {
            Info info = null;
            String image;
            String cssID;
            String capitals;
            try
            {
                conn.Open();
                ad = new SqlDataAdapter("select Εικόνα,cssid,Κεφαλαία from Ομάδες where Ονομασία = @name",conn);
                ad.SelectCommand.Parameters.AddWithValue("@name",name);
                set = new DataSet();
                ad.Fill(set, "Ομάδα");

                foreach (DataRow row in set.Tables["Ομάδα"].Rows)
                {
                    image = row["Εικόνα"].ToString();
                    cssID = row["cssid"].ToString();
                    capitals = row["Κεφαλαία"].ToString();
                    info = new Info(image, cssID, capitals);
                } //while
            } //try

            finally
            {
                conn.Close();
            }

            return info;
        }


        public static List<PastTeams> getTeamsSearched(String nam)
        {
            teamsSearched = new List<PastTeams>();
            String playerName;
            String team;
            String country;
            nam.Replace("\"", "'");

            if (!nam.Equals("") && !nam.Equals("'"))
            {

                try
                {
                    conn.Open();
                    ad = new SqlDataAdapter(@"select Όνομα, Ομάδα, Προηγούμενες_Ομάδες.Χώρα as Χώρα from Προηγούμενες_Ομάδες,Παίκτες
                    where Ομάδα like @name and Παίκτες.ID_Παίκτη = Προηγούμενες_Ομάδες.ID_Παίκτη 
                    order by Ομάδα",conn);
                    ad.SelectCommand.Parameters.AddWithValue("@name","%"+nam+"%");
                    set = new DataSet();
                    ad.Fill(set,"Ομάδες");

                    foreach (DataRow row in set.Tables["Ομάδες"].Rows)
                    {
                        playerName = row["Όνομα"].ToString();
                        team = row["Ομάδα"].ToString();
                        country = row["Χώρα"].ToString();
                        teamsSearched.Add(new PastTeams(playerName, team, country, 23));
                    }

                }

                finally
                {
                    conn.Close();
                }
                return teamsSearched;

            }
            return null;
        }

        public static String getLastPlayerID()
        {
            String lastpid = null;
            String query = "select top 1 ID_Παίκτη from Παίκτες order by ID_Παίκτη DESC";
            try
                {
                    conn.Open();
                    command.CommandText = query;
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        lastpid = reader.GetString(0);
                    }

                }

                finally
                {
                    conn.Close();
                }
            return lastpid;
        }

        public static string transferedPlayerId(String player, String team)
        {
            String playerid = null;
            try
            {
                conn.Open();
                ad = new SqlDataAdapter("select top 1 ID_Παίκτη from Παίκτες where Τρέχουσα_Ομάδα = @team and Όνομα = @name", conn);
                ad.SelectCommand.Parameters.AddWithValue("@team", team);
                ad.SelectCommand.Parameters.AddWithValue("@name", player);
                set = new DataSet();
                ad.Fill(set, "Παίκτες");
                foreach (DataRow row in set.Tables["Παίκτες"].Rows)
                {
                    playerid = row["ID_Παίκτη"].ToString();

                }
            }
            finally
            {
                conn.Close();
            }
            return playerid;
        }

        public static bool isPastTeam(String playerID, String team)
        {
            bool flag;
            try
            {
                conn.Open();
                ad = new SqlDataAdapter("select top 1 ID_Παίκτη from Προηγούμενες_Ομάδες where Ομάδα = @team and ID_Παίκτη = @playerID", conn);
                ad.SelectCommand.Parameters.AddWithValue("@team", team);
                ad.SelectCommand.Parameters.AddWithValue("@playerID", playerID);
                set = new DataSet();
                ad.Fill(set, "Προηγούμενες_Ομάδες");
                flag = false;
                foreach (DataRow row in set.Tables["Προηγούμενες_Ομάδες"].Rows)
                {
                    flag = true;
                }
            }
            finally
            {
                conn.Close();
            }
            return flag;

        }

        public static int getLeagueAppearancesTeamOrder(String playerID, String team, String league, bool present)
        {
            int order = 0;
            try
            {
                conn.Open();
                if (league.Equals("Superleague") && !present)
                    ad = new SqlDataAdapter("select ID_Παίκτη from Παίκτες where Τρέχουσα_Ομάδα = @team order by Εμφανίσεις_στη_Superleague desc", conn);
                else if(league.Equals("Superleague"))
                    ad = new SqlDataAdapter("select ID_Παίκτη from Παίκτες where Τρέχουσα_Ομάδα = @team order by Φετινές_Συμμετοχές_στη_Superleague desc", conn);
                else if(!present)
                    ad = new SqlDataAdapter("select ID_Παίκτη from Παίκτες where Τρέχουσα_Ομάδα = @team order by Εμφανίσεις_στη_Football_league desc", conn);
                else
                    ad = new SqlDataAdapter("select ID_Παίκτη from Παίκτες where Τρέχουσα_Ομάδα = @team order by Φετινές_Συμμετοχές_στη_Football_league desc", conn);

                ad.SelectCommand.Parameters.AddWithValue("@team", team);
                set = new DataSet();
                ad.Fill(set, "Παίκτες");
                foreach (DataRow row in set.Tables["Παίκτες"].Rows)
                {
                    order++;
                    if (playerID.Equals(row["ID_Παίκτη"].ToString()))
                        break;
                }
            }
            finally
            {
                conn.Close();
            }
            return order;
        }

        public static int getLeagueAppearancesLeagueOrder(String playerID, String league, bool present)
        {
            int order = 0;
            try
            {
                conn.Open();
                if(league.Equals("Superleague") && !present)
                    ad = new SqlDataAdapter(@"select ID_Παίκτη from Παίκτες inner join Ομάδες on Τρέχουσα_Ομάδα = Ονομασία
                        where Πρωτάθλημα = 'Superleague' order by Εμφανίσεις_στη_Superleague desc", conn);
                else if (league.Equals("Superleague"))
                    ad = new SqlDataAdapter(@"select ID_Παίκτη from Παίκτες inner join Ομάδες on Τρέχουσα_Ομάδα = Ονομασία
                        where Πρωτάθλημα = 'Superleague' order by Φετινές_Συμμετοχές_στη_Superleague desc", conn);
                else if (!present)
                    ad = new SqlDataAdapter(@"select ID_Παίκτη from Παίκτες inner join Ομάδες on Τρέχουσα_Ομάδα = Ονομασία
                        where Πρωτάθλημα = 'Football League' order by Εμφανίσεις_στη_Football_league desc", conn);
                else
                    ad = new SqlDataAdapter(@"select ID_Παίκτη from Παίκτες inner join Ομάδες on Τρέχουσα_Ομάδα = Ονομασία
                        where Πρωτάθλημα = 'Football League' order by Φετινές_Συμμετοχές_στη_Football_league desc", conn);
                set = new DataSet();
                ad.Fill(set, "Παίκτες");
                foreach (DataRow row in set.Tables["Παίκτες"].Rows)
                {
                    order++;
                    if (playerID.Equals(row["ID_Παίκτη"].ToString()))
                        break;
                }
            }
            finally
            {
                conn.Close();
            }
            return order;
        }



        public static int getLeagueGoalsTeamOrder(String playerID, String team, String league, bool present)
        {
            int order = 0;
            try
            {
                conn.Open();
                if (league.Equals("Superleague") && !present)
                    ad = new SqlDataAdapter("select ID_Παίκτη from Παίκτες where Τρέχουσα_Ομάδα = @team order by Γκολ_στη_Superleague desc", conn);
                else if (league.Equals("Superleague"))
                    ad = new SqlDataAdapter("select ID_Παίκτη from Παίκτες where Τρέχουσα_Ομάδα = @team order by Φετινά_Γκολ_στη_Superleague desc", conn);
                else if (!present)
                    ad = new SqlDataAdapter("select ID_Παίκτη from Παίκτες where Τρέχουσα_Ομάδα = @team order by Γκολ_στη_Football_league desc", conn);
                else
                    ad = new SqlDataAdapter("select ID_Παίκτη from Παίκτες where Τρέχουσα_Ομάδα = @team order by Φετινά_Γκολ_στη_Football_league desc", conn);

                ad.SelectCommand.Parameters.AddWithValue("@team", team);
                set = new DataSet();
                ad.Fill(set, "Παίκτες");
                foreach (DataRow row in set.Tables["Παίκτες"].Rows)
                {
                    order++;
                    if (playerID.Equals(row["ID_Παίκτη"].ToString()))
                        break;
                }
            }
            finally
            {
                conn.Close();
            }
            return order;
        }




        public static int getLeagueGoalsLeagueOrder(String playerID, String league, bool present)
        {
            int order = 0;
            try
            {
                conn.Open();
                if (league.Equals("Superleague") && !present)
                    ad = new SqlDataAdapter(@"select ID_Παίκτη from Παίκτες inner join Ομάδες on Τρέχουσα_Ομάδα = Ονομασία
                        where Πρωτάθλημα = 'Superleague' order by Γκολ_στη_Superleague desc", conn);
                else if (league.Equals("Superleague"))
                    ad = new SqlDataAdapter(@"select ID_Παίκτη from Παίκτες inner join Ομάδες on Τρέχουσα_Ομάδα = Ονομασία
                        where Πρωτάθλημα = 'Superleague' order by Φετινά_Γκολ_στη_Superleague desc", conn);
                else if (!present)
                    ad = new SqlDataAdapter(@"select ID_Παίκτη from Παίκτες inner join Ομάδες on Τρέχουσα_Ομάδα = Ονομασία
                        where Πρωτάθλημα = 'Football League' order by Γκολ_στη_Football_league desc", conn);
                else
                    ad = new SqlDataAdapter(@"select ID_Παίκτη from Παίκτες inner join Ομάδες on Τρέχουσα_Ομάδα = Ονομασία
                        where Πρωτάθλημα = 'Football League' order by Φετινά_Γκολ_στη_Football_league desc", conn);
                set = new DataSet();
                ad.Fill(set, "Παίκτες");
                foreach (DataRow row in set.Tables["Παίκτες"].Rows)
                {
                    order++;
                    if (playerID.Equals(row["ID_Παίκτη"].ToString()))
                        break;
                }
            }
            finally
            {
                conn.Close();
            }
            return order;
        }



        private static double getGoalRate(int appearances, int goals)
        {
            double rate = 0.0;
            if (appearances > 0)
                rate = Convert.ToDouble((Convert.ToDouble(goals) / Convert.ToDouble(appearances)));
            return rate;
        }


        
        public static int getLeagueGoalsRateTeamOrder(String playerID, String team, String league, bool present)
        {
            int order = 0;
            try
            {
                conn.Open();
                if (league.Equals("Superleague") && !present)
                    ad = new SqlDataAdapter(@"select ID_Παίκτη
                    from Παίκτες where Τρέχουσα_Ομάδα = @team order by cast(Γκολ_στη_Superleague as float) 
                    / case cast(Εμφανίσεις_στη_Superleague as float) 
                    when 0 then 1 else cast(Εμφανίσεις_στη_Superleague as float)
                    end desc, Εμφανίσεις_στη_Superleague desc", conn);
                else if (league.Equals("Superleague"))
                    ad = new SqlDataAdapter(@"select ID_Παίκτη
                    from Παίκτες where Τρέχουσα_Ομάδα = @team order by cast(Φετινά_Γκολ_στη_Superleague as float) 
                    / case cast(Φετινές_Συμμετοχές_στη_Superleague as float) 
                    when 0 then 1 else cast(Φετινές_Συμμετοχές_στη_Superleague as float)
                    end desc, Φετινές_Συμμετοχές_στη_Superleague desc", conn);
                else if (!present)
                    ad = new SqlDataAdapter(@"select ID_Παίκτη
                    from Παίκτες where Τρέχουσα_Ομάδα = @team order by cast(Γκολ_στη_Football_league as float) 
                    / case cast(Εμφανίσεις_στη_Football_league as float) 
                    when 0 then 1 else cast(Εμφανίσεις_στη_Football_league as float)
                    end desc, Εμφανίσεις_στη_Football_league desc", conn);
                else
                    ad = new SqlDataAdapter(@"select ID_Παίκτη
                    from Παίκτες where Τρέχουσα_Ομάδα = @team order by cast(Φετινά_Γκολ_στη_Football_league as float) 
                    / case cast(Φετινές_Συμμετοχές_στη_Football_league as float) 
                    when 0 then 1 else cast(Φετινές_Συμμετοχές_στη_Football_league as float)
                    end desc, Φετινές_Συμμετοχές_στη_Football_league desc", conn);

                ad.SelectCommand.Parameters.AddWithValue("@team", team);
                set = new DataSet();
                ad.Fill(set, "Παίκτες");
                foreach (DataRow row in set.Tables["Παίκτες"].Rows)
                {
                    order++;
                    if (playerID.Equals(row["ID_Παίκτη"].ToString()))
                        break;
                }
            }
            finally
            {
                conn.Close();
            }
            return order;
        }



        public static int getLeagueGoalsRateLeagueOrder(String playerID, String league, bool present)
        {
            int order = 0;
            try
            {
                conn.Open();
                if (league.Equals("Superleague") && !present)
                    ad = new SqlDataAdapter(@"select ID_Παίκτη from Παίκτες inner join Ομάδες on Τρέχουσα_Ομάδα = Ονομασία
                                            where Πρωτάθλημα = 'Superleague' 
                                            order by cast(Γκολ_στη_Superleague as float) /
                                            case cast(Εμφανίσεις_στη_Superleague as float) when 0 then 1 
                                            else cast(Εμφανίσεις_στη_Superleague as float) end desc,
                                            Εμφανίσεις_στη_Superleague desc", conn);
                else if (league.Equals("Superleague"))
                    ad = new SqlDataAdapter(@"select ID_Παίκτη from Παίκτες inner join Ομάδες on Τρέχουσα_Ομάδα = Ονομασία
                                            where Πρωτάθλημα = 'Superleague' 
                                            order by cast(Φετινά_Γκολ_στη_Superleague as float) /
                                            case cast(Φετινές_Συμμετοχές_στη_Superleague as float) when 0 then 1 
                                            else cast(Φετινές_Συμμετοχές_στη_Superleague as float) end desc,
                                            Φετινές_Συμμετοχές_στη_Superleague desc", conn);
                else if (!present)
                    ad = new SqlDataAdapter(@"select ID_Παίκτη from Παίκτες inner join Ομάδες on Τρέχουσα_Ομάδα = Ονομασία
                                            where Πρωτάθλημα = 'Football League' 
                                            order by cast(Γκολ_στη_Football_league as float) /
                                            case cast(Εμφανίσεις_στη_Football_league as float) when 0 then 1 
                                            else cast(Εμφανίσεις_στη_Football_league as float) end desc,
                                            Εμφανίσεις_στη_Football_league desc", conn);
                else
                    ad = new SqlDataAdapter(@"select ID_Παίκτη from Παίκτες inner join Ομάδες on Τρέχουσα_Ομάδα = Ονομασία
                                            where Πρωτάθλημα = 'Football League' 
                                            order by cast(Φετινά_Γκολ_στη_Football_league as float) /
                                            case cast(Φετινές_Συμμετοχές_στη_Football_league as float) when 0 then 1 
                                            else cast(Φετινές_Συμμετοχές_στη_Football_league as float) end desc,
                                            Φετινές_Συμμετοχές_στη_Football_league desc", conn);
                set = new DataSet();
                ad.Fill(set, "Παίκτες");
                foreach (DataRow row in set.Tables["Παίκτες"].Rows)
                {
                    order++;
                    if (playerID.Equals(row["ID_Παίκτη"].ToString()))
                        break;
                }
            }
            finally
            {
                conn.Close();
            }
            return order;
        }



        public static Player getPlayerByID(String playerID, String league)
        {
            Player player = null;
            int overallAppearancesSL = 0;
            int overallGoalsSL = 0;
            int overallAppearancesFL = 0;
            int overallGoalsFL = 0;
            int presentAppearancesSL = 0;
            int presentGoalsSL = 0;
            int presentAppearancesFL = 0;
            int presentGoalsFL = 0;
            double overallGoalRateSL = 0.0;
            double overallGoalRateFL = 0.0;
            double presentGoalRateSL = 0.0;
            double presentGoalRateFL = 0.0;

            try
            {
                conn.Open();
                ad = new SqlDataAdapter("select * from Παίκτες where ID_Παίκτη = @playerID"
                    , conn);
                ad.SelectCommand.Parameters.AddWithValue("@playerID", playerID);
                set = new DataSet();
                ad.Fill(set, "Παίκτης");
                foreach (DataRow row in set.Tables["Παίκτης"].Rows)
                {
                    overallAppearancesSL = Convert.ToInt32(row["Εμφανίσεις_στη_Superleague"]);
                    overallGoalsSL = Convert.ToInt32(row["Γκολ_στη_Superleague"]);
                    overallAppearancesFL = Convert.ToInt32(row["Εμφανίσεις_στη_Football_league"]);
                    overallGoalsFL = Convert.ToInt32(row["Γκολ_στη_Football_league"]);
                    presentAppearancesSL = Convert.ToInt32(row["Φετινές_Συμμετοχές_στη_Superleague"]);
                    presentGoalsSL = Convert.ToInt32(row["Φετινά_Γκολ_στη_Superleague"]);
                    presentAppearancesFL = Convert.ToInt32(row["Φετινές_Συμμετοχές_στη_Football_league"]);
                    presentGoalsFL = Convert.ToInt32(row["Φετινά_Γκολ_στη_Football_league"]);
                    overallGoalRateSL = getGoalRate(overallAppearancesSL,overallGoalsSL);
                    presentGoalRateSL = getGoalRate(presentAppearancesSL, presentGoalsSL);
                    overallGoalRateFL = getGoalRate(overallAppearancesFL, overallGoalsFL);
                    presentGoalRateFL = getGoalRate(presentAppearancesFL, presentGoalsFL);

                    player = new Player(playerID, row["Όνομα"].ToString(),row["Τρέχουσα_Ομάδα"].ToString(),
                        Convert.ToDouble(row["Ύψος"]), overallGoalsSL, overallGoalsFL, Convert.ToInt32(row["Ηλικία"]),
                        row["Θέση"].ToString(), row["Χώρα"].ToString(), overallAppearancesSL,
                        overallAppearancesFL, row["Δανεικός_Από"].ToString(),
                        presentAppearancesSL, presentAppearancesFL, presentGoalsSL, presentGoalsFL, 
                        overallGoalRateSL, overallGoalRateFL, presentGoalRateSL, presentGoalRateFL);
                }
            }

            finally
            {
                conn.Close();
            }
            return player;
        }

        public static List<PastTeams> getPastTeamsOfPlayer(String playerID) {
            List<PastTeams> pastTeams = new List<PastTeams>();

            try
            {
                conn.Open();
                ad = new SqlDataAdapter("select * from Προηγούμενες_Ομάδες where ID_Παίκτη = @playerID"
                    , conn);
                ad.SelectCommand.Parameters.AddWithValue("@playerID", playerID);
                set = new DataSet();
                ad.Fill(set, "Ομάδες");
                foreach (DataRow row in set.Tables["Ομάδες"].Rows)
                {
                    pastTeams.Add(new PastTeams(playerID, row["Ομάδα"].ToString(), row["Χώρα"].ToString()));
                }
            }
            finally
            {
                conn.Close();
            }
            return pastTeams;
        }

        /////////////////////////UPDATE and INSERT COMMANDS/////////////////////////////////////////////////////////////////////////////////////////////////////////

        public static int addNewPlayer(Player player)
        {
            StringBuilder sb = new StringBuilder();
            String query;
            int succ;

            sb.Append(String.Format(@"insert into Παίκτες(ID_Παίκτη,Όνομα,Τρέχουσα_Ομάδα,Ύψος,Εμφανίσεις_στη_Superleague,Γκολ_στη_Superleague
            ,Εμφανίσεις_στη_Football_league,Γκολ_στη_Football_league,Φετινές_Συμμετοχές_στη_Superleague,Φετινά_Γκολ_στη_Superleague
            ,Φετινές_Συμμετοχές_στη_Football_league,Φετινά_Γκολ_στη_Football_league
            ,Ηλικία,Θέση,Χώρα,Δανεικός_από)
            values('{0}','{1}','{2}',{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},'{13}','{14}','{15}')
            update Παίκτες set ύψος = ύψος * 0.01 where ID_Παίκτη = '{0}'"
            ,player.playerID,player.name,player.presentTeam,player.height,player.overallAppsSL,player.overallGoalsSL,player.overallAppsFL,
            player.overallGoalsFL,player.presentAppsSL,player.presentGoalsSL,player.presentAppsFL,player.presentGoalsFL,player.age,player.position,
            player.country,player.lentBy));

            query = sb.ToString();

            try
                {
                    conn.Open();
                    command.CommandText = query;
                    succ = command.ExecuteNonQuery();

                }

                finally
                {
                    conn.Close();
                }
            
            return succ;
        }

        public static int addPastTeamsOfNewPlayer(List<PastTeams> psts)
        {
            StringBuilder sb = new StringBuilder();
            String query;
            int succ;
            bool flag = false;

            sb.Append("insert into Προηγούμενες_Ομάδες(ID_Παίκτη,Ομάδα,Χώρα) values");
            foreach (PastTeams p in psts) 
            {
                if(!flag) {
                    sb.Append(String.Format("('{0}','{1}','{2}')",p.playerID,p.team,p.country));
                }                   
                else {
                    sb.Append(String.Format(",('{0}','{1}','{2}')",p.playerID,p.team,p.country));
                }
                flag = true; //is used so that the first past team is treated differently 
            }
            
            query = sb.ToString();

            try
            {
                conn.Open();
                command.CommandText = query;
                succ = command.ExecuteNonQuery();

            }

            finally
            {
                conn.Close();
            }

            return succ;
        }

        public static void transferPlayerToTeam(String playerID, String team,String loanedBy) 
        {
            try
            {
                conn.Open();
                ad = new SqlDataAdapter();
                ad.UpdateCommand = new SqlCommand("Update Παίκτες set Τρέχουσα_Ομάδα = @team, Δανεικός_Από = @loanedBy where ID_Παίκτη = @playerID", conn);
                ad.UpdateCommand.Parameters.AddWithValue("@team", team);
                ad.UpdateCommand.Parameters.AddWithValue("@playerID",playerID);
                ad.UpdateCommand.Parameters.AddWithValue("@loanedBy",loanedBy);
                ad.UpdateCommand.ExecuteNonQuery();
            }
            finally
            {
                conn.Close();
            }
        }

        public static void updateGoalsForAwayPointsAndGames(String team, int goalsFor, int goalsAgainst)
        {
            int points=0;
            if (goalsFor > goalsAgainst)
                points = 3;
            else if (goalsFor == goalsAgainst)
                points = 1;

                try
                {
                    ad = new SqlDataAdapter();
                    ad.UpdateCommand = new SqlCommand(@"Update Ομάδες set Αγώνες = Αγώνες + 1,
                                                    Γκολ_Υπέρ = Γκολ_Υπέρ + @goalsFor,
                                                    Γκολ_Κατά = Γκολ_Κατά + @goalsAgainst,
                                                    Βαθμοί = Βαθμοί + @points
                                                    where Ονομασία = @team", conn);
                    ad.UpdateCommand.Parameters.AddWithValue("@goalsFor", goalsFor);
                    ad.UpdateCommand.Parameters.AddWithValue("@goalsAgainst", goalsAgainst);
                    ad.UpdateCommand.Parameters.AddWithValue("@team", team);
                    ad.UpdateCommand.Parameters.AddWithValue("@points", points);
                    conn.Open();
                    ad.UpdateCommand.ExecuteNonQuery();
                }
                finally
                {
                    conn.Close();
                }
        }

        public static void updatePlayerAppearancesGoals(List<Scorer> players,String league)
        {
            try
            {
                conn.Open();
                foreach (Scorer player in players)
                {
                    
                    ad = new SqlDataAdapter();
                    if (league.Equals("Superleague"))
                    {
                        ad.UpdateCommand = new SqlCommand(@"update Παίκτες 
                        set Εμφανίσεις_στη_Superleague=Εμφανίσεις_στη_Superleague + 1 ,
                        Φετινές_Συμμετοχές_στη_Superleague = Φετινές_Συμμετοχές_στη_Superleague + 1,
                        Γκολ_στη_Superleague=Γκολ_στη_Superleague + @goals,
                        Φετινά_Γκολ_στη_Superleague = Φετινά_Γκολ_στη_Superleague + @goals
                        where ID_Παίκτη = @playerID", conn);
                        ad.UpdateCommand.Parameters.AddWithValue("@playerID", player.playerID);
                        ad.UpdateCommand.Parameters.AddWithValue("@goals", player.goals);
                        ad.UpdateCommand.ExecuteNonQuery();
                    }

                    else
                    {
                        ad.UpdateCommand = new SqlCommand(@"update Παίκτες
                        set Εμφανίσεις_στη_Football_league=Εμφανίσεις_στη_Football_league + 1 ,
                        Φετινές_Συμμετοχές_στη_Football_league = Φετινές_Συμμετοχές_στη_Football_league + 1,
                        Γκολ_στη_Football_league=Γκολ_στη_Football_league + @goals,
                        Φετινά_Γκολ_στη_Football_league = Φετινά_Γκολ_στη_Football_league + @goals
                        where ID_Παίκτη = @playerID", conn);
                        ad.UpdateCommand.Parameters.AddWithValue("@playerID", player.playerID);
                        ad.UpdateCommand.Parameters.AddWithValue("@goals", player.goals);
                        ad.UpdateCommand.ExecuteNonQuery();
                    }
                }
                
            }

                
            finally
            {
                conn.Close();
            }
        }

    }
}
