using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

using AdventOfCode2020.Days;

namespace AdventOfCode2020
{
    class AdventOfCode
    {
        static async Task Main()
        {
            foreach (var adventDay in GetAdventDays())
            {
                Console.WriteLine($"{(adventDay.Day > 1 ? "\n" : string.Empty)}Solution for {adventDay.Day:D2}.12.2020");
                var input = await LoadInputForDayAsync(adventDay.Day);
                adventDay.Solve(input);
            }
        }

        private static IEnumerable<IAdventDay> GetAdventDays()
        {
            var dayClassFiles = new DirectoryInfo("./Days").GetFiles("Day*.cs");

            var adventDays = dayClassFiles
                .Select(classFile => (IAdventDay)Activator.CreateInstance(null, $"AdventOfCode2020.Days.{Path.GetFileNameWithoutExtension(classFile.Name)}").Unwrap());

            return adventDays.ToArray();
        }

        private static Task<string[]> LoadInputForDayAsync(int day)
        {
            return File.ReadAllLinesAsync($"./Input/day-{day:D2}.txt");
        }
    }
}
