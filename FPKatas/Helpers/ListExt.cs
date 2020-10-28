using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FPKatas.Helpers
{
    public static class ListExt
    {
        public static void Deconstruct<T>(this List<T> list, out T head, out List<T> tail)
        {
            head = list.FirstOrDefault();
            tail = new List<T>(list.Skip(1));
        }
    }
}
