const fs = require("fs");

const input = fs.readFileSync("input.txt", "utf-8");

const info = input.split("\n").map((line) => {
  const [a, b] = line.split(";");
  const valve = a.substring(6, 8);
  const rate = Number(a.substring(23));
  const connections = b.substring(23).split(", ").map((c) => c.trim());
  return { valve, rate, connections };
});

const shortestPath = (from, to, graph) => {
  const nodes = {};
  graph.forEach((i) => {
    nodes[i.valve] = (from.valve === i.valve) ? 0 : Number.MAX_SAFE_INTEGER;
  });
  const visited = new Set();
  let unvisited = [
    from.valve
  ];

  while (unvisited.length) {
    unvisited = unvisited.sort((a, b) => a - b);
    const node = unvisited.shift();
    if (visited.has(node)) {
      continue;
    }
    const possiblePaths = graph.find((g) => g.valve === node).connections;
    for (let j = 0; j < possiblePaths.length; ++j) {
      const other = possiblePaths[j];
      if (visited.has(other)) {
        continue;
      }
      nodes[other] = Math.min(nodes[other], nodes[node] + 1);
      unvisited.push(other);
      if (other === to.valve) {
        return nodes[other];
      }
      visited.add(node);
    }
  }
  return -1;
}

for (let i = 0; i < info.length; ++i) {
  const from = info[i];
  from.others = {};
  for (let j = 0; j < info.length; ++j) {
    const to = info[j];
    if (from.valve === to.valve) {
      continue;
    }
    from.others[to.valve] = shortestPath(from, to, info);
  }
}

const OPENING_TIME_COST = 1;
const calculateTime = (path) => {
  let time = 1;
  let current = path[0];
  for (var i = 1; i < path.length; ++i) {
    const cost = info.find((j) => j.valve === current).others[path[i]];
    time += cost + OPENING_TIME_COST;
    current = path[i];
  }
  return time;
};

const buildPermutations = (start, possible, maxTime) => {
  const permutations = [];
  const build = (current, others) => {
    if (calculateTime(current) > maxTime) {
      return;
    }

    permutations.push(current);

    for (var i = 0; i < others.length; ++i) {
      build(
        [].concat(current, others[i]),
        others.filter((_o, index) => index !== i),
      );
    }
  };
  build(start, possible);
  return permutations;
}

const calculateScore = (path, maxTime) => {
  let time = 1;
  let score = 0;
  let current = path[0];
  for (var i = 1; i < path.length; ++i) {
    const cost = info.find((j) => j.valve === current).others[path[i]];
    time += cost;
    if (time > maxTime) {
      return score;
    }
    const rate = info.find((j) => j.valve === path[i]).rate;
    score += rate * (maxTime - time);
    time += OPENING_TIME_COST;
    current = path[i];
    if (time > maxTime) {
      return score;
    }
  }
  return score;
};

const part1 = () => {
  const MAX_TIME = 30;
  const START = 'AA';
  const permutations = buildPermutations([START], info.filter((i) => i.rate > 0).map((i) => i.valve), MAX_TIME);
  let maxScore = 0;
  permutations.forEach((p) => {
    const score = calculateScore(p, MAX_TIME);
    maxScore = Math.max(maxScore, score);
  });
  return maxScore;
};

const part2 = () => {
  const START = 'AA';
  const MAX_TIME = 26;
  const permutations = buildPermutations([START], info.filter((i) => i.rate > 0).map((i) => i.valve), MAX_TIME);
  let maxScore = 0;
  permutations.forEach((p) => {
    const score = calculateScore(p, MAX_TIME);
    const unopenedValves = info
      .filter((i) => i.rate > 0 && p.indexOf(i.valve) === -1)
      .map((i) => i.valve);

    const elephantPermutations = buildPermutations([START], unopenedValves);
    elephantPermutations.forEach((eP) => {
      const elephantScore = calculateScore(eP, MAX_TIME);
      const totalScore = score + elephantScore;
      maxScore = Math.max(maxScore, totalScore);
    });
    maxScore = Math.max(maxScore, score);
  });
  return maxScore;
};

console.log(`Part1: ${part1()}`);
console.log(`Part2: ${part2()}`);
