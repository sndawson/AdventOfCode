namespace AdventOfCode._2024
{
    public class Day12 : IDay
    {
        public int Part1(List<string> input)
        {
            var regions = GetRegions(input);
            var price = GetPrice(input, regions);
            return price;
        }

        public int Part2(List<string> input)
        {
            var regions = GetRegions(input);
            var price = GetDiscountedPrice(input, regions);
            return price;
        }

        private List<(char, List<(int, int)>)> GetRegions(List<string> input)
        {
            var regions = new List<(char, List<(int, int)>)>();
            for (int i = 0; i < input.Count; i++)
            {
                for (int j = 0; j < input[i].Length; j++)
                {
                    var plant = input[i][j];
                    if (regions.Where(x => x.Item1 == plant && x.Item2.Contains((i, j))).Any())
                    {
                        continue;
                    }
                    else
                    {
                        var regionCoordinates = GetRegionCoordinates(input, (i, j), new List<(int, int)>());
                        regions.Add((plant, regionCoordinates));
                    }
                }
            }
            return regions;
        }

        private int GetPrice(List<string> input, List<(char, List<(int, int)>)> regions)
        {
            var price = 0;
            foreach (var region in regions)
            {
                var area = region.Item2.Count;
                var perimeter = 0;
                foreach (var regionCoordinate in region.Item2)
                {
                    var neighbours = GetNeighbours(regionCoordinate, input.Count, input[0].Length);
                    var neighboursInRegion = neighbours.Where(x => input[x.Item1][x.Item2] == region.Item1).ToList();
                    perimeter += 4 - neighboursInRegion.Count;
                }
                price += area * perimeter;
            }

            return price;
        }

        private int GetDiscountedPrice(List<string> input, List<(char, List<(int, int)>)> regions)
        {
            var price = 0;
            foreach (var region in regions)
            {
                var area = region.Item2.Count;
                var numberOfSides = 0;
                // TODO
            }

            return price;
        }

        private List<(int,int)> GetRegionCoordinates(List<string> input, (int,int) startingCoord, List<(int,int)> regionCoords)
        {
            var plant = input[startingCoord.Item1][startingCoord.Item2];
            var localRegionCoords = regionCoords.ToList();
            localRegionCoords.Add(startingCoord);
            var neighbours = GetNeighbours(startingCoord, input.Count, input[0].Length);
            foreach (var neighbour in neighbours)
            {
                var neighbourValue = input[neighbour.Item1][neighbour.Item2];
                if (neighbourValue == plant && !regionCoords.Contains(neighbour))
                {
                    var neighbourCoords = GetRegionCoordinates(input, neighbour, localRegionCoords);
                    localRegionCoords.AddRange(neighbourCoords);
                }
            }

            return localRegionCoords.Distinct().ToList();
        }

        // copy-pasted from day 10, maybe should add this to a utils class
        private static List<(int, int)> GetNeighbours((int, int) coordinate, int iMax, int jMax)
        {
            var i = coordinate.Item1;
            var j = coordinate.Item2;
            var neighbours = new List<(int, int)> { (i - 1, j), (i, j - 1), (i, j + 1), (i + 1, j) };
            neighbours.RemoveAll(x => x.Item1 < 0 || x.Item1 >= iMax);
            neighbours.RemoveAll(x => x.Item2 < 0 || x.Item2 >= jMax);

            return neighbours;
        }

    }
}
