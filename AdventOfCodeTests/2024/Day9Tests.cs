using NUnit.Framework;
using System.Collections.Generic;
using AdventOfCode._2024;
using AdventOfCode;

namespace AdventOfCodeTests
{
    public class Day9Tests
    {
        readonly IDay day = new Day9();

        [SetUp]
        public void Setup()
        {
        }

        private static List<string> sampleInput1 = new List<string>
        {
            "2333133121414131402",
        };

        private static List<string> sampleInput2 = new List<string>
        {
            "12345"
        };

        private static readonly object[] inputTestCasesPart1 =
        {
            new object[]
            {
                sampleInput1,
                1928
            },
            new object[]
            {
                sampleInput2,
                60
            },
        };

        private static readonly object[] inputTestCasesPart2 =
        {
            new object[]
            {
                sampleInput1,
                2858
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