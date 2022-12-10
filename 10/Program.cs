namespace Day10
{
  class Solution
  {
    List<int> Registers = new List<int>();

    public Solution(string input)
    {
      Registers = Run(input);
    }

    public int Part1()
    {
      int[] cycles = new int[] { 20, 60, 100, 140, 180, 220 };
      return cycles.Aggregate(0, (acc, cycle) => acc + (cycle * Registers[cycle - 1]));
    }

    public string Part2()
    {
      string screen = "";
      for (var y = 0; y < 6; ++y) {
        string line = "";
        for (var x = 0; x < 40; ++x) {
          var current = Registers[((y * 40) + x)];
          line += (x >= current - 1 && x <= current + 1) ? "#" : ".";
        }
        screen += $"{line}\n";
      }
      return screen;
    }

    private List<int> Run(string input)
    {
      List<int> registers = new List<int>();
      var x = 1;

      var instructions = input.Split("\n");
      foreach (var instruction in instructions)
      {
        var instructionParts = instruction.Split(" ");
        switch (instructionParts[0])
        {
          case "noop":
            registers.Add(x);
            break;
          case "addx":
            registers.Add(x);
            registers.Add(x);
            x += int.Parse(instructionParts[1]);
            break;
        }
      }

      return registers;
    }
  }

  class Program
  {
    static void Main(string[] args)
    {
      string input = File.ReadAllText(@".\input.txt");

      var solution = new Solution(input);
      Console.WriteLine($"Part1: {solution.Part1()}");
      Console.WriteLine($"Part2: \n{solution.Part2()}");
    }
  }
}
