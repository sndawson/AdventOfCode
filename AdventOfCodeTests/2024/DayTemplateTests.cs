using NUnit.Framework;
using System.Collections.Generic;
using AdventOfCode._2024;
using AdventOfCode;

namespace AdventOfCodeTests
{
    public class DayTemplateTests
    {
        readonly IDay day = new DayTemplate();

        [SetUp]
        public void Setup()
        {
        }

        private static List<string> sampleInput1 = new List<string>
        {
            "",
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
                0
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