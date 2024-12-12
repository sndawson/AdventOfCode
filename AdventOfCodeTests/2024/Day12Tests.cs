using NUnit.Framework;
using System.Collections.Generic;
using AdventOfCode._2024;
using AdventOfCode;

namespace AdventOfCodeTests
{
    public class Day12Tests
    {
        readonly IDay day = new Day12();

        [SetUp]
        public void Setup()
        {
        }

        private static List<string> sampleInput1 = new List<string>
        {
            "AAAA",
            "BBCD",
            "BBCC",
            "EEEC",
        };

        private static List<string> sampleInput2 = new List<string>
        {
            "OOOOO",
            "OXOXO",
            "OOOOO",
            "OXOXO",
            "OOOOO"
        };

        private static List<string> sampleInput3 = new List<string>
        {
            "RRRRIICCFF",
            "RRRRIICCCF",
            "VVRRRCCFFF",
            "VVRCCCJFFF",
            "VVVVCJJCFE",
            "VVIVCCJJEE",
            "VVIIICJJEE",
            "MIIIIIJJEE",
            "MIIISIJEEE",
            "MMMISSJEEE",
        };

        private static List<string> sampleInput4 = new List<string>
        {
            "EEEEE",
            "EXXXX",
            "EEEEE",
            "EXXXX",
            "EEEEE",
        };

        private static List<string> sampleInput5 = new List<string>
        {
            "AAAAAA",
            "AAABBA",
            "AAABBA",
            "ABBAAA",
            "ABBAAA",
            "AAAAAA"
        };

        private static readonly object[] inputTestCasesPart1 =
        {
            new object[]
            {
                sampleInput1,
                140
            },
            new object[]
            {
                sampleInput2,
                772
            },
            new object[]
            {
                sampleInput3,
                1930
            },
        };

        private static readonly object[] inputTestCasesPart2 =
        {
            new object[]
            {
                sampleInput1,
                80
            },
            new object[]
            {
                sampleInput2,
                436
            },
            new object[]
            {
                sampleInput4,
                236
            },
            new object[]
            {
                sampleInput5,
                368
            },
            new object[]
            {
                sampleInput3,
                1206
            },
        };

        [TestCaseSource("inputTestCasesPart1")]
        public void TestPart1(List<string> input, int solution)
        {
            Assert.AreEqual(solution, day.Part1(input));
        }

        [TestCaseSource("inputTestCasesPart2")]
        public void TestPart2(List<string> input, int solution)
        {
            Assert.AreEqual(solution, day.Part2(input));
        }
    }
}