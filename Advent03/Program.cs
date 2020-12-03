using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Advent03
{
    class Program
    {
        static void Main(string[] args)
        {
            Part2();
        }

        private static void Part1()
        {
            var map = GetMap(3);
            var countTrees = 0;
            var rowIndex = 0;
            var columnIndex = 0;
            while(rowIndex <= map.Count -2)
            {
                columnIndex += 3;
                rowIndex += 1;
                var row = map[rowIndex];
                if(row[columnIndex].Equals('#'))
                {
                    countTrees++;
                }
            }
            Console.WriteLine(countTrees);
            Console.ReadLine();
        }


        private static void Part2()
        {
            var map = GetMap(7);
            var result1 = Traverse(map, 1, 1);
            var result2 = Traverse(map, 3, 1);
            var result3 = Traverse(map, 5, 1);
            var result4 = Traverse(map, 7, 1);
            var result5 = Traverse(map, 1, 2);
            var result = result1 * result2 * result3 * result4 * result5;
            Console.WriteLine(result);
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

        private static List<string> GetMap(int columnTraverses)
        {
            const int nrOfLines = 323;
            const int nrOfColumnsInLine = 32;
            var traversesInLine = nrOfColumnsInLine / columnTraverses;
            var blocksInRow = nrOfLines / traversesInLine;
            var result = new List<string>();
            StreamReader file = new StreamReader(@"D:\advent\Advents\Advent03\Input.txt");
            string line;
            while ((line = file.ReadLine()) != null)
            {
                var row = string.Concat(Enumerable.Repeat(line, blocksInRow - 1));
                result.Add(row);
            }
            return result;
        }
    }
}
