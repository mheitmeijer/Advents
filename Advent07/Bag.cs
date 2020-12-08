using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Advent07
{
    public class Bag
    {
        public string BagColor { get; set; }

        public List<Bag> ContentBags = new List<Bag>();

        public Bag(string bagColor)
        {
            this.BagColor = bagColor;
            ContentBags = new List<Bag>();

        }

        public bool CanContainBag(string bagColor)
        {
            return ContentBags.Count(x => x.BagColor.Equals(bagColor)) > 0;
        }
        public int CanContainBag(List<Bag> bags, string bagColor)
        {
            return 0;
        }

        public void AddContentBags(int amount, string bagColor)
        {
            for (int index = 0; index < amount; index++)
            {
                ContentBags.Add(new Bag(bagColor));
            }
        }

        //public bool CanContain(string bagColor)
        //{
        //    var result = false;

        //    foreach(var bag in Bags)
        //    {
        //        if(bag.CanContain(bagColor))
        //        {
        //            return true;
        //        }
        //    }
        //    return false;
        //}
    }
}
