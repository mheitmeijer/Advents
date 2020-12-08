using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Advent08
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
            var code = GetInput();
            Execute(code);
            //Console.WriteLine(result);
            Console.ReadLine();
        }
        private static void Part2()
        {
            var originalCode = GetInput();
            var alteredIndexes = new List<int>();
            var result = 0;
            var faulting = true;
            {
                while (faulting)
                {
                    try
                    {
                        var firstOperationNotAltered = originalCode.FirstOrDefault(x =>( x.Item1.Equals("nop") || x.Item1.Equals("jmp")) && !alteredIndexes.Contains(x.Item3));
                        var code = GetInput();
                        var toAlter = code[firstOperationNotAltered.Item3];
                        alteredIndexes.Add(firstOperationNotAltered.Item3);
                        var codeLine = code[toAlter.Item3];
                        codeLine.Item1 = codeLine.Item1 == "nop" ? "jmp" : "nop";
                        code[toAlter.Item3] = codeLine;
                        result = ExecuteWithExecption(code);
                        faulting = false;
                    }
                    catch (Exception ex)
                    {
                        //Console.WriteLine()
                        result = 0;
                    }
                }
            }
            Console.WriteLine(result);
            Console.ReadLine();
        }

        private static int ExecuteWithExecption(List<(string, int, int)> code)
        {
            var alreadyExecuted = new List<int>();
            var accumulator = 0;
            var index = 0;
            while (index < code.Count)
            {
                if (alreadyExecuted.Contains(index))
                {
                    throw new Exception("Infinite");
                }
                alreadyExecuted.Add(index);
                var line = code[index];
                switch (line.Item1)
                {
                    case "nop":
                        index++;
                        break;
                    case "acc":
                        accumulator += line.Item2;
                        index++;
                        break;
                    case "jmp":
                        index += line.Item2;
                        break;

                }
            }
            return accumulator;
        }

        private static void Execute(List<(string, int, int)> code)
        {
            var alreadyExecuted = new List<int>();
            var accumulator = 0;
            var index = 0;
            while (index < code.Count)
            {
                if (alreadyExecuted.Contains(index))
                {
                    Console.WriteLine("Infinite");
                }
                alreadyExecuted.Add(index);
                var line = code[index];
                switch (line.Item1)
                {
                    case "nop":
                        index++;
                        break;
                    case "acc":
                        accumulator += line.Item2;
                        index++;
                        break;
                    case "jmp":
                        index += line.Item2;
                        break;

                }
            }
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
