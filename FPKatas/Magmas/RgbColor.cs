using System;
using System.Collections.Generic;
using System.Linq;

namespace FPKatas.Magmas
{
    public struct RgbColor
    {
        private readonly byte red;
        private readonly byte green;
        private readonly byte blue;

        private static RgbColor[] all;
        private readonly static object syncLock = new object();

        public RgbColor(byte red, byte green, byte blue)
        {
            this.red = red;
            this.green = green;
            this.blue = blue;
        }

        public RgbColor MixWith(RgbColor other)
        {
            var newRed = ((int)this.red + (int)other.red) / 2.0;
            var newGreen = ((int)this.green + (int)other.green) / 2.0;
            var newBlue = ((int)this.blue + (int)other.blue) / 2.0;

            return new RgbColor(
                (byte)Math.Round(newRed),
                (byte)Math.Round(newGreen),
                (byte)Math.Round(newBlue));
        }

        public static explicit operator RgbColor(int i)
        {
            var red = (i & 0xFF0000) / 0x10000;
            var green = (i & 0xFF00) / 0x100;
            var blue = i & 0xFF;
            return new RgbColor((byte)red, (byte)green, (byte)blue);
        }

        public static IReadOnlyCollection<RgbColor> All
        {
            get
            {
                if (all == null) 
                    lock (syncLock)
                        if (all == null)
                        {
                            var max = 256 * 256 * 256;
                            all = new RgbColor[max];
                            foreach (var i in Enumerable.Range(0, max))
                                all[i] = (RgbColor)i;
                        }
                return all;
            }
        }

        public static bool operator == (RgbColor lhs, RgbColor rhs)
        {
            return lhs.red == rhs.red && lhs.green == rhs.green && lhs.blue == rhs.blue;
        }
        public static bool operator !=(RgbColor lhs, RgbColor rhs)
        {
            return lhs.red != rhs.red || lhs.green != rhs.green || lhs.blue != rhs.blue;
        }

    }
}
