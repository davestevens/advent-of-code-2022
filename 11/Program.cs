namespace Day11
{
  class Monkey {
    public Queue<ulong> Items;
    public Func<ulong, ulong> Operation;
    public ulong Test;
    public int MonkeyWhenTrue;
    public int MonkeyWhenFalse;
    public ulong InspectionCount;

    public Monkey(string input) {
      var lines = input.Split("\n");
      var list = lines[1].Substring(18).Split(", ").Select(ulong.Parse).ToList();
      Items = new Queue<ulong>(lines[1].Substring(18).Split(", ").Select(ulong.Parse));
      Operation = (ulong value) => {
        var parts = lines[2].Substring(23).Split(" ").ToList();
        var rhs = parts[1] == "old" ? value : ulong.Parse(parts[1]);
        switch (parts[0]) {
          case "*":
            return value * rhs;
          case "+":
            return value + rhs;
          default:
            throw new Exception($"Unknown operation type {parts[0]}");
        }
      };
      Test = ulong.Parse(lines[3].Substring(21));
      MonkeyWhenTrue = int.Parse(lines[4].Substring(29));
      MonkeyWhenFalse = int.Parse(lines[5].Substring(30));
      InspectionCount = 0;
    }
  }

  class Solution
  {
    public ulong Part1(string input)
    {
      var monkeys = input.Split("\n\n").Select((i) => new Monkey(i)).ToArray();

      for (var round = 0; round < 20; ++round)
      {
        foreach (var monkey in monkeys)
        {
          while (monkey.Items.Count > 0)
          {
            monkey.InspectionCount++;
            var item = monkey.Items.Dequeue();
            var updatedItem = (ulong)Math.Floor((double)(monkey.Operation(item) / 3));
            var monkeyToPasTo = (updatedItem % monkey.Test) == 0 ? monkey.MonkeyWhenTrue: monkey.MonkeyWhenFalse;
            monkeys[monkeyToPasTo].Items.Enqueue(updatedItem);
          }
        }
      }

      Array.Sort(monkeys, (a, b) => b.InspectionCount.CompareTo(a.InspectionCount));
      return monkeys.Take(2).Aggregate(1ul, (acc, monkey) => acc * monkey.InspectionCount);
    }

    public ulong Part2(string input)
    {
      var monkeys = input.Split("\n\n").Select((i) => new Monkey(i)).ToArray();
      ulong lcm = monkeys.Aggregate(1ul, (acc, monkey) => acc * monkey.Test);

      for (var round = 0; round < 10000; ++round)
      {
        foreach (var monkey in monkeys)
        {
          while (monkey.Items.Count > 0)
          {
            monkey.InspectionCount++;
            var item = monkey.Items.Dequeue();
            var updatedItem = monkey.Operation(item) % lcm;
            var monkeyToPasTo = (updatedItem % monkey.Test) == 0 ? monkey.MonkeyWhenTrue: monkey.MonkeyWhenFalse;
            monkeys[monkeyToPasTo].Items.Enqueue(updatedItem);
          }
        }
      }

      Array.Sort(monkeys, (a, b) => b.InspectionCount.CompareTo(a.InspectionCount));
      return monkeys.Take(2).Aggregate(1ul, (acc, monkey) => acc * monkey.InspectionCount);
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
