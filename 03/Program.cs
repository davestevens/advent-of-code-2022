namespace Day03
{
  class Solution
  {
    public int Part1(string input)
    {
      return input.Split("\n")
        .Select((line) => {
          var middle = line.Length / 2;
          var left = line.Substring(0, middle);
          var right = line.Substring(middle);
          var common = left.Intersect(right);
          return common.ElementAt(0);
        })
        .Aggregate(0, (acc, value) => acc + GetPriority(value));
    }

    public int Part2(string input)
    {
      return input.Split("\n")
        .Select((value, index) => new { index , value })
        .GroupBy((x) => x.index / 3)
        .Select((group) => group.Select(g => g.value))
        .Select((group) => {
          var common = group.ElementAt(0).Intersect(group.ElementAt(1)).Intersect(group.ElementAt(2));
          return common.ElementAt(0);
        })
        .Aggregate(0, (acc, value) => acc + GetPriority(value));
    }

    private int GetPriority(char input)
    {
      byte value = (byte)input;
      if (value >= 97)
      {
        return value - 96;
      }
      return value - 38;
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
