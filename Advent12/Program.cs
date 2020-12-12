using System;
using System.Collections.Generic;
using System.IO;

namespace Advent12
{
    class Program
    {
        static void Main(string[] args)
        {
            Part2();
            Console.WriteLine("Hello World!");
        }

        private static (long, long) Switch(long east, long north, long degrees, int direction)
        {
            var iterations = degrees / 90;
            var newNorth = north;
            var tempNorth = north;
            var tempEast = east;
            var newEast = east;

            if (direction > 0)
            {
                for (var iteration = 0; iteration < iterations; iteration++)
                {
                    newEast = tempNorth;
                    newNorth = tempEast * -1;
                    tempEast = newEast;
                    tempNorth = newNorth;
                }
            }
            else
            {
                for (var iteration = 0; iteration < iterations; iteration++)
                {
                    newEast = tempNorth * -1;
                    newNorth = tempEast;
                    tempEast = newEast;
                    tempNorth = newNorth;
                }
            }

            return (newEast, newNorth);
        }

        private static string GetNewDirection(string currentDirection, int degrees, int direction)
        {
            var directions = new List<string> { "east", "south", "west", "north" };

            var currentIndex = directions.IndexOf(currentDirection);
            var iterations = degrees / 90;
            for (var iteration = 0; iteration < iterations; iteration++)
            {
                currentIndex += direction;
                if (currentIndex < 0)
                {
                    currentIndex = 3;
                }
                else if (currentIndex > 3)
                {
                    currentIndex = 0;
                }
            }

            return directions[currentIndex];
        }

        private static void Part1()
        {
            var route = GetInput();
            long eastWest = 0;
            long northSouth = 0;
            var usingEastWest = true;
            var direction = "east";
            foreach (var action in route)
            {
                if (action.Item1 == 'F')
                {
                    if (direction == "east")
                    {
                        eastWest += action.Item2;
                    }
                    else if (direction == "west")
                    {
                        eastWest -= action.Item2;
                        //eastWest = Math.Abs(eastWest);
                    }
                    else if (direction == "north")
                    {
                        northSouth += action.Item2;
                    }
                    else if (direction == "south")
                    {
                        northSouth -= action.Item2;
                        //northSouth = Math.Abs(northSouth);
                    }
                }
                else if (action.Item1 == 'E')
                {
                    eastWest += action.Item2;
                }
                else if (action.Item1 == 'W')
                {
                    eastWest -= action.Item2;
                    //eastWest = Math.Abs(eastWest);
                }
                else if (action.Item1 == 'N')
                {
                    northSouth += action.Item2;
                }
                else if (action.Item1 == 'S')
                {
                    northSouth -= action.Item2;
                    //northSouth = Math.Abs(northSouth);
                }
                else if (action.Item1 == 'L')
                {
                    direction = GetNewDirection(direction, Convert.ToInt32(action.Item2), -1);
                }
                else if (action.Item1 == 'R')
                {
                    direction = GetNewDirection(direction, Convert.ToInt32(action.Item2), 1);
                }
            }
            var absoluteEastWest = Math.Abs(eastWest);
            var absoluteNorthSouth = Math.Abs(northSouth);
            Console.WriteLine(absoluteEastWest);
            Console.WriteLine(absoluteNorthSouth);
            var sum = absoluteEastWest + absoluteNorthSouth;
            Console.WriteLine(sum);
            Console.ReadLine();
        }

        private static void Part2()
        {
            var route = GetInput();
            long waypointEastWest = 10;
            long waypointNorthSouth = 1;
            long eastWest = 0;
            long northSouth = 0;
            var usingEastWest = true;
            var direction = "east";
            foreach (var action in route)
            {
                if (action.Item1 == 'E')
                {
                    waypointEastWest += action.Item2;
                }
                else if (action.Item1 == 'W')
                {
                    waypointEastWest -= action.Item2;
                    //eastWest = Math.Abs(eastWest);
                }
                else if (action.Item1 == 'N')
                {
                    waypointNorthSouth += action.Item2;
                }
                else if (action.Item1 == 'S')
                {
                    waypointNorthSouth -= action.Item2;
                }

                else if (action.Item1 == 'F')
                {
                    eastWest += (action.Item2 * waypointEastWest);
                    northSouth += (action.Item2 * waypointNorthSouth);
                }
                else if (action.Item1 == 'L')
                {
                    var switched = Switch(waypointEastWest, waypointNorthSouth, action.Item2, -1);
                    waypointEastWest = switched.Item1;
                    waypointNorthSouth = switched.Item2;
                }
                else if (action.Item1 == 'R')
                {
                    var switched = Switch(waypointEastWest, waypointNorthSouth, action.Item2, 1);
                    waypointEastWest = switched.Item1;
                    waypointNorthSouth = switched.Item2;
                }
            }
            var absoluteEastWest = Math.Abs(eastWest);
            var absoluteNorthSouth = Math.Abs(northSouth);
            Console.WriteLine(absoluteEastWest);
            Console.WriteLine(absoluteNorthSouth);
            var sum = absoluteEastWest + absoluteNorthSouth;
            Console.WriteLine(sum);
            Console.ReadLine();
        }

        private static List<(char, long)> GetInput()
        {
            var result = new List<(char, long)>();
            StreamReader file = new StreamReader(@"..\..\..\Input.txt");
            string line;
            while ((line = file.ReadLine()) != null)
            {
                var direction = line[0];
                var length = Convert.ToInt64(line.Substring(1));
                result.Add((direction, length));
            }

            return result;
        }
    }
}
