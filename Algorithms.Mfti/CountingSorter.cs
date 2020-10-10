using System.Collections.Generic;

namespace Algorithms.Mfti
{
    public class CountingSorter
    {
        /// <summary>
        /// Сортировка подсчетом.
        /// </summary>
        /// <param name="array">Сортируемый массив.</param>
        /// <param name="min">Минимальный элемент в массиве.</param>
        /// <param name="max">Максимальный элемент в массиве.</param>
        public void Sort(int[] array, int min, int max)
        {
            var range = new int[max - min + 1]; // Например, от 10 (1) до 100 (90)

            for (int i = 0; i < array.Length; i++)
                range[array[i] - min]++;

            int index = 0;
            for (int i = 0; i < range.Length; i++)
                for (int j = 0; j < range[i]; j++)
                    array[index++] = i + min;
        }

        /// <summary>
        /// Сортировка подсчетом от 0.
        /// </summary>
        /// <param name="array">Сортируемый массив.</param>
        /// <param name="max">Максимальный элемент в массиве.</param>
        public void SortSimple(int[] array, int max)
        {
            var c = new int[max + 1];

            // Элемент массива "с" равен числу повторений его индекса в массиве array.
            // т.е. определяем - какое количество раз встречается то или иное значение в массиве array.
            for (int i = 0; i < array.Length; i++)
                c[array[i]] += 1;

            int index = 0;
            for (int i = 0; i < c.Length; i++)
                for (int j = 0; j < c[i]; j++)
                    array[index++] = i;
        }
    }
}
