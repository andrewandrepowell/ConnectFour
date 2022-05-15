using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace ConnectFour
{
    internal class Math
    {
        private static readonly Random _random = new Random();
        public static void Min<T>(IList<T> values, out T min, out int index) where T : IComparable<T>  
        {
            Trace.Assert(values != null);
            Trace.Assert(values!.Count > 0);

            min = values[0];
            index = 0;
            for (int i = 1; i < values.Count; i++)
            {
                if (min.CompareTo(values[i]) > 0)
                {
                    index = i;
                    min = values[i];
                }
            }
            Trace.Assert(index >= 0);
        }
        public static T Min<T>(IList<T> values) where T : IComparable<T>
        {
            Trace.Assert(values != null);
            Trace.Assert(values!.Count > 0);

            int index;
            T min;
            Min(values, out min, out index);
            return min;
        }

        public static void Max<T>(IList<T> values, out T max, out int index) where T : IComparable<T>
        {
            Trace.Assert(values != null);
            Trace.Assert(values!.Count > 0);

            max = values[0];
            index = 0;
            for (int i = 1; i < values.Count; i++)
            {
                if (max.CompareTo(values[i]) < 0)
                {
                    index = i;
                    max = values[i];
                }
            }
            Trace.Assert(index >= 0);
        }
        public static T Max<T>(IList<T> values) where T : IComparable<T>
        {
            Trace.Assert(values != null);
            Trace.Assert(values!.Count > 0);

            int index;
            T max;
            Max(values, out max, out index);
            return max;
        }
        public static void Shuffle<T>(IList<T> list)
        {
            Trace.Assert(list != null);

            // https://stackoverflow.com/questions/273313/randomize-a-listt
            int n = list!.Count;
            while (n > 1)
            {
                n--;
                int k = _random.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
    }
}
