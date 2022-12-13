using System.Text.Json.Nodes;

namespace Day13
{
  class Solution
  {
    public List<JsonNode> Packets;

    public Solution(string input)
    {
      Packets = ParsePackets(input);
    }

    public int Part1()
    {
      var index = 1;
      return Packets.Chunk(2).Aggregate(0, (acc, chunk) => {
        var currentIndex = index++;
        return acc + (Compare(chunk[0], chunk[1]) == 1 ? currentIndex : 0);
      });
    }

    public int Part2()
    {
      var divider1 = JsonNode.Parse("[[2]]");
      var divider2 = JsonNode.Parse("[[6]]");

      Packets.Add(divider1!);
      Packets.Add(divider2!);

      Packets.Sort((a, b) => Compare(b, a));

      return (Packets.IndexOf(divider1!) + 1) * (Packets.IndexOf(divider2!) + 1);
    }

    private List<JsonNode> ParsePackets(string input)
    {
      List<JsonNode> packets = new List<JsonNode>();

      foreach (var pair in input.Split("\n\n"))
      {
        foreach (var item in pair.Split("\n"))
        {
          packets.Add(JsonNode.Parse(item)!);
        }
      }

      return packets;
    }

    private int Compare(JsonNode a, JsonNode b)
    {
      if (a is JsonValue aJsonValue && b is JsonValue bJsonValue)
      {
        var aInt = aJsonValue.GetValue<int>();
        var bInt = bJsonValue.GetValue<int>();
        return Math.Clamp(bInt - aInt, -1, 1);
      }
  
      if (a is not JsonArray aJsonArray)
      {
        aJsonArray = new JsonArray(a.GetValue<int>());
      }

      if (b is not JsonArray bJsonArray)
      {
        bJsonArray = new JsonArray(b.GetValue<int>());
      }

      var max = Math.Max(aJsonArray.Count, bJsonArray.Count);
      for (var i = 0; i < max; ++i)
      {
        if (i >= aJsonArray.Count)
        {
          return 1;
        }
        if (i >= bJsonArray.Count)
        {
          return -1;
        }
        var result = Compare(aJsonArray[i]!, bJsonArray[i]!);
        if (result != 0) { return result; }
      }
  
      return 0;
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
