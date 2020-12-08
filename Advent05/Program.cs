using System;
using System.Collections.Generic;
using System.IO;

namespace Advent05
{
    class Program
    {
        static void Main(string[] args)
        {
            Part2();
            Part1();

        }

        private static void Part2()
        {
            var passes = GetInput();

            long maxId = 0L;
            long id;
            var ids = new List<long>();
            foreach (var pass in passes)
            {
                var row = GetSpotBinary(pass.Substring(0, 7), 127L, 'F');
                var seat = GetSpotBinary(pass.Substring(7), 7L, 'L');
                id = row * 8 + seat;
                Console.WriteLine(id);
                ids.Add(id);
                maxId = Math.Max(maxId, id);

            }

            foreach(var currentId in ids)
            {
                if(ids.Contains(currentId + 2) && !ids.Contains(currentId + 1))
                {
                    Console.WriteLine("Yep");
                    Console.WriteLine(currentId + 1);
                }
            }
            Console.WriteLine(maxId);
            Console.ReadLine();
        }
        private static void Part1()
        {
            var passes = GetInput();

            long maxId = 0L;
            long id;
            foreach(var pass in passes)
            {
                var row = GetSpot(pass.Substring(0, 7), 127L, 'F');
                var seat = GetSpot(pass.Substring(7), 7L, 'L');
                id = row * 8 + seat;
                Console.WriteLine(id);
                maxId = Math.Max(maxId, id);
            }

            Console.WriteLine(maxId);
            Console.ReadLine();
        }

        private static int GetSpotBinary(string pass, long upperIndex, char firstCharacter)
        {
            var binaryString = string.Empty;
            long lowerIndex = 0L;
            long middle = 0L;
            foreach (var character in pass)
            {

                var rangeLength = upperIndex - lowerIndex + 1;
                middle = rangeLength / 2;
                if (character == firstCharacter)
                {
                    binaryString += "0";
                }
                else
                {
                    binaryString += "1";
                }
            }
            return Convert.ToInt32(binaryString, 2);
        }

        private static long GetSpot(string pass, long upperIndex, char firstCharacter)
        {
            var rowString = string.Empty;
            long lowerIndex = 0L;
            long middle = 0L;
            foreach (var character in pass)
            {

                var rangeLength = upperIndex - lowerIndex + 1;
                middle = rangeLength / 2;
                if (character == firstCharacter)
                {
                    rowString += "0";
                }
                else
                {
                    lowerIndex += middle;
                }
            }
            return (long)lowerIndex;
        }

        private static List<string> GetInput()
        {
            var result = new List<string>();
            StreamReader file = new StreamReader(@"D:\advent\Advents\Advent05\Input.txt");
            string line;
            while ((line = file.ReadLine()) != null)
            {
                result.Add(line);
            }
            return result;
        }
    }
}
