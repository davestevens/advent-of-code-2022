namespace Day09
{
  class Solution
  {
    Dictionary<char, int[]> DirectionLookup = new Dictionary<char, int[]>() {
      { 'R', new int[] {  1,  0 } },
      { 'L', new int[] { -1,  0 } },
      { 'U', new int[] {  0,  1 } },
      { 'D', new int[] {  0, -1 } },
    };

    int[,] Positions = new int[0, 0];

    public int Part1(string input)
    {
      var length = 1;
      Setup(length);
      return Run(input, length);
    }

    public int Part2(string input)
    {
      var length = 9;
      Setup(length);
      return Run(input, length);
    }

    private void Setup(int length)
    {
      Positions = new int[length + 1, 2];
      for (var i = 0; i < length + 1; ++i)
      {
        Positions[i, 0] = 0;
        Positions[i, 1] = 0;
      }
    }

    private int Run(string input, int length)
    {
      HashSet<string> VisitedPositions = new HashSet<string>();
      var instructions = input.Split("\n");
      foreach (var instruction in instructions)
      {
        var instructionParts = instruction.Split(" ");
        var direction = DirectionLookup[char.Parse(instructionParts[0])];
        var count = int.Parse(instructionParts[1]);

        for (var i = 0; i < count; ++i)
        {
          Positions[0, 0] += direction[0];
          Positions[0, 1] += direction[1];

          for (var j = 1; j < length + 1; ++j)
          {
            var xDiff = Positions[j - 1, 0] - Positions[j, 0];
            var yDiff = Positions[j - 1, 1] - Positions[j, 1];

            if (Math.Abs(xDiff) > 1 || Math.Abs(yDiff) > 1)
            {
              Positions[j, 0] += Math.Clamp(xDiff, -1, 1);
              Positions[j, 1] += Math.Clamp(yDiff, -1, 1);
            }
            else
            {
              break;
            }
          }

          VisitedPositions.Add($"{Positions[length, 0]},{Positions[length, 1]}");
        }

      }
      return VisitedPositions.Count;
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
