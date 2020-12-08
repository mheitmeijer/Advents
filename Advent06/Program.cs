using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Advent06
{
    class Program
    {
        private const string _alfabet = "abcdefghijklmnopqrstuvwxyz";
        static void Main(string[] args)
        {
           // Part1();
            Part2();
        }

        private static void Part1()
        {
            var result = string.Empty;
            var input = GetInput();
            var sum = 0;
            foreach (var line in input)
            {
                sum += line.Length;
            }
            Console.WriteLine(sum);
            Console.ReadLine();
        }

        private static void Part2()
        {
            var result = string.Empty;
            var count = Count();
            //Console.WriteLine(count);
            Console.ReadLine();
        }

        private static int Count()
        {
            var result = new List<string>();
            StreamReader file = new StreamReader(@"..\..\..\Input.txt");
            string line;
            var groupSize = 0;
            var sum = 0;

            var block = new StringBuilder();
            while ((line = file.ReadLine()) != null)
            {
                if (line.Equals(string.Empty))
                {
                    if (groupSize == 1)
                    {
                        sum += block.ToString().Length;
                    }
                    else
                    {

                        var groupInput = new string(block.ToString().ToArray());
                        foreach(var character in _alfabet)
                        {
                            if(groupInput.Count(x => x.Equals(character)) == groupSize)
                            {
                                sum += 1;
                            }    
                        }
                    }
                    groupSize = 0;
                    block.Clear();
                    continue;
                }
                groupSize++;
                block.Append(line);
            }

            if (groupSize == 1)
            {
                sum += block.ToString().Length;
            }
            else
            {

                var groupInput = new string(block.ToString().ToArray());
                foreach (var character in _alfabet)
                {
                    if (groupInput.Count(x => x.Equals(character)) == groupSize)
                    {
                        sum += 1;
                    }
                }
            }
            return sum;
        }

        private static List<string> GetInput()
        {
            var result = new List<string>();
            StreamReader file = new StreamReader(@"..\..\..\Input.txt");
            string line;

            var block = new StringBuilder();
            while ((line = file.ReadLine()) != null)
            {
                if (line.Equals(string.Empty))
                {
                    result.Add(new string(block.ToString().Distinct().ToArray()));
                    block.Clear();
                    continue;
                }
                block.Append(line);
            }
            result.Add(new string(block.ToString().Distinct().ToArray()));
            return result;
        }
    }
}
