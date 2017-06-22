using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Rosteras
{
    public class Player
    {
        public String playerID { get; set; }
        public String name { get; set; }
        public String presentTeam { get; set; }
        public double height { get; set; }
        public int overallGoalsSL { get; set; }
        public int overallGoalsFL { get; set; }
        public int age { get; set; }
        public String position { get; set; }
        public String country { get; set; }
        public int overallAppsSL { get; set; }
        public int overallAppsFL { get; set; }
        public String lentBy { get; set; }
        public int presentAppsSL { get; set; }
        public int presentAppsFL { get; set; }
        public int presentGoalsSL { get; set; }
        public int presentGoalsFL { get; set; }
        public String presentTeamPage { get; set; }
        public String presentTeamLeague { get; set; }
        public double overallGoalRateSL { get; set; }
        public double overallGoalRateFL { get; set; }
        public double presentGoalRateSL { get; set; }
        public double presentGoalRateFL { get; set; }

        public float goalRate { get; set; }

        public Player(String playerID, String name, String presentTeam, double height, int overallGoalsSL, int overallGoalsFL, int age, String position,
        String country, int overallAppsSL, int overallAppsFL, String lentBy, int presentAppsSL, int presentAppsFL, int presentGoalsSL,
        int presentGoalsFL)
        {

            this.playerID = playerID;
            this.name = name;
            this.presentTeam = presentTeam;
            this.height = height;
            this.overallGoalsSL = overallGoalsSL;
            this.overallGoalsFL = overallGoalsFL;
            this.age = age;
            this.position = position;
            this.country = country;
            this.overallAppsSL = overallAppsSL;
            this.overallAppsFL = overallAppsFL;
            this.lentBy = lentBy;
            this.presentAppsSL = presentAppsSL;
            this.presentAppsFL = presentAppsFL;
            this.presentGoalsSL = presentGoalsSL;
            this.presentGoalsFL = presentGoalsFL;
        }

        public Player(String playerID, String name, String presentTeam, double height, int overallGoalsSL, int overallGoalsFL, int age, String position,
        String country, int overallAppsSL, int overallAppsFL, String lentBy, int presentAppsSL, int presentAppsFL, int presentGoalsSL,
        int presentGoalsFL, String presentTeamLeague)
        {

            this.playerID = playerID;
            this.name = name;
            this.presentTeam = presentTeam;
            this.height = height;
            this.overallGoalsSL = overallGoalsSL;
            this.overallGoalsFL = overallGoalsFL;
            this.age = age;
            this.position = position;
            this.country = country;
            this.overallAppsSL = overallAppsSL;
            this.overallAppsFL = overallAppsFL;
            this.lentBy = lentBy;
            this.presentAppsSL = presentAppsSL;
            this.presentAppsFL = presentAppsFL;
            this.presentGoalsSL = presentGoalsSL;
            this.presentGoalsFL = presentGoalsFL;
            this.presentTeamLeague = presentTeamLeague;
        }

        public Player(String playerID, String name, String presentTeam, String presentTeamLeague)
        {
            this.playerID = playerID;
            this.name = name;
            this.presentTeam = presentTeam;
            this.presentTeamLeague = presentTeamLeague;
        }

        public Player(String playerID, String name, String presentTeam)
        {
            this.playerID = playerID;
            this.name = name;
            this.presentTeam = presentTeam;
        }

        public Player(String playerID, String name, int overallAppsSL, int overallGoalsSL)
        {
            this.playerID = playerID;
            this.name = name;
            this.overallAppsSL = overallAppsSL;
            this.overallGoalsSL = overallGoalsSL;
        }

        public Player(String playerID, float goalRate)
        {
            this.playerID = playerID;
            this.goalRate = goalRate;
        }

        public Player(String playerID, String name, String presentTeam, double height, int overallGoalsSL, int overallGoalsFL, int age, String position,
        String country, int overallAppsSL, int overallAppsFL, String lentBy, int presentAppsSL, int presentAppsFL, int presentGoalsSL,
        int presentGoalsFL, double overallGoalRateSL, double overallGoalRateFL, double presentGoalRateSL, double presentGoalRateFL)
        {

            this.playerID = playerID;
            this.name = name;
            this.presentTeam = presentTeam;
            this.height = height;
            this.overallGoalsSL = overallGoalsSL;
            this.overallGoalsFL = overallGoalsFL;
            this.age = age;
            this.position = position;
            this.country = country;
            this.overallAppsSL = overallAppsSL;
            this.overallAppsFL = overallAppsFL;
            this.lentBy = lentBy;
            this.presentAppsSL = presentAppsSL;
            this.presentAppsFL = presentAppsFL;
            this.presentGoalsSL = presentGoalsSL;
            this.presentGoalsFL = presentGoalsFL;
            this.overallGoalRateSL = overallGoalRateSL;
            this.overallGoalRateFL = overallGoalRateFL;
            this.presentGoalRateSL = presentGoalRateSL;
            this.presentGoalRateFL = presentGoalRateFL;
        }

    }
}