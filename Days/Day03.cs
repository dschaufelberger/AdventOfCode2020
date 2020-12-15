using System;
using System.Linq;

internal class Day03 : IAdventDay
{
    public int Day => 3;

    public void Solve(string[] input)
    {
        PartOne(input);
        PartTwo(input);
    }

    private static void PartOne(string[] input)
    {
        var slope = (3, 1);
        int numberOfTrees = CountTreesOnSlope(input, slope);

        Console.WriteLine($"Passed {numberOfTrees} trees");
    }

    private static void PartTwo(string[] input)
    {
        var slopes = new []
        {
            (1, 1),
            (3, 1),
            (5, 1),
            (7, 1),
            (1, 2),
        };

        var result = slopes
            .Select(slope => CountTreesOnSlope(input, slope))
            .Aggregate(1, (acc, current) => acc * current);
        
        Console.WriteLine($"Multiple of trees is {result}");
    }

    private static int CountTreesOnSlope(string[] input, (int, int) slope)
    {
        var (right, down) = slope;

        return Enumerable
                    .Range(0, input.Length)
                    .Select(number => (x: (number * right) % input[number].Length, y: number * down))
                    .Where(slope => slope.y < input.Length)
                    .Count(slope => input[slope.y][slope.x] == '#');
    }
}