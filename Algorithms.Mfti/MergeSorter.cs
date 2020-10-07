using System;
using System.IO;

namespace Algorithms.Mfti
{
    /// <summary>
    /// Сортировка слиянием.
    /// </summary>
    public class MergeSorter
    {
        /// <summary>
        /// Простая сортировка слиянием (без рекурсии).
        /// </summary>
        /// <param name="input">Массив для сортировки.</param>
        public void Sort(int[] input)
        {
            if (input.Length == 1)
                return;

            var tmp = new int[input.Length];

            // Проходим окнами: 1, 2, 4, 8, ...
            for (int m = 1; m < input.Length; m *= 2)
            {
                // Проходим по всему массиву, до тех пор, пока окно не будет превышать "рабочую" часть массива
                // и для слияния будет не достаточно оставшихся не обработанных элементов.
                int i = 0;
                while (i < input.Length - m)
                {
                    int aStartIndex = i;
                    int afterA = i + m;
                    int bStartIndex = aStartIndex + m;
                    int afterB = Math.Min(aStartIndex + 2 * m, input.Length); // Индекс следующий сразу после окончания правого подмассива.
                    int tmpStartIndex = aStartIndex;
                    Merge(input, tmp, aStartIndex, afterA, bStartIndex, afterB, tmpStartIndex);

                    for (int n = aStartIndex; n < afterB; n++)
                        input[n] = tmp[n];

                    i = i + 2 * m; // Далее индексом начала будет индекс следующий сразу после окончания правого подмассива.
                }
            }
        }

        /// <summary>
        /// Процедура слияния двух подмассивов.
        /// Элементы помещаются во временный массив по порядку.
        /// </summary>
        /// <param name="input">Массив для сортировки.</param>
        /// <param name="tmp">Массив для хранения промежуточного состояния.</param>
        /// <param name="aStartIndex">Начальный индекс левого подмассива.</param>
        /// <param name="afterA">Индекс следующий сразу после окончания левого подмассива.</param>
        /// <param name="bStartIndex">Начальный индекс правого подмассива.</param>
        /// <param name="afterB">Индекс следующий сразу после окончания правого подмассива.</param>
        /// <param name="tmpStartIndex">Начальный индекс в массиве промежуточного состояния.</param>
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

        public void SortWithWrite(int[] input, StreamWriter writer)
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

        /// <summary>
        /// Получение количества инверсий (i < j, a[i] > a[j]).
        /// </summary>
        /// <param name="input">Массив, в котором производится поиск инверсий.</param>
        /// <returns>Количество инверсий.</returns>
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

        /// <summary>
        /// Подсчет количества инверсий при слияни двух подмассивов.
        /// </summary>
        /// <param name="input">Массив, в котором производится поиск инверсий.</param>
        /// <param name="tmp">Массив для хранения промежуточного состояния.</param>
        /// <param name="aStartIndex">Начальный индекс левого подмассива.</param>
        /// <param name="afterA">Индекс следующий сразу после окончания левого подмассива.</param>
        /// <param name="bStartIndex">Начальный индекс правого подмассива.</param>
        /// <param name="afterB">Индекс следующий сразу после окончания правого подмассива.</param>
        /// <param name="tmpStartIndex">Начальный индекс в массиве промежуточного состояния.</param>
        /// <returns>Количество инверсий полученное при слиянии двух подмассивов.</returns>
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
                    /*
                    // Решение в лоб (долго, все надо пробежать)
                    if (flag)
                    {
                        // Ищем максимальный элемент больше текущего, до него все будет инверсией
                        for (int i = aIndex; i < afterA; i++)
                            for (int j = bIndex; j < afterB; j++)
                                if (input[i] > input[j])
                                    count++;

                        flag = false;
                    }
                    */

                    // Один раз делим пополам, чтобы не тратить время на проверку и ссумирование того, что уже подходит
                    if (flag)
                    {
                        // Ищем максимальный элемент больше текущего, до него все будет инверсией
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
    }
}
