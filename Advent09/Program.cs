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
        
        //Console.WriteLine(result);
        Console.ReadLine();
    }
    private static void Part2()
    {
        //Console.WriteLine(result);
        Console.ReadLine();
    }
        private static List<(string, int, int)> GetInput()
        {
            var result = new List<(string, int, int)>();
            StreamReader file = new StreamReader(@"..\..\..\Input.txt");
            string line;
            var index = 0;
            while ((line = file.ReadLine()) != null)
            {
                var operation = line.Substring(0, 3);
                var offset = Convert.ToInt32(line.Substring(4));
                result.Add((operation, offset, index));
                index++;
            }
            return result;
        }
    }
}
