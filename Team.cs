using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Rosteras
{
    public class Team
    {
        public String name { get; set; }
        public String league { get; set; }
        public String area { get; set; }
        public String colors { get; set; }
        public String foundationYear { get; set; }
        public int leaguesWon { get; set; }
        public int cupsWon { get; set; }
        public String lastTrophy { get; set; }
        public String field { get; set; }
        public String manager { get; set; }
        public String chairman { get; set; }
        public String page { get; set; }
        public int points { get; set; }
        public int position { get; set; }
        public int games { get; set; }
        public int goalsFor { get; set; }
        public int goalsBy { get; set; }

        public Team(String name,String league,String area,String colors,String foundationYear,int leaguesWon,
            int cupsWon,String lastTrophy,String field,String manager,String chairman, String page, int points, int games, int goalsFor, int goalsBy)
        {
            this.name = name;
            this.league = league;
            this.area = area;
            this.colors = colors;
            this.foundationYear = foundationYear;
            this.leaguesWon = leaguesWon;
            this.cupsWon = cupsWon;
            this.lastTrophy = lastTrophy;
            this.field = field;
            this.manager = manager;
            this.chairman = chairman;
            this.page = page;
            this.points = points;
            this.games = games;
            this.goalsFor = goalsFor;
            this.goalsBy = goalsBy;
        }

        public Team(String name,String league, String area,String foundationYear,int leaguesWon,int cupsWon,
            String lastTrophy,String field,String manager,String chairman,int points,int position,String page,int games,int goalsFor, int goalsBy)
        {
            this.name = name;
            this.league = league;
            this.area = area;
            this.foundationYear = foundationYear;
            this.leaguesWon = leaguesWon;
            this.cupsWon = cupsWon;
            this.lastTrophy = lastTrophy;
            this.field = field;
            this.manager = manager;
            this.chairman = chairman;
            this.points = points;
            this.position = position;
            this.page = page;
            this.games = games;
            this.goalsFor = goalsFor;
            this.goalsBy = goalsBy;
        }
        
    }
}