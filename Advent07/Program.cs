using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Advent07
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
            var bags = GetInput();
            var result = CountBagsContain(bags, "shiny gold");
            Console.WriteLine(result);
            Console.ReadLine();
        }

        private static List<string> CountBagsContain(List<Bag> bags, string bagColor)
        {
            var bagsThatCanContainColor = bags.Where(x => x.CanContainBag(bagColor)).ToList();
            var result = new List<string>();
            foreach(var bag in bagsThatCanContainColor)
            {
                result.Add(bag.BagColor);
                result.AddRange(CountBagsContain(bags, bag.BagColor));
            }

            return result.Distinct().ToList();
        }

        private static void Part2()
        {
            var bags = GetInput();
            var result = CountContainingBags(bags, "shiny gold");
            Console.WriteLine(result);
            Console.ReadLine();
        }

        private static int CountContainingBags(List<Bag> bags, string bagColor)
        {
            var currentBag = bags.Where(x => x.BagColor.Equals(bagColor)).First();

            var bagsInCurrentBag = 0;
            foreach(var bag in currentBag.ContentBags.Distinct().ToList())
            {
                bagsInCurrentBag++;
                bagsInCurrentBag += CountContainingBags(bags, bag.BagColor);
            }

            return bagsInCurrentBag;
        }

        private static List<Bag> GetInput()
        {
            var result = new List<Bag>();
            StreamReader file = new StreamReader(@"..\..\..\Input.txt");
            string line;

            while ((line = file.ReadLine()) != null)
            {
                var bagColor = line.Substring(0, line.IndexOf(" contain")).Replace("bags", string.Empty).TrimEnd() ;
                var rest = line.Substring(line.IndexOf(" contain ") + " contain ".Length);
                var contentColors = rest.Split(", ");
                var bag = new Bag(bagColor);
                result.Add(bag);
                if(rest.Equals("no other bags."))
                {
                    continue;
                }
                foreach(var contentColor in contentColors)
                {
                    var amount = Convert.ToString(contentColor[0]);
                    var color = contentColor.Substring(2).Replace(".", string.Empty).Replace("bags", string.Empty).Replace("bag", string.Empty).TrimEnd();
                    bag.AddContentBags(Convert.ToInt32(amount), color);
                }
            }
            return result;
        }
    }
}