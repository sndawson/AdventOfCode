using NUnit.Framework;
using System.Collections.Generic;
using AdventOfCode;
using AdventOfCode._2024;

namespace AdventOfCodeTests
{
    public class Day01Tests
    {
        readonly IDay day = new Day01();

        [SetUp]
        public void Setup()
        {
        }

        private static string sampleInputString = @"
3   4
4   3
2   5
1   3
3   9
3   3
            ";
        

        private static List<string> sampleInput = new List<string>
        {
            "3   4",
            "4   3",
            "2   5",
            "1   3",
            "3   9",
            "3   3"
        };

        private static readonly object[] inputTestCasesPart1 =
        {
            new object[]
            {
                sampleInput,
                11
            },
        };

        private static readonly object[] inputTestCasesPart2 =
        {
            new object[]
            {
                sampleInput,
                31
            },
        };

        [TestCaseSource("inputTestCasesPart1")]
        public void CalculateTotalDistance(List<string> input, int solution)
        {
            Assert.AreEqual(solution, day.Part1(input));
        }

        [TestCaseSource("inputTestCasesPart2")]
        public void CalculateTotalSimilarityScore(List<string> input, int solution)
        {
            Assert.AreEqual(solution, day.Part2(input));
        }
    }
}