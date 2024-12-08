namespace AdventOfCode._2024
{
    public class Day8 : IDay
    {
        public int Part1(List<string> input)
        {
            // first collect antenna locations
            var antennaDict = GetAntennaLocations(input);

            // then calculate antinodes
            var iMax = input.Count - 1;
            var jMax = input[0].Length - 1;
            var antinodeLocations = new HashSet<(int, int)>();
            foreach (var key in antennaDict.Keys)
            {
                var locationList = antennaDict[key];
                if (locationList.Count == 1)
                {
                    continue;
                }
                for (int i = 0; i < locationList.Count; i++)
                {
                    var nodeA = locationList[i];
                    var remainingNodes = locationList.GetRange(i + 1, locationList.Count - (i + 1));
                    foreach (var nodeB in remainingNodes)
                    {
                        var iDiff = nodeA.Item1 - nodeB.Item1;
                        var jDiff = nodeA.Item2 - nodeB.Item2;

                        var antiNode1 = (nodeA.Item1 + iDiff, nodeA.Item2 + jDiff);
                        var antiNode2 = (nodeB.Item1 - iDiff, nodeB.Item2 - jDiff);

                        if (WithinBounds(antiNode1, iMax, jMax))
                        {
                            antinodeLocations.Add(antiNode1);
                        }
                        if (WithinBounds(antiNode2, iMax, jMax))
                        {
                            antinodeLocations.Add(antiNode2);
                        }
                    }
                }
            }

            // print board for debugging
            PrintBoard(input, antinodeLocations);

            return antinodeLocations.Count;
        }

        public int Part2(List<string> input)
        {
            // first collect antenna locations
            var antennaDict = GetAntennaLocations(input);

            // then calculate antinodes
            var iMax = input.Count - 1;
            var jMax = input[0].Length - 1;
            var antinodeLocations = new HashSet<(int, int)>();
            foreach (var key in antennaDict.Keys)
            {

                var locationList = antennaDict[key];
                if (locationList.Count == 1)
                {
                    antinodeLocations.Add(locationList[0]);
                    continue;
                }
                for (int i = 0; i < locationList.Count; i++)
                {
                    antinodeLocations.Add(locationList[i]);
                    var nodeA = locationList[i];
                    var remainingNodes = locationList.GetRange(i + 1, locationList.Count - (i + 1));
                    foreach (var nodeB in remainingNodes)
                    {
                        var iDiff = nodeA.Item1 - nodeB.Item1;
                        var jDiff = nodeA.Item2 - nodeB.Item2;

                        var positiveAntinode = (nodeA.Item1 + iDiff, nodeA.Item2 + jDiff);
                        var multiplier = 2;
                        while (WithinBounds(positiveAntinode, iMax, jMax))
                        {
                            antinodeLocations.Add(positiveAntinode);
                            positiveAntinode = (nodeA.Item1 + (iDiff * multiplier), nodeA.Item2 + (jDiff * multiplier));
                            multiplier++;
                        }

                        // TODO: DRY this
                        var negativeAntinode = (nodeB.Item1 - iDiff, nodeB.Item2 - jDiff);
                        multiplier = 2;
                        while (WithinBounds(negativeAntinode, iMax, jMax))
                        {
                            antinodeLocations.Add(negativeAntinode);
                            negativeAntinode = (nodeB.Item1 - (iDiff * multiplier), nodeB.Item2 - (jDiff * multiplier));
                            multiplier++;
                        }
                    }
                }
            }

            // print board for debugging
            PrintBoard(input, antinodeLocations);

            return antinodeLocations.Count;
        }

        private static Dictionary<char, List<(int, int)>> GetAntennaLocations(List<string> input)
        {
            var antennaDict = new Dictionary<char, List<(int, int)>>();
            for (int i = 0; i < input.Count; i++)
            {
                for (int j = 0; j < input[i].Length; j++)
                {
                    var inputChar = input[i][j];
                    if (inputChar == '.')
                    {
                        continue;
                    }
                    else if (antennaDict.ContainsKey(inputChar))
                    {
                        antennaDict[inputChar].Add((i, j));
                    }
                    else
                    {
                        antennaDict[inputChar] = new List<(int, int)> { (i, j) };
                    }
                }
            }
            return antennaDict;
        }

        private static bool WithinBounds((int,int) node, int iMax, int jMax)
        {
            if (node.Item1 < 0 || node.Item1 > iMax)
            {
                return false;
            }
            if (node.Item2 < 0 || node.Item2 > jMax)
            {
                return false;
            }
            return true;
        }

        private static void PrintBoard(List<string> input, HashSet<(int, int)> antinodeLocations)
        {
            for (int i = 0; i < input.Count; i++)
            {
                for (int j = 0; j < input[0].Length; j++)
                {
                    if (antinodeLocations.Contains((i, j)))
                    {
                        Console.Write('#');
                    }
                    else
                    {
                        Console.Write(input[i][j]);
                    }
                }
                Console.Write('\n');
            }
        }
    }
}
