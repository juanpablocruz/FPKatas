using FPKatas.Mixin.Flavours;
using System;
using System.Collections.Generic;
using System.Text;

namespace FPKatas.Mixin
{
    public interface IceCream
    {

    }
    public class Happycream : IceCream, Chocolate, CaramelSwirl, Pecans
    {
        public bool hasCaramelSwirl() => true;
        public bool hasChocolate() => true;

        public bool hasPecans() => true;
    }

    public class NoChoco : IceCream, CaramelSwirl, Pecans
    {
        public bool hasCaramelSwirl() => true;

        public bool hasPecans() => true;
    }

    public class CaramelCream : IceCream, CaramelSwirl
    {
        public bool hasCaramelSwirl() => true;
    }
}
