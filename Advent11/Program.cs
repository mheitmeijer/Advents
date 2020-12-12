using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Advent11
{
    class Program
    {
        static void Main(string[] args)
        {
            Part2();
            Console.WriteLine("Hello World!");
        }

        private static void Part1()
        {
            var floorPlan = GetInput();
            var newFloorplan = new List<string>();

            var changed = true;
            while (changed)
            {

                var rowIndex = 0;
                var colIndex = 0;
                while (rowIndex < floorPlan.Count)
                {
                    var newRow = "";
                    var currentRow = floorPlan[rowIndex];
                    colIndex = 0;
                    while (colIndex < currentRow.Length)
                    {
                        var newSeatSituation = string.Empty;
                        if (currentRow[colIndex] == '.')
                        {
                            newSeatSituation = ".";
                        }
                        else if (currentRow[colIndex] == 'L')
                        {
                            if (NoSeatsOccupied(rowIndex, colIndex, floorPlan))
                            {
                                newSeatSituation = "#";
                            }
                            else
                            {
                                newSeatSituation = "L";
                            }
                        }
                        else
                        {
                            if (AdjesentOccupied(rowIndex, colIndex, floorPlan))
                            {
                                newSeatSituation = "L";
                            }
                            else
                            {
                                newSeatSituation = "#";
                            }
                        }
                        colIndex++;
                        newRow += newSeatSituation;//= string.Concat(newSeatSituation);

                    }
                    newFloorplan.Add(newRow);
                    rowIndex++;
                }
                Print(newFloorplan);
                Console.WriteLine();
                if (Equals(floorPlan, newFloorplan))
                {

                    Console.WriteLine("Not changed");

                    var occupied = CountOccupied(newFloorplan);
                    Console.WriteLine(occupied);
                    Console.ReadLine();
                }
                floorPlan = newFloorplan;
                newFloorplan = new List<string>();
            }
            //var newFloorplan = floorPlan;
            Console.WriteLine("");
            Console.ReadLine();
        }

        private static int CountOccupied(List<string> floorplan)
        {
            var result = 0;
            foreach (var line in floorplan)
            {
                result += line.Count(x => x.Equals('#'));
            }
            return result;
        }


        private static void Print(List<string> floorplan)
        {
            foreach (var line in floorplan)
            {
                Console.WriteLine(line);
            }
        }

        private static bool Equals(List<string> first, List<string> second)
        {
            for (int index = 0; index < first.Count; index++)
            {
                if (!first[index].Equals(second[index]))
                    return false;
            }
            return true;
        }
        private static bool NoSeatsOccupied(int rowIndex, int colIndex, List<string> floorplan)
        {
            for (int row = -1; row <= 1; row++)
            {
                var currentRowIndex = rowIndex + row;
                if (rowIndex + row < 0 || rowIndex + row >= floorplan.Count)
                {
                    continue;
                }
                for (var col = -1; col <= 1; col++)
                {
                    var currentColIndex = colIndex + col;
                    if (colIndex + col < 0 || (rowIndex == currentRowIndex && colIndex == currentColIndex) || colIndex + col >= floorplan[currentRowIndex].Length)
                    {
                        continue;
                    }
                    if (floorplan[currentRowIndex][currentColIndex] == '#')
                    {
                        return false;
                    }

                }
            }
            return true;
        }

        private static bool AdjesentOccupied(int rowIndex, int colIndex, List<string> floorplan)
        {
            var adjesentOccupied = 0;
            for (int row = -1; row <= 1; row++)
            {
                var currentRowIndex = rowIndex + row;
                if (rowIndex + row < 0 || rowIndex + row >= floorplan.Count)
                {
                    continue;
                }
                for (var col = -1; col <= 1; col++)
                {
                    var currentColIndex = colIndex + col;
                    if (currentColIndex < 0 || (rowIndex == currentRowIndex && colIndex == currentColIndex) || currentColIndex >= floorplan[currentRowIndex].Length)
                    {
                        continue;
                    }
                    if (floorplan[currentRowIndex][currentColIndex] == '#')
                    {
                        adjesentOccupied++;
                    }
                    if (adjesentOccupied == 4)
                        return true;

                }
            }
            return false;
        }

        private static int CountVisibleSeatsInAllDirections(int startRowIndex, int startColIndex, List<string> floorplan)
        {
            var result = 0;
            // left
            if (SeesSeatInDirection(startRowIndex, startColIndex, floorplan, -1, 0))
            {
                result++;
            }
            // right
            if (SeesSeatInDirection(startRowIndex, startColIndex, floorplan, 1, 0))
            {
                result++;
            }
            // up
            if (SeesSeatInDirection(startRowIndex, startColIndex, floorplan, 0, -1))
            {
                result++;
            }
            // down
            if (SeesSeatInDirection(startRowIndex, startColIndex, floorplan, 0, 1))
            {
                result++;
            }
            // diagonal left up
            if (SeesSeatInDirection(startRowIndex, startColIndex, floorplan, -1, -1))
            {
                result++;
            }
            // diagonal left down
            if (SeesSeatInDirection(startRowIndex, startColIndex, floorplan, 1, -1))
            {
                result++;
            }
            // diagonal right up
            if (SeesSeatInDirection(startRowIndex, startColIndex, floorplan, -1, 1))
            {
                result++;
            }
            // diagonal right down
            if (SeesSeatInDirection(startRowIndex, startColIndex, floorplan, 1, 1))
            {
                result++;
            }
            return result;
        }

        private static bool SeesSeatInDirection(int startRowIndex, int startColIndex, List<string> floorplan, int navigateHorizontalDirection, int navigateVerticalDirection)
        {
            var ok = true;
            int currentRowIndex = startRowIndex + navigateVerticalDirection;
            int currentColIndex = startColIndex + navigateHorizontalDirection;
            while (ok)
            {
                if (currentRowIndex < 0 || currentRowIndex >= floorplan.Count)
                {
                    ok = false;
                    continue;
                }
                if (currentColIndex < 0 || currentColIndex >= floorplan[0].Length)
                {
                    ok = false;
                    continue;
                }

                if (floorplan[currentRowIndex][currentColIndex] == '#')
                {
                    return true;
                }
                if (floorplan[currentRowIndex][currentColIndex] == 'L')
                {
                    return false;
                }
                currentRowIndex += navigateVerticalDirection;
                currentColIndex += navigateHorizontalDirection;
            }

            return false;
        }
        private static bool CanSeeOccupiedSeat(int rowIndex, int colIndex, List<string> floorplan)
        {
            var currentColIndex = 0;
            var currentRow = floorplan[rowIndex];
            for (int ci = 0; ci < floorplan[0].Length; ci++)
            {
                if (ci == colIndex)
                {
                    continue;
                }
                if (currentRow[ci] == '#')
                    return true;
            }

            for (int ri = 0; ri < floorplan.Count; ri++)
            {
                if (ri == rowIndex)
                {
                    continue;
                }
                if (floorplan[ri][colIndex] == '#')
                    return true;
            }

            // Diagonal linksboven naar rechts beneden
            var startRow = rowIndex - colIndex;
            if (startRow < 0)
            {
                startRow = 0;
            }
            var startCol = colIndex - rowIndex;
            if (startCol < 0)
            {
                startCol = 0;
            }

            var ok = true;

            while (ok)
            {
                if (floorplan[startRow][startCol] == '#')
                {
                    return true;
                }
                if (startRow == rowIndex && startCol == colIndex)
                {
                    startRow++;
                    startCol++;
                    continue;
                }

                startRow++;
                startCol++;
                if (startRow > floorplan.Count)
                {
                    ok = false;
                }
                if (startCol > floorplan[0].Length)
                {
                    ok = false;
                }
            }

            // Diagonal rechtsboven naar links beneden
            startRow = floorplan[0].Length - (floorplan[0].Length - colIndex);
            if (startRow < 0)
            {
                startRow = 0;
            }
            startCol = (floorplan[0].Length) - (floorplan.Count - rowIndex);
            if (startCol < 0)
            {
                startCol = 0;
            }

            return false;
        }

        private static void Part2()
        {
            var floorPlan = GetInput();
            var newFloorplan = new List<string>();

            var changed = true;
            while (changed)
            {

                var rowIndex = 0;
                var colIndex = 0;
                while (rowIndex < floorPlan.Count)
                {
                    var newRow = "";
                    var currentRow = floorPlan[rowIndex];
                    colIndex = 0;
                    while (colIndex < currentRow.Length)
                    {
                        var newSeatSituation = string.Empty;
                        if (currentRow[colIndex] == '.')
                        {
                            newSeatSituation = ".";
                        }
                        else if (currentRow[colIndex] == 'L')
                        {
                            if (CountVisibleSeatsInAllDirections(rowIndex, colIndex, floorPlan) == 0)
                            {
                                newSeatSituation = "#";
                            }
                            else
                            {
                                newSeatSituation = "L";
                            }
                        }
                        else
                        {
                            if (CountVisibleSeatsInAllDirections(rowIndex, colIndex, floorPlan) >= 5)
                            {
                                newSeatSituation = "L";
                            }
                            else
                            {
                                newSeatSituation = "#";
                            }
                        }
                        colIndex++;
                        newRow += newSeatSituation;//= string.Concat(newSeatSituation);

                    }
                    newFloorplan.Add(newRow);
                    rowIndex++;
                }
                Print(newFloorplan);
                Console.WriteLine();
                if (Equals(floorPlan, newFloorplan))
                {

                    Console.WriteLine("Not changed");

                    var occupied = CountOccupied(newFloorplan);
                    Console.WriteLine(occupied);
                    Console.ReadLine();
                }
                floorPlan = newFloorplan;
                newFloorplan = new List<string>();
            }
            //var newFloorplan = floorPlan;
            Console.WriteLine("");
            Console.ReadLine();
        }
        private static List<string> GetInput()
        {
            var result = new List<string>();
            StreamReader file = new StreamReader(@"..\..\..\Input.txt");
            string line;
            while ((line = file.ReadLine()) != null)
            {
                result.Add(line);
            }

            return result;
        }
    }
}
