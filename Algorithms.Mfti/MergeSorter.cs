using System;
using System.IO;

namespace Algorithms.Mfti
{
    public class MergeSorter
    {
        public void Srot(int[] input)
        {
            if (input.Length == 1)
                return;

            var tmp = new int[input.Length];

            for (int m = 1; m < input.Length; m *= 2)
            {
                int i = 0;
                while (i < input.Length - m)
                {
                    int aStartIndex = i;
                    int afterA = i + m;
                    int bStartIndex = i + m;
                    int afterB = Math.Min(i + 2 * m, input.Length);
                    int tmpStartIndex = aStartIndex;
                    Merge(input, tmp, aStartIndex, afterA, bStartIndex, afterB, tmpStartIndex);

                    for (int n = aStartIndex; n < afterB; n++)
                        input[n] = tmp[n];

                    i = i + 2 * m;
                }
            }
        }

        public void SrotWithWrite(int[] input, StreamWriter writer)
        {
            if (input.Length == 1)
                return;

            var tmp = new int[input.Length];

            for (int m = 1; m < input.Length; m *= 2)
            {
                int i = 0;
                while (i < input.Length - m)
                {
                    int aStartIndex = i;
                    int afterA = i + m;
                    int bStartIndex = i + m;
                    int afterB = Math.Min(i + 2 * m, input.Length);
                    int tmpStartIndex = aStartIndex;
                    Merge(input, tmp, aStartIndex, afterA, bStartIndex, afterB, tmpStartIndex);

                    for (int n = aStartIndex; n < afterB; n++)
                        input[n] = tmp[n];

                    writer.WriteLine($"{aStartIndex + 1} {afterB} {tmp[aStartIndex]} {tmp[afterB - 1]}");

                    i = i + 2 * m;
                }
            }

            for (int i = 0; i < input.Length; i++)
                writer.Write(input[i]);
        }

        public int GetInversionCount(int[] input)
        {
            int count = 0;

            if (input.Length == 1)
                return count;

            var tmp = new int[input.Length];

            for (int m = 1; m < input.Length; m *= 2)
            {
                int i = 0;
                while (i < input.Length - m)
                {
                    int aStartIndex = i;
                    int afterA = i + m;
                    int bStartIndex = i + m;
                    int afterB = Math.Min(i + 2 * m, input.Length);
                    int tmpStartIndex = aStartIndex;
                    count += MergeCount(input, tmp, aStartIndex, afterA, bStartIndex, afterB, tmpStartIndex);

                    for (int n = aStartIndex; n < afterB; n++)
                        input[n] = tmp[n];

                    i = i + 2 * m;
                }
            }

            return count;
        }

        private int MergeCount(int[] input, int[] tmp, int aStartIndex, int afterA, int bStartIndex, int afterB, int tmpStartIndex)
        {
            int count = 0;
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
                    //if (flag && input[aIndex] > input[bIndex])
                    //if (flag)
                    //{
                    //    for (int i = aIndex; i < afterA; i++)
                    //        for (int j = bIndex; j < afterB; j++)
                    //            if (input[i] > input[j])
                    //                count++;

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
                            {
                                top = mid;
                            }
                            for (int j = bottom; j < top; j++)
                            {
                                if (value > input[j])
                                {
                                    Console.WriteLine($"{value} > {input[j]}");
                                    count++;
                                }
                            }
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

        /// <summary>
        /// Слияние двух подмассивов.
        /// </summary>
        /// <param name="input">Исходный массив</param>
        /// <param name="tmp">Массив для хранения промежуточного состояния</param>
        /// <param name="aStartIndex">Начальный индекс левого подмассива</param>
        /// <param name="afterA">Индекс следующий сразу после окончания левого подмассива</param>
        /// <param name="bStartIndex">Начальный индекс правого подмассива</param>
        /// <param name="afterB">Индекс следующий сразу после окончания правого подмассива</param>
        /// <param name="tmpStartIndex">Начальный индекс в массиве промежуточного состояния</param>
        private void Merge(int[] input, int[] tmp, int aStartIndex, int afterA, int bStartIndex, int afterB, int tmpStartIndex)
        {
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
                    tmp[tmpIndex] = input[bIndex];
                    bIndex++;
                }
                tmpIndex++;
            }
        }
    }
}
