namespace Day05
{
  class Solution
  {
    public string Part1(string input)
    {
      var sections = input.Split("\n\n");
      List<List<char>> stacks = ParseStacks(sections.ElementAt(0).Split("\n"));

      foreach (var line in sections.ElementAt(1).Split("\n"))
      {
        var inst = line.Split(" ").ToArray();
        var count = int.Parse(inst[1]);
        var from = int.Parse(inst[3]) - 1;
        var to = int.Parse(inst[5]) - 1;

        var toMove = stacks[from].Take(count).ToArray();
        stacks[from].RemoveRange(0, count);
        stacks[to].InsertRange(0, toMove.Reverse());
      }

      return string.Join("", stacks.Select((stack) => stack[0]));
    }

    public string Part2(string input)
    {
      var sections = input.Split("\n\n");
      List<List<char>> stacks = ParseStacks(sections.ElementAt(0).Split("\n"));

      foreach (var line in sections.ElementAt(1).Split("\n"))
      {
        var inst = line.Split(" ").ToArray();
        var count = int.Parse(inst[1]);
        var from = int.Parse(inst[3]) - 1;
        var to = int.Parse(inst[5]) - 1;

        var toMove = stacks[from].Take(count).ToArray();
        stacks[from].RemoveRange(0, count);
        stacks[to].InsertRange(0, toMove);
      }

      return string.Join("", stacks.Select((stack) => stack[0]));
    }

    private List<List<char>> ParseStacks(string[] lines)
    {
      List<List<char>> stacks = new List<List<char>>();

      var input = lines.Take(lines.Count() - 1);
      foreach (var line in input)
      {
        for (var i = 0; i < line.Length; i += 4)
        {
          var stack = i / 4;
          if (stacks.Count() <= stack)
          {
            stacks.Add(new List<char>());
          }

          var current = line.Substring(i, 3);
          var letter = current[1];
          if (letter != ' ')
          {
            stacks[stack].Add(letter);
          }
        }
      }

      return stacks;
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
