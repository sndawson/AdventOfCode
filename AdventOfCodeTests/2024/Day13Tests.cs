using NUnit.Framework;
using System.Collections.Generic;
using AdventOfCode._2024;
using AdventOfCode;

namespace AdventOfCodeTests
{
    public class Day13Tests
    {
        readonly IDay day = new Day13();

        [SetUp]
        public void Setup()
        {
        }

        private static List<string> sampleInput1 = new List<string>
        {
            "Button A: X+94, Y+34",
            "Button B: X+22, Y+67",
            "Prize: X=8400, Y=5400",
            "",
            "Button A: X+26, Y+66",
            "Button B: X+67, Y+21",
            "Prize: X=12748, Y=12176",
            "",
            "Button A: X+17, Y+86",
            "Button B: X+84, Y+37",
            "Prize: X=7870, Y=6450",
            "",
            "Button A: X+69, Y+23",
            "Button B: X+27, Y+71",
            "Prize: X=18641, Y=10279",
        };

        private static List<string> sampleInput2 = new List<string>
        {
            ""
        };

        private static readonly object[] inputTestCasesPart1 =
        {
            new object[]
            {
                sampleInput1,
                480
            },
        };

        private static readonly object[] inputTestCasesPart2 =
        {
            new object[]
            {
                sampleInput2,
                0
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