namespace Day01
{
  class Solution
  {
    public int Part1(string input)
    {
      var calories = input.Split("\n\n")
        .Select(groupedLines => groupedLines.Split("\n").Select(int.Parse).Aggregate(0, (acc, v) => acc + v))
        .ToArray();

      Array.Sort(calories, (a, b) => b.CompareTo(a));

      return calories[0];
    }

    public int Part2(string input)
    {
      var calories = input.Split("\n\n")
        .Select(groupedLines => groupedLines.Split("\n").Select(int.Parse).Aggregate(0, (acc, v) => acc + v))
        .ToArray();

      Array.Sort(calories, (a, b) => b.CompareTo(a));

      return calories.Take(3).Aggregate(0, (acc, v) => acc + v);
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
