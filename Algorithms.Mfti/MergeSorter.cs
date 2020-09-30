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
            return 0;
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
