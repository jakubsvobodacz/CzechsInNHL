using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CzechsInNHL.Models
{
    public class CurrentTeamGameLog
    {
        public int id { get; set; }
        public string name { get; set; }
        public string link { get; set; }
    }

    public class PrimaryPositionGameLog
    {
        public string code { get; set; }
        public string name { get; set; }
        public string type { get; set; }
        public string abbreviation { get; set; }
    }

    public class TypeGameLog
    {
        public string displayName { get; set; }
    }

    public class Stat2GameLog
    {
        public string timeOnIce { get; set; }
        public int assists { get; set; }
        public int goals { get; set; }
        public int pim { get; set; }
        public int shots { get; set; }
        public int games { get; set; }
        public int hits { get; set; }
        public int powerPlayGoals { get; set; }
        public int powerPlayPoints { get; set; }
        public string powerPlayTimeOnIce { get; set; }
        public string evenTimeOnIce { get; set; }
        public string penaltyMinutes { get; set; }
        public double shotPct { get; set; }
        public int gameWinningGoals { get; set; }
        public int overTimeGoals { get; set; }
        public int shortHandedGoals { get; set; }
        public int shortHandedPoints { get; set; }
        public string shortHandedTimeOnIce { get; set; }
        public int blocked { get; set; }
        public int plusMinus { get; set; }
        public int points { get; set; }
        public int shifts { get; set; }
        //public int? faceOffPct { get; set; }
    }

    public class TeamGameLog
    {
        public int id { get; set; }
        public string name { get; set; }
        public string link { get; set; }
    }

    public class OpponentGameLog
    {
        public int id { get; set; }
        public string name { get; set; }
        public string link { get; set; }
    }

    public class ContentGameLog
    {
        public string link { get; set; }
    }

    public class GameGameLog
    {
        public int gamePk { get; set; }
        public string link { get; set; }
        public ContentGameLog content { get; set; }
    }

    public class SplitGameLog
    {
        public string season { get; set; }
        public Stat2GameLog stat { get; set; }
        public TeamGameLog team { get; set; }
        public OpponentGameLog opponent { get; set; }
        public string date { get; set; }
        public bool isHome { get; set; }
        public bool isWin { get; set; }
        public bool isOT { get; set; }
        public GameGameLog game { get; set; }
    }

    public class StatGameLog
    {
        public TypeGameLog type { get; set; }
        public List<SplitGameLog> splits { get; set; }
    }

    public class PersonGameLog
    {
        public int id { get; set; }
        public string fullName { get; set; }
        public string link { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string primaryNumber { get; set; }
        public string birthDate { get; set; }
        public int currentAge { get; set; }
        public string birthCity { get; set; }
        public string birthCountry { get; set; }
        public string nationality { get; set; }
        public string height { get; set; }
        public int weight { get; set; }
        public bool active { get; set; }
        public bool alternateCaptain { get; set; }
        public bool captain { get; set; }
        public bool rookie { get; set; }
        public string shootsCatches { get; set; }
        public string rosterStatus { get; set; }
        public CurrentTeamGameLog currentTeam { get; set; }
        public PrimaryPositionGameLog primaryPosition { get; set; }
        public List<StatGameLog> stats { get; set; }
    }

    public class RootObjectGameLog
    {
        public string copyright { get; set; }
        public List<PersonGameLog> people { get; set; }
    }
}
