using NUnit.Framework;
using System.Collections.Generic;
using AdventOfCode._2024;
using AdventOfCode;

namespace AdventOfCodeTests
{
    public class Day3Tests
    {
        readonly IDay day = new Day3();

        [SetUp]
        public void Setup()
        {
        }

        private static List<string> sampleInput1 = new List<string>
        {
            "xmul(2,4)%&mul[3,7]!@^do_not_mul(5,5)+mul(32,64]then(mul(11,8)mul(8,5))",
        };

        private static List<string> sampleInput2 = new List<string>
        {
            "xmul(2,4)&mul[3,7]!^don't()_mul(5,5)+mul(32,64](mul(11,8)undo()?mul(8,5))"
        };

        private static readonly object[] inputTestCasesPart1 =
        {
            new object[]
            {
                sampleInput1,
                161
            },
        };

        private static readonly object[] inputTestCasesPart2 =
        {
            new object[]
            {
                sampleInput2,
                48
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