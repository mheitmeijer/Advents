using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Advent04
{
    class Program
    {
        static void Main(string[] args)
        {
            //Part1();
            Part2();
        }

        private static void Part1()
        {
            var passports = GetPassports();
            var correct = CountValidPassports(passports);
            Console.WriteLine(correct);
            Console.ReadLine();
        }

        private static int CountValidPassports(List<string[]> passports)
        {
            var correctFields = new[] { "byr", "iyr", "eyr", "hgt", "hcl", "ecl", "pid"};
            var result = 0;
            foreach(var passport in passports)
            {
                int countCorrect = 0;
                foreach(var field in correctFields)
                {
                    if (passport.Contains(field))
                    {
                        countCorrect++;
                    }

                }
                if(countCorrect == 7)
                {
                    result++;
                }
            }

            return result;
        }

        private static void Part2()
        {
            var passports = GetPassports2();
            var valid = 0;
            foreach(var passport in passports)
            {
                if(passport.Validate())
                {
                    valid++;
                }
            }
            Console.WriteLine(valid);
            Console.ReadLine();
        }

        private static long Traverse(List<string> map, int columns, int rows)
        {
            var countTrees = 0;
            var rowIndex = 0;
            var columnIndex = 0;
            while (rowIndex <= map.Count - (1 + rows))
            {
                columnIndex += columns;
                rowIndex += rows;
                var row = map[rowIndex];
                if (row[columnIndex].Equals('#'))
                {
                    countTrees++;
                }
            }
            return countTrees;
        }

        private static List<string[]> GetPassports()
        {
            var passports = new List<string[]>();
            StreamReader file = new StreamReader(@"D:\advent\Advents\Advent04\Input.txt");
            string line;
            var currentKeys = new List<string>();
            while ((line = file.ReadLine()) != null)
            {
                if(line.Equals(string.Empty))
                {
                    passports.Add(currentKeys.ToArray());
                    currentKeys.Clear();
                    continue;
                }

                var sets = line.Split(' ');
                foreach(var set in sets)
                {
                    var splitted = set.Split(":");
                    currentKeys.Add(splitted[0]);
                }
            }
            passports.Add(currentKeys.ToArray());
            return passports;
        }

        private static List<Passport> GetPassports2()
        {
            var passports = new List<Passport>();
            StreamReader file = new StreamReader(@"D:\advent\Advents\Advent04\Input.txt");
            string line;
            var currentPassport = new Passport();
            while ((line = file.ReadLine()) != null)
            {
                if (line.Equals(string.Empty))
                {
                    passports.Add(currentPassport);
                    currentPassport = new Passport();
                    continue;
                }

                var sets = line.Split(' ');
                foreach (var set in sets)
                {
                    var splitted = set.Split(":");
                    currentPassport.Fields.Add(splitted[0], splitted[1]);
                }
            }
            passports.Add(currentPassport);
            return passports;
        }
    }
}
