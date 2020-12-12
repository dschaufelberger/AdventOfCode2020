using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

using AdventOfCode2020.Days;

namespace AdventOfCode2020
{
    class AdventOfCode
    {
        static async Task Main(string[] args)
        {
            new Day01().Solve(await LoadInputForDay(1));
        }

        private static IEnumerable<IAdventDay> GetAdventDays()
        {
            return new IAdventDay[]
            {
                new Day01()
            };
        }

        private static Task<string[]> LoadInputForDay(int day)
        {
            return File.ReadAllLinesAsync($"./Input/day-{day:D2}.txt");
        }
    }
}
