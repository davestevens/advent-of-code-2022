namespace Day06
{
  class Solution
  {
    public int Part1(string input)
    {
      var signalLength = 4;
      return GetStartOfPacket(input, signalLength);
    }

    public int Part2(string input)
    {
      var signalLength = 14;
      return GetStartOfPacket(input, signalLength);
    }

    private int GetStartOfPacket(string input, int signalLength)
    {
      for (var i = 0; i < input.Length; ++i)
      {
        var currentWindow = input.Skip(i).Take(signalLength);
        var uniqueChars = new HashSet<char>(currentWindow);
        if (uniqueChars.Count() == signalLength) {
          return i + signalLength;
        }
      }
      return -1;
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
