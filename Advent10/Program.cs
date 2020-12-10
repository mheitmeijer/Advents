using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Advent10
{
    class Program
    {
        private static Dictionary<int, long> _nodes = new Dictionary<int, long>();

        static void Main(string[] args)
        {
            //Part1();
            Part2();
        }

        private static void Part1()
        {
            var adapters = GetInput();
            var currentIndex = 0;
            var currentValue = 0;
            var countOne = 0;
            var countThree = 1;
            while (currentIndex < adapters.Length)
            {
                var currentAdapter = adapters[currentIndex];
                if (currentAdapter - currentValue == 1)
                {
                    countOne++;
                }
                else if (currentAdapter - currentValue == 3)
                {
                    countThree++;
                }
                currentValue = currentAdapter;
                currentIndex++;
            }
            Console.WriteLine(countOne * countThree);
            Console.ReadLine();
        }

        private static void Part2()
        {
            var adapters = GetInput();

            var currentIndex = 0;
            var currentValue = 0;

            var splits = GetPossible(adapters, currentValue);
            Console.WriteLine("");
            Console.ReadLine();
        }

        private static long GetPossible(int[] adapters, int currentValue)
        {
            var possibleAdapters = adapters.Where(x => x - 3 == currentValue || x - 2 == currentValue || x - 1 == currentValue).ToList();
            var possibilities = possibleAdapters.Count();


            if (_nodes.ContainsKey(currentValue))
            {
                return _nodes[currentValue];
            }
            
            if (possibilities == 0)
            {
                _nodes.Add(currentValue, 1);
                return 1;
            }

            var result = 0L;
            var max = 0;
            foreach (var adapter in possibleAdapters)
            {
                result += GetPossible(adapters, adapter);
            }
            _nodes.Add(currentValue, result);
            return result;
        }

        private static int[] GetInput()
        {
            var result = new List<int>();
            StreamReader file = new StreamReader(@"..\..\..\Input.txt");
            string line;
            while ((line = file.ReadLine()) != null)
            {
                result.Add(Convert.ToInt32(line));
            }

            return result.OrderBy(x => x).ToArray();
        }
    }
}
