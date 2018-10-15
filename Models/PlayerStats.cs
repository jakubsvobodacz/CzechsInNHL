using System.Collections.Generic;
public class CurrentTeam
{
    public int id { get; set; }
    public string name { get; set; }
    public string link { get; set; }
}

public class PrimaryPosition
{
    public string code { get; set; }
    public string name { get; set; }
    public string type { get; set; }
    public string abbreviation { get; set; }
}

public class Type
{
    public string displayName { get; set; }
}

public class Stat2
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
    //public int faceOffPct { get; set; }
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
    public string timeOnIcePerGame { get; set; }
    public string evenTimeOnIcePerGame { get; set; }
    public string shortHandedTimeOnIcePerGame { get; set; }
    public string powerPlayTimeOnIcePerGame { get; set; }
}

public class Split
{
    public string season { get; set; }
    public Stat2 stat { get; set; }
}

public class Stat
{
    public Type type { get; set; }
    public List<Split> splits { get; set; }
}

public class Person
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
    public CurrentTeam currentTeam { get; set; }
    public PrimaryPosition primaryPosition { get; set; }
    public List<Stat> stats { get; set; }
}

public class RootObject
{
    public string copyright { get; set; }
    public List<Person> people { get; set; }
}