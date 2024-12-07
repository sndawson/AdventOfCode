﻿using System.Text.RegularExpressions;

namespace AdventOfCode._2024
{
    public class Day5 : IDay
    {
        public int Part1(List<string> input)
        {
            var rules = new Dictionary<string, List<string>>();
            var result = 0;
            var processRules = true;
            foreach (var line in input)
            {
                if (line == "")
                {
                    // done processing rules, updates come next
                    processRules = false;
                    continue;
                }
                if (processRules)
                {
                    var pageNumbers = line.Split('|');
                    if (rules.ContainsKey(pageNumbers[0]))
                    {
                        rules[pageNumbers[0]].Add(pageNumbers[1]);
                    }
                    else
                    {
                        rules.Add(pageNumbers[0], new List<string> { pageNumbers[1] });
                    }
                }
                else
                {
                    var update = line.Split(',').ToList();
                    if (IsValidUpadate(update, rules))
                    {
                        result += int.Parse(GetMiddleValue(update));
                    }
                }
            }

            return result;
        }

        public int Part2(List<string> input)
        {
            var rules = new Dictionary<string, List<string>>();
            var result = 0;
            var processRules = true;
            foreach (var line in input)
            {
                if (line == "")
                {
                    // done processing rules, updates come next
                    processRules = false;
                    continue;
                }
                if (processRules)
                {
                    var pageNumbers = line.Split('|');
                    if (rules.ContainsKey(pageNumbers[0]))
                    {
                        rules[pageNumbers[0]].Add(pageNumbers[1]);
                    }
                    else
                    {
                        rules.Add(pageNumbers[0], new List<string> { pageNumbers[1] });
                    }
                }
                else
                {
                    var update = line.Split(',').ToList();
                    if (!IsValidUpadate(update, rules))
                    {
                        var fixedOrder = FixOrdering(update, rules);
                        result += int.Parse(GetMiddleValue(fixedOrder));
                    }
                }
            }

            return result;
        }

        private static bool IsValidUpadate(List<string> update, Dictionary<string, List<string>> rules)
        {
            for (int i = 1; i < update.Count; i++)
            {
                if (rules.ContainsKey(update[i]))
                {
                    var sublist = update.GetRange(0, i);
                    var ruleDetails = rules[update[i]];
                    foreach (var detail in ruleDetails)
                    {
                        if (sublist.Contains(detail))
                        {
                            return false;
                        }
                    }
                }
            }
            return true;
        }

        private static string GetMiddleValue(List<string> inputList)
        {
            var middleIndex = (int)Math.Ceiling((double)inputList.Count / 2) - 1;
            return inputList[middleIndex];
        }

        private static List<string> FixOrdering(List<string> inputList, Dictionary<string, List<string>> rules)
        {
            // TODO
            return inputList;
        }
    }
}
