using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    public class Day1
    {


        public static int CalculateTotalDistance(List<string> input)
        {
            List<int> list1;
            List<int> list2;

            (list1, list2) = ParseIntoLists(input);

            list1.Sort();
            list2.Sort();

            var totalDistance = 0;
            for (int i = 0; i < list1.Count; i++)
            {
                var distance = 0;
                if (list1[i] > list2[i])
                {
                    distance = list1[i] - list2[i];
                } else if (list1[i] < list2[i])
                {
                    distance = list2[i] - list1[i];
                }

                totalDistance += distance;
            }

            return totalDistance;
        }

        public static int CalculateTotalSimilarityScore(List<string> input)
        {
            // not doing the most efficient solution to start
            List<int> list1;
            List<int> list2;

            (list1, list2) = ParseIntoLists(input);

            var totalSimilarity = 0;
            for (int i = 0; i< list1.Count; i++)
            {
                var list2Occurrences = list2.Where(x => x.Equals(list1[i])).Count();
                var similarity = list1[i] * list2Occurrences;
                totalSimilarity += similarity;
            }

            return totalSimilarity;
        }


        private static (List<int>, List<int>) ParseIntoLists(List<string> input)
        {
            var list1 = new List<int>();
            var list2 = new List<int>();

            foreach (var item in input)
            {
                string[] nums = item.Split("   ");
                list1.Add(int.Parse(nums[0]));
                list2.Add(int.Parse(nums[1]));
            }

            return (list1, list2);
        }
    }
}
