using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode2020.Days
{
    internal class Day04 : IAdventDay
    {
        public int Day => 4;

        public void Solve(string[] input)
        {
            var initial = new List<List<string>> { new List<string>() };
            var passports = input
                .Aggregate(initial, AccumulateInput)
                .Select(passportList => string.Join(' ', passportList))
                .Select(passportLine => ParseToPassport(passportLine));

            PartOne(passports);
            PartTwo(passports);
        }

        private static void PartOne(IEnumerable<Passport> passports)
        {
            var numberOfValidPassports = passports.Count(PassportContainsRequiredFields);

            Console.WriteLine($"{numberOfValidPassports} are valid");
        }

        private static bool PassportContainsRequiredFields(Passport passport)
        {
            return Passport.RequiredFields.All(field => passport.ContainsKey(field));
        }

        private static Passport ParseToPassport(string passportLine)
        {
            var passport = new Passport();

            foreach (var trait in passportLine.Split(' '))
            {
                var (field, value) = ParseField(trait);
                passport[field] = value;
            }

            return passport;
        }

        private static (string, string) ParseField(string trait)
        {
            var splitByColon = trait.Split(':');

            return (splitByColon[0], splitByColon[1]);
        }

        private static List<List<string>> AccumulateInput(List<List<string>> passportList, string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                passportList.Add(new List<string>());
                return passportList;
            }

            var passportLines = passportList.Last();
            passportLines.Add(input);

            return passportList;
        }

        private static void PartTwo(IEnumerable<Passport> passports)
        {
            var numberOfValidPassports = passports.Count(passport =>
                PassportContainsRequiredFields(passport) && FieldsAreValid(passport));

            Console.WriteLine($"{numberOfValidPassports} are valid");
        }

        private static bool FieldsAreValid(Passport passport)
        {
            return passport.All(fieldAndValue => FieldIsValid(fieldAndValue.Key, fieldAndValue.Value));
        }

        private static bool FieldIsValid(string key, string value) =>
            key switch
            {
                "byr" => IsValidYear(value, 1920, 2002),
                "iyr" => IsValidYear(value, 2010, 2020),
                "eyr" => IsValidYear(value, 2020, 2030),
                "hgt" => IsValidHeight(value),
                "hcl" => IsValidHairColor(value),
                "ecl" => IsValidEyeColor(value),
                "pid" => IsValidPassportId(value),
                "cid" => true,
                _ => throw new NotImplementedException(),
            };

        private static bool IsValidPassportId(string value)
        {
            return value.Length == 9 && value.All(character => char.IsDigit(character));
        }

        private static bool IsValidEyeColor(string value)
        {
            return new[] { "amb", "blu", "brn", "gry", "grn", "hzl", "oth"}.Any(color => color == value);
        }

        private static bool IsValidHairColor(string value)
        {
            return value.Length == 7 && Regex.IsMatch(value, "#[0-9a-f]{6}");
        }

        private static bool IsValidHeight(string value)
        {
            if (value.EndsWith("in"))
            {
                var height = int.Parse(value[0..^2]);

                return height >= 59 && height <= 76;
            }

            if (value.EndsWith("cm"))
            {
                var height = int.Parse(value[0..^2]);

                return height >= 150 && height <= 193;
            }

            return false;
        }

        private static bool IsValidYear(string value, int min, int max)
        {
            var birthYear = int.Parse(value);

            return value.Length == 4 && birthYear >= min && birthYear <= max;
        }

        private class Passport : Dictionary<string, string>
        {
            public static string[] RequiredFields =
            {
            "byr",
            "iyr",
            "eyr",
            "hgt",
            "hcl",
            "ecl",
            "pid",
        };
        }
    }
}