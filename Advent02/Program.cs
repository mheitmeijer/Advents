using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Advent02
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
            var list = GetData();
            var correct = list.Count(x => PasswordCorrect(x));
            Console.WriteLine(correct);
            Console.ReadLine();
        }

        private static bool PasswordCorrect((int, int, char, string) tuple)
        {
            var characterMatches = tuple.Item4.Count(x => x.Equals(tuple.Item3));
            return characterMatches >= tuple.Item1 && characterMatches <= tuple.Item2;
        }

        private static void Part2()
        {
            var list = GetData();
            var correct = list.Count(x => PasswordCorrectWithCorrectMethod(x));
            Console.WriteLine(correct);
            Console.ReadLine();
        }

        private static bool PasswordCorrectWithCorrectMethod((int, int, char, string) tuple)
        {
            var password = tuple.Item4;
            var firstCharacterCorrect = password[tuple.Item1 - 1] == tuple.Item3;
            var secondCharacterCorrect = password[tuple.Item2 - 1] == tuple.Item3;
            return firstCharacterCorrect ^ secondCharacterCorrect;
        }

        private static List<(int, int, char, string)> GetData()
        {
            var result = new List<(int, int, char, string)>();
            StreamReader file = new StreamReader(@"D:\advent\Advents\Advent02\Input.txt");
            string line;
            while ((line = file.ReadLine()) != null)
            {
                var splitted = line.Split(' ');
                var range = splitted[0];
                var rangeLower = Convert.ToInt32(range.Split('-')[0]);
                var rangeUpper = Convert.ToInt32(range.Split('-')[1]);
                var character = splitted[1][0];
                var password = splitted[2];
                var tuple = (rangeLower, rangeUpper, character, password);
                result.Add(tuple);
            }
            return result;
        }
    }
}
