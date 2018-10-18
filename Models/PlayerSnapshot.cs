namespace CzechsInNHL.Models
{
    public class PlayerSnapshot
    {
        //person-specific data
        public string fullName { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string primaryNumber { get; set; }
        public int currentAge { get; set; }
        public string birthCity { get; set; }
        public string height { get; set; }
        public double weight { get; set; }
        public string position { get; set; }
        public string currentTeam { get; set; }

        //stats
        public int assists { get; set; }
        public int goals { get; set; }
        public double shotPct { get; set; }
        public int hits { get; set; }
        public string penaltyMinutes { get; set; }
        public int plusMinus { get; set; }
        public string timeOnIce { get; set; }
        public string lastGameDate { get; set; }
        public string lastOpponent { get; set; }
    }
}