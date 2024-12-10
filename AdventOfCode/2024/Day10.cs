namespace AdventOfCode._2024
{
    public class Day10 : IDay
    {
        public int Part1(List<string> input)
        {
            var sumOfTrailheadScores = 0;
            for (int i = 0; i < input.Count; i++)
            {
                for (int j = 0; j < input[0].Length; j++)
                {
                    if (input[i][j] == '0')
                    {
                        var trailHeadScore = DistinctReachableNines(input, (i, j)).Count;
                        sumOfTrailheadScores += trailHeadScore;
                    }
                }
            }
            return sumOfTrailheadScores;
        }

        public int Part2(List<string> input)
        {
            var sumOfTrailheadScores = 0;
            for (int i = 0; i < input.Count; i++)
            {
                for (int j = 0; j < input[0].Length; j++)
                {
                    if (input[i][j] == '0')
                    {
                        var trailHeadScore = TotalReachableNines(input, (i, j)).Count;
                        sumOfTrailheadScores += trailHeadScore;
                    }
                }
            }
            return sumOfTrailheadScores;
        }

        private static List<(int,int)> DistinctReachableNines(List<string> map, (int,int) currentPosition)
        {
            var reachableNines = new List<(int,int)> { };
            var currentHeight = int.Parse(map[currentPosition.Item1][currentPosition.Item2].ToString());
            if (currentHeight == 9)
            {
                reachableNines.Add(currentPosition);
                return reachableNines;
            }

            var neighbours = GetNeighbours(currentPosition, map.Count, map[0].Length);
            
            foreach (var neighbour in neighbours)
            {
                var neighbourHeight = int.Parse(map[neighbour.Item1][neighbour.Item2].ToString());
                if (neighbourHeight == currentHeight + 1)
                {
                    var reachableNinesFromNeighbour = DistinctReachableNines(map, neighbour);
                    if (reachableNinesFromNeighbour.Count != 0)
                    {
                        reachableNines.AddRange(reachableNinesFromNeighbour);
                    }
                }
            }

            return reachableNines.Distinct().ToList();
        }

        private static List<(int, int)> TotalReachableNines(List<string> map, (int, int) currentPosition)
        {
            var reachableNines = new List<(int, int)> { };
            var currentHeight = int.Parse(map[currentPosition.Item1][currentPosition.Item2].ToString());
            if (currentHeight == 9)
            {
                reachableNines.Add(currentPosition);
                return reachableNines;
            }

            var neighbours = GetNeighbours(currentPosition, map.Count, map[0].Length);

            foreach (var neighbour in neighbours)
            {
                var neighbourHeight = int.Parse(map[neighbour.Item1][neighbour.Item2].ToString());
                if (neighbourHeight == currentHeight + 1)
                {
                    var reachableNinesFromNeighbour = TotalReachableNines(map, neighbour);
                    if (reachableNinesFromNeighbour.Count != 0)
                    {
                        reachableNines.AddRange(reachableNinesFromNeighbour);
                    }
                }
            }

            return reachableNines;
        }

        private static List<(int,int)> GetNeighbours((int,int) coordinate, int iMax, int jMax)
        {
            var i = coordinate.Item1;
            var j = coordinate.Item2;
            var neighbours = new List<(int, int)> { (i-1,j), (i,j-1), (i,j+1), (i+1,j) };
            neighbours.RemoveAll(x => x.Item1 < 0 || x.Item1 >= iMax);
            neighbours.RemoveAll(x => x.Item2 < 0 || x.Item2 >= jMax);

            return neighbours;
        }

    }
}
