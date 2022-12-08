namespace Day08
{
  class Solution
  {
    List<List<int>> TreeMap;
    int Width;
    int Height;
  
    public Solution(string input)
    {
      TreeMap = input.Split("\n").Select((row) => row.ToCharArray().Select((a) => int.Parse(a.ToString())).ToList()).ToList();
      Width = TreeMap[0].Count;
      Height = TreeMap.Count;
      Console.WriteLine($"{Width},{Height}");
    }

    public int Part1()
    {
      var visibleCount = 0;
      for (var y = 0; y < Height; ++y)
      {
        for (var x = 0; x < Width; ++x)
        {
          if (GetIsVisible(x, y))
          {
            visibleCount++;
          }
        }
      }
      return visibleCount;
    }

    public int Part2()
    {
      List<int> ScenicScores = new List<int>();
      for (var y = 0; y < Height; ++y)
      {
        for (var x = 0; x < Width; ++x)
        {
          ScenicScores.Add(GetScenicScore(x, y));
        }
      }
      return ScenicScores.Max();
    }

    private bool GetIsVisible(int x, int y)
    {
      var current = TreeMap[y][x];
      return IsVisible(GetTreesToLeft(x, y), current) ||
        IsVisible(GetTreesAbove(x, y), current) ||
        IsVisible(GetTreesToRight(x, y), current) ||
        IsVisible(GetTreesBelow(x, y), current);
    }

    private bool IsVisible(List<int[]> others, int current)
    {
      List<int> othersHeights = new List<int>() { -1 };
      foreach (var other in others)
      {
        othersHeights.Add(TreeMap[other[0]][other[1]]);
      }
      return current > othersHeights.Max();
    }

    private int GetScenicScore(int x, int y)
    {
      var current = TreeMap[y][x];
      return CalculateScenicScore(GetTreesToLeft(x, y), current) *
        CalculateScenicScore(GetTreesAbove(x, y), current) *
        CalculateScenicScore(GetTreesToRight(x, y), current) *
        CalculateScenicScore(GetTreesBelow(x, y), current);
    }

    private int CalculateScenicScore(List<int[]> others, int current)
    {
      int score = 0;
      foreach (var other in others)
      {
        score++;
        if (TreeMap[other[0]][other[1]] >= current)
        {
          break;
        }
      }
      return score;
    }

    private List<int[]> GetTreesToLeft(int x, int y)
    {
      List<int[]> others = new List<int[]>();
      for (var i = x - 1; i >= 0; --i)
      {
        others.Add(new int[] { y, i });
      }
      return others;
    }

    private List<int[]> GetTreesAbove(int x, int y)
    {
      List<int[]> others = new List<int[]>();
      for (var i = y - 1; i >= 0; --i)
      {
        others.Add(new int[] { i, x });
      }
      return others;
    }

    private List<int[]> GetTreesToRight(int x, int y)
    {
      List<int[]> others = new List<int[]>();
      for (var i = x + 1; i < Width; ++i)
      {
        others.Add(new int[] { y, i });
      }
      return others;
    }

    private List<int[]> GetTreesBelow(int x, int y)
    {
      List<int[]> others = new List<int[]>();
      for (var i = y + 1; i < Height; ++i)
      {
        others.Add(new int[] { i, x });
      }
      return others;
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
