namespace Day02
{
  enum Hand { ROCK, PAPER, SCISSORS };
  enum Result { WIN, LOSE, DRAW };

  class Solution
  {
    public Dictionary<string, Hand> Player1HandLookup = new Dictionary<string, Hand>() {
      { "A", Hand.ROCK },
      { "B", Hand.PAPER },
      { "C", Hand.SCISSORS },
    };

    public Dictionary<Hand, int> ScoreLookup = new Dictionary<Hand, int>() {
      { Hand.ROCK, 1 },
      { Hand.PAPER, 2 },
      { Hand.SCISSORS, 3 },
    };
    public Dictionary<Result, int> OutcomeLookup = new Dictionary<Result, int>() {
      { Result.WIN, 6 },
      { Result.DRAW, 3 },
      { Result.LOSE, 0 },
    };
    public Dictionary<Hand, Dictionary<Hand, Result>> TestingOutcome = new Dictionary<Hand, Dictionary<Hand, Result>>() {
      { Hand.ROCK, new Dictionary<Hand, Result>() { { Hand.PAPER, Result.WIN }, { Hand.SCISSORS, Result.LOSE }, { Hand.ROCK, Result.DRAW } } },
      { Hand.PAPER, new Dictionary<Hand, Result>() { { Hand.SCISSORS, Result.WIN }, { Hand.ROCK, Result.LOSE }, { Hand.PAPER, Result.DRAW } } },
      { Hand.SCISSORS, new Dictionary<Hand, Result>() { { Hand.ROCK, Result.WIN }, { Hand.PAPER, Result.LOSE }, { Hand.SCISSORS, Result.DRAW } } },
    };

    public int Part1(string input)
    {
      var player2HandLookup = new Dictionary<string, Hand>() {
        { "X", Hand.ROCK },
        { "Y", Hand.PAPER },
        { "Z", Hand.SCISSORS },
      };

      return input.Split("\r\n").Aggregate(0, (acc, a) =>
      {
        var players = a.Split(" ");
        Hand player1 = Player1HandLookup[players[0]];
        Hand player2 = player2HandLookup[players[1]];
        var outcome = TestingOutcome[player1][player2];
        var score = ScoreLookup[player2] + OutcomeLookup[outcome];
        return acc + score;
      });
    }

    public int Part2(string input)
    {
      var expectedOutcomeLookup = new Dictionary<string, Result>() {
        { "X", Result.LOSE },
        { "Y", Result.DRAW },
        { "Z", Result.WIN },
      };

      return input.Split("\r\n").Aggregate(0, (acc, a) =>
      {
        var players = a.Split(" ");
        Hand player1 = Player1HandLookup[players[0]];
        Result expectedOutcome = expectedOutcomeLookup[players[1]];
        Hand player2 = TestingOutcome[player1].FirstOrDefault(outcome => outcome.Value == expectedOutcome).Key;
        var outcome = TestingOutcome[player1][player2];
        var score = ScoreLookup[player2] + OutcomeLookup[outcome];
        return acc + score;
      });
    }
  }

  class Program
  {
    static void Main(string[] args)
    {
      string input = File.ReadAllText(@".\input.txt");

      var solution = new Solution();
      Console.WriteLine($"Part1: {solution.Part1(input)}");
      Console.WriteLine($"Part2: {solution.Part2(input)}");
    }
  }
}
