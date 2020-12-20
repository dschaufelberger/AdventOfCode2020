using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020.Days
{
    public class Day05 : IAdventDay
    {
        public int Day => 5;

        public void Solve(string[] input)
        {
            PartOne(input);
            PartTwo(input);
        }

        private void PartTwo(string[] input)
        {
            var missingSeatIdsInTheFront = Enumerable.Range(0, 8).Select(column => 0 * 8 + column);            
            var missingSeatIdsInTheBack = Enumerable.Range(0, 8).Select(column => 127 * 8 + column);
            var orderedSeatIds = input
                .Select(GetSeatId)
                .OrderBy(seatId => seatId)
                .ToArray();

            var missingSeatIds = GetMissingSeats(orderedSeatIds);
            var mySeatId = missingSeatIds
                .Single(missingSeatId => !missingSeatIdsInTheBack.Contains(missingSeatId) && !missingSeatIdsInTheFront.Contains(missingSeatId));
        }

        private IEnumerable<int> GetMissingSeats(int[] orderedSeatIds)
        {
            for (int i = 0; i < orderedSeatIds.Length - 1 ; i++)
            {
                if (orderedSeatIds[i + 1] - orderedSeatIds[i] > 1)
                {
                    yield return orderedSeatIds[i] + 1;
                }
            }
        }

        private static void PartOne(string[] input)
        {
            var highestSeatId = input.Max(GetSeatId);

            Console.WriteLine($"Highest seat id is #{highestSeatId}");
        }

        private static int GetSeatId(string seatSpecification)
        {
            var row = seatSpecification.Take(7).Aggregate(new Bounds(0, 127), (bounds, letter) => SplitSeatArea(letter, bounds));
            var column = seatSpecification.TakeLast(3).Aggregate(new Bounds(0, 7), (bounds, letter) => SplitSeatArea(letter, bounds));

            return row.Lower * 8 + column.Lower;
        }

        private static Bounds SplitSeatArea(char letter, Bounds bounds)
        {
            if (letter == 'F' || letter == 'L')
            {
                return new Bounds()
                {
                    Lower = bounds.Lower,
                    Upper = bounds.Upper - (bounds.Upper - bounds.Lower) / 2 - 1,
                };
            }

            if (letter == 'B' || letter == 'R')
            {
                return new Bounds()
                {
                    Lower = bounds.Lower + (bounds.Upper - bounds.Lower) / 2 + 1,
                    Upper = bounds.Upper,
                };
            }

            throw new ArgumentOutOfRangeException(nameof(letter));
        }

        private class Bounds
        {
            public int Upper { get; set; }

            public int Lower { get; set; }

            public Bounds()
            {
            }

            public Bounds(int lower, int upper)
            {
                Lower = lower;
                Upper = upper;
            }
        }
    }
}