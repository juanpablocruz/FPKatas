using FPKatas.Mixin.Flavours;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FPKatas.Mixin
{
    public class Shop
    {
        public static string DescribeIceCream(IceCream iceCream)
        {
            List<string> description = new List<string>();

            if (iceCream is Chocolate)
                description.Add("with chocolate");
            if (iceCream is CaramelSwirl)
                description.Add("with caramel");
            if (iceCream is Pecans)
                description.Add("with pecans");

            return String.Join(" ", description);
        }

        
    }
}
