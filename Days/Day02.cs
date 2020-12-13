using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

internal class Day02 : IAdventDay
{
    public int Day => 2;

    public void Solve(string[] input)
    {
        var passwordPolicies = input.Select(line => SplitPasswordPolicyLine(line));

        PartOne(passwordPolicies);
        PartTwo(passwordPolicies);
    }

    private static void PartOne(IEnumerable<(Policy, char, string)> passwordPolicies)
    {
        var numberOfValidPasswords = passwordPolicies
        .Select(passwordPolicy => (passwordPolicy.Item1, CountOccurrences(passwordPolicy.Item3, passwordPolicy.Item2)))
        .Count(passwordPolicy => OccursInPolicyRange(passwordPolicy.Item1, passwordPolicy.Item2));

        Console.WriteLine($"{numberOfValidPasswords} passwords are valid");
    }

    private static void PartTwo(IEnumerable<(Policy, char, string)> passwordPolicies)
    {
        var numberOfValidPasswords = passwordPolicies
        .Count(passwordPolicy => LetterOccursInEitherPosition(passwordPolicy.Item1, passwordPolicy.Item2, passwordPolicy.Item3));

        Console.WriteLine($"{numberOfValidPasswords} passwords are valid");
    }

    private static bool LetterOccursInEitherPosition(Policy policy, char letter, string password)
    {
        return password[policy.Min - 1] == letter ^ password[policy.Max - 1] == letter;
    }

    private static bool OccursInPolicyRange(Policy policy, int occurrence)
    {
        return occurrence >= policy.Min && occurrence <= policy.Max;
    }

    private (Policy, char, string) SplitPasswordPolicyLine(string input)
    {
        var splitted = input.Split(" ");
        var policy = new Policy(splitted[0]);
        var letter = splitted[1][0];
        var password = splitted[2];

        return (policy, letter, password);
    }

    private static int CountOccurrences(string password, char letter)
    {
        return password.Count(character => character == letter);
    }

    private class Policy
    {
        public int Min { get; set; }

        public int Max { get; set; }

        public Policy(string policy)
        {
            var splitted = policy.Split("-");
            Min = int.Parse(splitted[0]);
            Max = int.Parse(splitted[1]);
        }
    }
}