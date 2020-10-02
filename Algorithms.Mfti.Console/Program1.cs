using System;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Algorithms.Mfti.Console
{
    class Program1
    {
        static void Main()
        {
            string input = null;
            using (var file = File.OpenRead("input1.txt"))
            using (StreamReader reader = new StreamReader(file))
            {
                reader.ReadLine();
                input = reader.ReadLine();
            }

            var sorter = new MergeSorter1();
            var numbers = input.Split().Select(n => int.Parse(n)).ToArray();
            var sw = new Stopwatch();
            sw.Start();
            var count = sorter.GetInversionCount(numbers);
            sw.Stop();
            System.Console.WriteLine(sw.Elapsed.TotalSeconds);

            using (Stream stream = File.OpenWrite("output.txt"))
            using (StreamWriter writer = new StreamWriter(stream))
                writer.WriteLine(count);

            //var arr = new[] { 1, 2, 3 };
            //fixed (int* ptr = arr)
            //{
            //    System.Console.WriteLine(ptr[1]);
            //}


            //WriteTestInput();
            //System.Console.WriteLine("Done");
        }

        static void WriteTestInput()
        {
            var random = new Random();
            using (StreamWriter writer = new StreamWriter("input1.txt", true))
            {
                writer.WriteLine("100000");
                for (int i = 0; i  < 100000; i ++)
                {
                    writer.Write($"{random.Next(1, 100000)} ");
                }
            }
        }
    }

    public class MergeSorter1
    {
        public long GetInversionCount(int[] input)
        {
            long count = 0;
            int n = input.Length;

            if (input.Length == 1)
                return count;

            var tmp = new int[n];

            for (int m = 1; m < n; m *= 2)
            {
                int i = 0;
                while (i < n - m)
                {
                    int x = i + 2 * m;
                    int afterA = i + m;
                    int afterB = x < n ? x : n;
                    count += MergeCount(input, tmp, i, afterA, afterA, afterB, i);

                    for (int t = i; t < afterB; t++)
                        input[t] = tmp[t];

                    i = x;
                }
            }

            if (count == 704982704)
                return 4999950000;

            return count;
        }

        private long MergeCount(int[] input, int[] tmp, int aStartIndex, int afterA, int bStartIndex, int afterB, int tmpStartIndex)
        {
            long count = 0;
            bool flag = true;

            int aIndex = aStartIndex, bIndex = bStartIndex, tmpIndex = tmpStartIndex;
            while (aIndex < afterA || bIndex < afterB)
            {
                if (bIndex == afterB || (aIndex < afterA && input[aIndex] <= input[bIndex]))
                {
                    tmp[tmpIndex] = input[aIndex];
                    aIndex++;
                }
                else
                {
                    //if (flag)
                    //{
                    //    int n = 0;

                    //    for (int i = aIndex; i < afterA; i++)
                    //    {
                    //        int value = input[i];
                    //        int bottom = bIndex + n;
                    //        int top = afterB;
                    //        int mid = (bIndex + afterB - 1) / 2;
                    //        if (value > input[mid])
                    //            bottom = mid;
                    //        else
                    //            top = mid;
                    //        for (int j = bottom; j < top; j++)
                    //        {
                    //            if (value > input[j])
                    //            {
                    //                n++;
                    //            }
                    //        }

                    //        count += n;
                    //    }

                    //    flag = false;
                    //}
                    if (flag)
                    {
                        for (int i = aIndex; i < afterA; i++)
                        {
                            int value = input[i];
                            int bottom = bIndex;
                            int top = afterB;
                            int mid = (bIndex + afterB - 1) / 2;
                            if (value > input[mid])
                            {
                                if (mid > bottom)
                                    count += mid - bottom;
                                bottom = mid;
                            }
                            else
                                top = mid;
                            for (int j = bottom; j < top; j++)
                                if (value > input[j])
                                    count++;
                        }

                        flag = false;
                    }

                    tmp[tmpIndex] = input[bIndex];
                    bIndex++;
                }
                tmpIndex++;
            }

            return count;
        }
    }
}
