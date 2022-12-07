namespace Day07
{
  class Solution
  {
    Dictionary<string, int> DirectorySizes;

    public Solution(string input)
    {
      DirectorySizes = ParseInput(input);
    }

    public int Part1()
    {
      var atMost = 100_000;
      return DirectorySizes.Values
        .Where((dir) => dir <= atMost)
        .Aggregate(0, (acc, dir) => acc + dir);
    }

    public int Part2()
    {
      var unused = 70_000_000 - DirectorySizes["/"];
      var required = 30_000_000 - unused;
      return DirectorySizes.Values
        .Where((dir) => dir > required)
        .Min();
    }

    Dictionary<string, int> ParseInput(string input)
    {
      Dictionary<string, int> directorySizes = new Dictionary<string, int>();
      List<string> stack = new List<string>();

      var lines = input.Split("\n");
      for (var i = 0; i < lines.Length; ++i)
      {
        var parts = lines[i].Split(" ");
        if (parts[0] != "$")
        {
          continue;
        }
        switch (parts[1])
        {
          case "ls":
          var j = i + 1;
          for (; j < lines.Length; ++j)
          {
            if (lines[j][0] == '$')
            {
              break;
            }
            var fileParts = lines[j].Split(" ");
            switch(fileParts[0])
            {
              case "dir":
              break;
              default:
              for (var stackIndex = 0; stackIndex < stack.Count; ++stackIndex) {
                var current = string.Join("/", stack.ToList().GetRange(0, stackIndex + 1));
                directorySizes.TryGetValue(current, out int currentSize);
                directorySizes[current] = currentSize + int.Parse(fileParts[0]);
              }
              break;
            }
          }
          i = j - 1;
          break;
          case "cd":
            if (parts[2] == "..")
            {
              stack.RemoveAt(stack.Count - 1);
            }
            else
            {
              stack.Add(parts[2]);
            }
          break;
        }
      }

      return directorySizes;
    }
  }

  class Program
  {
    static void Main(string[] args)
    {
      string input = File.ReadAllText(@".\input.txt");

      var solution = new Solution(input);
      Console.WriteLine($"Part1: {solution.Part1()}");
      Console.WriteLine($"Part2: {solution.Part2()}");
    }
  }
}
