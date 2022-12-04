namespace Day04
{
  class Solution
  {
    public int Part1(string input)
    {
      return input.Split("\n")
        .Where((line) => {
          var sections = line.Split(",");
          var a = sections.ElementAt(0).Split("-").Select(int.Parse).ToArray();
          var b = sections.ElementAt(1).Split("-").Select(int.Parse).ToArray();
          return (a[0] >= b[0] && a[1] <= b[1]) || (b[0] >= a[0] && b[1] <= a[1]);
        })
        .Count();
    }

    public int Part2(string input)
    {
      return input.Split("\n")
        .Where((line) => {
          var sections = line.Split(",");
          var a = sections.ElementAt(0).Split("-").Select(int.Parse).ToArray();
          var b = sections.ElementAt(1).Split("-").Select(int.Parse).ToArray();
          return ( a[0] >= b[0] &&  a[0] <= b[1]) || (b[0] >=  a[0] && b[0] <= a[1]);
        })
        .Count();
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
