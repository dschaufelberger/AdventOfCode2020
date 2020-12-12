using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AdventOfCode2020.Days
{
    internal class Day01 : IAdventDay
    {
        public void Solve(string[] input)
        {
            var inputNumbers = input.Select(text => int.Parse(text));
            var numbers = new HashSet<int>(inputNumbers);
            var twentytwenty = 2020;

            PartOne(numbers, twentytwenty);
            PartTwo(numbers, twentytwenty);
        }

        private static void PartTwo(HashSet<int> numbers, int result)
        {
            foreach (var first in numbers)
            {
                var intermediate = result - first;

                try
                {
                    var (second, third) = FindEntriesThatSumUpTo(numbers, intermediate);
                    Console.WriteLine($"{first} + {second} + {third} = {first + second + third} => {first} * {second} * {third} = {first * second * third}");
                    return;
                }
                catch (ArgumentOutOfRangeException)
                {
                    continue;
                }
            }
        }

        private static void PartOne(ISet<int> numbers, int result)
        {
            var (first, second) = FindEntriesThatSumUpTo(numbers, result);
            Console.WriteLine($"{first} + {second} = {first + second} => {first} * {second} = {first * second}");
        }

        private static (int, int) FindEntriesThatSumUpTo(ISet<int> numbers, int result)
        {
            foreach (var number in numbers)
            {
                var diff = result - number;

                if (numbers.Contains(diff))
                {
                    return (number, diff);
                }
            }

            throw new ArgumentOutOfRangeException(nameof(numbers));
        }
    }
}
