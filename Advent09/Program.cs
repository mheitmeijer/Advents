using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Advent09
{
    class Program
    {
        static void Main(string[] args)
        {
            Part1();
            Part2();
        }


        private static void Part1()
        {
            var input = GetInput();
            int preambleLength = 25;
            int preambleStartIndex = 0;
            int preambleEndIndex = preambleLength - 1;
            int currentIndex = preambleLength;
            bool valid = true;
            while (valid)
            {
                var current = input[currentIndex];
                if (!IsValid(input, preambleStartIndex, preambleEndIndex, current))
                {
                    Console.WriteLine("Error!");
                    Console.WriteLine(current);
                    Console.WriteLine(currentIndex);
                    valid = false;
                }
                currentIndex++;
                preambleStartIndex++;
                preambleEndIndex++;
            }
            Console.ReadLine();
        }

        private static bool IsValid(long[] preamble, int preambleStartIndex, int preambleEndIndex, long value)
        {
            int currentIndex = preambleStartIndex;
            while (currentIndex <= preambleEndIndex)
            {
                long firstNumber = preamble[currentIndex];
                int secondIndex = currentIndex + 1;

                while (secondIndex <= preambleEndIndex)
                {
                    if(firstNumber + preamble[secondIndex] == value)
                    {
                        return true;
                    }
                    secondIndex++;
                }
                currentIndex++;
            }
            return false;
        }

        private static void Part2()
        {
            var input = GetInput();
            var value =  69316178L;
            var firstIndex = 0;
            while (firstIndex < input.Length)
            {
                int secondIndex = firstIndex + 1;
                while (secondIndex < input.Length)
                {
                    if(GetSum(input, firstIndex, secondIndex) == value)
                    {
                        Console.WriteLine("Numbers found");
                        var range = input.Skip(firstIndex).Take(secondIndex - firstIndex + 1);
                        var smallest = range.Min();
                        var largest = range.Max();
                        var sum = smallest + largest;
                        Console.WriteLine("Sum");
                        Console.WriteLine(sum);
                        Console.ReadLine();
                    }
                    secondIndex++;
                }
                firstIndex++;
            }
            Console.ReadLine();
        }

        private static long GetSum(long[] input, int startIndex, int endIndex)
        {
            var sum = 0L;// input[startIndex];
            var sumIndex = startIndex;
            while (sumIndex <= endIndex)
            {
                sum += input[sumIndex];
                sumIndex++;
            }
            return sum;
        }
        private static long[] GetInput()
        {
            var result = new List<long>();
            StreamReader file = new StreamReader(@"..\..\..\Input.txt");
            string line;
            while ((line = file.ReadLine()) != null)
            {
                result.Add(Convert.ToInt64(line));
            }
            return result.ToArray();
        }
    }
}
