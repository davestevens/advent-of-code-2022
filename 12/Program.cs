namespace Day12
{
  public record Point(int X, int Y);

  class Solution
  {
    public Dictionary<Point, int> Map = new Dictionary<Point, int>();
    public Point StartPosition = new (0, 0);
    public Point EndPosition = new (0, 0);
    public int Width;
    public int Height;
    public List<Point> Surround = new List<Point> {
      new Point(-1, 0),
      new Point(1, 0),
      new Point(0, -1),
      new Point(0, 1),
    };

    public Solution(string input)
    {
      var rowIndex = 0;
      var columnIndex = 0;
      foreach (var row in input.Split("\n"))
      {
        columnIndex = 0;
        foreach (var c in row.ToCharArray())
        {
          char heightChar;
          switch (c) {
            case 'S':
            StartPosition = new Point(columnIndex, rowIndex);
            heightChar = 'a';
            break;
            case 'E':
            EndPosition = new Point(columnIndex, rowIndex);
            heightChar = 'z';
            break;
            default:
            heightChar = c;
            break;
          }
          Map[new Point(columnIndex, rowIndex)] = (int)heightChar;
          columnIndex++;
        }
        rowIndex++;
      }

      Width = columnIndex;
      Height = rowIndex;
    }

    public int Part1()
    {
      Point[] startPositions = new [] { StartPosition };
      return FindPath(startPositions, EndPosition);
    }

    public int Part2()
    {
      Point[] startPositions = Map.Where((m) => m.Value == (int)'a').Select((point) => point.Key).ToArray();
      return FindPath(startPositions, EndPosition);
    }

    private int FindPath(Point[] startPositions, Point end)
    {
      Dictionary<Point, int> nodes = new Dictionary<Point, int>();
      for (var y = 0; y < Height; ++y) {
        for (var x = 0; x < Width; ++x) {
          var point = new Point(x, y);
          var value = Map[point];
          nodes[new Point(x, y)] = int.MaxValue;
        }
      }
      foreach (var startPosition in startPositions)
      {
        nodes[new Point(startPosition.X, startPosition.Y)] = 0;
      }

      List<Point> unvisited = new List<Point>(startPositions);
      HashSet<Point> visited = new HashSet<Point>();

      while (unvisited.Count > 0)
      {
        unvisited.Sort((a, b) => nodes[a] - nodes[b]);
        var currentNode = unvisited[0];
        unvisited.RemoveAt(0);

        if (visited.Contains(currentNode))
        {
          continue;
        }

        foreach (var position in Surround)
        {
          var neighbour = new Point(currentNode.X + position.X, currentNode.Y + position.Y);
          if (neighbour.X >= 0 && neighbour.X < Width && neighbour.Y >= 0 && neighbour.Y < Height && Map[neighbour] - Map[currentNode] < 2)
          {
            if (visited.Contains(neighbour))
            {
              continue;
            }
            var neighbourNode = nodes[neighbour];
            nodes[neighbour] = Math.Min(neighbourNode, nodes[currentNode] + 1);
            if (neighbour == end)
            {
              break;
            }
            unvisited.Add(neighbour);
          }
        }
        visited.Add(currentNode);
      }
      return nodes[end];
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
